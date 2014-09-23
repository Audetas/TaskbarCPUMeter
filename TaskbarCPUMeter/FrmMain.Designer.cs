﻿namespace TaskbarCPUMeter
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
            this.itemOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.itemCredits = new System.Windows.Forms.ToolStripMenuItem();
            this.tmrRepaint = new System.Windows.Forms.Timer(this.components);
            this.itemExit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuSettings
            // 
            this.menuSettings.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itemOptions,
            this.itemCredits,
            this.itemExit});
            this.menuSettings.Name = "contextMenuStrip1";
            this.menuSettings.Size = new System.Drawing.Size(264, 156);
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
            // tmrRepaint
            // 
            this.tmrRepaint.Enabled = true;
            this.tmrRepaint.Interval = 32;
            this.tmrRepaint.Tick += new System.EventHandler(this.tmrRepaint_Tick);
            // 
            // itemExit
            // 
            this.itemExit.Name = "itemExit";
            this.itemExit.Size = new System.Drawing.Size(263, 36);
            this.itemExit.Text = "Exit";
            this.itemExit.Click += new System.EventHandler(this.itemExit_Click);
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
            this.Name = "FrmMain";
            this.Text = "Form1";
            this.Shown += new System.EventHandler(this.FrmMain_Shown);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.FrmMain_Paint);
            this.menuSettings.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip menuSettings;
        private System.Windows.Forms.ToolStripMenuItem itemOptions;
        private System.Windows.Forms.ToolStripMenuItem itemCredits;
        private System.Windows.Forms.Timer tmrRepaint;
        private System.Windows.Forms.ToolStripMenuItem itemExit;



    }
}
