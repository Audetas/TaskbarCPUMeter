using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaskbarCPUMeter.Modes
{
    public interface IMode
    {
        void Start();
        void Update(Form target);
        void Draw(Form target, Graphics g);
    }
}
