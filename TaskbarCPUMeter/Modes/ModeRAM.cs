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
    class ModeRAM : IMode
    {
        float TargetUsage = 0.0f;
        int Used = 0;

        Rectangle RectUsageFull;
        float _currentUsage = 0.30f;
        Font MainFont = new Font("Segoe UI", 10.0f, FontStyle.Bold);
        Brush MainBrush = new SolidBrush(Color.FromArgb(75, 0, 0, 0));

        public void Start() { }

        public void Update(Form target)
        {
            Int64 available = Native.GetPhysicalAvailableMemoryInMiB();
            Int64 total = Native.GetTotalMemoryInMiB();
            decimal percentFree = ((decimal)available / (decimal)total) * 100;
            decimal percentOccupied = 100 - percentFree;
            TargetUsage = (float)Math.Round(percentOccupied / 100, 2);
            Used = (int)(total - available);
        }

        public void Draw(Form target, Graphics g)
        {
            RectUsageFull = new Rectangle(10, target.Height / 5 * 3, target.Width - 20, target.Height / 4);

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
                string percentage = Math.Round(TargetUsage * 100, 0)  + "%";
                g.DrawString(percentage,
                    MainFont, Brushes.White,
                    new PointF(target.Width - TextRenderer.MeasureText(g, percentage, MainFont).Width, 5));
            }
            //Draw the current CPU clock speed
            if (Config.Default.ShowClock)
            {
                g.DrawString(Used + "MB",
                    MainFont, Brushes.White, new PointF(10, 5));
            }
        }
    }
}
