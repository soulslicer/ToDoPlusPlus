//@qianpan A0103985Y
using System;
using System.Collections.Generic;

namespace ToDo
{
    public class OperationSearch : Operation
    {
        // ******************************************************************
        // Search Parameters
        // ******************************************************************

        #region Search Parameters
        private string searchString = "";
        private DateTime? startTime = null, endTime = null;
        private DateTimeSpecificity isSpecific;
        private SearchType searchType;
        #endregion

        // ******************************************************************
        // Constructors
        // ******************************************************************

        #region Constructors

        /// <summary>
        /// Constructor for Search operation. Takes in the following parameters
        /// to define the search filters to be used when the operation is executed.
        /// </summary>
        /// <param name="searchString">The string which the task name must contain to match the search.</param>
        /// <param name="startTime">The start time by which the task must within to match the search.</param>
        /// <param name="endTime">The end time by which the task must within to match the search.</param>
        /// <param name="isSpecific">The specificty of the time ranges.</param>
        /// <param name="searchType">The type of search filter to use in addition to the other filters.</param>
        /// <param name="sortType">The type of sort to sort the diplay list by after the operation is executed.</param>
        /// <returns>Response containing the search results, the operation's success or failure, and the new sort type if any.</returns>
        public OperationSearch(
            string searchString,
            DateTime? startTime,
            DateTime? endTime,
            DateTimeSpecificity isSpecific,
            SearchType searchType,
            SortType sortType)
            : base(sortType)
        {
            this.searchString = searchString;
            this.startTime = startTime;
            this.endTime = endTime;
            this.isSpecific = isSpecific;
            this.searchType = searchType;
        }
        #endregion

        #region ExecuteOperation
        /// <summary>
        /// Executes the operation according to this operation's parameters.
        /// </summary>
        /// <param name="taskList">List of task this method will operate on.</param>
        /// <param name="storageIO">Storage controller that will be used to store neccessary data.</param>
        /// <returns>Response indicating the result of the operation execution.</returns>
        public override Response Execute(List<Task> taskList, Storage storageIO)
        {
            Response response = null;

            SetMembers(taskList, storageIO);            

            List<Task> searchResults = SearchForTasks(searchString, false, startTime, endTime, searchType);

            if (searchResults.Count == 0)
                response = new Response(Result.FAILURE, sortType, this.GetType());

            else
            {
                currentListedTasks = new List<Task>(searchResults);

                string[] criteria;
                SetArgumentsForSearchFeedbackString(out criteria, searchString, startTime, endTime, searchType);
                response = new Response(Result.SUCCESS, sortType, this.GetType(), currentListedTasks, criteria);
            }
            return response;
        }
        #endregion
    }   
}
