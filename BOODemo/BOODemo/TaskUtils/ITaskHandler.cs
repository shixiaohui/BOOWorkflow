using System.Collections.Generic;

namespace BOODemo.TaskUtils
{
    /// <summary>
    /// 任务处理器接口类：为任务处理器提供公共的方法接口
    /// 所有的任务处理器的实现都要实现该接口
    /// </summary>
    internal interface ITaskHandler
    {
        /// <summary>
        /// 初始化任务处理器
        /// </summary>
        /// <param name="paraDict">参数字典，键是形参，键值是实参对象</param>
        /// <returns>初始化任务是否成功</returns>
        bool Init(Dictionary<string, object> paraDict);

        /// <summary>
        /// 开始处理任务
        /// </summary>
        /// <returns>任务是否启动成功</returns>
        bool Begin();

        /// <summary>
        /// 强制结束任务
        /// </summary>
        /// <returns>是否已经成功强制结束了任务</returns>
        bool Terminate();

        /// <summary>
        /// 查询任务是否已经完成
        /// </summary>
        /// <returns>任务是否已经完成</returns>
        bool IsFinished();

        /// <summary>
        /// 获取任务处理的返回结果
        /// </summary>
        /// <param name="result">[out] 返回结果的包装</param>
        /// <returns>是否成功获取到了要返回的执行结果</returns>
        bool GetResult(out object result);

        /// <summary>
        /// 获取该处理器绑定的状态机ID
        /// </summary>
        /// <returns>状态机的唯一编号</returns>
        string GetBindingExecutorId();

        /// <summary>
        /// 获取该处理器是否已经被强制终止
        /// </summary>
        /// <returns>处理器是否停机</returns>
        bool IsAbort();
    }
}
