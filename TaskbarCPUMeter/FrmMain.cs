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
using System.Windows.Input;
using TaskbarCPUMeter.Modes;

namespace TaskbarCPUMeter
{
    public partial class FrmMain : Form
    {
        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        bool InitFinished = false;
        Point MouseDownPnt;
        float TargetUsage = 0.0f;
        float ClockSpeed = 0.0f;
        ObjectQuery WQL;
        ManagementObjectSearcher Searcher;
        PerformanceCounter CPUCounter;

        //Members used in drawing
        int Offset = 0;
        Rectangle RectUsageFull;
        float _currentUsage = 0.10f;
        Font MainFont = new Font("Segoe UI", 10.0f, FontStyle.Bold);
        Font SecondaryFont = new Font("Segoe UI", 7.0f);
        Brush MainBrush = new SolidBrush(Color.FromArgb(75, 0, 0, 0));
        IMode CurrentMode;

        public FrmMain()
        {
            InitializeComponent();

            if (System.Environment.OSVersion.Version.ToString().StartsWith("6.1"))
                Offset += 2;

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
            this.Location = new Point(Config.Default.PositionX, 0 + Offset);
            this.Size = new Size(Config.Default.Width, taskbarSize.Height - Offset);
            RectUsageFull = new Rectangle(10, this.Height / 5 * 3, this.Width - 20, this.Height / 4);

            if(Config.Default.RotateViews)
            {
                tmrRotateViews.Interval = Config.Default.RotateViewsInterval;
                tmrRotateViews.Enabled = true;
            }

        }

        private void RotateView()
        {

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

        //Enable borderless form resizing
        protected override void WndProc(ref Message m)
        {
            const UInt32 WM_NCHITTEST = 0x0084;
            const UInt32 WM_MOUSEMOVE = 0x0200;

            const UInt32 HTLEFT = 10;
            const UInt32 HTRIGHT = 11;

            const int RESIZE_HANDLE_SIZE = 10;
            bool handled = false;
            if (m.Msg == WM_NCHITTEST || m.Msg == WM_MOUSEMOVE)
            {
                Size formSize = this.Size;
                Point screenPoint = new Point(m.LParam.ToInt32());
                Point clientPoint = this.PointToClient(screenPoint);

                Dictionary<UInt32, Rectangle> boxes = new Dictionary<UInt32, Rectangle>() 
                {
                    {HTRIGHT, new Rectangle(formSize.Width - RESIZE_HANDLE_SIZE, RESIZE_HANDLE_SIZE, RESIZE_HANDLE_SIZE, formSize.Height - 2*RESIZE_HANDLE_SIZE)},
                    {HTLEFT, new Rectangle(0, RESIZE_HANDLE_SIZE, RESIZE_HANDLE_SIZE, formSize.Height - 2*RESIZE_HANDLE_SIZE) }
                };

                foreach (KeyValuePair<UInt32, Rectangle> hitBox in boxes)
                {
                    if (hitBox.Value.Contains(clientPoint))
                    {
                        m.Result = (IntPtr)hitBox.Key;
                        InitFinished = true;
                        handled = true;
                        break;
                    }
                }
            }

            if (!handled)
                base.WndProc(ref m);
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

        private void FrmMain_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
                this.Location = new Point(this.Location.X + (e.X - MouseDownPnt.X), 0 + Offset);
        }

        private void FrmMain_MouseUp(object sender, MouseEventArgs e)
        {
            Config.Default.PositionX = this.Location.X;
            Config.Default.Save();
        }

        private void FrmMain_MouseDown(object sender, MouseEventArgs e)
        {
            MouseDownPnt = e.Location;
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Config.Default.Save();
        }

        private void FrmMain_SizeChanged(object sender, EventArgs e)
        {
            if (InitFinished)
            {
                Config.Default.Width = this.Width;
                Config.Default.PositionX = this.Location.X;
                Config.Default.Save();
                ApplySettings();
            }
        }

        private void tmrRotateViews_Tick(object sender, EventArgs e)
        {
            RotateView();
        }

    }
}
