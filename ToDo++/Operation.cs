using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToDo
{
    // ******************************************************************
    // Abstract definition for Operation
    // ******************************************************************
    #region Abstract definition for Operation
    public abstract class Operation
    {
        #region Feedback Strings
        protected const string RESPONSE_ADD_SUCCESS = "Added \"{0}\" successfully.";
        protected const string RESPONSE_ADD_FAILURE = "Failed to add task!";
        protected const string RESPONSE_DELETE_SUCCESS = "Deleted task \"{0}\" successfully.";
        protected const string RESPONSE_DELETE_FAILURE = "No matching task found!";
        protected const string RESPONSE_DELETE_ALREADY = "Task has already been deleted!";
        protected const string RESPONSE_MODIFY_SUCCESS = "Modified task \"{0}\" into \"{1}\"  successfully.";
        protected const string RESPONSE_DISPLAY_NOTASK = "There are no tasks for display.";
        protected const string RESPONSE_UNDO_SUCCESS = "Removed task successfully.";
        protected const string RESPONSE_UNDO_FAILURE = "Cannot undo last executed task!";
        protected const string RESPONSE_MARKASDONE_SUCCESS = "Successfully marked \"{0}\" as done.";
        protected const string RESPONSE_XML_READWRITE_FAIL = "Failed to read/write from XML file!";
        protected const string REPONSE_INVALID_COMMAND = "Invalid command input!";
        protected const string RESPONSE_INVALID_TASK_INDEX = "Invalid task index!";
        #endregion

        protected bool successFlag;
        public abstract string Execute(List<Task> taskList, Storage storageXML);
        public abstract string Undo(List<Task> taskList, Storage strorageXML);
    }
    #endregion
}
