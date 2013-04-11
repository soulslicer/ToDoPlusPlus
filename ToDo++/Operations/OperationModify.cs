//@qianpan A0103985Y
using System.Collections.Generic;
using System;

namespace ToDo
{
    class OperationModify : Operation
    {
        // ******************************************************************
        // Parameters
        // ******************************************************************

        #region Parameters
        private string taskName;
        private int startIndex;
        private int endIndex;
        private bool hasIndex;
        private bool isAll;
        private SearchType searchType;
        private DateTime? startTime = null, endTime = null;
        private DateTimeSpecificity isSpecific;
        #endregion

        #region Task Tracking
        private Task oldTask;
        private Task newTask;
        #endregion

        // ******************************************************************
        // Constructors
        // ******************************************************************

        #region Constructors
        /// <summary>
        /// This is the constructor for the Modify operation.
        /// It will modify the task indicated by the index range to the new
        /// parameters specified by the given arguments. If an arguement
        /// is left empty or null, that parameter will remain unchanged.
        /// </summary>
        /// <param name="taskName">The name of the task to modified. Can be a substring of it.</param>
        /// <param name="indexRange">The display index of the task to be modified.</param>
        /// <param name="startTime">The new start date to set for the task.</param>
        /// <param name="endTime">The new end date to set for the task.</param>
        /// <param name="isSpecific">The new Specificity of the dates for the task.</param>
        /// <param name="isAll">If this boolean is true, the operation will be invalid.</param>
        /// <param name="searchType">The type of search to be carried out (in addition to the other filters) if required.</param>
        /// <param name="sortType">The type of sort to sort the diplay list by after the operation is executed.</param>
        /// <returns>Nothing.</returns>
        public OperationModify(string taskName, int[] indexRange, DateTime? startTime,
            DateTime? endTime, DateTimeSpecificity isSpecific, bool isAll, SearchType searchType, SortType sortType)
            : base(sortType)
        {
            if (indexRange == null) hasIndex = false;
            else
            {
                hasIndex = true;
                this.startIndex = indexRange[TokenIndexRange.START_INDEX] - 1;
                this.endIndex = indexRange[TokenIndexRange.END_INDEX] - 1;
            }
            if (taskName == null) this.taskName = "";
            else this.taskName = taskName;
            this.startTime = startTime;
            this.endTime = endTime;
            this.isSpecific = isSpecific;
            this.isAll = isAll;
            this.searchType = searchType;
        }
        #endregion

        // ******************************************************************
        // Override for Executing this operation
        // ******************************************************************

        #region ExecuteOperation        
        /// <summary>
        /// Executes the operation and adds it to the operation history.
        /// Modifies the task indicated by the index range to the new
        /// parameters in this operation. If a parameter is left empty or null,
        /// that parameter will remain unchanged in the new task.
        /// </summary>
        /// <param name="taskList">List of task this operation will operate on.</param>
        /// <param name="storageIO">Storage controller that will be used to store neccessary data.</param>
        /// <returns>Response indicating the result of the operation execution.</returns>
        public override Response Execute(List<Task> taskList, Storage storageIO)
        {
            SetMembers(taskList, storageIO);

            Response response = null;

            // No index, do a search instead
            if (!hasIndex && !isAll)
            {
                SetMembers(taskList, storageIO);
                List<Task> searchResults = SearchForTasks(taskName, false, startTime, endTime, searchType);
                response = DisplaySearchResults(searchResults, taskName, startTime, endTime, searchType);
            }
            else
            {                
                response = CheckIfIndexesAreValid(startIndex, endIndex);
                if (response != null) return response;

                if (MultipleTasksSelected())
                    return new Response(Result.INVALID_TASK, sortType, this.GetType());

                oldTask = currentListedTasks[startIndex];

                // copy over taskName from indexed task if didn't specify a name
                if (!IsValidString(taskName))
                    taskName = oldTask.TaskName;
                // copy over date/times from indexed task if didn't specify time
                else if (startTime == null && endTime == null)
                    oldTask.CopyDateTimes(ref startTime, ref endTime, ref isSpecific);

                newTask = Task.CreateNewTask(taskName, startTime, endTime, isSpecific);

                response = ModifyTask(oldTask, newTask);
            }

            if (response.IsSuccessful())
            {
                AddToOperationHistory();
            }

            return response;
        }

        /// <summary>
        /// Returns true if more than one tasks were selected by the user.
        /// </summary>
        /// <returns>Boolean indicated if multiple tasks were selected.</returns>
        private bool MultipleTasksSelected()
        {
            return startIndex != endIndex || isAll == true;
        }
        
        /// <summary>
        /// Modifies a task in the list specified by SetMembers by replacing 
        /// it with the a new given new task.
        /// Does not preserve the hash and actual object of the original task.
        /// </summary>
        /// <param name="taskToModify">The old task to be modified.</param>
        /// <param name="newTask">The new task to replace the old task.</param>
        /// <returns></returns>
        private Response ModifyTask(Task taskToModify, Task newTask)
        {
            Response response = null;
            response = DeleteTask(taskToModify);
            if (response.IsSuccessful()) response = AddTask(newTask);
            if (response.IsSuccessful())
                return new Response(Result.SUCCESS, sortType, typeof(OperationModify), currentListedTasks);
            else
                return response;
        }
        #endregion

        // ******************************************************************
        // Overrides for Undoing and Redoing this operation
        // ******************************************************************

        #region Undo and Redo
        /// <summary>
        /// Undoes this operation.
        /// </summary>
        /// <param name="taskList">List of task this method will operate on.</param>
        /// <param name="storageIO">Storage controller that will be used to store neccessary data.</param>
        /// <returns>Response indicating the result of the undo operation.</returns>
        public override Response Undo(List<Task> taskList, Storage storageIO)
        {
            SetMembers(taskList, storageIO);
            Response response = ModifyTask(newTask, oldTask);
            return response;
        }

        /// <summary>
        /// Redoes this operation.
        /// </summary>
        /// <param name="taskList">List of task this method will operate on.</param>
        /// <param name="storageIO">Storage controller that will be used to store neccessary data.</param>
        /// <returns>Response indicating the result of the undo operation.</returns>
        public override Response Redo(List<Task> taskList, Storage storageIO)
        {
            SetMembers(taskList, storageIO);
            Response response = ModifyTask(oldTask, newTask);
            return response;
        }
        #endregion

    }
}
