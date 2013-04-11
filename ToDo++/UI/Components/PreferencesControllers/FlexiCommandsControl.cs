//@raaj A0081202Y
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ToDo
{


    public partial class FlexiCommandsControl : UserControl
    {
        Settings settings;
        CommandType selectedCommand;
        ContextType selectedContext;
        SelectedType selectedType;
        TimeRangeKeywordsType selectedTimeRangeKeywordType;
        TimeRangeType selectedTimeRangeType;
        List<string> selectedFlexiCommands;

        // ******************************************************************
        // Constructor - Intialize with Settings and load all UI Elements
        // ******************************************************************

        #region Constructor

        /// <summary>
        /// Intialize FlexiCommands with an instance of settings
        /// </summary>
        /// <param name="settings"></param>
        public void InitializeFlexiCommands(Settings settings)
        {
            this.settings = settings;
            this.selectedFlexiCommands=new List<string>();
        }

        /// <summary>
        /// Load all TreeView Lists
        /// </summary>
        public FlexiCommandsControl()
        {
            InitializeComponent();
            LoadContextList();
            LoadTimeKeywordRangeList();
            LoadTimeRangeList();
            LoadCommandList();
            UpdateTabDescription();
            this.rangeController.Enabled = false;
            schedPostponePanel.Hide();
        }

        #endregion

        // ******************************************************************
        // Parses through CustomDictionary and convert strings to enums
        // ******************************************************************

        #region ConversionStringToEnum

        public enum SelectedType { CommandSelected = 1, ContextSelected, TimeRangeKeywordsSelected, TimeRangeSelected,INVALID };

        private CommandType ConvertStringToCommand(string command)
        {
            return (CommandType)Enum.Parse(typeof(CommandType), command);
        }

        private ContextType ConvertStringToContext(string context)
        {
            return (ContextType)Enum.Parse(typeof(ContextType), context);
        }

        private TimeRangeKeywordsType ConvertStringToTimeRangeKeyword(string rangeKeyword)
        {
            return (TimeRangeKeywordsType)Enum.Parse(typeof(TimeRangeKeywordsType), rangeKeyword);
        }

        private TimeRangeType ConvertStringToTimeRange(string timeRange)
        {
            return (TimeRangeType)Enum.Parse(typeof(TimeRangeType), timeRange);
        }

        #endregion

        // ******************************************************************
        // Parses through CustomDictionary and loads all Keywords into UI
        // ******************************************************************

        #region LoadTreeLists

        private void LoadCommandList()
        {
            commandTree.Nodes.Clear();
            TreeNode treeNode = new TreeNode();
            foreach (string commandType in Enum.GetNames(typeof(CommandType)))
            {
                if (!((commandType == "INVALID") || (commandType == "EXIT") || (commandType == "DISPLAY")))
                {
                    treeNode = new TreeNode(commandType);
                    commandTree.Nodes.Add(treeNode);
                }
            }
        }

        private void LoadContextList()
        {
            contextTree.Nodes.Clear();
            TreeNode treeNode = new TreeNode();
            foreach (string contextType in Enum.GetNames(typeof(ContextType)))
            {
                if (!((contextType == "INVALID") || (contextType == "EXIT")))
                {
                    treeNode = new TreeNode(contextType);
                    contextTree.Nodes.Add(treeNode);
                }
            }
        }

        private void LoadTimeKeywordRangeList()
        {
            timeRangeKeywordTree.Nodes.Clear();
            TreeNode treeNode = new TreeNode("MORNING");
            timeRangeKeywordTree.Nodes.Add(treeNode);
            treeNode = new TreeNode("AFTERNOON");
            timeRangeKeywordTree.Nodes.Add(treeNode);
            treeNode = new TreeNode("EVENING");
            timeRangeKeywordTree.Nodes.Add(treeNode);
            treeNode = new TreeNode("NIGHT");
            timeRangeKeywordTree.Nodes.Add(treeNode);
        }

        private void LoadTimeRangeList()
        {
            timeRangeTree.Nodes.Clear();
            TreeNode treeNode = new TreeNode("HOUR");
            timeRangeTree.Nodes.Add(treeNode);
            treeNode = new TreeNode("DAY");
            timeRangeTree.Nodes.Add(treeNode);
            treeNode = new TreeNode("MONTH");
            timeRangeTree.Nodes.Add(treeNode);
        }

        #endregion

        // ******************************************************************
        // Event Handlers
        // ******************************************************************

        #region EventHandlers

        #region Add/Remove

        private void addButton_Click(object sender, EventArgs e)
        {
            ShowUserInputBox();
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            try
            {
                RemoveFlexiCommandFromSettings(this.listedFlexiCommands.SelectedItem.ToString());
                UpdateFlexiCommandList();
            }
            catch (NullReferenceException)
            {
                AlertBox.Show("Please select a Command");
            }
        }

        #endregion

        #region TreeViewControlsSelection

        private void commandTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.contextTree.SelectedNode = null;
            this.timeRangeKeywordTree.SelectedNode = null;
            this.timeRangeTree.SelectedNode = null;
            this.rangeController.Enabled = false;

            this.selectedType = SelectedType.CommandSelected;
            string selected = commandTree.SelectedNode.Text;
            this.selectedCommand = ConvertStringToCommand(selected);

            if ((this.selectedCommand == CommandType.POSTPONE) || (this.selectedCommand == CommandType.SCHEDULE))
                schedPostponePanel.Show();
            else
                schedPostponePanel.Hide();

            UpdateFlexiCommandList();
            UpdateDescription();
            UpdateSchedulePostponeLabel();
        }

        private void contextTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.commandTree.SelectedNode = null;
            this.timeRangeKeywordTree.SelectedNode = null;
            this.timeRangeTree.SelectedNode = null;
            this.rangeController.Enabled = false;
            schedPostponePanel.Hide();

            this.commandTree.SelectedNode = null;
            this.selectedType = SelectedType.ContextSelected;
            string selected = contextTree.SelectedNode.Text;
            this.selectedContext = ConvertStringToContext(selected);

            UpdateFlexiCommandList();
            UpdateDescription();
        }

        private void timeRangeKeywordTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.commandTree.SelectedNode = null;
            this.contextTree.SelectedNode = null;
            this.timeRangeTree.SelectedNode = null;
            this.rangeController.Enabled = true;
            schedPostponePanel.Hide();

            this.commandTree.SelectedNode = null;
            this.selectedType = SelectedType.TimeRangeKeywordsSelected;
            string selected = timeRangeKeywordTree.SelectedNode.Text;
            this.selectedTimeRangeKeywordType = ConvertStringToTimeRangeKeyword(selected);

            UpdateFlexiCommandList();
            UpdateDescription();
            UpdateTimeRangeUI();
        }

        private void timeRangeTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.contextTree.SelectedNode = null;
            this.commandTree.SelectedNode = null;
            this.timeRangeKeywordTree.SelectedNode = null;
            this.rangeController.Enabled = false;
            schedPostponePanel.Hide();

            this.commandTree.SelectedNode = null;
            this.selectedType = SelectedType.TimeRangeSelected;
            string selected = timeRangeTree.SelectedNode.Text;
            this.selectedTimeRangeType = ConvertStringToTimeRange(selected);

            UpdateFlexiCommandList();
            UpdateDescription();
        }

        #endregion

        #region TreeViewControlDoubleClick

        private void commandTree_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ShowUserInputBox();
        }

        private void contextTree_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            ShowUserInputBox();
        }

        private void timeRangeKeywordTree_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ShowUserInputBox();
        }

        private void timeRangeTree_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ShowUserInputBox();
        }

        #endregion

        #region TabAndRangeEventHandlers

        private void rangeController_RangeChanged(object sender, EventArgs e)
        {
            if (this.selectedTimeRangeKeywordType == TimeRangeKeywordsType.NIGHT)
            {
                settings.SetTimeRange(selectedTimeRangeKeywordType, rangeController.RangeMaximum, rangeController.RangeMinimum);
                UpdateDescription();
            }
            else
            {
                settings.SetTimeRange(selectedTimeRangeKeywordType, rangeController.RangeMinimum, rangeController.RangeMaximum);
                UpdateDescription();
            }
        }

        private void flatTabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            schedPostponePanel.Hide();
            if (flexiCommandTab.SelectedIndex == 0)
            {
                UpdateTabDescription();
                ClearSelectedCommands();
                //commandTree.Focus();
                Size tempSize = descriptionLabel.Size;
                tempSize.Height = flexiCommandTab.Height - 70;
                descriptionLabel.Size = tempSize;
                
            }
            else if (flexiCommandTab.SelectedIndex == 1)
            {
                UpdateTabDescription();
                ClearSelectedCommands();
                //contextTree.Focus();
                Size tempSize = descriptionLabel.Size;
                tempSize.Height = flexiCommandTab.Height - 70;
                descriptionLabel.Size = tempSize;
            }
            else if (flexiCommandTab.SelectedIndex == 2)
            {
                UpdateTabDescription();
                ClearSelectedCommands();
                //timeRangeKeywordTree.Focus();
                Size tempSize = descriptionLabel.Size;
                tempSize.Height = 93;
                descriptionLabel.Size = tempSize;
            }
        }

        #endregion

        #region ComboBoxPostponeScheduleEventHandlers

        bool allowComboBoxChanges = true;
        private void timeComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            if (allowComboBoxChanges == true)
            {
                int time = Convert.ToInt32(timeComboBox.SelectedItem.ToString());

                if ((time < 1) || (time > 24))
                {
                    AlertBox.Show("Select a Time Range between 1-23");
                    return;
                }

                if (selectedCommand == CommandType.POSTPONE)
                {
                    settings.SetDefaultPostponeDurationLength(time);
                    UpdateSchedulePostponeLabel();
                }
                else if (selectedCommand == CommandType.SCHEDULE)
                {
                    settings.SetDefaultScheduleTimeLength(time);
                    UpdateSchedulePostponeLabel();
                }
            }
        }

        private void typeComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            if (allowComboBoxChanges == true)
            {
                string x = typeComboBox.SelectedItem.ToString();
                TimeRangeType timeRangeType = (TimeRangeType)Enum.Parse(typeof(TimeRangeType), x);

                if (selectedCommand == CommandType.POSTPONE)
                {
                    settings.SetDefaultPostponeDurationType(timeRangeType);
                    UpdateSchedulePostponeLabel();
                }
                else if (selectedCommand == CommandType.SCHEDULE)
                {
                    settings.SetDefaultScheduleTimeLengthType(timeRangeType);
                    UpdateSchedulePostponeLabel();
                }
            }
        }

        #endregion

        #endregion

        // ******************************************************************
        // Formatting Functions for Rich Text Boxes
        // ******************************************************************

        #region FormattingControl

        /// <summary>
        /// Set Formatting of Text to be set into OutputBox
        /// </summary>
        public void SetFormat(Color color, string text, int size)
        {
            RichTextBox box = descriptionLabel;
            int start = box.TextLength;
            box.AppendText(text);
            int end = box.TextLength;

            box.Select(start, end - start + 1);
            box.SelectionColor = color;
            box.SelectionFont = new Font("Century Gothic", size, FontStyle.Regular);
            box.SelectionLength = 0;
        }

        #endregion

        // ******************************************************************
        // Function/API calls to Update UI Elements
        // ******************************************************************

        #region UpdateUIElemets

        /// <summary>
        /// Clear Selected Commands and set to nothing selected
        /// </summary>
        private void ClearSelectedCommands()
        {
            listedFlexiCommands.Items.Clear();
            selectedType = SelectedType.INVALID;
        }

        /// <summary>
        /// Shows the User Input Box to enter new flexiCommands
        /// </summary>
        private void ShowUserInputBox()
        {
            UserInputBox.Show("Add Command", "Enter your new command here");
            if (UserInputBox.ValidData())
                AddFlexiCommandToSettings(UserInputBox.GetInput());

            UpdateFlexiCommandList();
        }

        /// <summary>
        /// Updates the flexiCommand list with the updated flexiCommands
        /// </summary>
        private void UpdateFlexiCommandList()
        {
            listedFlexiCommands.Items.Clear();
            if (this.selectedType == SelectedType.CommandSelected)
            {
                this.selectedFlexiCommands.Clear();
                foreach (string command in settings.GetFlexiKeywordList(this.selectedCommand))
                    this.selectedFlexiCommands.Add(command);
            }
            else if(this.selectedType==SelectedType.ContextSelected)
            {
                this.selectedFlexiCommands.Clear();
                foreach (string flexiCommand in settings.GetFlexiKeywordList(this.selectedContext))
                    this.selectedFlexiCommands.Add(flexiCommand);
            }
            else if (this.selectedType == SelectedType.TimeRangeKeywordsSelected)
            {
                this.selectedFlexiCommands.Clear();
                foreach (string flexiCommand in settings.GetFlexiKeywordList(this.selectedTimeRangeKeywordType))
                    this.selectedFlexiCommands.Add(flexiCommand);
            }
            else if (this.selectedType == SelectedType.TimeRangeSelected)
            {
                this.selectedFlexiCommands.Clear();
                foreach (string flexiCommand in settings.GetFlexiKeywordList(this.selectedTimeRangeType))
                    this.selectedFlexiCommands.Add(flexiCommand);
            }

            foreach (string flexiCommand in this.selectedFlexiCommands)
                listedFlexiCommands.Items.Add(flexiCommand);
        }

        /// <summary>
        /// Updates the time ranges set by the user
        /// </summary>
        private void UpdateTimeRangeUI()
        {
            if (this.selectedTimeRangeKeywordType == TimeRangeKeywordsType.NIGHT)
            {
                this.rangeController.InnerColor = Color.Gray;
                this.rangeController.RangeMaximum = this.settings.GetStartTime(selectedTimeRangeKeywordType);
                this.rangeController.RangeMinimum = this.settings.GetEndTime(selectedTimeRangeKeywordType);
            }
            else
            {
                this.rangeController.InnerColor = Color.Black;
                this.rangeController.RangeMinimum = this.settings.GetStartTime(selectedTimeRangeKeywordType);
                this.rangeController.RangeMaximum = this.settings.GetEndTime(selectedTimeRangeKeywordType);
            }
        }

        /// <summary>
        /// Adds the user defined flexiCommand to settings to be saved
        /// </summary>
        /// <param name="flexiCommand"></param>
        private void AddFlexiCommandToSettings(string flexiCommand)
        {
            try
            {
                if (this.selectedType == SelectedType.CommandSelected)
                    settings.AddFlexiKeyword(flexiCommand, this.selectedCommand);
                else if (this.selectedType == SelectedType.ContextSelected)
                    settings.AddFlexiKeyword(flexiCommand, this.selectedContext);
                else if (this.selectedType == SelectedType.TimeRangeKeywordsSelected)
                    settings.AddFlexiKeyword(flexiCommand, this.selectedTimeRangeKeywordType);
                else if (this.selectedType == SelectedType.TimeRangeSelected)
                    settings.AddFlexiKeyword(flexiCommand, this.selectedTimeRangeType);
            }
            catch (RepeatCommandException e)
            {
                AlertBox.Show(e.Message);
            }

        }

        /// <summary>
        /// Removes the user selected flexiCommand and saves
        /// </summary>
        /// <param name="flexiCommand"></param>
        private void RemoveFlexiCommandFromSettings(string flexiCommand)
        {
            try
            {
                if (this.selectedType == SelectedType.CommandSelected)
                    settings.RemoveFlexiKeyword(flexiCommand, selectedCommand);
                else if (this.selectedType == SelectedType.ContextSelected)
                    settings.RemoveFlexiKeyword(flexiCommand,selectedContext);
                else if (this.selectedType == SelectedType.TimeRangeKeywordsSelected)
                    settings.RemoveFlexiKeyword(flexiCommand, selectedTimeRangeKeywordType);
                else if (this.selectedType == SelectedType.TimeRangeSelected)
                    settings.RemoveFlexiKeyword(flexiCommand, selectedTimeRangeType);
            }
            catch (InvalidDeleteFlexiException e)
            {
                AlertBox.Show(e.Message);
            }
        }

        /// <summary>
        /// Updates the description of the selected flexiCommand
        /// </summary>
        private void UpdateDescription()
        {
            string title="";
            string description="";
            descriptionLabel.Text = "";

            if (this.selectedType == SelectedType.CommandSelected)
            {
                title = commandTree.SelectedNode.Text;
                titleLabel.Text = title;
                switch (selectedCommand)
                {
                    case CommandType.ADD:
                        SetFormat(Color.Brown, "Add a task anyway you like\n", 9);
                        SetFormat(Color.Gray, "eg. add finish project\n", 9);
                        SetFormat(Color.Gray, "eg. add check todo++ in 2 hours\n", 9);
                        SetFormat(Color.Gray, "eg. add max birthday 4pm tomorrow \n", 9);
                        SetFormat(Color.Gray, "eg. add team meeting 2pm-4pm next wed \n", 9);
                        SetFormat(Color.Gray, "eg. add do cs2103 CE2 by sat midnight \n", 9);
                        break;

                    case CommandType.DELETE:
                        SetFormat(Color.Brown, "Delete Tasks anyway you like:\n", 9);
                        SetFormat(Color.Gray, "eg. delete 3\n", 9);
                        SetFormat(Color.Gray, "eg. delete task\n", 9);
                        SetFormat(Color.Gray, "eg. delete 1-3\n", 9);
                        SetFormat(Color.Gray, "eg. remove Sunday after 1500hrs\n", 9);
                        SetFormat(Color.Gray, "eg. delete 31 December after 10pm \n", 9);
                        break;

                    case CommandType.SEARCH:
                        SetFormat(Color.Brown, "Display/Search tasks in many ways:\n", 9);
                        SetFormat(Color.Gray, "eg. display 06/09/2012\n", 9);
                        SetFormat(Color.Gray, "eg. display Sunday after 1500hrs\n", 9);
                        SetFormat(Color.Gray, "eg. display 6 sept before 10pm\n", 9);
                        SetFormat(Color.Gray, "eg. display next Saturday\n", 9);
                        SetFormat(Color.Gray, "eg. display tmr\n", 9);
                        SetFormat(Color.Gray, "eg. display buy milk\n", 9);
                        SetFormat(Color.Gray, "eg. display floating\n", 9);
                        break;

                    case CommandType.DONE:
                        SetFormat(Color.Brown, "Set Tasks as completed:\n", 9);
                        SetFormat(Color.Gray, "eg. done task\n", 9);
                        SetFormat(Color.Gray, "eg. done 3\n", 9);
                        SetFormat(Color.Gray, "eg. done 1-3\n", 9);
                        SetFormat(Color.Gray, "eg. done all\n", 9);
                        break;

                    case CommandType.UNDONE:
                        SetFormat(Color.Brown, "Set Tasks as incomplete:\n", 9);
                        SetFormat(Color.Gray, "eg. undone task\n", 9);
                        SetFormat(Color.Gray, "eg. undone 3\n", 9);
                        SetFormat(Color.Gray, "eg. undone 1-3\n", 9);
                        SetFormat(Color.Gray, "eg. undone all\n", 9);
                        break;

                    case CommandType. MODIFY:
                        SetFormat(Color.Brown, "Modify tasks as you please:\n", 9);
                        SetFormat(Color.Gray, "eg. modify 1 buy car\n", 9);
                        SetFormat(Color.Gray, "eg. modify 1 3pm\n", 9);
                        SetFormat(Color.Gray, "eg. modify buy milk 9 Nov\n", 9);
                        break;

                    case CommandType.POSTPONE:
                        SetFormat(Color.Brown, "Postpone task as you please:\n", 9);
                        SetFormat(Color.Gray, "eg. postpone 1-2 2 hours\n", 9);
                        SetFormat(Color.Gray, "eg. postpone 1 2 days\n", 9);
                        SetFormat(Color.Gray, "eg. postpone task (default)\n", 9);
                        SetFormat(Color.Black, "Change Postpone range below\n", 9);
                        break;

                    case CommandType.SCHEDULE:
                        SetFormat(Color.Brown, "Schedule task as you please:\n", 9);
                        SetFormat(Color.Gray, "eg. schedule buy milk\n", 9);
                        SetFormat(Color.Gray, "eg. schedule task tmr 2-5pm\n", 9);
                        SetFormat(Color.Gray, "eg. schedule task tmr (default)\n", 9);
                        SetFormat(Color.Black, "Change Schedule range below\n", 9);
                        break;

                    case CommandType.UNDO:
                        SetFormat(Color.Brown, "Undo last command:\n", 9);
                        SetFormat(Color.Gray, "eg. undo\n", 9);
                        break;

                    case CommandType.REDO:
                        SetFormat(Color.Brown, "Redo last command:\n", 9);
                        SetFormat(Color.Gray, "eg. redo\n", 9);
                        break;
                        
                    case CommandType.SORT:
                        SetFormat(Color.Brown, "Sort last command:\n", 9);
                        SetFormat(Color.Gray, "eg. sort date \n", 9);
                        SetFormat(Color.Gray, "eg. sort name \n", 9);
                        break;

                    default:
                        description = "Not Implemented Exception";
                        break;
                }
            }
            else if (this.selectedType == SelectedType.ContextSelected)
            {
                title = contextTree.SelectedNode.Text;
                titleLabel.Text = title;
                switch (selectedContext)
                {
                    case ContextType.STARTTIME:
                        SetFormat(Color.Brown, "Set the time you wish to start your task:\n", 9);
                        SetFormat(Color.Gray, "eg. add task (from) 2pm-3pm \n", 9);
                        SetFormat(Color.Gray, "eg. add task (on) 12/11/2012 \n", 9);
                        break;

                    case ContextType.ENDTIME:
                        SetFormat(Color.Brown, "Set a range for your task:\n", 9);
                        SetFormat(Color.Gray, "eg. add task from 2pm(-)3pm \n", 9);
                        SetFormat(Color.Gray, "eg. add task from 6 Nov (to) 7 Nov \n", 9);
                        break;

                    case ContextType.DEADLINE:
                        SetFormat(Color.Brown, "Set a task that must be completed by deadline:\n", 9);
                        SetFormat(Color.Gray, "eg. add task (by) 3pm next sunday \n", 9);
                        break;

                    case ContextType.CURRENT:
                        SetFormat(Color.Brown, "Set a task deadline with 'this':\n", 9);
                        SetFormat(Color.Gray, "eg. add task buy milk by (this) sunday \n", 9);
                        break;

                    case ContextType.NEXT:
                        SetFormat(Color.Brown, "Set a task deadline with 'next':\n", 9);
                        SetFormat(Color.Gray, "eg. add task buy milk by (next) month \n", 9);
                        break;

                    case ContextType.FOLLOWING:
                        SetFormat(Color.Brown, "Set a task deadline in two weeks:\n", 9);
                        SetFormat(Color.Gray, "eg. add task buy milk by (following) sunday \n", 9);
                        break;

                    default:
                        description = "Not Implemented Exception";
                        break;
                }
            }
            else if (this.selectedType == SelectedType.TimeRangeKeywordsSelected)
            {
                title = timeRangeKeywordTree.SelectedNode.Text;
                titleLabel.Text = title;
                switch (selectedTimeRangeKeywordType)
                {
                    default:
                        UpdateTimeRangeDescription();
                        return;
                }
            }
            else if (this.selectedType == SelectedType.TimeRangeSelected)
            {
                title = timeRangeTree.SelectedNode.Text;
                titleLabel.Text = title;
                switch (selectedTimeRangeKeywordType)
                {
                    default:
                        SetFormat(Color.Gray, "Set your own keyword for these time ranges\n", 9);
                        break;
                }
            }
        }

        /// <summary>
        /// Updates the description of time ranges
        /// </summary>
        private void UpdateTimeRangeDescription()
        {
            descriptionLabel.Clear();
            SetFormat(Color.Brown, "Set the range of default timings\n", 9);
            SetFormat(Color.Black, "MORNING: ", 9);
            SetFormat(Color.Gray, settings.GetStartTime(TimeRangeKeywordsType.MORNING).ToString(), 9);
            SetFormat(Color.Gray, "-", 9);
            SetFormat(Color.Gray, settings.GetEndTime(TimeRangeKeywordsType.MORNING).ToString(), 9);
            SetFormat(Color.Gray, "\n", 9);
            SetFormat(Color.Black, "AFTERNOON: ", 9);
            SetFormat(Color.Gray, settings.GetStartTime(TimeRangeKeywordsType.AFTERNOON).ToString(), 9);
            SetFormat(Color.Gray, "-", 9);
            SetFormat(Color.Gray, settings.GetEndTime(TimeRangeKeywordsType.AFTERNOON).ToString(), 9);
            SetFormat(Color.Gray, "\n", 9);
            SetFormat(Color.Black, "EVENING: ", 9);
            SetFormat(Color.Gray, settings.GetStartTime(TimeRangeKeywordsType.EVENING).ToString(), 9);
            SetFormat(Color.Gray, "-", 9);
            SetFormat(Color.Gray, settings.GetEndTime(TimeRangeKeywordsType.EVENING).ToString(), 9);
            SetFormat(Color.Gray, "\n", 9);
            SetFormat(Color.Black, "NIGHT: ", 9);
            SetFormat(Color.Gray, settings.GetStartTime(TimeRangeKeywordsType.NIGHT).ToString(), 9);
            SetFormat(Color.Gray, "-", 9);
            SetFormat(Color.Gray, settings.GetEndTime(TimeRangeKeywordsType.NIGHT).ToString(), 9);
            SetFormat(Color.Gray, "\n", 9);
        }

        /// <summary>
        /// Updates description of schedule and postpone default time ranges
        /// </summary>
        private void UpdateSchedulePostponeLabel()
        {
            if (selectedCommand == CommandType.POSTPONE)
            {
                string x = String.Format("Postpone by {0} {1}", settings.GetDefaultPostponeDurationLength(), settings.GetDefaultPostponeDurationType().ToString().ToLower());
                schedulePostponeLabel.Text = x;
                allowComboBoxChanges = false;
                timeComboBox.Text = settings.GetDefaultPostponeDurationLength().ToString();
                typeComboBox.Text = settings.GetDefaultPostponeDurationType().ToString();
                allowComboBoxChanges = true;
            }
            else if (selectedCommand == CommandType.SCHEDULE)
            {
                string x = String.Format("Schedule by {0} {1}", settings.GetDefaultScheduleTimeLength(), settings.GetDefaultScheduleTimeLengthType().ToString().ToLower());
                schedulePostponeLabel.Text = x;
                allowComboBoxChanges = false;
                timeComboBox.Text = settings.GetDefaultScheduleTimeLength().ToString();
                typeComboBox.Text = settings.GetDefaultScheduleTimeLengthType().ToString();
                allowComboBoxChanges = true;

            }
        }

        /// <summary>
        /// Updates description of the selected tabs
        /// </summary>
        private void UpdateTabDescription()
        {
            this.descriptionLabel.Text = "";

            if (flexiCommandTab.SelectedIndex==0)
            {
                this.titleLabel.Text = "Command Keywords";
                SetFormat(Color.Black, "Commands are what let you interact with ToDo++. Click on a command to find out more about it, modify it's settings and add your own custom flexiCommands :)", 9);
            }
            else if (flexiCommandTab.SelectedIndex == 1)
            {
                this.titleLabel.Text = "Context Keywords";
                SetFormat(Color.Black, "Contextes are natural words and characters you use, to set time ranges any way you please. Click on a context to find out more about it, modify it's settings and add your own custom flexiCommands :)", 9);
            }
            else if (flexiCommandTab.SelectedIndex == 2)
            {
                this.titleLabel.Text = "Time Keywords";
                SetFormat(Color.Black, "Time Keywords are how ToDo++ sets Time Ranges. For example, MORNING would let make ToDo++ set the \"5am to 10am\" time range. You can modify or add your own custom flexiCommands :)", 9);

            }
        }

        #endregion















    }
}
