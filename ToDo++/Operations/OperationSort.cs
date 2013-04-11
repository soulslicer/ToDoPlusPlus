//@qianpan A0103985Y
using System.Collections.Generic;

namespace ToDo
{
    public class OperationSort : Operation
    {
        /// <summary>
        /// Constructor for sort operation.
        /// </summary>
        /// <param name="sortType">The SortType to sort the currently displayed list by.</param>
        /// <returns>Nothing.</returns>
        public OperationSort(SortType sortType)
            :base (sortType)
        {
            this.sortType = sortType;
        }

        /// <summary>
        /// Executes this operation. Returns the currently displayed list back as a Response
        /// with the new sort type.
        /// </summary>
        /// <param name="taskList">The task list which derived operations may operate on.</param>
        /// <param name="storageIO">The storage controller to use to store task data.</param>
        /// <returns>Response with the currently displayed list and the new sort type.</returns>
        public override Response Execute(List<Task> taskList, Storage storageIO)
        {
            this.storageIO = storageIO;
            Response response;
            
            // sorting is done On-The-Fly in TaskListViewControl.
            if(sortType == SortType.DEFAULT)
                response = new Response(Result.FAILURE, sortType, this.GetType(), currentListedTasks);
            else
                response = new Response(Result.SUCCESS, sortType, this.GetType(), currentListedTasks);
            return response;
        }
    }
}
