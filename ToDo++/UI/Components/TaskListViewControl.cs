//@ivan A0086401M
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using BrightIdeasSoftware;

namespace ToDo
{
    /// <summary>
    /// Component which controls how the tasks are displayed to the user.
    /// </summary>
    class TaskListViewControl : ObjectListView
    {
        #region String Constants
        const string MESSAGE_EMPTY_LIST = "You have no tasks in your ToDo++ list.\r\nClick on the ? icon above to find out how to get started!";
        const string MESSAGE_NO_TASKS = "No tasks to display!";
        const string MESSAGE_STYLE_DONE = "[DONE]";
        const string COL_NAME_TASK_NAME = "TaskName";
        const string COL_NAME_DONE_STATE = "DoneState";  
        #endregion
      
        #region Attributes
        List<Task> displayedTasks;
        OLVColumn defaultCol;
        Settings settings;
        #endregion

        /// <summary>
        /// The default constructor for this class.
        /// </summary>
        /// <returns></returns>
        public TaskListViewControl()
        {
            displayedTasks = null;
        }

        /// <summary>
        /// Initializes this component.
        /// </summary>
        private void InitializeComponent()
        {
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
        }

        /// <summary>
        /// Initializes settings for this component.
        /// </summary>
        /// <param name="settings">The settings to use to initialize this component with.</param>
        internal void InitializeSettings(Settings settings)
        {
            this.settings = settings;
            this.SetFormatting(settings.GetTextSize(), settings.GetFontSelection());
            this.RowHeight = 32;
            this.ShowItemToolTips = true;
            this.HeaderStyle = ColumnHeaderStyle.None;

            this.defaultCol = this.AllColumns.Find(e => e.AspectName == COL_NAME_TASK_NAME);
            this.defaultCol.WordWrap = true;
            this.AlwaysGroupByColumn = defaultCol;

            this.AllColumns.Find(e => e.AspectName == COL_NAME_DONE_STATE).AspectToStringConverter = delegate(object state)
            {
                if ((bool)state == true) return MESSAGE_STYLE_DONE;
                else return String.Empty;
            };

            SetGroupingByDateTime();

            EmptyListMsg = MESSAGE_EMPTY_LIST;
        }

        /// <summary>
        /// Updates and sorts the display according to the input Response.
        /// </summary>
        /// <param name="response">The response to update the display with.</param>
        /// <returns>The displayed list of tasks after updating the display with the input Response.</returns>
        public List<Task> UpdateDisplay(Response response)
        {                        
            if (response.TasksToBeDisplayed == null) return displayedTasks; // don't update display list.

            displayedTasks = response.TasksToBeDisplayed;  

            switch (response.FormatType)
            {
                case SortType.NAME:
                    displayedTasks.Sort(Task.CompareByName);
                    SetGroupingByName();
                    break;
                case SortType.DATE_TIME:
                    displayedTasks.Sort(Task.CompareByDateTime);
                    SetGroupingByDateTime();
                    break;
                default:
                    Logger.Info("No change in sorting format", "UpdateDisplay::TaskListViewControl");
                    break;
            }            

            this.SetObjects(displayedTasks);
            
            List<Task> reorderedList = GenerateReorderedList();

            return reorderedList;
        }
                
        /// <summary>
        /// Generates a ordered list of tasks based on the currently displayed list.
        /// </summary>
        /// <returns>The currently displayed ordered list of tasks.</returns>
        private List<Task> GenerateReorderedList()
        {
            Task reorderedTask = null;
            List<Task> reorderedList = new List<Task>();
            for (int i = 0; i < this.Items.Count; i++)
            {
                reorderedTask = (Task)this.GetNthItemInDisplayOrder(i).RowObject;
                reorderedList.Add(reorderedTask);
            }
            displayedTasks = reorderedList;
            return reorderedList;
        }

        /// <summary>
        /// Toggles the display message shown when no tasks are being displayed based on the input flag.
        /// Sets the message to MESSAGE_EMPTY_LIST if true, and MESSAGE_NO_TASKS if false.
        /// </summary>
        /// <param name="empty"></param>
        public void SetMessageTaskListIsEmpty(bool empty)
        {
            if (empty)
                EmptyListMsg = MESSAGE_EMPTY_LIST;
            else
                EmptyListMsg = MESSAGE_NO_TASKS;
        }

        /// <summary>
        /// Capture CTRL+ to prevent resize of all columns.
        /// </summary>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyValue == 107 && e.Modifiers == Keys.Control)
            {
                e.Handled = true;
            }
            else
            {
                base.OnKeyDown(e);
            }
        }


        // ******************************************************************
        // Display formatting
        // ******************************************************************

