namespace TaskbarCPUMeter
{
    partial class FrmOptions
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
            this.label1 = new System.Windows.Forms.Label();
            this.tbxPosition = new System.Windows.Forms.TextBox();
            this.tbxWidth = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.chkShowUsage = new System.Windows.Forms.CheckBox();
            this.chkShowClock = new System.Windows.Forms.CheckBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(249, 30);
            this.label1.TabIndex = 0;
            this.label1.Text = "Position on the Taskbar: ";
            // 
            // tbxPosition
            // 
            this.tbxPosition.Location = new System.Drawing.Point(261, 23);
            this.tbxPosition.Name = "tbxPosition";
            this.tbxPosition.Size = new System.Drawing.Size(105, 36);
            this.tbxPosition.TabIndex = 1;
            this.tbxPosition.TextChanged += new System.EventHandler(this.tbxPosition_TextChanged);
            // 
            // tbxWidth
            // 
            this.tbxWidth.Location = new System.Drawing.Point(261, 65);
            this.tbxWidth.Name = "tbxWidth";
            this.tbxWidth.Size = new System.Drawing.Size(105, 36);
            this.tbxWidth.TabIndex = 3;
            this.tbxWidth.TextChanged += new System.EventHandler(this.tbxWidth_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(203, 30);
            this.label2.TabIndex = 2;
            this.label2.Text = "Width of the meter:";
            // 
            // chkShowUsage
            // 
            this.chkShowUsage.AutoSize = true;
            this.chkShowUsage.Location = new System.Drawing.Point(17, 130);
            this.chkShowUsage.Name = "chkShowUsage";
            this.chkShowUsage.Size = new System.Drawing.Size(324, 34);
            this.chkShowUsage.TabIndex = 4;
            this.chkShowUsage.Text = "Show CPU usage percentage";
            this.chkShowUsage.UseVisualStyleBackColor = true;
            this.chkShowUsage.CheckedChanged += new System.EventHandler(this.chkShowUsage_CheckedChanged);
            // 
            // chkShowClock
            // 
            this.chkShowClock.AutoSize = true;
            this.chkShowClock.Location = new System.Drawing.Point(17, 170);
            this.chkShowClock.Name = "chkShowClock";
            this.chkShowClock.Size = new System.Drawing.Size(249, 34);
            this.chkShowClock.TabIndex = 5;
            this.chkShowClock.Text = "Show CPU core clock";
            this.chkShowClock.UseVisualStyleBackColor = true;
            this.chkShowClock.CheckedChanged += new System.EventHandler(this.chkShowClock_CheckedChanged);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(17, 242);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(349, 59);
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // FrmOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 30F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(381, 319);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.chkShowClock);
            this.Controls.Add(this.chkShowUsage);
            this.Controls.Add(this.tbxWidth);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbxPosition);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FrmOptions";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Options";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbxPosition;
        private System.Windows.Forms.TextBox tbxWidth;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkShowUsage;
        private System.Windows.Forms.CheckBox chkShowClock;
        private System.Windows.Forms.Button btnClose;
    }
}