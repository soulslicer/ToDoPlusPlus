namespace ToDo
{
    partial class StartingOptions
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.minimisedCheckbox = new System.Windows.Forms.CheckBox();
            this.loadOnStartupCheckbox = new System.Windows.Forms.CheckBox();
            this.stayOnTopCheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // minimisedCheckbox
            // 
            this.minimisedCheckbox.AutoSize = true;
            this.minimisedCheckbox.BackColor = System.Drawing.Color.Gainsboro;
            this.minimisedCheckbox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.minimisedCheckbox.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.minimisedCheckbox.ForeColor = System.Drawing.Color.Black;
            this.minimisedCheckbox.Location = new System.Drawing.Point(8, 4);
            this.minimisedCheckbox.Name = "minimisedCheckbox";
            this.minimisedCheckbox.Size = new System.Drawing.Size(106, 20);
            this.minimisedCheckbox.TabIndex = 2;
            this.minimisedCheckbox.Text = "Start Minimized";
            this.minimisedCheckbox.UseVisualStyleBackColor = false;
            this.minimisedCheckbox.CheckedChanged += new System.EventHandler(this.minimisedCheckbox_CheckedChanged);
            // 
            // loadOnStartupCheckbox
            // 
            this.loadOnStartupCheckbox.AutoSize = true;
            this.loadOnStartupCheckbox.BackColor = System.Drawing.Color.Gainsboro;
            this.loadOnStartupCheckbox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.loadOnStartupCheckbox.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loadOnStartupCheckbox.ForeColor = System.Drawing.Color.Black;
            this.loadOnStartupCheckbox.Location = new System.Drawing.Point(8, 24);
            this.loadOnStartupCheckbox.Name = "loadOnStartupCheckbox";
            this.loadOnStartupCheckbox.Size = new System.Drawing.Size(115, 20);
            this.loadOnStartupCheckbox.TabIndex = 3;
            this.loadOnStartupCheckbox.Text = "Load On Startup";
            this.loadOnStartupCheckbox.UseVisualStyleBackColor = false;
            this.loadOnStartupCheckbox.CheckedChanged += new System.EventHandler(this.loadOnStartupCheckbox_CheckedChanged);
            // 
            // stayOnTopCheckBox
            // 
            this.stayOnTopCheckBox.AutoSize = true;
            this.stayOnTopCheckBox.BackColor = System.Drawing.Color.Gainsboro;
            this.stayOnTopCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.stayOnTopCheckBox.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stayOnTopCheckBox.ForeColor = System.Drawing.Color.Black;
            this.stayOnTopCheckBox.Location = new System.Drawing.Point(8, 45);
            this.stayOnTopCheckBox.Name = "stayOnTopCheckBox";
            this.stayOnTopCheckBox.Size = new System.Drawing.Size(91, 20);
            this.stayOnTopCheckBox.TabIndex = 4;
            this.stayOnTopCheckBox.Text = "Stay On Top";
            this.stayOnTopCheckBox.UseVisualStyleBackColor = false;
            this.stayOnTopCheckBox.CheckedChanged += new System.EventHandler(this.stayOnTopCheckBox_CheckedChanged);
            // 
            // StartingOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.Controls.Add(this.stayOnTopCheckBox);
            this.Controls.Add(this.loadOnStartupCheckbox);
            this.Controls.Add(this.minimisedCheckbox);
            this.Name = "StartingOptions";
            this.Size = new System.Drawing.Size(248, 66);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox minimisedCheckbox;
        private System.Windows.Forms.CheckBox loadOnStartupCheckbox;
        private System.Windows.Forms.CheckBox stayOnTopCheckBox;
    }
}