        #region Display formatting
        /// <summary>
        /// Sets the formatting of all font used by this control.
        /// </summary>
        /// <param name="size">The size of font to use.</param>
        /// <param name="fontName">The font type to use.</param>
        private void SetFormatting(int size,string fontName)
        {
            Font x = new Font(fontName, size, FontStyle.Regular);
            this.Font = x;
        }

        /// <summary>
        /// Formats the first row of the task list view to show the row index.
        /// </summary>
        public void SetRowIndex(BrightIdeasSoftware.FormatRowEventArgs row)
        {
            // Display index -will- change if doing a column sort.
            row.Item.SubItems[1].Text = "[" + (row.DisplayIndex + 1).ToString() + "]";
        }

        /// <summary>
        /// Colors the input row depending on the the task's date and time which it contains.
        /// </summary>
        /// <param name="row">The rows to format</param>
        public void ColorRows(BrightIdeasSoftware.FormatRowEventArgs row)
        {
            Task task = (Task)row.Item.RowObject;

            if (task == null) return; // log exception

            // Task is done!
            if (task.DoneState == true)
            {
                ColorSubItems(row, settings.GetTaskDoneColor());
            }

            else if (task is TaskDeadline)
            {
                // Deadline task is over time limit!
                if (task.IsWithinTime(DateTime.MinValue, DateTime.Now))
                    ColorSubItems(row, settings.GetTaskMissedDeadlineColor());
                // Task is within the next 24 hrs!
                else if (task.IsWithinTime(DateTime.Now, DateTime.Now.AddDays(1)))
                    ColorSubItems(row, settings.GetTaskNearingDeadlineColor());
            }

            else if (task is TaskEvent)
            {
                // Task has already started or is over!
                if (task.IsWithinTime(DateTime.MinValue, DateTime.Now))
                    ColorSubItems(row, settings.GetTaskOverColor());
            }
        }

        /// <summary>
        /// Colors all subitems except the index to the given color.
        /// </summary>
        /// <param name="row"></param>
        /// <param name="newColor"></param>
        /// <returns></returns>
        public void ColorSubItems(BrightIdeasSoftware.FormatRowEventArgs row, Color newColor)
        {
            int numOfCol = row.Item.SubItems.Count;
            for (int i = 2; i < numOfCol; i++)
                row.Item.GetSubItem(i).ForeColor = newColor;
        }
        #endregion

        // ******************************************************************
        // Grouping Delegates
        // ******************************************************************

        #region Group By Date/Time
        /// <summary>
        /// Sets the control to group tasks by date/time.
        /// </summary>
        private void SetGroupingByDateTime()
        {
            defaultCol.GroupKeyGetter = GroupKeyByDateTime;
            defaultCol.GroupKeyToTitleConverter = GenerateGroupFromKeyDateTime;
        }

        /// <summary>
        ///The delegate which returns the key to use to sort the rows into groups.
        /// </summary>
        /// <param name="task">The object contained by a row.</param>
        /// <returns>The key by which to group the rows.</returns>
        private object GroupKeyByDateTime(object task)
        {
            if (task is TaskFloating)
            {
                return null;
            }
            else if (task is TaskEvent)
            {
                TaskEvent checkTask = (TaskEvent)task;

                if (checkTask.IsSpecific.StartDate.Day == false)
                    return null;
                else
                    return checkTask.StartDateTime.Date;
            }
            else if (task is TaskDeadline)
            {
                TaskDeadline checkTask = (TaskDeadline)task;

                if (checkTask.IsSpecific.EndDate.Day == false)
                    return null;
                return checkTask.EndDateTime.Date;
            }
            else
            {
                Debug.Assert(false,"TaskListViewControl failed to initialize: Object is not a task object!");
                return null;
            }
        }

        /// <summary>
        /// The delegate to generate the display string from a given group key
        /// when sorting by date/time.
        /// </summary>
        /// <param name="groupKey">The group's key.</param>
        /// <returns>String representation of the group.</returns>
        private string GenerateGroupFromKeyDateTime(object groupKey)
        {
            if (groupKey == null)
                return "Other Tasks";
            DateTime date = (DateTime)groupKey;
            if (date == DateTime.Now.Date)
                return "Today";
            else if (date > DateTime.Now.Date.AddDays(6))
                return date.ToString("D");
            else
                return date.DayOfWeek.ToString();
        }

        /// <summary>
        /// Sets the control to group tasks by name.
        /// </summary>
        private void SetGroupingByName()
        {
            defaultCol.UseInitialLetterForGroup = true;
            defaultCol.GroupKeyGetter = null;
            defaultCol.GroupKeyToTitleConverter = null;
        }
        #endregion
    }
}
