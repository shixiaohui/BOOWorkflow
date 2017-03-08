using System;
using System.Collections.Generic;

namespace BOODemo.TaskUtils
{
    /// <summary>
    /// 抽象任务解决器类：为任务解决器提供公共字段包装
    /// </summary>
    internal abstract class AbstractTaskHandler : ITaskHandler
    {
        /// <summary>
        /// 开始处理任务
        /// </summary>
        /// <returns>任务是否成功结束</returns>
        abstract public bool Begin();

        /// <summary>
        /// 获取任务处理的返回结果
        /// </summary>
        /// <param name="result">[out] 返回结果的包装</param>
        /// <returns>是否成功获取到了要返回的执行结果</returns>
        abstract public bool GetResult(out object result);

        /// <summary>
        /// 初始化任务处理器
        /// </summary>
        /// <param name="paraDict">参数字典，键是形参，键值是实参对象</param>
        /// <returns>初始化任务是否成功</returns>
        abstract public bool Init(Dictionary<string, object> paraDict);

        /// <summary>
        /// 查询任务是否已经完成
        /// </summary>
        /// <returns>任务是否已经完成</returns>
        abstract public bool IsFinished();

        /// <summary>
        /// 强制结束任务
        /// </summary>
        /// <returns>是否已经成功强制结束了任务</returns>
        abstract public bool Terminate();

        /// <summary>
        /// 任务是否已经结束的标记
        /// </summary>
        protected bool isFinished = false;
    }
}
