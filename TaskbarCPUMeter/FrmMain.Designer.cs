namespace TaskbarCPUMeter
{
    partial class FrmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuSettings = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.itemMode = new System.Windows.Forms.ToolStripMenuItem();
            this.itemCPUStats = new System.Windows.Forms.ToolStripMenuItem();
            this.itemCPUTemp = new System.Windows.Forms.ToolStripMenuItem();
            this.itemRAMStats = new System.Windows.Forms.ToolStripMenuItem();
            this.itemBattery = new System.Windows.Forms.ToolStripMenuItem();
            this.itemCycle = new System.Windows.Forms.ToolStripMenuItem();
            this.itemOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.itemCredits = new System.Windows.Forms.ToolStripMenuItem();
            this.itemExit = new System.Windows.Forms.ToolStripMenuItem();
            this.tmrRepaint = new System.Windows.Forms.Timer(this.components);
            this.tmrRotateViews = new System.Windows.Forms.Timer(this.components);
            this.tmrUpdate = new System.Windows.Forms.Timer(this.components);
            this.itemBatteryStats2 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuSettings
            // 
            this.menuSettings.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itemMode,
            this.itemOptions,
            this.itemCredits,
            this.itemExit});
            this.menuSettings.Name = "contextMenuStrip1";
            this.menuSettings.Size = new System.Drawing.Size(264, 192);
            // 
            // itemMode
            // 
            this.itemMode.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itemCPUStats,
            this.itemCPUTemp,
            this.itemRAMStats,
            this.itemBattery,
            this.itemBatteryStats2,
            this.itemCycle});
            this.itemMode.Name = "itemMode";
            this.itemMode.Size = new System.Drawing.Size(263, 36);
            this.itemMode.Text = "Mode";
            // 
            // itemCPUStats
            // 
            this.itemCPUStats.Name = "itemCPUStats";
            this.itemCPUStats.Size = new System.Drawing.Size(284, 36);
            this.itemCPUStats.Text = "CPU Statistics";
            this.itemCPUStats.Click += new System.EventHandler(this.itemCycle_Click);
            // 
            // itemCPUTemp
            // 
            this.itemCPUTemp.Name = "itemCPUTemp";
            this.itemCPUTemp.Size = new System.Drawing.Size(284, 36);
            this.itemCPUTemp.Text = "CPU Temperature";
            this.itemCPUTemp.Click += new System.EventHandler(this.itemCycle_Click);
            // 
            // itemRAMStats
            // 
            this.itemRAMStats.Name = "itemRAMStats";
            this.itemRAMStats.Size = new System.Drawing.Size(284, 36);
            this.itemRAMStats.Text = "RAM Statistics";
            this.itemRAMStats.Click += new System.EventHandler(this.itemCycle_Click);
            // 
            // itemBattery
            // 
            this.itemBattery.Name = "itemBattery";
            this.itemBattery.Size = new System.Drawing.Size(284, 36);
            this.itemBattery.Text = "Battery Statistics";
            this.itemBattery.Click += new System.EventHandler(this.itemCycle_Click);
            // 
            // itemCycle
            // 
            this.itemCycle.Name = "itemCycle";
            this.itemCycle.Size = new System.Drawing.Size(284, 36);
            this.itemCycle.Text = "Cycle";
            this.itemCycle.Click += new System.EventHandler(this.itemCycle_Click);
            // 
            // itemOptions
            // 
            this.itemOptions.Name = "itemOptions";
            this.itemOptions.Size = new System.Drawing.Size(263, 36);
            this.itemOptions.Text = "Options";
            this.itemOptions.Click += new System.EventHandler(this.itemOptions_Click);
            // 
            // itemCredits
            // 
            this.itemCredits.Name = "itemCredits";
            this.itemCredits.Size = new System.Drawing.Size(263, 36);
            this.itemCredits.Text = "Made by Kronks";
            this.itemCredits.Click += new System.EventHandler(this.itemCredits_Click);
            // 
            // itemExit
            // 
            this.itemExit.Name = "itemExit";
            this.itemExit.Size = new System.Drawing.Size(263, 36);
            this.itemExit.Text = "Exit";
            this.itemExit.Click += new System.EventHandler(this.itemExit_Click);
            // 
            // tmrRepaint
            // 
            this.tmrRepaint.Enabled = true;
            this.tmrRepaint.Interval = 32;
            this.tmrRepaint.Tick += new System.EventHandler(this.tmrRepaint_Tick);
            // 
            // tmrRotateViews
            // 
            this.tmrRotateViews.Enabled = true;
            this.tmrRotateViews.Interval = 100000;
            this.tmrRotateViews.Tick += new System.EventHandler(this.tmrRotateViews_Tick);
            // 
            // tmrUpdate
            // 
            this.tmrUpdate.Enabled = true;
            this.tmrUpdate.Interval = 1000;
            this.tmrUpdate.Tick += new System.EventHandler(this.tmrUpdate_Tick);
            // 
            // itemBatteryStats2
            // 
            this.itemBatteryStats2.Name = "itemBatteryStats2";
            this.itemBatteryStats2.Size = new System.Drawing.Size(284, 36);
            this.itemBatteryStats2.Text = "Battery Statistics 2";
            this.itemBatteryStats2.Click += new System.EventHandler(this.itemCycle_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(140, 100);
            this.ContextMenuStrip = this.menuSettings;
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmMain";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.Shown += new System.EventHandler(this.FrmMain_Shown);
            this.SizeChanged += new System.EventHandler(this.FrmMain_SizeChanged);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.FrmMain_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FrmMain_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FrmMain_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FrmMain_MouseUp);
            this.menuSettings.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip menuSettings;
        private System.Windows.Forms.ToolStripMenuItem itemOptions;
        private System.Windows.Forms.ToolStripMenuItem itemCredits;
        private System.Windows.Forms.Timer tmrRepaint;
        private System.Windows.Forms.ToolStripMenuItem itemExit;
        private System.Windows.Forms.Timer tmrUpdate;
        private System.Windows.Forms.Timer tmrRotateViews;
        private System.Windows.Forms.ToolStripMenuItem itemMode;
        private System.Windows.Forms.ToolStripMenuItem itemCPUStats;
        private System.Windows.Forms.ToolStripMenuItem itemCPUTemp;
        private System.Windows.Forms.ToolStripMenuItem itemRAMStats;
        private System.Windows.Forms.ToolStripMenuItem itemBattery;
        private System.Windows.Forms.ToolStripMenuItem itemCycle;
        private System.Windows.Forms.ToolStripMenuItem itemBatteryStats2;



    }
}

