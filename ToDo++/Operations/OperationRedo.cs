//@qianpan A0103985Y
using System;
using System.Collections.Generic;

namespace ToDo
{
    class OperationRedo : Operation
    {        
        // ******************************************************************
        // Constructors
        // ******************************************************************

        #region Constructors
        /// <summary>
        /// Derived constructor to create a Redo Operation.
        /// </summary>
        /// <param name="sortType">The type of sorting to use on the displayed task after executing this Operation.</param>
        /// <returns>Nothing.</returns>
        public OperationRedo(SortType sortType)
            : base(sortType)
        { }
        #endregion

        // ******************************************************************
        // Override for Executing this operation
        // ******************************************************************

        #region ExecuteOperation
        /// <summary>
        /// Executes the operation and adds it to the global operation history.
        /// </summary>
        /// <param name="taskList">List of task this method will operate on.</param>
        /// <param name="storageIO">Storage controller that will be used to store neccessary data.</param>
        /// <returns>Response indicating the result of the operation execution.</returns>
        public override Response Execute(List<Task> taskList, Storage storageIO)
        {
            SetMembers(taskList, storageIO);

            Operation redoOp = GetLastRevertedOperation();
            if (redoOp == null)
                return new Response(Result.FAILURE, sortType, this.GetType());
            
            Response result = redoOp.Redo(taskList, storageIO);
            if (result == null)
                return result;

            if (result.IsSuccessful())
            {
                undoStack.Push(redoOp);
                result = new Response(Result.SUCCESS, sortType, typeof(OperationRedo), currentListedTasks);
            }
            else
                result = new Response(Result.FAILURE, sortType, typeof(OperationRedo), currentListedTasks);

            return result;
        }

        
        /// <summary>
        /// Gets the last undone operation from history.
        /// </summary>
        /// <returns></returns>
        private Operation GetLastRevertedOperation()
        {
            if (redoStack.Count == 0)
                return null;
            try
            {
                Operation lastRevertedOperation = Operation.redoStack.Pop();
                return lastRevertedOperation;
            }
            catch (Exception e)
            {
                Logger.Error(e, "GetLastRevertedOperation::OperationRedo");
                return null;
            }
        }
        #endregion
    }
}
