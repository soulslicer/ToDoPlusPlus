namespace ToDo
{
    partial class TopMenuControl
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
            this.questionButton = new System.Windows.Forms.Button();
            this.closeButton = new System.Windows.Forms.Button();
            this.minButton = new System.Windows.Forms.Button();
            this.updownButton = new System.Windows.Forms.Button();
            this.settingsButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // questionButton
            // 
            this.questionButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.questionButton.ForeColor = System.Drawing.Color.DimGray;
            this.questionButton.Image = global::ToDo.Properties.Resources.questionButton;
            this.questionButton.Location = new System.Drawing.Point(0, 0);
            this.questionButton.Name = "questionButton";
            this.questionButton.Size = new System.Drawing.Size(35, 30);
            this.questionButton.TabIndex = 4;
            this.questionButton.UseVisualStyleBackColor = false;
            this.questionButton.Click += new System.EventHandler(this.questionButton_Click);
            // 
            // closeButton
            // 
            this.closeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.closeButton.ForeColor = System.Drawing.Color.DimGray;
            this.closeButton.Image = global::ToDo.Properties.Resources.closeButton;
            this.closeButton.Location = new System.Drawing.Point(140, 0);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(35, 30);
            this.closeButton.TabIndex = 3;
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // minButton
            // 
            this.minButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.minButton.ForeColor = System.Drawing.Color.DimGray;
            this.minButton.Image = global::ToDo.Properties.Resources.minimizeButton;
            this.minButton.Location = new System.Drawing.Point(105, 0);
            this.minButton.Name = "minButton";
            this.minButton.Size = new System.Drawing.Size(35, 30);
            this.minButton.TabIndex = 2;
            this.minButton.UseVisualStyleBackColor = true;
            this.minButton.Click += new System.EventHandler(this.minButton_Click);
            // 
            // updownButton
            // 
            this.updownButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.updownButton.ForeColor = System.Drawing.Color.DimGray;
            this.updownButton.Image = global::ToDo.Properties.Resources.upButton;
            this.updownButton.Location = new System.Drawing.Point(70, 0);
            this.updownButton.Name = "updownButton";
            this.updownButton.Size = new System.Drawing.Size(35, 30);
            this.updownButton.TabIndex = 1;
            this.updownButton.UseVisualStyleBackColor = true;
            this.updownButton.Click += new System.EventHandler(this.updownButton_Click);
            // 
            // settingsButton
            // 
            this.settingsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.settingsButton.ForeColor = System.Drawing.Color.DimGray;
            this.settingsButton.Image = global::ToDo.Properties.Resources.gearButton;
            this.settingsButton.Location = new System.Drawing.Point(35, 0);
            this.settingsButton.Name = "settingsButton";
            this.settingsButton.Size = new System.Drawing.Size(35, 30);
            this.settingsButton.TabIndex = 0;
            this.settingsButton.UseVisualStyleBackColor = true;
            this.settingsButton.Click += new System.EventHandler(this.settingsButton_Click);
            // 
            // TopMenuControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.Controls.Add(this.questionButton);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.minButton);
            this.Controls.Add(this.updownButton);
            this.Controls.Add(this.settingsButton);
            this.Name = "TopMenuControl";
            this.Size = new System.Drawing.Size(180, 33);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button settingsButton;
        private System.Windows.Forms.Button updownButton;
        private System.Windows.Forms.Button minButton;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Button questionButton;
    }
}
