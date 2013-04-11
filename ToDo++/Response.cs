//@ivan A0086401M
using System;
using System.Collections.Generic;

namespace ToDo
{
    public enum Result { SUCCESS, SUCCESS_MULTIPLE, FAILURE, INVALID_TASK, INVALID_COMMAND, XML_READWRITE_FAIL, TASK_MISSING_FROM_FILE, EXCEPTION_FAILURE };
    public class Response
    {
        // ******************************************************************
        // Feedback Strings
        // ******************************************************************

        #region Feedback Strings
        const string STRING_ADD_SUCCESS = "Added new task \"{0}\" successfully.";
        const string STRING_ADD_FAILURE = "Failed to add task!";
        const string STRING_DELETE_SUCCESS = "Deleted task \"{0}\" successfully.";
        const string STRING_DELETE_SUCCESS_MULTI = "Deleted all indicated tasks successfully.";
        const string STRING_DELETE_FAILURE = "Failed to carry out delete operation!";
        const string STRING_DELETE_INVALID_TASK = "No task to delete!";
        const string STRING_MODIFY_SUCCESS = "Modified task successfully.";
        const string STRING_MODIFY_FAILURE = "Failed to modify task..!";
        const string STRING_MODIFY_INVALID_TASK = "Cannot modify more than one task at once!";
        const string STRING_DISPLAY_NO_TASK = "There are no tasks for display.";
        const string STRING_SEARCH_SUCCESS = "Displaying all {0}tasks{1}.";
        const string STRING_SEARCH_FAILURE = "No matching tasks found!";
        const string STRING_SORT_SUCCESS = "Sorting by {0}.";
        const string STRING_SORT_FAILURE = "Please specify sort type.";
        const string STRING_UNDO_SUCCESS = "Undid last operation.";
        const string STRING_UNDO_FAILURE = "Cannot undo anymore!";
        const string STRING_REDO_SUCCESS = "Redid last operation.";
        const string STRING_REDO_FAILURE = "Cannot redo anymore!";
        const string STRING_POSTPONE_SUCCESS = "Postponed task \"{0}\" successfully.";
        const string STRING_POSTPONE_SUCCESS_MULTI = "Postponed all tasks successfully.";
        const string STRING_POSTPONE_INVALID_TASK = "Trying to postpone a task by a more specific time than it allows!";
        const string STRING_POSTPONE_FAILURE = "Failed to postpone task!";
        const string STRING_MARKASDONE_SUCCESS = "Successfully marked \"{0}\" as done.";
        const string STRING_MARKASDONE_SUCCESS_MULTI = "Successfully marked all tasks as done.";
        const string STRING_MARKASDONE_FAILURE = "No matching tasks found!";
        const string STRING_MARKASDONE_INVALID_TASK = "Task already marked as done.";
        const string STRING_MARKASUNDONE_SUCCESS = "Successfully marked \"{0}\" as undone.";
        const string STRING_MARKASUNDONE_SUCCESS_MULTI = "Successfully marked all tasks as undone.";
        const string STRING_MARKASUNDONE_FAILURE = "No matching tasks found!";
        const string STRING_MARKASUNDONE_INVALID_TASK = "Task already marked as undone.";
        const string STRING_SCHEDULE_INVALID_TASK = "Task duration exceeds specified time range!";
        const string STRING_SCHEDULE_FAILURE = "Task could not be scheduled within specified time range (no free slot)!";
        const string STRING_SCHEDULE_SUCCESS = "Scheduled new task \"{0}\" successfully.";
        const string STRING_XML_READWRITE_FAIL = "Failed to read/write from XML file!";
        const string STRING_CALLED_INVALID_TASK_INDEX = "Invalid task index!";
        const string STRING_INVALID_COMMAND = "Invalid command input!";        
        const string STRING_UNDEFINED = "Undefined feedback string!";
        const string STRING_EXCEPTION_FAILURE = "An unrecoverable exception occured.";

        const string STRING_NAME = "name";
        const string STRING_DATE = "date";
        #endregion
        
        #region Parameter Indices
        public const int MODIFY_PARAM_OLD_TASK = 0;
        public const int MODIFY_PARAM_NEW_TASK = 1;
        public const int MODIFY_PARAM_NUM = 2;
        public const int SEARCH_PARAM_DONE = 0;
        public const int SEARCH_PARAM_SEARCH_STRING = 1;
        public const int SEARCH_PARAM_NUM = 2;
        #endregion

