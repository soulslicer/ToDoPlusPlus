namespace ToDo
{
    partial class HelpControl
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
            this.components = new System.ComponentModel.Container();
            this.fadeInTimer = new System.Windows.Forms.Timer(this.components);
            this.fadeOutTimer = new System.Windows.Forms.Timer(this.components);
            this.customPanelControl = new ToDo.CustomPanelControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.transpControl = new TranspControl.TranspControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.introButton = new System.Windows.Forms.Button();
            this.manualButton = new System.Windows.Forms.Button();
            this.customPanelControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // fadeInTimer
            // 
            this.fadeInTimer.Interval = 1;
            this.fadeInTimer.Tick += new System.EventHandler(this.fadeInTimer_Tick);
            // 
            // fadeOutTimer
            // 
            this.fadeOutTimer.Interval = 1;
            this.fadeOutTimer.Tick += new System.EventHandler(this.fadeOutTimer_Tick);
            // 
            // customPanelControl
            // 
            this.customPanelControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.customPanelControl.Controls.Add(this.tabPage1);
            this.customPanelControl.Controls.Add(this.tabPage2);
            this.customPanelControl.Location = new System.Drawing.Point(3, 3);
            this.customPanelControl.Name = "customPanelControl";
            this.customPanelControl.SelectedIndex = 0;
            this.customPanelControl.Size = new System.Drawing.Size(506, 366);
            this.customPanelControl.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.Gainsboro;
            this.tabPage1.Controls.Add(this.pictureBox);
            this.tabPage1.Controls.Add(this.transpControl);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(498, 340);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "IntroPage";
            // 
            // pictureBox
            // 
            this.pictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox.BackColor = System.Drawing.Color.Gainsboro;
            this.pictureBox.Image = global::ToDo.Properties.Resources.helpFrame1;
            this.pictureBox.Location = new System.Drawing.Point(3, 3);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(492, 331);
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            // 
            // transpControl
            // 
            this.transpControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.transpControl.BackColor = System.Drawing.Color.Transparent;
            this.transpControl.BackImage = null;
            this.transpControl.FillColor = System.Drawing.Color.Gainsboro;
            this.transpControl.ForeColor = System.Drawing.Color.Gainsboro;
            this.transpControl.GlassColor = System.Drawing.Color.Transparent;
            this.transpControl.GlassMode = true;
            this.transpControl.LineWidth = 2;
            this.transpControl.Location = new System.Drawing.Point(3, 3);
            this.transpControl.Name = "transpControl";
            this.transpControl.Opacity = 100;
            this.transpControl.Size = new System.Drawing.Size(492, 331);
            this.transpControl.TabIndex = 1;
            this.transpControl.TranspKey = System.Drawing.Color.White;
            this.transpControl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.transpControl_MouseDown);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.Gainsboro;
            this.tabPage2.Controls.Add(this.manualButton);
            this.tabPage2.Controls.Add(this.introButton);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(498, 340);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Help Page";
            // 
            // introButton
            // 
            this.introButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.introButton.BackColor = System.Drawing.Color.DimGray;
            this.introButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.introButton.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.introButton.ForeColor = System.Drawing.Color.Gainsboro;
            this.introButton.Location = new System.Drawing.Point(3, 6);
            this.introButton.Name = "introButton";
            this.introButton.Size = new System.Drawing.Size(492, 26);
            this.introButton.TabIndex = 1;
            this.introButton.Text = "View Intro Again";
            this.introButton.UseVisualStyleBackColor = false;
            this.introButton.Click += new System.EventHandler(this.introButton_Click);
            // 
            // manualButton
            // 
            this.manualButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.manualButton.BackColor = System.Drawing.Color.DimGray;
            this.manualButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.manualButton.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.manualButton.ForeColor = System.Drawing.Color.Gainsboro;
            this.manualButton.Location = new System.Drawing.Point(3, 38);
            this.manualButton.Name = "manualButton";
            this.manualButton.Size = new System.Drawing.Size(492, 26);
            this.manualButton.TabIndex = 2;
            this.manualButton.Text = "User Manual";
            this.manualButton.UseVisualStyleBackColor = false;
            this.manualButton.Click += new System.EventHandler(this.manualButton_Click);
            // 
            // HelpControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.Controls.Add(this.customPanelControl);
            this.Name = "HelpControl";
            this.Size = new System.Drawing.Size(512, 372);
            this.Leave += new System.EventHandler(this.HelpControl_Leave);
            this.customPanelControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private CustomPanelControl customPanelControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private TranspControl.TranspControl transpControl;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Timer fadeInTimer;
        private System.Windows.Forms.Timer fadeOutTimer;
        private System.Windows.Forms.Button introButton;
        private System.Windows.Forms.Button manualButton;
    }
}
