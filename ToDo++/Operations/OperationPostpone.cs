//@ivan A0086401M
using System;
using System.Collections.Generic;

namespace ToDo
{
    class OperationPostpone : Operation
    {
        // ******************************************************************
        // Parameters
        // ******************************************************************

        #region Parameters
        private int startIndex;
        private int endIndex;
        private bool hasIndex;
        private bool isAll;
        private string taskName;
        private DateTime? startTime = null, endTime = null;
        private DateTimeSpecificity isSpecific = new DateTimeSpecificity();
        private SearchType searchType;
        private TimeSpan postponeDuration;
        #endregion Parameters

        // ******************************************************************
        // Constructors
        // ******************************************************************

        #region Constructors
        /// <summary>
        /// This is the constructor for the Postpone operation which accepts arguments
        /// to define the way this operation will be executed.
        /// If a valid index range is specified or the isAll set to true, the operation will be carried out
        /// those corresponding indicies or all displayed tasks respectively.
        /// If search parameters are specified instead, a search operation will be carried out instead.
        /// The operation will be carried out on the search results if the isAll flag is true.
        /// </summary>
        /// <param name="taskName">The name of the task to postpone. Can be a substring of it.</param>
        /// <param name="indexRange">The display index of the task to postpone.</param>
        /// <param name="startTime">The start date of the range of tasks to be postponed.</param>
        /// <param name="endTime">The end date of the range of tasks to be postponed.</param>
        /// <param name="isSpecific">The specificity of the start and end date ranges.</param>
        /// <param name="isAll">If this boolean is true, the current displayed tasks or results of the search
        /// carried out will all be postponed without futher confirmation.</param>
        /// <param name="searchType">The type of search to be carried out (in addition to the other filters) if required.</param>
        /// <param name="postponeDuration">The duration to postpone the task by.</param>
        /// <param name="sortType">The type of sort to sort the diplay list by after the operation is executed.</param>
        /// <returns>Nothing.</returns>
        public OperationPostpone(string taskName, int[] indexRange, DateTime? startTime,
            DateTime? endTime, DateTimeSpecificity isSpecific, bool isAll, SearchType searchType, TimeSpan postponeDuration, SortType sortType)
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
            this.postponeDuration = postponeDuration;
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
        /// </summary>
        /// <param name="taskList">List of task this operation will operate on.</param>
        /// <param name="storageIO">Storage controller that will be used to store neccessary data.</param>
        /// <returns>Response indicating the result of the operation execution.</returns>
        public override Response Execute(List<Task> taskList, Storage storageIO)
        {
            SetMembers(taskList, storageIO);

            Func<Task, TimeSpan, Response> action = PostponeTask;
            object[] args = { postponeDuration };
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

        /// <summary>
        /// Postpones a task by the specified duration.
        /// </summary>
        /// <param name="taskToPostpone">The task to postpone.</param>
        /// <param name="postponeDuration">A timespan representing the duration to postpone the task.</param>
        /// <returns></returns>
        private Response PostponeTask(Task taskToPostpone, TimeSpan postponeDuration)
        {
            if (taskToPostpone.Postpone(postponeDuration) == false)
                return new Response(Result.INVALID_TASK, sortType, this.GetType(), currentListedTasks);
            else
                executedTasks.Enqueue(taskToPostpone);
            if (storageIO.UpdateTask(taskToPostpone))
                return GenerateStandardSuccessResponse(taskToPostpone);
            else
                return GenerateXMLFailureResponse();
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

            Response response = null;

            for (int i = 0; i < executedTasks.Count; i++)
            {
                Task taskToUndo = executedTasks.Dequeue();
                response = PostponeTask(taskToUndo, postponeDuration.Negate());
                if (!response.IsSuccessful())
                    return response;
            }

            if (response == null)
                response = new Response(Result.FAILURE, sortType, this.GetType());

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

            Response response = new Response(Result.FAILURE, sortType, this.GetType());

            for (int i = 0; i < executedTasks.Count; i++)
            {
                Task taskToRedo = executedTasks.Dequeue();
                response = PostponeTask(taskToRedo, postponeDuration);
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