        #region Parameters
        Result result;
        SortType sortType;
        bool feedbackAsWarning = false;
        string[] args;
        string feedbackString = null;
        List<Task> tasksToBeDisplayed;
        #endregion

        #region Getters
        public SortType FormatType
        {
            get { return sortType; }
        }
        public string FeedbackString
        {
            get { return feedbackString; }
        }
        public Result Result
        {
            get { return result; }
        }
        public List<Task> TasksToBeDisplayed
        {
            get { return tasksToBeDisplayed; }
        }
        #endregion

        /// <summary>
        /// Constructor for creating a response.
        /// </summary>
        /// <param name="resultType">The type of result this response should signify.</param>
        /// <param name="sortType">The type of sort the displayed task list should be formatted in.</param>
        /// <param name="operationType">The type of operation which created the response.</param>
        /// <param name="tasks">The list of task to display.</param>
        /// <param name="args">Additional arguments to format the feedback string.</param>        
        public Response(
            Result resultType,
            SortType sortType = SortType.DEFAULT,
            Type operationType = null,
            List<Task> tasks = null,
            params string[] args
            )
        {
            this.sortType = sortType;
            this.tasksToBeDisplayed = tasks;
            this.result = resultType;
            this.args = args;
            SetFeedbackString(resultType, operationType);            
        }

        /// <summary>
        /// Gets a boolean indicating the success of the executed operation.
        /// </summary>
        /// <returns>True if successful. False if unsuccessful.</returns>
        public bool IsSuccessful()
        {
            if (result == Result.SUCCESS || result == Result.SUCCESS_MULTIPLE) return true;
            else return false;
        }
        
        /// <summary>
        /// Returns a flag signifying whether this response should signal a warning.
        /// </summary>
        /// <returns>True if the user should be warn; False if not.</returns>
        internal bool WarnUser()
        {
            return feedbackAsWarning;
        }

        /// <summary>
        /// Returns a flag signifiying whether the user called for an invalid task.
        /// </summary>
        /// <returns>True if the user tried to operate on an invalid task; False if not.</returns>
        public bool IsInvalidTask()
        {
            if (result == Result.INVALID_TASK)
                return true;
            else return false;
        }

