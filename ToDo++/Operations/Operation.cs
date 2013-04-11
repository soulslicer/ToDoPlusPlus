//@ivan A0086401M
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ToDo
{
    // ******************************************************************
    // Abstract definition for Operation
    // ******************************************************************
    /// <summary>
    /// This class contains the necessary information representing a user's
    /// requested operation. It can be executed by providing a list of Tasks 
    /// to execute the command on, as well as a Storage controller to store
    /// necessary data.
    /// </summary>
    public abstract class Operation
    {

        #region Variables for Task and Operation tracking/storage
        protected static List<Task> currentListedTasks;     // The currently displayed list of tasks.        
        protected static Stack<Operation> undoStack;        // Contains the Operation history of all Operations.
        protected static Stack<Operation> redoStack;        // Contains the Operation Undo history of all Operations.        
        protected Queue<Task> executedTasks;                // Contains a history of tasks which this operation has manipulated.        
        protected List<Task> taskList;                      // The entire list of tasks which this Operation will execute on.        
        protected Storage storageIO;                        // The Storage controller which will be used for reading / writing from tasks to file.
        protected SortType sortType;
        #endregion

        /// <summary>
        /// Initializes the static variables used by all Operations.
        /// </summary>
        static Operation()
        {
            currentListedTasks = new List<Task>();
            undoStack = new Stack<Operation>();
            redoStack = new Stack<Operation>();
        }

        /// <summary>
        /// Initializes the neccesary variables for all Operation.
        /// </summary>
        protected Operation(SortType sortType)
        {
            this.sortType = sortType;
            executedTasks = new Queue<Task>();
        }

        /// <summary>
        /// Sets the currently displayed list of tasks shared by all Operations
        /// to the input list of tasks.
        /// </summary>
        /// <param name="tasks">The list of tasks to be displayed.</param>
        public static void UpdateCurrentListedTasks(List<Task> tasks)
        {
            currentListedTasks = tasks;
        }

        /// <summary>
        /// Sets the operating task list and storage IO controller
        /// this Operation will use to the instances referred to by the input parameters.
        /// </summary>
        /// <param name="taskList">The task list this Operation will execute on.</param>
        /// <param name="storageIO">The storage IO controller this Operation will use to store data.</param>
        /// <returns></returns>
        protected void SetMembers(List<Task> taskList, Storage storageIO)
        {
            this.storageIO = storageIO;
            this.taskList = taskList;
        }

        /// <summary>
        /// Adds this Operation to the history of successful operations.
        /// </summary>
        /// <returns>Nothing.</returns>
        protected void AddToOperationHistory()
        {
            undoStack.Push(this);
            redoStack.Clear();
        }

        /// <summary>
        /// Base method to execute this Operation. Must be overriden by all derived Operations.
        /// </summary>
        /// <param name="taskList">The list of task to execute the Operation on.</param>
        /// <param name="storageIO">The Storage controller to use for reading/writing to file.</param>
        /// <returns></returns>
        public abstract Response Execute(List<Task> taskList, Storage storageIO);

        /// <summary>
        /// Base Undo method. All undoable operations must override this method.
        /// This base method will throw an assertion if called without being overriden
        /// and debug mode is on.
        /// </summary>
        /// <param name="taskList">Current task list for task updates to be applied on.</param>
        /// <param name="storageIO">Storage controller to be used to write task changes.</param>
        public virtual Response Undo(List<Task> taskList, Storage storageIO)
        {
            Debug.Assert(false, "This operation should not be undoable!");
            return null;
        }

        /// <summary>
        /// Base Redo method. All undoable operations must override this method.
        /// This base method will throw an assertion if it is called without being overriden
        /// and debug mode is on.
        /// </summary>
        /// <param name="taskList">Current task list for task updates to be applied on.</param>
        /// <param name="storageIO">Storage controller to be used to write task changes.</param>
        /// <returns>Null</returns>
        public virtual Response Redo(List<Task> taskList, Storage storageIO)
        {
            Debug.Assert(false, "This operation should not be redoable!");
            return null;
        }

        /// <summary>
        /// Indicates whether the Operation should allow a multiple-task
        /// execution to continue if one of the tasks execute unsuccessfully.
        /// This method can be overriden to specify when this condition should be allowed.
        /// If it is not overriden, it will return false by default.
        /// </summary>
        /// <param name="response"></param>
        /// <returns>False</returns>
        public virtual bool AllowSkipOver(Response response)
        {
            return false;
        }

        // ******************************************************************
        // Task Handling Methods
        // ******************************************************************

        #region Task Handling Methods
        /// <summary>
        /// Adds a task to the system. The newly added task is written to file
        /// using the Storage controller specified with SetMembers.
        /// </summary>
        /// <param name="taskToAdd">The task to add.</param>
        /// <returns>Response indicating the result of the operation.</returns>
        protected Response AddTask(Task taskToAdd)
        {
            try
            {
                if (TaskIsInvalid(taskToAdd))
                {
                    Logger.Warning("Attempted to add an invalid task.", "AddTask::Operation");
                    return new Response(Result.FAILURE, sortType, typeof(OperationAdd), currentListedTasks);
                }

                taskList.Add(taskToAdd);
                AddToOperationHistory(taskToAdd);

                bool success = storageIO.AddTaskToFile(taskToAdd);
                if (success)
                {
                    currentListedTasks.Add(taskToAdd);
                    Logger.Info("Added a new task successfully.", "AddTask::Operation");
                    return GenerateStandardSuccessResponse(taskToAdd);
                }
                else
                    return GenerateXMLFailureResponse();
            }
            catch (Exception e)
            {
                Logger.Error(e, "AddTask::Operation");
                return new Response(Result.FAILURE, sortType, typeof(OperationAdd), currentListedTasks);
            }
        }

        /// <summary>
        /// Removes a task from the system. The deleted task is removed from file
        /// using the Storage controller specified with SetMembers.
        /// </summary>
        /// <param name="taskToDelete"></param>
        /// <returns></returns>
        protected Response DeleteTask(Task taskToDelete)
        {
            try
            {
                taskList.Remove(taskToDelete);
                AddToOperationHistory(taskToDelete);

                if (currentListedTasks.Contains(taskToDelete))
                    currentListedTasks.Remove(taskToDelete);

                if (storageIO.RemoveTaskFromFile(taskToDelete))
                {
                    Logger.Info("Deleted a task successfully.", "DeleteTask::Operation");
                    return GenerateStandardSuccessResponse(taskToDelete);
                }
                else
                    return GenerateXMLFailureResponse();
            }
            catch (Exception e)
            {
                Logger.Error(e, "DeleteTask::Operation");
                return new Response(Result.FAILURE, sortType, typeof(OperationAdd), currentListedTasks);
            }
        }

        /// <summary>
        /// Marks a task's [done] state depending on the given flag. Marks as done if true, undone if false.
        /// </summary>
        /// <param name="taskToMark">Task to mark.</param>
        /// <param name="doneState">Flag indiciating whether to mark as done or undone.</param>
        /// <returns></returns>
        protected Response MarkTaskAs(Task taskToMark, bool doneState)
        {
            SetMembers(taskList, storageIO);

            if (taskToMark.DoneState == doneState)
                return new Response(Result.INVALID_TASK, sortType, this.GetType());
            else
                taskToMark.DoneState = doneState;

            executedTasks.Enqueue(taskToMark);

            if (storageIO.MarkTaskAs(taskToMark, doneState))
                return GenerateStandardSuccessResponse(taskToMark);
            else
                return GenerateXMLFailureResponse();
        }

        /// <summary>
        /// Searches the Tasks specified in SetMembers using the specified
        /// parameters as filters and returns the Tasks matching those filters.
        /// </summary>
        /// <param name="searchString">The string to match the Task's name against.</param>
        /// <param name="requireExactMatch">The flag indicating whether the string comparison should be exact or not.</param>
        /// <param name="startTime">The start time by which Tasks falling before are not matched.</param>
        /// <param name="endTime">The end time by which Tasks falling after are not matched.</param>
        /// <param name="searchType">The type of search to perform.</param>
        /// <returns></returns>
        protected List<Task> SearchForTasks(
            string searchString,
            bool requireExactMatch = false,
            DateTime? startTime = null,
            DateTime? endTime = null,
            SearchType searchType = SearchType.NONE
            )
        {
            List<Task> filteredTasks = taskList;
            try
            {
                filteredTasks = FilterByTaskName(filteredTasks, searchString, requireExactMatch);
                filteredTasks = FilterByTaskTime(filteredTasks, startTime, endTime);
                filteredTasks = FilterBySearchType(filteredTasks, searchType);
            }
            catch (Exception e)
            {
                Logger.Error(e, "SearchForTasks::Operation");
                return new List<Task>(); // return empty list.
            }
            return filteredTasks;
        }

        /// <summary>
        /// Filters the input list of tasks by time, returning a list of the filtered results.
        /// Tasks within the specified start and end times are returned.
        /// As long as part of the task is within the specified tasks,
        /// they are considered as validly matched.
        /// </summary>
        /// <param name="tasks">The list of task to apply the filter on.</param>
        /// <param name="startTime">The start time by which the filtered tasks should be within.</param>
        /// <param name="endTime">The end time by which the filtered tasks should be within.</param>
        /// <returns></returns>
        private List<Task> FilterByTaskTime(List<Task> tasks, DateTime? startTime, DateTime? endTime)
        {
            List<Task> filteredTasks = new List<Task>(tasks);
            if (!(startTime == null && endTime == null))
                filteredTasks = (from task in filteredTasks
                                 where task.IsWithinTime(startTime, endTime)
                                 select task).ToList();
            return filteredTasks;
        }

        /// <summary>
        /// Filters the input list of tasks by task name, returning the list of tasks with names matching the search string.
        /// </summary>
        /// <param name="tasks">The list of task to apply the filter on.</param>
        /// <param name="searchString">The string by which to filter the task name against.</param>
        /// <param name="exact">The flag indicating whether the string comparison should exact or not.</param>
        /// <returns>The filtered list of tasks.</returns>
        private List<Task> FilterByTaskName(List<Task> tasks, string searchString, bool exact)
        {
            List<Task> filteredTasks = new List<Task>(tasks);
            if (IsValidString(searchString))
                filteredTasks = (from task in filteredTasks
                                 where ((task.TaskName.IndexOf(searchString) >= 0 && exact == false) ||
                                       (String.Compare(searchString, task.TaskName, true) == 0 && exact == true))
                                 select task).ToList();
            return filteredTasks;
        }

        /// <summary>
        /// Filters the input list of tasks by the specified SearchType, returning a list of the filtered results.
        /// </summary>
        /// <param name="tasks">The list of task to apply the filter on.</param>
        /// <param name="searchType">The type of search to execute.</param>
        /// <returns></returns>
        private List<Task> FilterBySearchType(List<Task> tasks, SearchType searchType)
        {
            List<Task> filteredTasks = new List<Task>(tasks);
            bool doneMatch;
            if (searchType == SearchType.DONE)
                doneMatch = true;
            else if (searchType == SearchType.UNDONE)
                doneMatch = false;
            else return filteredTasks; // don't sort anymore.

            filteredTasks = (from task in filteredTasks
                             where task.DoneState == doneMatch
                             select task).ToList();
            return filteredTasks;
        }

        /// <summary>
        /// This method checks for the validity of the specified task.
        /// </summary>
        /// <param name="taskToCheck">The Task to check validity of.</param>
        /// <returns>True if invalid. False if valid.</returns>
        private bool TaskIsInvalid(Task taskToCheck)
        {
            if (taskToCheck == null)
                return true;
            if (!IsValidString(taskToCheck.TaskName))
                return true;
            return false;
        }

        /// <summary>
        /// This method adds the specified task to the history executed tasks for this operation.
        /// </summary>
        /// <param name="task">The task to add to history.</param>
        /// <returns></returns>
        private void AddToOperationHistory(Task task)
        {
            executedTasks.Enqueue(task);
        }
        #endregion

        // ****************************************************************************************
        // Task Selection And Delegate Invoking Methods
        // ****************************************************************************************

        #region Task Selection And Delegate Invoking Methods

        /// <summary>
        /// Searches for one or more tasks using the provided search paramters
        /// and then executes the specified action on them if there is only one result,
        /// or if the immediateExecutionFlag is set.
        /// Returns the list of search results otherwise.
        /// </summary>
        /// <param name="searchString">The string to search the tasks' names against.</param>
        /// <param name="startTime">The start time by which the task must fall within.</param>
        /// <param name="endTime">The end time by which the task must fall within.</param>
        /// <param name="immediateExecutionFlag">Flag which indicates whether the Delegate should be performed immediately if there are more than one results.</param>
        /// <param name="searchType">The type of search to perform.</param>
        /// <param name="action">The Delegate method to be invoked which must accept a Task as the first parameter.</param>
        /// <param name="args">Any subsequent parameters to be passed into the Delegate method and then invoked.</param>
        /// <returns>Response indicating the result of the operation.</returns>
        protected Response ExecuteBySearch(
                        string searchString,
                        DateTime? startTime,
                        DateTime? endTime,
                        bool immediateExecutionFlag,
                        SearchType searchType,
                        Delegate action,
                        params object[] args
                        )
        {
            List<Task> searchResults = new List<Task>();
            Response response = null;

            // User specified to execute immediately without returning search results.
            if (immediateExecutionFlag)
            {
                response = ExecuteAllBySearch(searchString, startTime, endTime, searchType, action, args);
                return response;
            }

            searchResults = SearchForTasks(searchString, true, startTime, endTime, searchType);

            // If no results and not trying to execute on all, try non-exact search.
            if (searchResults.Count == 0 && !immediateExecutionFlag)
                response = TrySearchNonExact(searchString, startTime, endTime, searchType);

            // If only one result and is searching by name, delete immediately.
            else if (searchResults.Count == 1 && IsValidString(searchString))
            {
                var parameters = AddTaskToParameters(args, searchResults[0]);
                response = InvokeAction(action, parameters);
            }
            // If not, display search results.
            else
            {
                response = DisplaySearchResults(searchResults, searchString, startTime, endTime, searchType);
            }
            return response;
        }

        /// <summary>
        /// Searches for one or more tasks using the provided search paramters
        /// and then executes the specified action on them immediately.
        /// Does an exact search only if all times provided are null.
        /// </summary>
        /// <param name="searchString">The string to search the tasks' names against.</param>
        /// <param name="startTime">The start time by which the task must fall within.</param>
        /// <param name="endTime">The end time by which the task must fall within.</param>        
        /// <param name="searchType">The type of search to perform.</param>
        /// <param name="action">The Delegate method to be invoked which must accept a Task as the first parameter.</param>
        /// <param name="args">Any subsequent parameters to be passed into the Delegate method and then invoked.</param>
        /// <returns>Response indicating the result of the operation.</returns>
        private Response ExecuteAllBySearch(
            string searchString,
            DateTime? startTime,
            DateTime? endTime,
            SearchType searchType,
            Delegate action,
            object[] args)
        {
            List<Task> searchResults = new List<Task>();
            Response response = null;

            if (HasValidTime(startTime, endTime))
                searchResults = SearchForTasks(searchString, false, startTime, endTime, searchType);
            else
                searchResults = SearchForTasks(searchString, true, startTime, endTime, searchType);

            if (searchResults.Count == 0)
            {
                Logger.Info("Tried to execute all with no results", "ExecuteAllBySearch::Operation");
                return new Response(Result.FAILURE, sortType, typeof(OperationSearch));
            }

            if (HasValidTime(startTime, endTime) || IsValidString(searchString) || searchType != SearchType.NONE)
                response = ExecuteOnAll(searchResults, action, args);
            else
                response = ExecuteOnAll(currentListedTasks, action, args);

            return response;
        }

        /// <summary>
        /// Executes the given Delegate for every Task in the given list of Tasks, with each of the Tasks as the first parameter.        
        /// Returns the Response indicating the result of this operation.
        /// </summary>
        /// <param name="tasks">The list of tasks to operate on.</param>
        /// <param name="action">The delegate to invoke.</param>
        /// <param name="args">An optional list of additional parameters to invoke with the delegate
        /// which will be added after the Task parameter.</param>
        /// <returns>Response indicating the result of the operation.</returns>
        private Response ExecuteOnAll(List<Task> tasks, Delegate action, params object[] args)
        {
            Response response = null;
            for (int i = tasks.Count - 1; i >= 0; i--)
            {
                Task task = tasks[i];

                if (task == null)
                    response = new Response(Result.FAILURE, sortType, this.GetType(), currentListedTasks);

                var parameters = AddTaskToParameters(args, task);

                response = InvokeAction(action, parameters);

                if (!response.IsSuccessful() && !AllowSkipOver(response))
                    return response;
            }
            response = new Response(Result.SUCCESS_MULTIPLE, sortType, this.GetType(), currentListedTasks);
            return response;
        }

        /// <summary>
        /// Performs a non exact search and updates the displayed list if there is a non-zero number of matches.
        /// </summary>
        /// <param name="taskName">The string to search the tasks' names against.</param>
        /// <param name="startTime">The start time by which the task must fall within.</param>
        /// <param name="endTime">The end time by which the task must fall within.</param>        
        /// <param name="searchType">The type of search to perform.</param>
        /// <returns></returns>
        private Response TrySearchNonExact(
            string taskName,
            DateTime? startTime,
            DateTime? endTime,
            SearchType searchType
            )
        {
            Response response = null;
            List<Task> searchResults = new List<Task>();

            searchResults = SearchForTasks(taskName, false, startTime, endTime, searchType);
            response = DisplaySearchResults(searchResults, taskName, startTime, endTime, searchType);

            return response;
        }
        
        /// <summary>
        /// Updates the current display list to show all tasks in the given search results list.
        /// </summary>
        /// <param name="searchResults">The search result list of tasks to display.</param>
        /// <param name="searchString">The search string to display in the feedback string for the user.</param>
        /// <param name="startTime">The limiting start time of the search.</param>
        /// <param name="endTime">The limiting end time of the search.</param>
        /// <param name="searchType">The search type of the search.</param>
        /// <returns>Response containing the new list of tasks to be displayed.</returns>
        protected Response DisplaySearchResults(
            List<Task> searchResults,
            string searchString,
            DateTime? startTime,
            DateTime? endTime,
            SearchType searchType
            )
        {
            Response response;
            if (searchResults.Count == 0)
                response = new Response(Result.FAILURE, sortType, typeof(OperationSearch));

            else
            {
                currentListedTasks = new List<Task>(searchResults);

                string[] criteria;
                SetArgumentsForSearchFeedbackString(out criteria, searchString, startTime, endTime, searchType);
                response = new Response(Result.SUCCESS, sortType, typeof(OperationSearch), currentListedTasks, criteria);
            }
            return response;
        }

        /// <summary>
        /// Executes a Delegate action on one or more tasks chosen by the currently displayed index.
        /// The indices provided must be valid for the current displayed list.
        /// </summary>
        /// <param name="startIndex">The start index of the tasks for the Delegate to invoke with.</param>
        /// <param name="endIndex">The end index of the tasks for the Delegate to invoke with.</param>
        /// <param name="action">The Delegate method to be invoked which must accept a Task as the first parameter.</param>
        /// <param name="args">Any subsequent parameters to be passed into the Delegate method and then invoked.</param>
        /// <returns>Response indicating the result of the operation.</returns>
        protected Response ExecuteByIndex(int startIndex, int endIndex, Delegate action, params object[] args)
        {
            Response response = null;

            Debug.Assert(startIndex >= 0 && endIndex < currentListedTasks.Count(), "Invalid indexes were passed to ExecuteByIndex!");

            for (int i = endIndex; i >= startIndex; i--)
            {
                Task task = currentListedTasks[i];
                if (task == null)
                    response = new Response(Result.FAILURE, sortType, this.GetType(), currentListedTasks);
                else
                {
                    var parameters = AddTaskToParameters(args, task);
                    response = InvokeAction(action, parameters);
                    if (!response.IsSuccessful() && !AllowSkipOver(response)) return response;
                }
            }

            if (startIndex != endIndex)
                response = new Response(Result.SUCCESS_MULTIPLE, sortType, this.GetType(), currentListedTasks);
            return response;
        }
        #endregion

        // ******************************************************************
        // Response Generation Methods
        // ******************************************************************

        #region Response Generation Methods

        /// <summary>
        /// This method checks if the two indices referring to a set of tasks in the currently displayed
        /// list of tasks are valid and returns null if they are valid.
        /// </summary>
        /// <param name="startIndex">The start index of the user requested tasks</param>
        /// <param name="endIndex">The end index of the user requested tasks</param>
        /// <returns>Returns a null value if indexes are valid. Returns a Response indicating the error if the indexes are invalid.</returns>
        protected Response CheckIfIndexesAreValid(int startIndex, int endIndex)
        {
            // No tasks to delete
            if (taskList.Count == 0)
                return new Response(Result.INVALID_TASK, sortType, this.GetType());
            // Invalid index ranges
            else if (endIndex < startIndex)
                return new Response(Result.INVALID_TASK, sortType);
            else if (startIndex < 0 || endIndex > currentListedTasks.Count - 1)
                return new Response(Result.INVALID_TASK, sortType);
            else return null;
        }

        /// <summary>
        /// Generates and returns a standard success Response indicating that the specified task has been
        /// successfully executed on by this Operation.
        /// </summary>
        /// <param name="task">The task to generate the success response for.</param>
        /// <returns></returns>
        protected Response GenerateStandardSuccessResponse(Task task)
        {
            string[] args = new string[1];
            args[0] = task.TaskName;
            return new Response(Result.SUCCESS, sortType, this.GetType(), currentListedTasks, args);
        }

        /// <summary>
        /// Generates and returns a Response indicating that an XML IO failure has occured.
        /// </summary>
        /// <returns></returns>
        protected Response GenerateXMLFailureResponse()
        {
            return new Response(Result.XML_READWRITE_FAIL);
        }

        /// <summary>
        /// Formats and sets the arguments for the feedback string for a search operation.
        /// </summary>
        /// <param name="criteria"></param>
        /// <param name="searchString"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="searchType"></param>
        /// <returns></returns>
        protected void SetArgumentsForSearchFeedbackString(out string[] criteria, string searchString, DateTime? startTime, DateTime? endTime, SearchType searchType)
        {
            criteria = new string[Response.SEARCH_PARAM_NUM];
            criteria[Response.SEARCH_PARAM_DONE] = "";
            criteria[Response.SEARCH_PARAM_SEARCH_STRING] = "";

            if (searchType == SearchType.DONE)
                criteria[Response.SEARCH_PARAM_DONE] = "[DONE] ";
            else if (searchType == SearchType.UNDONE)
                criteria[Response.SEARCH_PARAM_DONE] = "undone ";

            if (searchString != "" && searchString != null)
                criteria[Response.SEARCH_PARAM_SEARCH_STRING] += " matching \"" + searchString + "\"";
            if (startTime != null || endTime != null)
                criteria[Response.SEARCH_PARAM_SEARCH_STRING] += " within";
            if (startTime != null)
            {
                criteria[Response.SEARCH_PARAM_SEARCH_STRING] += " " + startTime.Value.ToString("g");
                if (endTime != null)
                    criteria[Response.SEARCH_PARAM_SEARCH_STRING] += " to";
            }
            if (endTime != null)
                criteria[Response.SEARCH_PARAM_SEARCH_STRING] += " " + endTime.Value.ToString("g");
        }
        #endregion

        // ******************************************************************
        // Validation Methods
        // ******************************************************************

        #region Validation Methods        
        /// <summary>
        /// Checks a string for it's validity. A string is considered valid
        /// if it is not null or empty.
        /// </summary>
        /// <param name="checkString">The string to check.</param>
        /// <returns>A boolean indicating the string's validity. True if valid, false if invalid.</returns>
        protected static bool IsValidString(string checkString)
        {
            return checkString != null && checkString != String.Empty;
        }

        /// <summary>
        /// Checks the two input Nullable DateTimes for whether they have values.
        /// Returns true if at least one of them has a value, and false if both of them are null.
        /// </summary>
        /// <param name="timeOne">First DateTime to check.</param>
        /// <param name="timeTwo">Second DateTime to check.</param>
        /// <returns>Boolean indicating if at least one time is not null.</returns>
        private static bool HasValidTime(DateTime? timeOne, DateTime? timeTwo)
        {
            return timeOne != null || timeTwo != null;
        }
        #endregion

        // ******************************************************************
        // Delegate Invocation and Parameter Generation Methods
        // ******************************************************************

        #region Delegate Invocation and Parameter Generation Methods        
        /// <summary>
        /// Accepts an array of objects and returns the same array of objects with
        /// a Task inserted in the first (zeroth-index) position.
        /// </summary>
        /// <param name="args">The given array of objects.</param>
        /// <param name="task">The task to insert into the first position.</param>
        /// <returns>The output array with the Task inserted.</returns>
        private static object[] AddTaskToParameters(object[] args, Task task)
        {
            if (args == null) return new object[1] { task };

            var parameterList = args.ToList();
            parameterList.Insert(0, task);
            var parameters = parameterList.ToArray();
            return parameters;
        }

        /// <summary>
        /// Invokes the given delegate with the given parameters.
        /// </summary>
        /// <param name="action">Delegate to invoke.</param>
        /// <param name="parameters">Parameters to invoke the delegate with.</param>
        /// <returns>The Response returned by the Delegate after it is invoked.</returns>
        private static Response InvokeAction(Delegate action, object[] parameters)
        {
            Response response;
            try
            {
                response = (Response)action.DynamicInvoke(parameters);
            }
            catch (System.Exception ex)
            {
                response = new Response(Result.EXCEPTION_FAILURE);
                Logger.Error(ex, "InvokeAction::Operation");
            }
            return response;
        }
        #endregion
    }
}
