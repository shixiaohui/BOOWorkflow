using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

namespace BOODemo.TaskUtils
{
    /// <summary>
    /// Task Factory Class: Provides a way for the application to generate the corresponding processor for the task
    /// </summary>
    internal static class TaskFactory
    {
        /// <summary>
        /// Get the task object through the task name
        /// </summary>
        /// <param name="taskName">task name</param>
        /// <returns>The processor object that task name corresponds to</returns>
        public static ITaskHandler GetTaskHandlerByTaskName(string taskName)
        {
            var findHandlerName = String.Format("{0}Handler", taskName);
            var findType = TaskFactory.TypeVector.Find((x) => String.Compare(x.Name, findHandlerName, true) == 0);
            // Creation a task processor by reflection
            if (findType != null)
            {
                return (ITaskHandler)Activator.CreateInstance(findType);
            }
            else
            {
                throw new TaskHandlerNotExistException(taskName);
            }
        }

        /// <summary>
        /// Static constructor
        /// </summary>
        static TaskFactory()
        {
            var TypeArr = Assembly.GetExecutingAssembly().GetTypes();
            var HandlerTypeArr = from h in TypeArr
                                 where h.FullName.StartsWith(GlobalDataContext.TaskHandlerWarehouseNamespace, StringComparison.CurrentCultureIgnoreCase)
                                 select h;
            TaskFactory.TypeVector = new List<Type>(HandlerTypeArr);
        }

        /// <summary>
        /// Task Processor Type Vector
        /// </summary>
        private static List<Type> TypeVector;

        /// <summary>
        /// The task processor does not have exception
        /// </summary>
        private class TaskHandlerNotExistException : Exception
        {
            /// <summary>
            /// Constructor
            /// </summary>
            /// <param name="taskName">Task name be called</param>
            public TaskHandlerNotExistException(string taskName)
                : base(String.Format("BOOWorkflow called task: [{0}], but its handler does not exist.", taskName))
            { }
        }
    }
}
