namespace ToDo
{
    partial class FlexiCommandsControl
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
            this.descriptionLabel = new System.Windows.Forms.RichTextBox();
            this.listedFlexiCommands = new System.Windows.Forms.ListBox();
            this.grouper1 = new CodeVendor.Controls.Grouper();
            this.flexiCommandTab = new FlatTabControl.FlatTabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.commandTree = new System.Windows.Forms.TreeView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.contextTree = new System.Windows.Forms.TreeView();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.rangeController = new Zzzz.ZzzzRangeBar();
            this.timeRangeTree = new System.Windows.Forms.TreeView();
            this.timeRangeKeywordTree = new System.Windows.Forms.TreeView();
            this.titleLabel = new System.Windows.Forms.Label();
            this.schedPostponePanel = new System.Windows.Forms.Panel();
            this.typeComboBox = new System.Windows.Forms.ComboBox();
            this.timeComboBox = new System.Windows.Forms.ComboBox();
            this.schedulePostponeLabel = new System.Windows.Forms.Label();
            this.addButton = new System.Windows.Forms.Button();
            this.removeButton = new System.Windows.Forms.Button();
            this.grouper1.SuspendLayout();
            this.flexiCommandTab.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.schedPostponePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // descriptionLabel
            // 
            this.descriptionLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.descriptionLabel.BackColor = System.Drawing.Color.Gainsboro;
            this.descriptionLabel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.descriptionLabel.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.descriptionLabel.ForeColor = System.Drawing.Color.Black;
            this.descriptionLabel.Location = new System.Drawing.Point(110, 58);
            this.descriptionLabel.Name = "descriptionLabel";
            this.descriptionLabel.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.descriptionLabel.Size = new System.Drawing.Size(292, 160);
            this.descriptionLabel.TabIndex = 13;
            this.descriptionLabel.Text = "Please go ahead and select a command to see it\'s description";
            // 
            // listedFlexiCommands
            // 
            this.listedFlexiCommands.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listedFlexiCommands.BackColor = System.Drawing.Color.Gainsboro;
            this.listedFlexiCommands.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listedFlexiCommands.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listedFlexiCommands.ForeColor = System.Drawing.Color.Black;
            this.listedFlexiCommands.FormattingEnabled = true;
            this.listedFlexiCommands.ItemHeight = 17;
            this.listedFlexiCommands.Location = new System.Drawing.Point(5, 13);
            this.listedFlexiCommands.Name = "listedFlexiCommands";
            this.listedFlexiCommands.Size = new System.Drawing.Size(396, 51);
            this.listedFlexiCommands.TabIndex = 2;
            // 
            // grouper1
            // 
            this.grouper1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grouper1.BackgroundColor = System.Drawing.Color.Gainsboro;
            this.grouper1.BackgroundGradientColor = System.Drawing.Color.Gainsboro;
            this.grouper1.BackgroundGradientMode = CodeVendor.Controls.Grouper.GroupBoxGradientMode.None;
            this.grouper1.BorderColor = System.Drawing.Color.Black;
            this.grouper1.BorderThickness = 1F;
            this.grouper1.Controls.Add(this.listedFlexiCommands);
            this.grouper1.CustomGroupBoxColor = System.Drawing.Color.White;
            this.grouper1.GroupImage = null;
            this.grouper1.GroupTitle = "";
            this.grouper1.Location = new System.Drawing.Point(3, 221);
            this.grouper1.Name = "grouper1";
            this.grouper1.Padding = new System.Windows.Forms.Padding(20);
            this.grouper1.PaintGroupBox = false;
            this.grouper1.RoundCorners = 10;
            this.grouper1.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouper1.ShadowControl = false;
            this.grouper1.ShadowThickness = 3;
            this.grouper1.Size = new System.Drawing.Size(408, 66);
            this.grouper1.TabIndex = 14;
            // 
            // flexiCommandTab
            // 
            this.flexiCommandTab.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flexiCommandTab.Controls.Add(this.tabPage1);
            this.flexiCommandTab.Controls.Add(this.tabPage2);
            this.flexiCommandTab.Controls.Add(this.tabPage3);
            this.flexiCommandTab.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.flexiCommandTab.Location = new System.Drawing.Point(3, 3);
            this.flexiCommandTab.myBackColor = System.Drawing.Color.Gainsboro;
            this.flexiCommandTab.Name = "flexiCommandTab";
            this.flexiCommandTab.SelectedIndex = 0;
            this.flexiCommandTab.Size = new System.Drawing.Size(408, 225);
            this.flexiCommandTab.TabIndex = 17;
            this.flexiCommandTab.SelectedIndexChanged += new System.EventHandler(this.flatTabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.Gainsboro;
            this.tabPage1.Controls.Add(this.commandTree);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(400, 196);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Commands";
            // 
            // commandTree
            // 
            this.commandTree.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.commandTree.BackColor = System.Drawing.Color.Gainsboro;
            this.commandTree.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.commandTree.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.commandTree.LineColor = System.Drawing.Color.Gainsboro;
            this.commandTree.Location = new System.Drawing.Point(-14, 0);
            this.commandTree.Name = "commandTree";
            this.commandTree.Size = new System.Drawing.Size(111, 252);
            this.commandTree.TabIndex = 0;
            this.commandTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.commandTree_AfterSelect);
            this.commandTree.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.commandTree_MouseDoubleClick);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.Gainsboro;
            this.tabPage2.Controls.Add(this.contextTree);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(400, 196);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Context";
            // 
            // contextTree
            // 
            this.contextTree.BackColor = System.Drawing.Color.Gainsboro;
            this.contextTree.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.contextTree.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.contextTree.ForeColor = System.Drawing.Color.Black;
            this.contextTree.LineColor = System.Drawing.Color.Gainsboro;
            this.contextTree.Location = new System.Drawing.Point(-14, 10);
            this.contextTree.Name = "contextTree";
            this.contextTree.Size = new System.Drawing.Size(111, 188);
            this.contextTree.TabIndex = 10;
            this.contextTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.contextTree_AfterSelect);
            this.contextTree.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.contextTree_NodeMouseDoubleClick);
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.Color.Gainsboro;
            this.tabPage3.Controls.Add(this.rangeController);
            this.tabPage3.Controls.Add(this.timeRangeTree);
            this.tabPage3.Controls.Add(this.timeRangeKeywordTree);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(400, 196);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Timing";
            // 
            // rangeController
            // 
            this.rangeController.DivisionNum = 23;
            this.rangeController.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.rangeController.HeightOfBar = 8;
            this.rangeController.HeightOfMark = 24;
            this.rangeController.HeightOfTick = 6;
            this.rangeController.InnerColor = System.Drawing.Color.Black;
            this.rangeController.Location = new System.Drawing.Point(3, 148);
            this.rangeController.Name = "rangeController";
            this.rangeController.Orientation = Zzzz.ZzzzRangeBar.RangeBarOrientation.horizontal;
            this.rangeController.RangeMaximum = 5;
            this.rangeController.RangeMinimum = 3;
            this.rangeController.ScaleOrientation = Zzzz.ZzzzRangeBar.TopBottomOrientation.bottom;
            this.rangeController.Size = new System.Drawing.Size(394, 45);
            this.rangeController.TabIndex = 19;
            this.rangeController.TotalMaximum = 23;
            this.rangeController.TotalMinimum = 0;
            this.rangeController.RangeChanged += new Zzzz.ZzzzRangeBar.RangeChangedEventHandler(this.rangeController_RangeChanged);
            // 
            // timeRangeTree
            // 
            this.timeRangeTree.BackColor = System.Drawing.Color.Gainsboro;
            this.timeRangeTree.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.timeRangeTree.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timeRangeTree.ForeColor = System.Drawing.Color.Black;
            this.timeRangeTree.LineColor = System.Drawing.Color.Gainsboro;
            this.timeRangeTree.Location = new System.Drawing.Point(-14, 82);
            this.timeRangeTree.Name = "timeRangeTree";
            this.timeRangeTree.Size = new System.Drawing.Size(111, 68);
            this.timeRangeTree.TabIndex = 18;
            this.timeRangeTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.timeRangeTree_AfterSelect);
            this.timeRangeTree.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.timeRangeTree_MouseDoubleClick);
            // 
            // timeRangeKeywordTree
            // 
            this.timeRangeKeywordTree.BackColor = System.Drawing.Color.Gainsboro;
            this.timeRangeKeywordTree.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.timeRangeKeywordTree.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timeRangeKeywordTree.ForeColor = System.Drawing.Color.Black;
            this.timeRangeKeywordTree.LineColor = System.Drawing.Color.Gainsboro;
            this.timeRangeKeywordTree.Location = new System.Drawing.Point(-14, 10);
            this.timeRangeKeywordTree.Name = "timeRangeKeywordTree";
            this.timeRangeKeywordTree.Size = new System.Drawing.Size(111, 76);
            this.timeRangeKeywordTree.TabIndex = 17;
            this.timeRangeKeywordTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.timeRangeKeywordTree_AfterSelect);
            this.timeRangeKeywordTree.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.timeRangeKeywordTree_MouseDoubleClick);
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.Font = new System.Drawing.Font("Century Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleLabel.ForeColor = System.Drawing.Color.Black;
            this.titleLabel.Location = new System.Drawing.Point(107, 32);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(186, 25);
            this.titleLabel.TabIndex = 12;
            this.titleLabel.Text = "Nothing Selected";
            // 
            // schedPostponePanel
            // 
            this.schedPostponePanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.schedPostponePanel.Controls.Add(this.typeComboBox);
            this.schedPostponePanel.Controls.Add(this.timeComboBox);
            this.schedPostponePanel.Controls.Add(this.schedulePostponeLabel);
            this.schedPostponePanel.Location = new System.Drawing.Point(108, 155);
            this.schedPostponePanel.Name = "schedPostponePanel";
            this.schedPostponePanel.Size = new System.Drawing.Size(293, 69);
            this.schedPostponePanel.TabIndex = 13;
            // 
            // typeComboBox
            // 
            this.typeComboBox.BackColor = System.Drawing.Color.Gainsboro;
            this.typeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.typeComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.typeComboBox.FormattingEnabled = true;
            this.typeComboBox.Items.AddRange(new object[] {
            "HOUR",
            "DAY",
            "WEEK",
            "MONTH"});
            this.typeComboBox.Location = new System.Drawing.Point(105, 44);
            this.typeComboBox.Name = "typeComboBox";
            this.typeComboBox.Size = new System.Drawing.Size(85, 21);
            this.typeComboBox.TabIndex = 3;
            this.typeComboBox.SelectedValueChanged += new System.EventHandler(this.typeComboBox_SelectedValueChanged);
            // 
            // timeComboBox
            // 
            this.timeComboBox.BackColor = System.Drawing.Color.Gainsboro;
            this.timeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.timeComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.timeComboBox.FormattingEnabled = true;
            this.timeComboBox.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23"});
            this.timeComboBox.Location = new System.Drawing.Point(3, 44);
            this.timeComboBox.Name = "timeComboBox";
            this.timeComboBox.Size = new System.Drawing.Size(96, 21);
            this.timeComboBox.TabIndex = 2;
            this.timeComboBox.SelectedValueChanged += new System.EventHandler(this.timeComboBox_SelectedValueChanged);
            // 
            // schedulePostponeLabel
            // 
            this.schedulePostponeLabel.AutoSize = true;
            this.schedulePostponeLabel.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.schedulePostponeLabel.Location = new System.Drawing.Point(0, 26);
            this.schedulePostponeLabel.Name = "schedulePostponeLabel";
            this.schedulePostponeLabel.Size = new System.Drawing.Size(123, 17);
            this.schedulePostponeLabel.TabIndex = 1;
            this.schedulePostponeLabel.Text = "Postpone Response";
            // 
            // addButton
            // 
            this.addButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.addButton.BackColor = System.Drawing.Color.DimGray;
            this.addButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addButton.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addButton.ForeColor = System.Drawing.Color.Gainsboro;
            this.addButton.Location = new System.Drawing.Point(3, 290);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(171, 23);
            this.addButton.TabIndex = 18;
            this.addButton.Text = "Add Command";
            this.addButton.UseVisualStyleBackColor = false;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // removeButton
            // 
            this.removeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.removeButton.BackColor = System.Drawing.Color.DimGray;
            this.removeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.removeButton.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.removeButton.ForeColor = System.Drawing.Color.Gainsboro;
            this.removeButton.Location = new System.Drawing.Point(235, 290);
            this.removeButton.Name = "removeButton";
            this.removeButton.Size = new System.Drawing.Size(176, 23);
            this.removeButton.TabIndex = 19;
            this.removeButton.Text = "Remove Command";
            this.removeButton.UseVisualStyleBackColor = false;
            this.removeButton.Click += new System.EventHandler(this.removeButton_Click);
            // 
            // FlexiCommandsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.Controls.Add(this.removeButton);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.schedPostponePanel);
            this.Controls.Add(this.titleLabel);
            this.Controls.Add(this.descriptionLabel);
            this.Controls.Add(this.flexiCommandTab);
            this.Controls.Add(this.grouper1);
            this.Name = "FlexiCommandsControl";
            this.Size = new System.Drawing.Size(414, 317);
            this.grouper1.ResumeLayout(false);
            this.flexiCommandTab.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.schedPostponePanel.ResumeLayout(false);
            this.schedPostponePanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView contextTree;
        private System.Windows.Forms.RichTextBox descriptionLabel;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.ListBox listedFlexiCommands;
        private CodeVendor.Controls.Grouper grouper1;
        private System.Windows.Forms.TreeView timeRangeKeywordTree;
        private FlatTabControl.FlatTabControl flexiCommandTab;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TreeView timeRangeTree;
        private Zzzz.ZzzzRangeBar rangeController;
        private System.Windows.Forms.TreeView commandTree;
        private System.Windows.Forms.Panel schedPostponePanel;
        private System.Windows.Forms.ComboBox typeComboBox;
        private System.Windows.Forms.ComboBox timeComboBox;
        private System.Windows.Forms.Label schedulePostponeLabel;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Button removeButton;
    }
}
