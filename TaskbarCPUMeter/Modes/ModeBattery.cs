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
    class ModeBattery : IMode
    {
        float TargetUsage = 0.0f;
        string TimeLeft = "";

        Rectangle RectUsageFull;
        float _currentUsage = 1.0f;
        Font MainFont = new Font("Segoe UI", 10.0f, FontStyle.Bold);
        Brush MainBrush = new SolidBrush(Color.FromArgb(75, 0, 0, 0));

        public void Start() { }

        public void Update(Form target)
        {
            RectUsageFull = new Rectangle(10, target.Height / 5 * 3, target.Width - 20, target.Height / 4);
            TargetUsage = SystemInformation.PowerStatus.BatteryLifePercent;
            TimeLeft = TimeSpan.FromSeconds(
                SystemInformation.PowerStatus.BatteryLifeRemaining)
                .ToString("h'h 'm'm'");
        }

        public void Draw(Form target, Graphics g)
        {
            _currentUsage = (float)Math.Round(_currentUsage, 3);
            if (_currentUsage < TargetUsage)
                _currentUsage += 0.005f;
            else if (_currentUsage > TargetUsage)
                _currentUsage -= 0.005f;

            g.FillRectangle(MainBrush, RectUsageFull);
            //Usage
            g.FillRectangle(Brushes.White,
                    new Rectangle(RectUsageFull.X, RectUsageFull.Y,
                        (int)(RectUsageFull.Width * _currentUsage), RectUsageFull.Height));
            //Usage Percentage
            if (Config.Default.ShowPercentage)
            {
                string percentage = Math.Round(TargetUsage * 100, 0) + "%";
                g.DrawString(percentage,
                    MainFont, Brushes.White,
                    new PointF(target.Width - TextRenderer.MeasureText(g, percentage, MainFont).Width, 5));
            }
            //Draw the current time left
            if (Config.Default.ShowClock)
            {
                if (SystemInformation.PowerStatus.BatteryChargeStatus == BatteryChargeStatus.Charging ||
                    TimeLeft == "0h 0m")
                    g.DrawString("Charging", MainFont, Brushes.White, new PointF(10, 5));
                else if (SystemInformation.PowerStatus.BatteryChargeStatus == BatteryChargeStatus.Low ||
                    SystemInformation.PowerStatus.BatteryChargeStatus == BatteryChargeStatus.Critical)
                    g.DrawString(TimeLeft,
                        MainFont, Brushes.Red, new PointF(10, 5));
                else
                    g.DrawString(TimeLeft,
                        MainFont, Brushes.White, new PointF(10, 5));
            }
        }
    }
}
