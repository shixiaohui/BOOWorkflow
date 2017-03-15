using System;
using System.Collections.Generic;

namespace BOODemo.TaskWarehouse
{
    /// <summary>
    /// 任务解决器：归档
    /// </summary>
    internal sealed class ArchiveTaskHandler : TaskUtils.AbstractTaskHandler
    {
        /// <summary>
        /// 开始处理任务
        /// </summary>
        /// <returns>任务是否成功开始</returns>
        public override bool Begin()
        {
            // simulate archived, in producing environment this handler always saves data to DB
            lock (GlobalDataContext.ConsolePrintMutex)
            {
                Console.WriteLine(String.Format("[{0}] Archive task called. Archive successfully.", DateTime.Now));
            }
            return this.isFinished = true;
        }
        
        /// <summary>
        /// 初始化任务处理器
        /// </summary>
        /// <param name="paraDict">参数字典，键是形参，键值是实参对象</param>
        /// <returns>初始化任务是否成功</returns>
        public override bool Init(Dictionary<string, object> paraDict)
        {
            return true;
        }
    }
}
