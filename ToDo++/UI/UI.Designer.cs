namespace ToDo
{
    partial class UI
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
            BrightIdeasSoftware.OLVColumn taskDateTimeCol;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UI));
            this.notifyIcon_taskBar = new System.Windows.Forms.NotifyIcon(this.components);
            this.timerCollapse = new System.Windows.Forms.Timer(this.components);
            this.timerExpand = new System.Windows.Forms.Timer(this.components);
            this.timerFadeIn = new System.Windows.Forms.Timer(this.components);
            this.timerFadeOut = new System.Windows.Forms.Timer(this.components);
            this.grayFadePictureBox = new TranspControl.TranspControl();
            this.grayFadeTimer = new System.Windows.Forms.Timer(this.components);
            this.transpControl1 = new TranspControl.TranspControl();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.textInput = new ToDo.InputBox();
            this.customPanelControl = new ToDo.CustomPanelControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.taskListViewControl = new ToDo.TaskListViewControl();
            this.bufferCol = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.taskIndexCol = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.taskNameCol = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.taskDoneStateCol = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.preferencesPanel = new ToDo.PreferencesPanel();
            this.consoleDisplay = new System.Windows.Forms.TabPage();
            this.outputBox = new ToDo.OutputBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.inputBox2 = new ToDo.InputBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.inputBox1 = new ToDo.InputBox();
            this.pictureBox7 = new System.Windows.Forms.PictureBox();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.helpTab = new System.Windows.Forms.TabPage();
            this.helpControl = new ToDo.HelpControl();
            this.topMenuControl = new ToDo.TopMenuControl();
            taskDateTimeCol = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.customPanelControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.taskListViewControl)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.consoleDisplay.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.helpTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // taskDateTimeCol
            // 
            taskDateTimeCol.AspectName = "GetTimeString";
            taskDateTimeCol.CellPadding = null;
            taskDateTimeCol.FillsFreeSpace = true;
            taskDateTimeCol.Width = 149;
            // 
            // notifyIcon_taskBar
            // 
            this.notifyIcon_taskBar.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon_taskBar.Icon")));
            this.notifyIcon_taskBar.Text = "notifyIcon";
            this.notifyIcon_taskBar.Visible = true;
            this.notifyIcon_taskBar.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.NotifyIcon_MouseDoubleClick);
            // 
            // timerCollapse
            // 
            this.timerCollapse.Interval = 1;
            this.timerCollapse.Tick += new System.EventHandler(this.timerCollapse_Tick);
            // 
            // timerExpand
            // 
            this.timerExpand.Interval = 1;
            this.timerExpand.Tick += new System.EventHandler(this.timerExpand_Tick);
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
            // grayFadePictureBox
            // 
            this.grayFadePictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grayFadePictureBox.BackColor = System.Drawing.Color.Transparent;
            this.grayFadePictureBox.BackImage = null;
            this.grayFadePictureBox.FillColor = System.Drawing.Color.Gainsboro;
            this.grayFadePictureBox.GlassColor = System.Drawing.Color.Gainsboro;
            this.grayFadePictureBox.GlassMode = true;
            this.grayFadePictureBox.LineWidth = 2;
            this.grayFadePictureBox.Location = new System.Drawing.Point(1, 37);
            this.grayFadePictureBox.Name = "grayFadePictureBox";
            this.grayFadePictureBox.Opacity = 100;
            this.grayFadePictureBox.Size = new System.Drawing.Size(519, 347);
            this.grayFadePictureBox.TabIndex = 18;
            this.grayFadePictureBox.TranspKey = System.Drawing.Color.White;
            // 
            // grayFadeTimer
            // 
            this.grayFadeTimer.Interval = 1;
            this.grayFadeTimer.Tick += new System.EventHandler(this.grayFadeTimer_Tick);
            // 
            // transpControl1
            // 
            this.transpControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.transpControl1.BackColor = System.Drawing.Color.Transparent;
            this.transpControl1.BackImage = null;
            this.transpControl1.Enabled = false;
            this.transpControl1.FillColor = System.Drawing.Color.Gainsboro;
            this.transpControl1.GlassColor = System.Drawing.Color.Gray;
            this.transpControl1.GlassMode = true;
            this.transpControl1.LineWidth = 2;
            this.transpControl1.Location = new System.Drawing.Point(0, 0);
            this.transpControl1.Name = "transpControl1";
            this.transpControl1.Opacity = 100;
            this.transpControl1.Size = new System.Drawing.Size(521, 390);
            this.transpControl1.TabIndex = 19;
            this.transpControl1.TranspKey = System.Drawing.Color.White;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Enabled = false;
            this.pictureBox1.Image = global::ToDo.Properties.Resources.logo4;
            this.pictureBox1.Location = new System.Drawing.Point(-2, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(250, 30);
            this.pictureBox1.TabIndex = 20;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox2.BackColor = System.Drawing.Color.DimGray;
            this.pictureBox2.Enabled = false;
            this.pictureBox2.Location = new System.Drawing.Point(-17, -4);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(539, 35);
            this.pictureBox2.TabIndex = 16;
            this.pictureBox2.TabStop = false;
            // 
            // textInput
            // 
            this.textInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textInput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(148)))), ((int)(((byte)(148)))));
            this.textInput.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textInput.Cursor = System.Windows.Forms.Cursors.Hand;
            this.textInput.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textInput.ForeColor = System.Drawing.Color.White;
            this.textInput.Location = new System.Drawing.Point(11, 394);
            this.textInput.Multiline = true;
            this.textInput.Name = "textInput";
            this.textInput.Size = new System.Drawing.Size(501, 20);
            this.textInput.TabIndex = 0;
            this.textInput.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_input_KeyPress);
            // 
            // customPanelControl
            // 
            this.customPanelControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.customPanelControl.Controls.Add(this.tabPage1);
            this.customPanelControl.Controls.Add(this.tabPage2);
            this.customPanelControl.Controls.Add(this.consoleDisplay);
            this.customPanelControl.Controls.Add(this.helpTab);
            this.customPanelControl.Location = new System.Drawing.Point(7, 37);
            this.customPanelControl.Name = "customPanelControl";
            this.customPanelControl.SelectedIndex = 0;
            this.customPanelControl.Size = new System.Drawing.Size(509, 347);
            this.customPanelControl.TabIndex = 12;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.Gainsboro;
            this.tabPage1.Controls.Add(this.taskListViewControl);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(501, 321);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Main";
            // 
            // taskListViewControl
            // 
            this.taskListViewControl.AllColumns.Add(this.bufferCol);
            this.taskListViewControl.AllColumns.Add(this.taskIndexCol);
            this.taskListViewControl.AllColumns.Add(this.taskNameCol);
            this.taskListViewControl.AllColumns.Add(taskDateTimeCol);
            this.taskListViewControl.AllColumns.Add(this.taskDoneStateCol);
            this.taskListViewControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.taskListViewControl.BackColor = System.Drawing.Color.LightGray;
            this.taskListViewControl.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.taskListViewControl.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.bufferCol,
            this.taskIndexCol,
            this.taskNameCol,
            taskDateTimeCol,
            this.taskDoneStateCol});
            this.taskListViewControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.taskListViewControl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.taskListViewControl.Location = new System.Drawing.Point(3, 3);
            this.taskListViewControl.Name = "taskListViewControl";
            this.taskListViewControl.Size = new System.Drawing.Size(495, 315);
            this.taskListViewControl.TabIndex = 17;
            this.taskListViewControl.UseCellFormatEvents = true;
            this.taskListViewControl.UseCompatibleStateImageBehavior = false;
            this.taskListViewControl.View = System.Windows.Forms.View.Details;
            this.taskListViewControl.BeforeSorting += new System.EventHandler<BrightIdeasSoftware.BeforeSortingEventArgs>(this.taskListViewControl_BeforeSorting);
            this.taskListViewControl.FormatRow += new System.EventHandler<BrightIdeasSoftware.FormatRowEventArgs>(this.taskListViewControl_FormatRow);
            this.taskListViewControl.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.SelectTextInput);
            this.taskListViewControl.MouseEnter += new System.EventHandler(this.taskListViewControl_MouseEnter);
            this.taskListViewControl.MouseLeave += new System.EventHandler(this.taskListViewControl_MouseLeave);
            // 
            // bufferCol
            // 
            this.bufferCol.CellPadding = null;
            this.bufferCol.MinimumWidth = 10;
            this.bufferCol.Width = 10;
            // 
            // taskIndexCol
            // 
            this.taskIndexCol.CellPadding = null;
            this.taskIndexCol.MaximumWidth = 40;
            this.taskIndexCol.MinimumWidth = 25;
            this.taskIndexCol.Width = 40;
            // 
            // taskNameCol
            // 
            this.taskNameCol.AspectName = "TaskName";
            this.taskNameCol.CellPadding = null;
            this.taskNameCol.FillsFreeSpace = true;
            this.taskNameCol.Width = 213;
            this.taskNameCol.WordWrap = true;
            // 
            // taskDoneStateCol
            // 
            this.taskDoneStateCol.AspectName = "DoneState";
            this.taskDoneStateCol.CellPadding = null;
            this.taskDoneStateCol.CellVerticalAlignment = System.Drawing.StringAlignment.Far;
            this.taskDoneStateCol.Width = 82;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.Gainsboro;
            this.tabPage2.Controls.Add(this.preferencesPanel);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(501, 321);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Preferences";
            // 
            // preferencesPanel
            // 
            this.preferencesPanel.BackColor = System.Drawing.Color.Gainsboro;
            this.preferencesPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.preferencesPanel.Location = new System.Drawing.Point(3, 3);
            this.preferencesPanel.Name = "preferencesPanel";
            this.preferencesPanel.Size = new System.Drawing.Size(495, 315);
            this.preferencesPanel.TabIndex = 0;
            // 
            // consoleDisplay
            // 
            this.consoleDisplay.BackColor = System.Drawing.Color.Gainsboro;
            this.consoleDisplay.Controls.Add(this.outputBox);
            this.consoleDisplay.Controls.Add(this.textBox3);
            this.consoleDisplay.Controls.Add(this.inputBox2);
            this.consoleDisplay.Controls.Add(this.textBox2);
            this.consoleDisplay.Controls.Add(this.textBox1);
            this.consoleDisplay.Controls.Add(this.inputBox1);
            this.consoleDisplay.Controls.Add(this.pictureBox7);
            this.consoleDisplay.Controls.Add(this.pictureBox6);
            this.consoleDisplay.Controls.Add(this.pictureBox5);
            this.consoleDisplay.Controls.Add(this.pictureBox4);
            this.consoleDisplay.Controls.Add(this.pictureBox3);
            this.consoleDisplay.Location = new System.Drawing.Point(4, 22);
            this.consoleDisplay.Name = "consoleDisplay";
            this.consoleDisplay.Padding = new System.Windows.Forms.Padding(3);
            this.consoleDisplay.Size = new System.Drawing.Size(501, 321);
            this.consoleDisplay.TabIndex = 2;
            this.consoleDisplay.Text = "Console";
            // 
            // outputBox
            // 
            this.outputBox.BackColor = System.Drawing.Color.Gainsboro;
            this.outputBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.outputBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outputBox.Location = new System.Drawing.Point(3, 3);
            this.outputBox.Name = "outputBox";
            this.outputBox.ReadOnly = true;
            this.outputBox.Size = new System.Drawing.Size(495, 315);
            this.outputBox.TabIndex = 28;
            this.outputBox.Text = "";
            // 
            // textBox3
            // 
            this.textBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox3.Location = new System.Drawing.Point(200, -32768);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(0, 20);
            this.textBox3.TabIndex = 24;
            // 
            // inputBox2
            // 
            this.inputBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.inputBox2.BackColor = System.Drawing.Color.Black;
            this.inputBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.inputBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.inputBox2.ForeColor = System.Drawing.Color.White;
            this.inputBox2.Location = new System.Drawing.Point(0, -32768);
            this.inputBox2.Name = "inputBox2";
            this.inputBox2.Size = new System.Drawing.Size(0, 19);
            this.inputBox2.TabIndex = 22;
            // 
            // textBox2
            // 
            this.textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox2.BackColor = System.Drawing.Color.Black;
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox2.Location = new System.Drawing.Point(0, -32768);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(0, 19);
            this.textBox2.TabIndex = 21;
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.BackColor = System.Drawing.Color.Black;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(0, -32768);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(0, 19);
            this.textBox1.TabIndex = 19;
            // 
            // inputBox1
            // 
            this.inputBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.inputBox1.BackColor = System.Drawing.Color.Black;
            this.inputBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.inputBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.inputBox1.ForeColor = System.Drawing.Color.White;
            this.inputBox1.Location = new System.Drawing.Point(3, -32768);
            this.inputBox1.Name = "inputBox1";
            this.inputBox1.Size = new System.Drawing.Size(0, 19);
            this.inputBox1.TabIndex = 17;
            // 
            // pictureBox7
            // 
            this.pictureBox7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox7.BackColor = System.Drawing.Color.Black;
            this.pictureBox7.Location = new System.Drawing.Point(391, -32768);
            this.pictureBox7.Name = "pictureBox7";
            this.pictureBox7.Size = new System.Drawing.Size(0, 50);
            this.pictureBox7.TabIndex = 27;
            this.pictureBox7.TabStop = false;
            // 
            // pictureBox6
            // 
            this.pictureBox6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox6.BackColor = System.Drawing.Color.Black;
            this.pictureBox6.Location = new System.Drawing.Point(224, -32768);
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.Size = new System.Drawing.Size(0, 50);
            this.pictureBox6.TabIndex = 26;
            this.pictureBox6.TabStop = false;
            // 
            // pictureBox5
            // 
            this.pictureBox5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox5.BackColor = System.Drawing.Color.Black;
            this.pictureBox5.Enabled = false;
            this.pictureBox5.Location = new System.Drawing.Point(-3, -32768);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(0, 5);
            this.pictureBox5.TabIndex = 23;
            this.pictureBox5.TabStop = false;
            // 
            // pictureBox4
            // 
            this.pictureBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox4.BackColor = System.Drawing.Color.Black;
            this.pictureBox4.Enabled = false;
            this.pictureBox4.Location = new System.Drawing.Point(0, -32768);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(0, 5);
            this.pictureBox4.TabIndex = 20;
            this.pictureBox4.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox3.BackColor = System.Drawing.Color.Black;
            this.pictureBox3.Enabled = false;
            this.pictureBox3.Location = new System.Drawing.Point(-3, -32768);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(0, 5);
            this.pictureBox3.TabIndex = 18;
            this.pictureBox3.TabStop = false;
            // 
            // helpTab
            // 
            this.helpTab.BackColor = System.Drawing.Color.Gainsboro;
            this.helpTab.Controls.Add(this.helpControl);
            this.helpTab.Location = new System.Drawing.Point(4, 22);
            this.helpTab.Name = "helpTab";
            this.helpTab.Padding = new System.Windows.Forms.Padding(3);
            this.helpTab.Size = new System.Drawing.Size(501, 321);
            this.helpTab.TabIndex = 3;
            this.helpTab.Text = "Help Page";
            // 
            // helpControl
            // 
            this.helpControl.BackColor = System.Drawing.Color.Gainsboro;
            this.helpControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.helpControl.Location = new System.Drawing.Point(3, 3);
            this.helpControl.Name = "helpControl";
            this.helpControl.Size = new System.Drawing.Size(495, 315);
            this.helpControl.TabIndex = 0;
            // 
            // topMenuControl
            // 
            this.topMenuControl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.topMenuControl.BackColor = System.Drawing.Color.DimGray;
            this.topMenuControl.Location = new System.Drawing.Point(336, 0);
            this.topMenuControl.Name = "topMenuControl";
            this.topMenuControl.Size = new System.Drawing.Size(182, 31);
            this.topMenuControl.TabIndex = 17;
            // 
            // UI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(148)))), ((int)(((byte)(148)))));
            this.ClientSize = new System.Drawing.Size(522, 420);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.textInput);
            this.Controls.Add(this.customPanelControl);
            this.Controls.Add(this.topMenuControl);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.grayFadePictureBox);
            this.Controls.Add(this.transpControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(522, 420);
            this.Name = "UI";
            this.Text = "ToDo++";
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.SelectTextInput);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.UI_MouseDown);
            this.Move += new System.EventHandler(this.UI_Move);
            this.Resize += new System.EventHandler(this.UI_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.customPanelControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.taskListViewControl)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.consoleDisplay.ResumeLayout(false);
            this.consoleDisplay.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.helpTab.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon_taskBar;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage1;
        private CustomPanelControl customPanelControl;
        private PreferencesPanel preferencesPanel;
        private InputBox textInput;
        private System.Windows.Forms.TabPage consoleDisplay;
        private System.Windows.Forms.PictureBox pictureBox3;
        private InputBox inputBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.PictureBox pictureBox5;
        private InputBox inputBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.PictureBox pictureBox6;
        private System.Windows.Forms.PictureBox pictureBox7;
        private OutputBox outputBox;
        private System.Windows.Forms.PictureBox pictureBox2;
        private TopMenuControl topMenuControl;
        private TaskListViewControl taskListViewControl;
        private BrightIdeasSoftware.OLVColumn taskNameCol;
        private BrightIdeasSoftware.OLVColumn taskDoneStateCol;
        private BrightIdeasSoftware.OLVColumn taskIndexCol;
        private BrightIdeasSoftware.OLVColumn bufferCol;
        private System.Windows.Forms.Timer timerCollapse;
        private System.Windows.Forms.Timer timerExpand;
        private System.Windows.Forms.Timer timerFadeIn;
        private System.Windows.Forms.Timer timerFadeOut;
        private TranspControl.TranspControl grayFadePictureBox;
        private System.Windows.Forms.Timer grayFadeTimer;
        private TranspControl.TranspControl transpControl1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TabPage helpTab;
        private HelpControl helpControl;
    }
}

