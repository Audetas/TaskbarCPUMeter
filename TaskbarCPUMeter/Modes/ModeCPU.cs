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
    class ModeCPU : IMode
    {
        float TargetUsage = 0.0f;
        float ClockSpeed = 0.0f;
        ObjectQuery WQL;
        ManagementObjectSearcher Searcher;
        PerformanceCounter CPUCounter;

        Rectangle RectUsageFull;
        float _currentUsage = 0.10f;
        Font MainFont = new Font("Segoe UI", 10.0f, FontStyle.Bold);
        Brush MainBrush = new SolidBrush(Color.FromArgb(75, 0, 0, 0));

        public void Start()
        {
            WQL = new ObjectQuery("SELECT * FROM Win32_Processor");
            Searcher = new ManagementObjectSearcher(WQL);
            CPUCounter = new PerformanceCounter();

            CPUCounter.CategoryName = "Processor";
            CPUCounter.CounterName = "% Processor Time";
            CPUCounter.InstanceName = "_Total";
        }

        public void Update(Form target)
        {
            //Update the Clock
            foreach (ManagementObject result in Searcher.Get())
                ClockSpeed = (float)Math.Round(int.Parse(result["CurrentClockSpeed"].ToString()) / 1000.0f, 2);
            
            //Update the usage
            TargetUsage = (float)Math.Round(CPUCounter.NextValue() / 100.0f, 2);
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
                string percentage = Math.Round(TargetUsage, 2) * 100 + "%";
                g.DrawString(percentage,
                    MainFont, Brushes.White,
                    new PointF(target.Width - TextRenderer.MeasureText(g, percentage, MainFont).Width, 5));
            }
            //Draw the current CPU clock speed
            if (Config.Default.ShowClock)
            {
                g.DrawString(ClockSpeed + "GHz",
                    MainFont, Brushes.White, new PointF(10, 5));
            }
        }
    }
}
