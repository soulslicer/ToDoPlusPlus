//@qianpan A0103985Y
using System;
using System.Collections.Generic;
using System.Linq;

namespace ToDo
{
    class OperationDisplayDefault : Operation
    {
        const int MAX_TASKS = 10;

        // ******************************************************************
        // Constructors
        // ******************************************************************

        #region Constructors
        public OperationDisplayDefault()
            : base(SortType.DEFAULT)
        { }

        public OperationDisplayDefault(SortType sortType)
            : base(sortType)
        { }
        #endregion

        // ******************************************************************
        // Override for Executing this operation
        // ******************************************************************

        #region ExecuteOperation
        /// <summary>
        /// Executes the operation.
        /// </summary>
        /// <param name="taskList">List of task this method will operate on.</param>
        /// <param name="storageIO">Storage controller that will be used to store neccessary data.</param>
        /// <returns>Response indicating the result of the operation execution.</returns>
        public override Response Execute(List<Task> taskList, Storage storageIO)
        {
            SetMembers(taskList, storageIO);

            DateTimeSpecificity isSpecific = new DateTimeSpecificity();
            
            List<Task> mostRecentTasks =
                (from task in taskList
                 where task.IsWithinTime(DateTime.Today, DateTime.Today.AddDays(7))
                 select task).ToList();

            mostRecentTasks.Sort(Task.CompareByDateTime);

            if (mostRecentTasks.Count > MAX_TASKS)
                mostRecentTasks = mostRecentTasks.GetRange(0, MAX_TASKS);

            mostRecentTasks.AddRange(from task in taskList where task is TaskFloating select task);

            currentListedTasks = new List<Task>(mostRecentTasks);

            return new Response(Result.SUCCESS, SortType.DATE_TIME, this.GetType(), currentListedTasks);
        }
        #endregion

    }
}