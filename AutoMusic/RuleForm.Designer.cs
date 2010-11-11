namespace AutoMusic
{
    partial class RuleForm
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
            this.FirstHour = new System.Windows.Forms.NumericUpDown();
            this.FirstMinute = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.Save = new System.Windows.Forms.Button();
            this.Cancel = new System.Windows.Forms.Button();
            this.Allow = new System.Windows.Forms.RadioButton();
            this.Disallow = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.SecondMinute = new System.Windows.Forms.NumericUpDown();
            this.SecondHour = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.FirstHour)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FirstMinute)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SecondMinute)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SecondHour)).BeginInit();
            this.SuspendLayout();
            // 
            // FirstHour
            // 
            this.FirstHour.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.FirstHour.Location = new System.Drawing.Point(89, 72);
            this.FirstHour.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.FirstHour.Name = "FirstHour";
            this.FirstHour.Size = new System.Drawing.Size(53, 31);
            this.FirstHour.TabIndex = 1;
            this.FirstHour.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // FirstMinute
            // 
            this.FirstMinute.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.FirstMinute.Location = new System.Drawing.Point(148, 72);
            this.FirstMinute.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.FirstMinute.Name = "FirstMinute";
            this.FirstMinute.Size = new System.Drawing.Size(53, 31);
            this.FirstMinute.TabIndex = 2;
            this.FirstMinute.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(12, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "... between";
            // 
            // Save
            // 
            this.Save.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Save.Location = new System.Drawing.Point(12, 165);
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(127, 31);
            this.Save.TabIndex = 5;
            this.Save.Text = "Add Rule";
            this.Save.UseVisualStyleBackColor = true;
            this.Save.Click += new System.EventHandler(this.Sync_Click);
            // 
            // Cancel
            // 
            this.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancel.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Cancel.Location = new System.Drawing.Point(145, 165);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(103, 31);
            this.Cancel.TabIndex = 6;
            this.Cancel.Text = "Cancel";
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // Allow
            // 
            this.Allow.AutoSize = true;
            this.Allow.Checked = true;
            this.Allow.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Allow.ForeColor = System.Drawing.Color.DarkGreen;
            this.Allow.Location = new System.Drawing.Point(16, 12);
            this.Allow.Name = "Allow";
            this.Allow.Size = new System.Drawing.Size(117, 19);
            this.Allow.TabIndex = 7;
            this.Allow.TabStop = true;
            this.Allow.Text = "Allow playback ...";
            this.Allow.UseVisualStyleBackColor = true;
            // 
            // Disallow
            // 
            this.Disallow.AutoSize = true;
            this.Disallow.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Disallow.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.Disallow.Location = new System.Drawing.Point(16, 37);
            this.Disallow.Name = "Disallow";
            this.Disallow.Size = new System.Drawing.Size(131, 19);
            this.Disallow.TabIndex = 8;
            this.Disallow.TabStop = true;
            this.Disallow.Text = "Disallow playback ...";
            this.Disallow.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(42, 118);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 15);
            this.label1.TabIndex = 9;
            this.label1.Text = "... and";
            // 
            // SecondMinute
            // 
            this.SecondMinute.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.SecondMinute.Location = new System.Drawing.Point(148, 110);
            this.SecondMinute.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.SecondMinute.Name = "SecondMinute";
            this.SecondMinute.Size = new System.Drawing.Size(53, 31);
            this.SecondMinute.TabIndex = 11;
            this.SecondMinute.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // SecondHour
            // 
            this.SecondHour.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.SecondHour.Location = new System.Drawing.Point(89, 110);
            this.SecondHour.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.SecondHour.Name = "SecondHour";
            this.SecondHour.Size = new System.Drawing.Size(53, 31);
            this.SecondHour.TabIndex = 10;
            this.SecondHour.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // RuleForm
            // 
            this.AcceptButton = this.Save;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.Cancel;
            this.ClientSize = new System.Drawing.Size(260, 208);
            this.Controls.Add(this.SecondMinute);
            this.Controls.Add(this.SecondHour);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Disallow);
            this.Controls.Add(this.Allow);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.Save);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.FirstMinute);
            this.Controls.Add(this.FirstHour);
            this.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RuleForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add a rule";
            ((System.ComponentModel.ISupportInitialize)(this.FirstHour)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FirstMinute)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SecondMinute)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SecondHour)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button Save;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.NumericUpDown FirstHour;
        public System.Windows.Forms.NumericUpDown FirstMinute;
        public System.Windows.Forms.NumericUpDown SecondMinute;
        public System.Windows.Forms.NumericUpDown SecondHour;
        public System.Windows.Forms.RadioButton Allow;
        public System.Windows.Forms.RadioButton Disallow;
    }
}