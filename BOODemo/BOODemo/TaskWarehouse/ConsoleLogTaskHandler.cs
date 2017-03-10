using System;
using System.Collections.Generic;

namespace BOODemo.TaskWarehouse
{
    /// <summary>
    /// 控制台日志输出任务处理器
    /// </summary>
    internal sealed class ConsoleLogTaskHandler : TaskUtils.AbstractTaskHandler
    {
        /// <summary>
        /// 初始化任务处理器
        /// </summary>
        /// <param name="paraDict">参数字典，键是形参，键值是实参对象</param>
        /// <returns>初始化任务是否成功</returns>
        public override bool Init(Dictionary<string, object> paraDict)
        {
            try
            {
                this.label = paraDict["label"] as string;
                this.message = paraDict["message"] as string;
            }
            catch
            {
                this.label = paraDict.ContainsKey("label") ? paraDict["label"] as string : "null";
                this.message = paraDict.ContainsKey("message") ? paraDict["message"] as string : "null";
            }
            return true;
        }

        /// <summary>
        /// 开始处理任务
        /// </summary>
        /// <returns>任务是否成功开始</returns>
        public override bool Begin()
        {
            lock (GlobalDataContext.ConsolePrintMutex)
            {
                Console.WriteLine(String.Format("[APPLOG {0}] {1} : {2}", DateTime.Now, this.label, this.message));
            }
            return this.isFinished = true;
        }

        /// <summary>
        /// 强制结束任务
        /// </summary>
        /// <returns>是否已经成功强制结束了任务</returns>
        public override bool Terminate()
        {
            return false;
        }

        /// <summary>
        /// 查询任务是否已经完成
        /// </summary>
        /// <returns>任务是否已经完成</returns>
        public override bool IsFinished()
        {
            return this.isFinished;
        }

        /// <summary>
        /// 获取任务处理的返回结果
        /// </summary>
        /// <param name="result">[out] 返回结果的包装</param>
        /// <returns>是否成功获取到了要返回的执行结果</returns>
        public override bool GetResult(out object result)
        {
            result = "1";
            return true;
        }

        /// <summary>
        /// 获取或设置记录的标签
        /// </summary>
        private string label { get; set; }

        /// <summary>
        /// 获取或设置记录的内容
        /// </summary>
        private string message { get; set; }
    }
}
