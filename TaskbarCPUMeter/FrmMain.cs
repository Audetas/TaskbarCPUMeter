using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaskbarCPUMeter
{
    public partial class FrmMain : Form
    {
        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        float TargetUsage = 0.0f;
        float ClockSpeed = 0.0f;
        ObjectQuery WQL;
        ManagementObjectSearcher Searcher;
        PerformanceCounter CPUCounter;

        //Members used in drawing
        Rectangle RectUsageFull;
        float _currentUsage = 0.10f;
        Font MainFont = new Font("Segoe UI", 10.0f, FontStyle.Bold);
        Font SecondaryFont = new Font("Segoe UI", 7.0f);
        Brush MainBrush = new SolidBrush(Color.FromArgb(75, 0, 0, 0));

        public FrmMain()
        {
            InitializeComponent();

            WQL = new ObjectQuery("SELECT * FROM Win32_Processor");
            Searcher = new ManagementObjectSearcher(WQL);
            CPUCounter = new PerformanceCounter();

            CPUCounter.CategoryName = "Processor";
            CPUCounter.CounterName = "% Processor Time";
            CPUCounter.InstanceName = "_Total";

            Task.Run(() =>
                {
                    while (!this.IsDisposed)
                    {
                        //Update the Clock
                        foreach (ManagementObject result in Searcher.Get())
                            ClockSpeed = (float)Math.Round(int.Parse(result["CurrentClockSpeed"].ToString()) / 1000.0f, 2);
                        //Update the usage
                        TargetUsage = (float)Math.Round(CPUCounter.NextValue() / 100.0f, 2);
                    }
                });
        }

        private void FrmMain_Shown(object sender, EventArgs e)
        {
            IntPtr taskbar = FindWindow("Shell_TrayWnd", "");
            SetParent(this.Handle, taskbar);

            ApplySettings();
        }

        public void ApplySettings()
        {
            Size taskbarSize = Native.GetWindowSize(
                    FindWindow("Shell_TrayWnd", ""));

            if (Config.Default.FirstTime) //It's the users first time using the app
            {
                Config.Default.FirstTime = false;
                Config.Default.PositionX = taskbarSize.Width / 7 * 5;
                Config.Default.Width = taskbarSize.Width / 10;
                Config.Default.Save();

                MessageBox.Show("Thanks for using the Taskbar CPU Meter by Kronks. Rightclick the CPU Meter to adjust settings.", "Taskbar CPU Meter");
            }

            this.Size = new Size(Config.Default.Width, taskbarSize.Height);
            this.Location = new Point(Config.Default.PositionX, 0);
            RectUsageFull = new Rectangle(10, this.Height / 5 * 3, this.Width - 20, this.Height / 4);
        }

        private void FrmMain_Paint(object sender, PaintEventArgs e)
        {
            //Draw a progress bar of the current CPU usage
            _currentUsage = (float)Math.Round(_currentUsage, 3);
            if (_currentUsage < TargetUsage)
                _currentUsage += 0.005f;
            else if (_currentUsage > TargetUsage)
                _currentUsage -= 0.005f;

            //Background
            e.Graphics.FillRectangle(MainBrush, RectUsageFull);
            //Usage
            e.Graphics.FillRectangle(Brushes.White,
                    new Rectangle(RectUsageFull.X, RectUsageFull.Y,
                        (int)(RectUsageFull.Width * _currentUsage), RectUsageFull.Height));
            //Usage Percentage
            if (Config.Default.ShowPercentage)
            {
                string percentage = Math.Round(TargetUsage, 2) * 100 + "%";
                e.Graphics.DrawString(percentage,
                    MainFont, Brushes.White,
                    new PointF(this.Width - TextRenderer.MeasureText(e.Graphics, percentage, MainFont).Width, 5));
            }
            //Draw the current CPU clock speed
            if (Config.Default.ShowClock)
            {
                e.Graphics.DrawString(ClockSpeed + "GHz",
                    MainFont, Brushes.White, new PointF(10, 5));
            }
        }

        private void tmrRepaint_Tick(object sender, EventArgs e)
        {
            //Force the window to refresh itself constantly
            this.Refresh();
        }

        private void itemOptions_Click(object sender, EventArgs e)
        {
            new FrmOptions(this).Show();
        }

        private void itemCredits_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/KrazyShank");
        }

        private void itemExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
