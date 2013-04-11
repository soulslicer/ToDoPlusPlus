//@jenna A0083536B
using System;
using System.Xml.Linq;

namespace ToDo
{
    // ******************************************************************
    // Abstract definition for task
    // ******************************************************************

    public abstract class Task
    {

        // ******************************************************************
        // Task base attributes.
        // ******************************************************************

        #region Task attributes.
        protected string taskName;
        public string TaskName
        {
            get { return taskName; }
        }

        protected Boolean doneState;
        public Boolean DoneState
        {
            get { return doneState; }
            set { doneState = value; }
        }

        protected int id;
        public int ID
        {
            get { return id; }
        }
        #endregion

        /// <summary>
        /// Base constructor which can be inherited by derived tasks.
        /// </summary>
        /// <param name="taskName">The task's display name.</param>
        /// <param name="state">The task's done state.</param>
        /// <param name="forceID">The task's ID. Can be set to -1 to generate a new ID.</param>
        /// <returns></returns>
        public Task(string taskName, Boolean state, int forceID)
        {
            this.taskName = taskName;
            this.doneState = state;
            if (forceID < 0)
                id = this.GetHashCode();
            else id = forceID;
            Logger.Info("Called task base constructor", "Task::Task");
        }

        /// <summary>
        /// Creates a new task with the given parameters.
        /// </summary>
        /// <param name="taskName">The task name of the new task.</param>
        /// <param name="startTime">The start time of the new task.</param>
        /// <param name="endTime">The end time of the new task.</param>
        /// <param name="isSpecific">The specificity of the new task's times.</param>
        /// <returns>The newly created task.</returns>
        public static Task CreateNewTask(
            string taskName,
            DateTime? startTime,
            DateTime? endTime,
            DateTimeSpecificity isSpecific
            )
        {
            if (taskName == String.Empty || taskName == null)
            {
                Logger.Warning("Attempted to create a task with no task name", "GenerateNewTask::Task");
                return null; // don't accept empty task names
            }
            if (startTime == null && endTime == null)
            {
                Logger.Info("Creating a floating task", "GenerateNewTask::Task");
                return new TaskFloating(taskName);
            }
            else if (startTime == null && endTime != null)
            {
                Logger.Info("Creating a deadline task", "GenerateNewTask::Task");
                return new TaskDeadline(taskName, (DateTime)endTime, isSpecific);
            }
            else if (startTime != null && endTime == null)
            {
                // If endTime is not specified set endTime based on startTime.
                endTime = startTime;
                isSpecific.EndTime = isSpecific.StartTime;
                isSpecific.EndDate = isSpecific.StartDate;
                if (!isSpecific.StartTime)
                {
                    endTime = ((DateTime)endTime).AddDays(1).AddMinutes(-1);
                }
                Logger.Info("Creating an event task with only one user specified datetime", "GenerateNewTask::Task");
                return new TaskEvent(taskName, (DateTime)startTime, (DateTime)startTime, isSpecific);
            }
            else
            {
                Logger.Info("Creating an event task with user specified start and end datetimes", "GenerateNewTask::Task");
                return new TaskEvent(taskName, (DateTime)startTime, (DateTime)endTime, isSpecific);
            }
        }

        /// <summary>
        /// Casts this task as a unique and reversible XElement which can be written
        /// to a standard XML file.
        /// </summary>
        /// <returns>The XElement representation of this task.</returns>
        public abstract XElement ToXElement();

        /// <summary>
        /// Checks if this task is within the given start and end times.
        /// </summary>
        /// <param name="start">The start time which the task must fall within.</param>
        /// <param name="end">The end time which the task must fall within.</param>
        /// <returns>True if the task is within the time range given, false if it is not.</returns>
        public abstract bool IsWithinTime(DateTime? start, DateTime? end);

        /// <summary>
        /// Copies over the start and end date times and specificity of this task into the reference inputs.
        /// </summary>
        /// <param name="startTime">The start time to copy to.</param>
        /// <param name="endTime">The end time to copy to.</param>
        /// <param name="specific">The specificity to copy to.</param>        
        public abstract void CopyDateTimes(ref DateTime? startTime, ref DateTime? endTime, ref DateTimeSpecificity specific);

        /// <summary>
        /// Returns string representation of this task's times.
        /// </summary>
        /// <returns>The string representation of this task's times.</returns>
        public virtual string GetTimeString()
        {
            return String.Empty;
        }

        /// <summary>
        /// Gets a flag indicating if the task can be scheduled over by the scheduler.
        /// </summary>
        /// <returns>True if task times can be scheduled over; False if not.</returns>
        public virtual bool CanBeScheduledOver()
        {
            return true;
        }

        /// <summary>
        /// Postpones time by the given TimeSpan duration.
        /// </summary>
        /// <param name="postponeDuration">Duration by which this task should be postponed by.</param>
        /// <returns>True if the operation was successful; False if not.</returns>
        public virtual bool Postpone(TimeSpan postponeDuration)
        {
            return false;
        }

        /// <summary>
        /// Comparer which compares two tasks by their task name, and returns an
        /// integer representing their compare position.
        /// </summary>
        /// <param name="a">First task to compare.</param>
        /// <param name="b">Second task to compare.</param>
        /// <returns>-1 if x is less than y, 1 if x is more than y, 0 if they are equal</returns>
        public static int CompareByDateTime(Task a, Task b)
        {
            // A [DONE] task always sorts after an undone task.
            if (a.DoneState == true && b.DoneState == false)
            {
                return 1;
            }
            else if (b.DoneState == true && a.DoneState == false)
            {
                return -1;
            }

            // If they have the same state, continue sort by DateTime.
            if (a is TaskFloating)
            {
                if (b is TaskFloating)
                {
                    return a.TaskName.CompareTo(b.TaskName);
                }
                else
                {
                    return 1;
                }
            }
            else if (b is TaskFloating)
            {
                return -1;
            }

            DateTime aDT, bDT;
            if (a is TaskEvent)
            {
                aDT = ((TaskEvent)a).StartDateTime;
            }
            else
            {
                aDT = ((TaskDeadline)a).EndDateTime;
            }

            if (b is TaskEvent)
            {
                bDT = ((TaskEvent)b).StartDateTime;
            }
            else
            {
                bDT = ((TaskDeadline)b).EndDateTime;
            }

            return DateTime.Compare(aDT, bDT);
        }

        /// <summary>
        /// Comparer which compares two tasks by their task name, and returns an
        /// integer representing their compare position.
        /// </summary>
        /// <param name="x">First task to compare.</param>
        /// <param name="y">Second task to compare.</param>
        /// <returns>-1 if x is less than y, 1 if x is more than y, 0 if they are equal</returns>
        public static int CompareByName(Task x, Task y)
        {
            int compare = x.TaskName.CompareTo(y.TaskName);
            if (compare == 0)
            {
                return CompareByDateTime(x, y);
            }
            return compare;
        }
        
        public override int GetHashCode()
        {
            int newHashCode = Math.Abs(base.GetHashCode() ^ (int)DateTime.Now.ToBinary());
            return newHashCode;
        }
    }
}