        /// <summary>
        /// Sets the feedback string based on the given result and type of operation.
        /// </summary>
        /// <param name="resultType">The result of the operation.</param>
        /// <param name="operationType">The type of operation.</param>
        /// <returns></returns>
        private void SetFeedbackString(Result resultType, Type operationType)
        {
            try
            {
                switch (resultType)
                {
                    case Result.SUCCESS:
                        if (operationType == typeof(OperationAdd))
                            feedbackString = String.Format(STRING_ADD_SUCCESS, args);
                        if (operationType == typeof(OperationDelete))
                            feedbackString = String.Format(STRING_DELETE_SUCCESS, args);
                        if (operationType == typeof(OperationModify))
                            feedbackString = String.Format(STRING_MODIFY_SUCCESS, args);
                        if (operationType == typeof(OperationMarkAsDone))
                            feedbackString = String.Format(STRING_MARKASDONE_SUCCESS, args);
                        if (operationType == typeof(OperationMarkAsUndone))
                            feedbackString = String.Format(STRING_MARKASUNDONE_SUCCESS, args);
                        if (operationType == typeof(OperationSchedule))
                            feedbackString = String.Format(STRING_SCHEDULE_SUCCESS, args);
                        if (operationType == typeof(OperationSearch))
                            feedbackString = String.Format(STRING_SEARCH_SUCCESS, args);
                        if (operationType == typeof(OperationPostpone))
                            feedbackString = String.Format(STRING_POSTPONE_SUCCESS, args);
                        if (operationType == typeof(OperationUndo))
                            feedbackString = STRING_UNDO_SUCCESS;
                        if (operationType == typeof(OperationRedo))
                            feedbackString = STRING_REDO_SUCCESS;
                        if (operationType == typeof(OperationSort))
                            feedbackString = GetSortTypeString();
                        break;
                    case Result.SUCCESS_MULTIPLE:
                        if (operationType == typeof(OperationDelete))
                            feedbackString = STRING_DELETE_SUCCESS_MULTI;
                        if (operationType == typeof(OperationMarkAsDone))
                            feedbackString = STRING_MARKASDONE_SUCCESS_MULTI;
                        if (operationType == typeof(OperationMarkAsUndone))
                            feedbackString = STRING_MARKASUNDONE_SUCCESS_MULTI;
                        if (operationType == typeof(OperationPostpone))
                            feedbackString = STRING_POSTPONE_SUCCESS_MULTI;
                        break;
                    case Result.FAILURE:
                        if (operationType == typeof(OperationAdd))
                            feedbackString = STRING_ADD_FAILURE;
                        if (operationType == typeof(OperationDelete))
                            feedbackString = STRING_DELETE_FAILURE;
                        if (operationType == typeof(OperationModify))
                            feedbackString = STRING_MODIFY_FAILURE;
                        if (operationType == typeof(OperationPostpone))
                            feedbackString = STRING_POSTPONE_FAILURE;
                        if (operationType == typeof(OperationMarkAsDone))
                            feedbackString = STRING_MARKASDONE_FAILURE;
                        if (operationType == typeof(OperationMarkAsUndone))
                            feedbackString = STRING_MARKASUNDONE_FAILURE;
                        if (operationType == typeof(OperationSchedule))
                            feedbackString = STRING_SCHEDULE_FAILURE;
                        if (operationType == typeof(OperationSearch))
                        {
                            feedbackString = STRING_SEARCH_FAILURE;
                            feedbackAsWarning = true;
                        }
                        if (operationType == typeof(OperationUndo))
                            feedbackString = STRING_UNDO_FAILURE;
                        if (operationType == typeof(OperationRedo))
                            feedbackString = STRING_REDO_FAILURE;
                        if (operationType == typeof(OperationSort))
                        {
                            feedbackString = STRING_SORT_FAILURE;
                            feedbackAsWarning = true;
                        }
                        break;
                    case Result.INVALID_TASK:
                        if (operationType == typeof(OperationDisplayDefault) ||
                            operationType == typeof(OperationSearch))
                            feedbackString = STRING_DISPLAY_NO_TASK;
                        else if (operationType == typeof(OperationDelete))
                            feedbackString = STRING_DELETE_INVALID_TASK;
                        else if (operationType == typeof(OperationModify))
                            feedbackString = STRING_MODIFY_INVALID_TASK;
                        else if (operationType == typeof(OperationPostpone))
                            feedbackString = STRING_POSTPONE_INVALID_TASK;
                        else if (operationType == typeof(OperationMarkAsDone))
                            feedbackString = STRING_MARKASDONE_INVALID_TASK;
                        else if (operationType == typeof(OperationMarkAsUndone))
                            feedbackString = STRING_MARKASUNDONE_INVALID_TASK;
                        else if (operationType == typeof(OperationSchedule))
                            feedbackString = STRING_SCHEDULE_INVALID_TASK;
                        else
                            feedbackString = STRING_CALLED_INVALID_TASK_INDEX;
                        break;
                    case Result.INVALID_COMMAND:
                        feedbackString = STRING_INVALID_COMMAND;
                        break;
                    case Result.XML_READWRITE_FAIL:
                        feedbackString = STRING_XML_READWRITE_FAIL;
                        break;
                    case Result.EXCEPTION_FAILURE:
                        feedbackString = STRING_EXCEPTION_FAILURE;
                        break;
                    default:
                        throw new Exception("Type of Result in invalid!");
                }
            }
            catch (FormatException e)
            {
                // add to log! what params were given and what Response.ToString() is this?
                Logger.Error(e, "SetFeedbackString::Response");
                feedbackString = STRING_UNDEFINED;
                resultType = Result.EXCEPTION_FAILURE;
            }
            if (feedbackString == null)
            {
                feedbackString = STRING_UNDEFINED;
            }
        }

        /// <summary>
        /// Gets the string representing the type of sort the user called for if any.
        /// </summary>
        /// <returns>The string representing the type of sort used.</returns>
        private string GetSortTypeString()
        {
            string sortTypeString = "";
            switch (sortType)
            {
                case SortType.NAME:
                    sortTypeString = STRING_NAME;
                    break;
                case SortType.DATE_TIME:
                    sortTypeString = STRING_DATE;
                    break;
                case SortType.DEFAULT:
                    break;
            }
            return String.Format(STRING_SORT_SUCCESS, sortTypeString);
        }
    }
}
