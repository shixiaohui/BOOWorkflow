using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

namespace BOODemo.TaskUtils
{
    /// <summary>
    /// 任务工厂类：为应用程序提供产生任务相应的处理器的方法
    /// </summary>
    internal static class TaskFactory
    {
        /// <summary>
        /// 通过任务名字获取任务处理器对象
        /// </summary>
        /// <param name="taskName">任务的名字</param>
        /// <returns>该任务名字所对应的处理器对象</returns>
        public static ITaskHandler GetTaskHandlerByTaskName(string taskName)
        {
            var findHandlerName = String.Format("{0}Handler", taskName);
            var findType = TaskFactory.TypeVector.Find((x) => String.Compare(x.Name, findHandlerName, true) == 0);
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
        /// 静态构造函数
        /// </summary>
        static TaskFactory()
        {
            var TypeArr = Assembly.GetExecutingAssembly().GetTypes();
            var HandlerTypeArr = from h in TypeArr
                                 where h.FullName.StartsWith("BOODemo.TaskWarehouse.", StringComparison.CurrentCultureIgnoreCase)
                                 select h;
            TaskFactory.TypeVector = new List<Type>(HandlerTypeArr);
        }

        /// <summary>
        /// 任务处理器类型向量
        /// </summary>
        private static List<Type> TypeVector;

        /// <summary>
        /// 任务处理器不存在错误
        /// </summary>
        private class TaskHandlerNotExistException : Exception
        {
            /// <summary>
            /// 构造函数
            /// </summary>
            /// <param name="taskName">被调用任务名</param>
            public TaskHandlerNotExistException(string taskName)
                : base(String.Format("BOOWorkflow called task: [{0}], but its handler does not exist.", taskName))
            { }
        }
    }
}
