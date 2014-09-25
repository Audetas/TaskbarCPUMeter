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
    class ModeTemperature : IMode
    {
        float TargetUsage = 0.5f;
        string CPUTemp = "0c";
        ManagementObjectSearcher searcher;

        Rectangle RectUsageFull;
        float _currentUsage = 0.50f;
        Font MainFont = new Font("Segoe UI", 10.0f, FontStyle.Bold);
        Brush MainBrush = new SolidBrush(Color.FromArgb(75, 0, 0, 0));

        public void Start() 
        {
            searcher = new ManagementObjectSearcher(@"root\WMI", "SELECT * FROM MSAcpi_ThermalZoneTemperature");
        }

        public void Update(Form target)
        {
            foreach (ManagementObject obj in searcher.Get())
            {
                Double temp = Convert.ToDouble(obj["CurrentTemperature"].ToString());
                temp = (temp - 2732) / 10.0;
                TargetUsage = (float)temp / 100.0f;
                CPUTemp = "CPU: " + temp.ToString() + "c";
            }
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
                string percentage = Math.Round(TargetUsage * 100, 0) + "%";
                g.DrawString(percentage,
                    MainFont, Brushes.White,
                    new PointF(target.Width - TextRenderer.MeasureText(g, percentage, MainFont).Width, 5));
            }
            //Draw the current CPU clock speed
            if (Config.Default.ShowClock)
            {
                g.DrawString(CPUTemp,
                    MainFont, Brushes.White, new PointF(10, 5));
            }
        }
    }
}
