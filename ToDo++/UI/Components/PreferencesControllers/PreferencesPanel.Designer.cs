namespace ToDo
{
    partial class PreferencesPanel
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
            this.grouper1 = new CodeVendor.Controls.Grouper();
            this.preferencesTitle = new CodeVendor.Controls.Grouper();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.preferencesSelector = new ToDo.CustomPanelControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.startingOptionsControl = new ToDo.StartingOptions();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.flexiCommandsControl = new ToDo.FlexiCommandsControl();
            this.fontPage = new System.Windows.Forms.TabPage();
            this.fontColorSettingsControl = new ToDo.FontColorSettings();
            this.preferencesTree = new ToDo.TreeViewNoFlicker();
            this.grouper1.SuspendLayout();
            this.preferencesTitle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.preferencesSelector.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.fontPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // grouper1
            // 
            this.grouper1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.grouper1.BackgroundColor = System.Drawing.Color.Gainsboro;
            this.grouper1.BackgroundGradientColor = System.Drawing.Color.Gainsboro;
            this.grouper1.BackgroundGradientMode = CodeVendor.Controls.Grouper.GroupBoxGradientMode.None;
            this.grouper1.BorderColor = System.Drawing.Color.Black;
            this.grouper1.BorderThickness = 1F;
            this.grouper1.Controls.Add(this.pictureBox1);
            this.grouper1.Controls.Add(this.preferencesTree);
            this.grouper1.CustomGroupBoxColor = System.Drawing.Color.White;
            this.grouper1.GroupImage = null;
            this.grouper1.GroupTitle = "";
            this.grouper1.Location = new System.Drawing.Point(4, -6);
            this.grouper1.Name = "grouper1";
            this.grouper1.Padding = new System.Windows.Forms.Padding(20);
            this.grouper1.PaintGroupBox = false;
            this.grouper1.RoundCorners = 10;
            this.grouper1.ShadowColor = System.Drawing.Color.Gray;
            this.grouper1.ShadowControl = true;
            this.grouper1.ShadowThickness = 3;
            this.grouper1.Size = new System.Drawing.Size(110, 315);
            this.grouper1.TabIndex = 5;
            // 
            // preferencesTitle
            // 
            this.preferencesTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.preferencesTitle.BackgroundColor = System.Drawing.Color.Gainsboro;
            this.preferencesTitle.BackgroundGradientColor = System.Drawing.Color.Gainsboro;
            this.preferencesTitle.BackgroundGradientMode = CodeVendor.Controls.Grouper.GroupBoxGradientMode.None;
            this.preferencesTitle.BorderColor = System.Drawing.Color.Black;
            this.preferencesTitle.BorderThickness = 1F;
            this.preferencesTitle.Controls.Add(this.preferencesSelector);
            this.preferencesTitle.CustomGroupBoxColor = System.Drawing.Color.White;
            this.preferencesTitle.GroupImage = null;
            this.preferencesTitle.GroupTitle = "";
            this.preferencesTitle.Location = new System.Drawing.Point(123, -6);
            this.preferencesTitle.Name = "preferencesTitle";
            this.preferencesTitle.Padding = new System.Windows.Forms.Padding(20);
            this.preferencesTitle.PaintGroupBox = false;
            this.preferencesTitle.RoundCorners = 10;
            this.preferencesTitle.ShadowColor = System.Drawing.Color.Gray;
            this.preferencesTitle.ShadowControl = true;
            this.preferencesTitle.ShadowThickness = 3;
            this.preferencesTitle.Size = new System.Drawing.Size(419, 315);
            this.preferencesTitle.TabIndex = 6;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.pictureBox1.BackColor = System.Drawing.Color.Black;
            this.pictureBox1.Location = new System.Drawing.Point(-9, 23);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(10, 270);
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            // 
            // preferencesSelector
            // 
            this.preferencesSelector.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.preferencesSelector.Controls.Add(this.tabPage1);
            this.preferencesSelector.Controls.Add(this.tabPage2);
            this.preferencesSelector.Controls.Add(this.fontPage);
            this.preferencesSelector.Location = new System.Drawing.Point(5, 14);
            this.preferencesSelector.Name = "preferencesSelector";
            this.preferencesSelector.SelectedIndex = 0;
            this.preferencesSelector.Size = new System.Drawing.Size(407, 295);
            this.preferencesSelector.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.Gainsboro;
            this.tabPage1.Controls.Add(this.startingOptionsControl);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(399, 269);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Starting Options";
            // 
            // startingOptionsControl
            // 
            this.startingOptionsControl.BackColor = System.Drawing.Color.Gainsboro;
            this.startingOptionsControl.Location = new System.Drawing.Point(6, 6);
            this.startingOptionsControl.Name = "startingOptionsControl";
            this.startingOptionsControl.Size = new System.Drawing.Size(273, 66);
            this.startingOptionsControl.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.Gainsboro;
            this.tabPage2.Controls.Add(this.flexiCommandsControl);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(399, 269);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "FlexiCommands";
            // 
            // flexiCommandsControl
            // 
            this.flexiCommandsControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flexiCommandsControl.BackColor = System.Drawing.Color.Gainsboro;
            this.flexiCommandsControl.Location = new System.Drawing.Point(0, 0);
            this.flexiCommandsControl.Name = "flexiCommandsControl";
            this.flexiCommandsControl.Size = new System.Drawing.Size(396, 269);
            this.flexiCommandsControl.TabIndex = 0;
            // 
            // fontPage
            // 
            this.fontPage.BackColor = System.Drawing.Color.Gainsboro;
            this.fontPage.Controls.Add(this.fontColorSettingsControl);
            this.fontPage.Location = new System.Drawing.Point(4, 22);
            this.fontPage.Name = "fontPage";
            this.fontPage.Padding = new System.Windows.Forms.Padding(3);
            this.fontPage.Size = new System.Drawing.Size(399, 269);
            this.fontPage.TabIndex = 2;
            this.fontPage.Text = "Font";
            // 
            // fontColorSettingsControl
            // 
            this.fontColorSettingsControl.BackColor = System.Drawing.Color.Gainsboro;
            this.fontColorSettingsControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fontColorSettingsControl.Location = new System.Drawing.Point(3, 3);
            this.fontColorSettingsControl.Name = "fontColorSettingsControl";
            this.fontColorSettingsControl.Size = new System.Drawing.Size(393, 263);
            this.fontColorSettingsControl.TabIndex = 0;
            // 
            // preferencesTree
            // 
            this.preferencesTree.BackColor = System.Drawing.Color.Gainsboro;
            this.preferencesTree.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.preferencesTree.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.preferencesTree.LineColor = System.Drawing.Color.Gainsboro;
            this.preferencesTree.Location = new System.Drawing.Point(-14, 23);
            this.preferencesTree.Name = "preferencesTree";
            this.preferencesTree.Scrollable = false;
            this.preferencesTree.ShowNodeToolTips = true;
            this.preferencesTree.Size = new System.Drawing.Size(112, 260);
            this.preferencesTree.TabIndex = 1;
            this.preferencesTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.preferencesTree_AfterSelect);
            // 
            // PreferencesPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.Controls.Add(this.preferencesTitle);
            this.Controls.Add(this.grouper1);
            this.Name = "PreferencesPanel";
            this.Size = new System.Drawing.Size(545, 314);
            this.grouper1.ResumeLayout(false);
            this.preferencesTitle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.preferencesSelector.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.fontPage.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private CustomPanelControl preferencesSelector;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private StartingOptions startingOptionsControl;
        private FlexiCommandsControl flexiCommandsControl;
        private System.Windows.Forms.TabPage fontPage;
        private FontColorSettings fontColorSettingsControl;
        private CodeVendor.Controls.Grouper grouper1;
        private CodeVendor.Controls.Grouper preferencesTitle;
        private TreeViewNoFlicker preferencesTree;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}
