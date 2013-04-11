//@ivan A0086401M
using System;
using System.Collections.Generic;

namespace ToDo
{
    class OperationMarkAsDone : Operation
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
        #endregion Parameters

        // ******************************************************************
        // Constructors
        // ******************************************************************

        #region Constructors
        /// <summary>
        /// This is the constructor for the MarkAsDone operation which accepts arguments
        /// to define the way this operation will be executed.
        /// If a valid index range is specified or the isAll set to true, the operation will be carried out
        /// those corresponding indicies or all displayed tasks respectively.
        /// If search parameters are specified instead, a search operation will be carried out instead.
        /// The operation will be carried out on the search results if the isAll flag is true.
        /// </summary>
        /// <param name="taskName">The name of the task to mark as done. Can be a substring of it.</param>
        /// <param name="indexRange">The display index of the task to be marked.</param>
        /// <param name="startTime">The start date from which to mark all tasks as done.</param>
        /// <param name="endTime">The end date to which to mark all tasks as done.</param>
        /// <param name="isSpecific">The specificity of the start and end ranges</param>
        /// <param name="isAll">If this boolean is true, the current displayed tasks or results of the search
        /// carried out will all be marked as done.</param>
        /// <param name="searchType">The type of search to be carried out (in addition to the other filters) if required.</param>
        /// <param name="sortType">The type of sort to sort the diplay list by after the operation is executed.</param>
        /// <returns>Nothing.</returns>
        public OperationMarkAsDone(string taskName, int[] indexRange, DateTime? startTime,
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

        public override bool AllowSkipOver(Response response)
        {
            if (response.IsInvalidTask())
                return true;
            else return false;
        }

        // ******************************************************************
        // Override for Executing this operation
        // ******************************************************************

        #region ExecuteOperation
        /// <summary>
        /// Executes the operation and adds it to the operation history.
        /// This operation tries to mark one or more tasks as done using the given parameters.
        /// If an index exist, it will mark all tasks by index.
        /// If not, it will perform a search, marking tasks immediately if the isAll flag
        /// is set.
        /// </summary>
        /// <param name="taskList">List of task this operation will operate on.</param>
        /// <param name="storageIO">Storage controller that will be used to store neccessary data.</param>
        /// <returns>Response indicating the result of the operation execution.</returns>
        public override Response Execute(List<Task> taskList, Storage storageIO)
        {
            SetMembers(taskList, storageIO);

            Func<Task, bool, Response> action = MarkTaskAs;
            object[] args = { (bool)true };

            Response response = null;

            response = CheckIfIndexesAreValid(startIndex, endIndex);
            if (response != null) return response;

            if (!hasIndex)
                response = ExecuteBySearch(taskName, startTime, endTime, isAll, searchType, action, args);

            else if (hasIndex)
                response = ExecuteByIndex(startIndex, endIndex, action, args);

            else
                response = new Response(Result.FAILURE, sortType, this.GetType());

            if (response.IsSuccessful())
                AddToOperationHistory();

            return response;
        }
        #endregion

        // ******************************************************************
        // Overrides for Undoing and Redoing this operation
        // ******************************************************************

        #region Undo and Redo
        /// <summary>
        /// Undo this operation.
        /// </summary>
        /// <param name="taskList">List of task this method will operate on.</param>
        /// <param name="storageIO">Storage controller that will be used to store neccessary data.</param>
        /// <returns>Response indicating the result of the undo operation.</returns>
        public override Response Undo(List<Task> taskList, Storage storageIO)
        {
            SetMembers(taskList, storageIO);

            Response response = null;

            for (int i = 0; i < executedTasks.Count; i++)
            {
                Task taskToUndo = executedTasks.Dequeue();
                response = MarkTaskAs(taskToUndo, false);
                if (!response.IsSuccessful())
                    return response;
            }
            
            if (response == null ) 
                response = new Response(Result.FAILURE, sortType, this.GetType());

            return response;
        }

        /// <summary>
        /// Redo this operation.
        /// </summary>
        /// <param name="taskList">List of task this method will operate on.</param>
        /// <param name="storageIO">Storage controller that will be used to store neccessary data.</param>
        /// <returns>Response indicating the result of the undo operation.</returns>
        public override Response Redo(List<Task> taskList, Storage storageIO)
        {
            SetMembers(taskList, storageIO);

            Response response = null;

            for (int i = 0; i < executedTasks.Count; i++)
            {
                Task taskToUndo = executedTasks.Dequeue();
                response = MarkTaskAs(taskToUndo, true);
                if (!response.IsSuccessful())
                    return response;
            }

            if (response == null)
                response = new Response(Result.FAILURE, sortType, this.GetType());

            return response;
        }
        #endregion
    } 
}
