//@jenna A0083536B
using System;
using System.Xml.Linq;

namespace ToDo
{
    public class TaskFloating : Task
    {
        /// <summary>
        /// Constructor for floating tasks.
        /// </summary>
        /// <param name="taskName">The task's display name.</param>
        /// <param name="isDone">The task's done state. Is set to false by default.</param>
        /// <param name="forceID">The task's ID. Is set to -1 by default for the base constructor to generate a new ID.</param>
        /// <returns></returns>
        public TaskFloating(string taskName, Boolean isDone = false, int forceID = -1)
            : base(taskName, isDone, forceID)
        {
            Logger.Info("Created an floating task", "TaskFloating::TaskFloating");
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
                            new XAttribute("type", "Floating"),
                            new XElement("Name", taskName),
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
            return false;
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
            endTime = null;
            specific = new DateTimeSpecificity();
        }
    }
}