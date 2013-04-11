//@jenna A0083536B
using System;
using System.Xml.Linq;

namespace ToDo
{
    public class TaskDeadline : Task
    {
        #region Task Attributes
        private DateTimeSpecificity isSpecific;
        public DateTimeSpecificity IsSpecific
        {
            get { return isSpecific; }
        }
        private DateTime endDateTime;
        public DateTime EndDateTime
        {
            get { return endDateTime; }
        }
        #endregion

        /// <summary>
        /// Constructor for deadline tasks.
        /// </summary>
        /// <param name="taskName">The task's display name.</param>
        /// <param name="endTime">The task's deadline.</param>
        /// <param name="endDateSpecificity">The task's deadline specificity.</param>
        /// <param name="isDone">The task's done state. Is set to false by default.</param>
        /// <param name="forceID">The task's ID. Is set to -1 by default for the base constructor to generate a new ID.</param>
        /// <returns></returns>
        public TaskDeadline(
            string taskName,
            DateTime endTime,
            DateTimeSpecificity endDateSpecificity,
            Boolean isDone = false,
            int forceID = -1)
            : base(taskName, isDone, forceID)
        {
            this.endDateTime = endTime;
            isSpecific = endDateSpecificity;
            Logger.Info("Created a deadline task", "TaskDeadline::TaskDeadline");
        }

        /// <summary>
        /// Casts this task as a unique and reversible XElement which can be written
        /// to a standard XML file.
        /// </summary>
        /// <returns>The XElement representation of this task.</returns>
        public override XElement ToXElement()
        {
            XElement task = new XElement("Task",
                            new XAttribute("id", id.ToString()),
                            new XAttribute("type", "Deadline"),
                            new XElement("Name", taskName),
                            new XElement("EndTime", endDateTime.ToString()),
                            new XElement("Done", doneState.ToString())
                            );
            return task;
        }

        /// <summary>
        /// Checks if this task is within the given start and end times.
        /// </summary>
        /// <param name="start">The start time which the task must fall within.</param>
        /// <param name="end">The end time which the task must fall within.</param>
        /// <returns>True if the task is within the time range given, false if it is not.</returns>
        public override bool IsWithinTime(DateTime? start, DateTime? end)
        {
            bool isWithinTime = true;
            DateTime startCompare, endCompare;

            // Start search
            if (start != null)
            {
                startCompare = (DateTime)start;

                if (!isSpecific.StartTime)
                {
                    ExtendStartSearchRange(ref startCompare);
                }
                if (endDateTime < startCompare)
                {
                    isWithinTime = false;
                }
            }

            if (end != null)
            {
                endCompare = (DateTime)end;

                if (!isSpecific.EndTime)
                {
                    ExtendEndSearchRange(ref endCompare);
                }

                if (endDateTime > endCompare)
                {
                    isWithinTime = false;
                }
            }
            return isWithinTime;
        }

        /// <summary>
        /// Extends the given start search time to the appropriate start of day/month/year
        /// depending on the specificity of this task.
        /// </summary>
        /// <param name="startCompare">The start search time to extend.</param>
        private void ExtendStartSearchRange(ref DateTime startCompare)
        {
            if (!isSpecific.StartDate.Month)
            {
                startCompare = new DateTime(startCompare.Year, 1, 1);
            }
            else if (!isSpecific.StartDate.Day)
            {
                startCompare = new DateTime(startCompare.Year, startCompare.Month, 1);
            }
            else
            {
                startCompare = startCompare.Date;
            }
        }

        /// <summary>
        /// Extends the given end search time to the appropriate start of day/month/year
        /// depending on the specificity of this task.
        /// </summary>
        /// <param name="endCompare">The end search time to extend.</param>
        private void ExtendEndSearchRange(ref DateTime endCompare)
        {
            if (!isSpecific.EndDate.Month)
            {
                endCompare = new DateTime(endCompare.Year + 1, 1, 1);
            }
            else if (!isSpecific.EndDate.Day)
            {
                endCompare = endCompare.AddMonths(1);
                endCompare = new DateTime(endCompare.Year, endCompare.Month, 1);
            }
            else
            {
                endCompare = endCompare.Date.AddDays(1);
            }
            endCompare = endCompare.AddMinutes(-1);
        }

        /// <summary>
        /// Returns string representation of this task's times.
        /// </summary>
        /// <returns>The string representation of this task's times.</returns>
        public override string GetTimeString()
        {
            string timeString = "BY ";
            if (isSpecific.EndDate.Day) timeString += endDateTime.ToString("d ");
            timeString += endDateTime.ToString("MMM");
            if (endDateTime.Year != DateTime.Now.Year) timeString += " " + endDateTime.Year;
            if (isSpecific.EndTime) timeString += ", " + endDateTime.ToShortTimeString();
            return timeString;
        }

        /// <summary>
        /// Postpones time by the given TimeSpan duration.
        /// </summary>
        /// <param name="postponeDuration">Duration by which this task should be postponed by.</param>
        /// <returns>True if the operation was successful; False if not.</returns>
        public override bool Postpone(TimeSpan postponeDuration)
        {
            bool result = true;

            // Return failure if trying to postpone at a higher specificity level then task allows.
            if ((!isSpecific.EndTime && postponeDuration.Hours != 0) ||
                (!isSpecific.EndDate.Day && postponeDuration.Days != 0))
            {
                Logger.Warning("Attempted to postpone an ambiguous deadline task.", "Postpone::TaskDeadline");
                return false;
            }

            try
            {
                endDateTime = endDateTime.Add(postponeDuration);
            }
            catch
            {
                result = false;
                Logger.Warning("Failed to postpone deadline task.", "Postpone::TaskDeadline");
            }
            return result;
        }

        /// <summary>
        /// Copies over the start and end date times and specificity of this task into the reference inputs.
        /// </summary>
        /// <param name="startTime">The start time to copy to.</param>
        /// <param name="endTime">The end time to copy to.</param>
        /// <param name="specific">The specificity to copy to.</param>        
        public override void CopyDateTimes(ref DateTime? startTime, ref DateTime? endTime, ref DateTimeSpecificity specific)
        {
            startTime = null;
            endTime = this.endDateTime;
            specific = this.isSpecific;
        }
    }
}
