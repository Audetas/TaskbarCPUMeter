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
        int Offset = 0;
        Point MouseDownPnt;
        IMode CurrentMode;

        public FrmMain()
        {
            InitializeComponent();
            CurrentMode = new ModeCPU();
            
            //CurrentMode.Update(this);
            if (System.Environment.OSVersion.Version.ToString().StartsWith("6.1"))
                Offset += 2;
        }

        public void SwitchMode(IMode mode)
        {
            CurrentMode = mode;
            CurrentMode.Start();
        }

        private void FrmMain_Shown(object sender, EventArgs e)
        {
            IntPtr taskbar = FindWindow("Shell_TrayWnd", "");
            SetParent(this.Handle, taskbar);

            ApplySettings();
            CurrentMode.Start();
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
        }

        private void FrmMain_Paint(object sender, PaintEventArgs e)
        {
            CurrentMode.Draw(this, e.Graphics);
        }

        //Enable borderless form resizing
        const UInt32 WM_NCHITTEST = 0x0084;
        const UInt32 WM_MOUSEMOVE = 0x0200;

        const UInt32 HTLEFT = 10;
        const UInt32 HTRIGHT = 11;

        const int RESIZE_HANDLE_SIZE = 10;

        protected override void WndProc(ref Message m)
        {
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

        private async void tmrUpdate_Tick(object sender, EventArgs e)
        {
            //Async update the current modes variables
            await Task.Run(() =>
            {
                CurrentMode.Update(this);
            }); 
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
    }
}
