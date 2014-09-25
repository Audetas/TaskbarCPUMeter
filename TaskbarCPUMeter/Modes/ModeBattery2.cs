using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaskbarCPUMeter.Modes
{
    class ModeBattery2 : IMode
    {
        float TargetUsage = 1.0f;
        string TimeLeft = "0h 0m";

        Rectangle RectUsageFull;
        float _currentUsage = 1.0f;
        Font MainFont = new Font("Segoe UI", 10.0f, FontStyle.Bold);
        Brush MainBrush = new SolidBrush(Color.FromArgb(75, 0, 0, 0));

        public void Start() { }

        public void Update(Form target)
        {
            TargetUsage = SystemInformation.PowerStatus.BatteryLifePercent;
            TimeLeft = TimeSpan.FromSeconds(
                SystemInformation.PowerStatus.BatteryLifeRemaining)
                .ToString("h'h 'm'm'");
        }

        public void Draw(Form target, Graphics g)
        {
            RectUsageFull = new Rectangle(10, 10, target.Width - 30, target.Height - 20);

            _currentUsage = (float)Math.Round(_currentUsage, 3);
            if (_currentUsage < TargetUsage)
                _currentUsage += 0.005f;
            else if (_currentUsage > TargetUsage)
                _currentUsage -= 0.005f;

            g.FillRectangle(MainBrush, RectUsageFull);
            //Usage
            g.DrawRectangle(Pens.White,
                    new Rectangle(RectUsageFull.X, RectUsageFull.Y,
                        (int)(RectUsageFull.Width), RectUsageFull.Height));

            g.DrawRectangle(Pens.White,
                    new Rectangle(target.Width - 20, target.Height / 3,
                        10, target.Height / 3));

            g.FillRectangle(Brushes.White,
                    new Rectangle(RectUsageFull.X + 4, RectUsageFull.Y + 4,
                        (int)(RectUsageFull.Width * _currentUsage) - 8, RectUsageFull.Height - 8));
            //Draw the current time left
            if (Config.Default.ShowClock)
            {
                if (SystemInformation.PowerStatus.BatteryChargeStatus == BatteryChargeStatus.Charging ||
                    TimeLeft == "0h 0m")
                    g.DrawString("Charging", MainFont, Brushes.Black, new PointF(15, 15));
                else if (SystemInformation.PowerStatus.BatteryChargeStatus == BatteryChargeStatus.Low ||
                    SystemInformation.PowerStatus.BatteryChargeStatus == BatteryChargeStatus.Critical)
                    g.DrawString(TimeLeft,
                        MainFont, Brushes.Red, new PointF(15, 15));
                else
                    g.DrawString(TimeLeft,
                        MainFont, Brushes.Black, new PointF(15, 15));
            }
        }
    }
}
