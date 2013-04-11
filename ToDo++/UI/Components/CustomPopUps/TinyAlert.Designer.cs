namespace ToDo
{
    partial class TinyAlert
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
            this.timerFadeIn = new System.Windows.Forms.Timer(this.components);
            this.timerFadeOut = new System.Windows.Forms.Timer(this.components);
            this.tinyAlertLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // timerFadeIn
            // 
            this.timerFadeIn.Interval = 15;
            this.timerFadeIn.Tick += new System.EventHandler(this.timerFadeIn_Tick);
            // 
            // timerFadeOut
            // 
            this.timerFadeOut.Interval = 15;
            this.timerFadeOut.Tick += new System.EventHandler(this.timerFadeOut_Tick);
            // 
            // tinyAlertLabel
            // 
            this.tinyAlertLabel.AutoSize = true;
            this.tinyAlertLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tinyAlertLabel.ForeColor = System.Drawing.Color.White;
            this.tinyAlertLabel.Location = new System.Drawing.Point(8, 7);
            this.tinyAlertLabel.Name = "tinyAlertLabel";
            this.tinyAlertLabel.Size = new System.Drawing.Size(35, 13);
            this.tinyAlertLabel.TabIndex = 0;
            this.tinyAlertLabel.Text = "label1";
            // 
            // TinyAlert
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Maroon;
            this.ClientSize = new System.Drawing.Size(468, 27);
            this.Controls.Add(this.tinyAlertLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "TinyAlert";
            this.Text = "displayForm";
            this.Resize += new System.EventHandler(this.displayForm_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timerFadeIn;
        private System.Windows.Forms.Timer timerFadeOut;
        private System.Windows.Forms.Label tinyAlertLabel;
    }
}