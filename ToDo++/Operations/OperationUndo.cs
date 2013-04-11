//@qianpan A0103985Y
using System;
using System.Collections.Generic;

namespace ToDo
{
    class OperationUndo : Operation
    {
        // ******************************************************************
        // Constructors
        // ******************************************************************

        #region Constructors
        /// <summary>
        /// Derived constructor to create an Undo Operation.
        /// </summary>
        /// <param name="sortType">The type of sorting to use on the displayed task after executing this Operation.</param>
        public OperationUndo(SortType sortType)
            : base(sortType)
        { }
        #endregion

        // ******************************************************************
        // Override for Executing this operation
        // ******************************************************************

        #region ExecuteOperation
        /// <summary>
        /// Executes the operation and adds it to the Undone operations history.
        /// </summary>
        /// <param name="taskList">List of task this method will operate on.</param>
        /// <param name="storageIO">Storage controller that will be used to store neccessary data.</param>
        /// <returns>Response indicating the result of the operation execution.</returns>
        public override Response Execute(List<Task> taskList, Storage storageIO)
        {
            SetMembers(taskList, storageIO);

            Operation undoOp = GetLastOperation();
            if (undoOp == null)
                return new Response(Result.FAILURE, sortType, this.GetType());

            Response result = undoOp.Undo(taskList, storageIO);
            if (result == null)
                return result;

            if (result.IsSuccessful())
            {
                redoStack.Push(undoOp);
                result = new Response(Result.SUCCESS, sortType, typeof(OperationUndo), currentListedTasks);
            }
            else
                result = new Response(Result.FAILURE, sortType, typeof(OperationUndo), currentListedTasks);

            return result;
        }

        /// <summary>
        /// Gets the last executed operation from history.
        /// </summary>
        /// <returns></returns>
        private Operation GetLastOperation()
        {            
            if (undoStack.Count == 0)
                return null;
            try
            {
                Operation lastOperation = Operation.undoStack.Pop();
                return lastOperation;
            }
            catch (Exception e)
            {
                Logger.Error(e, "GetLastOperation::OperationUndo");
                return null;
            }
        }
        #endregion
    }
}
    