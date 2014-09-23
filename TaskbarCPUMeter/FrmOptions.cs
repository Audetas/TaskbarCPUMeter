using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaskbarCPUMeter
{
    public partial class FrmOptions : Form
    {
        FrmMain Form;

        public FrmOptions(FrmMain mainForm)
        {
            InitializeComponent();
            this.Form = mainForm;

            this.tbxPosition.Text = Config.Default.PositionX.ToString();
            this.tbxWidth.Text = Config.Default.Width.ToString();
            this.chkShowClock.Checked = Config.Default.ShowClock;
            this.chkShowUsage.Checked = Config.Default.ShowPercentage;
        }

        private void tbxPosition_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Config.Default.PositionX = int.Parse(tbxPosition.Text);
                Config.Default.Save();
                Form.ApplySettings();
            }
            catch { } 
        }

        private void tbxWidth_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Config.Default.Width = int.Parse(tbxWidth.Text);
                Config.Default.Save();
                Form.ApplySettings();
            }
            catch { } 
        }

        private void chkShowUsage_CheckedChanged(object sender, EventArgs e)
        {
            Config.Default.ShowPercentage = chkShowUsage.Checked;
            Config.Default.Save();
        }

        private void chkShowClock_CheckedChanged(object sender, EventArgs e)
        {
            Config.Default.ShowClock = chkShowClock.Checked;
            Config.Default.Save();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
