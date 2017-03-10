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
        /// <returns>任务是否成功开始</returns>
        abstract public bool Begin();

        /// <summary>
        /// 初始化任务处理器
        /// </summary>
        /// <param name="paraDict">参数字典，键是形参，键值是实参对象</param>
        /// <returns>初始化任务是否成功</returns>
        abstract public bool Init(Dictionary<string, object> paraDict);

        /// <summary>
        /// 强制结束任务
        /// </summary>
        /// <returns>是否已经成功强制结束了任务</returns>
        virtual public bool Terminate()
        {
            return this.isAborted = true;
        }

        /// <summary>
        /// 查询任务是否已经完成
        /// </summary>
        /// <returns>任务是否已经完成</returns>
        virtual public bool IsFinished()
        {
            return this.isFinished;
        }

        /// <summary>
        /// 获取任务处理的返回结果
        /// </summary>
        /// <param name="result">[out] 返回结果的包装</param>
        /// <returns>是否成功获取到了要返回的执行结果</returns>
        virtual public bool GetResult(out object result)
        {
            result = null;
            return true;
        }

        /// <summary>
        /// 获取该处理器绑定的状态机ID
        /// </summary>
        /// <returns>状态机的唯一编号</returns>
        virtual public int GetBindingExecutorId()
        {
            return this.bindingExecutorID;
        }

        /// <summary>
        /// 设置该处理器绑定的状态机ID
        /// </summary>
        /// <param name="execId">要绑定的状态机的ID</param>
        virtual public void Binding(int execId)
        {
            this.bindingExecutorID = execId;
        }

        /// <summary>
        /// 获取该处理器是否已经被强制终止
        /// </summary>
        /// <returns>处理器是否停机</returns>
        virtual public bool IsAbort()
        {
            return this.isAborted;
        }

        /// <summary>
        /// 任务是否已经结束的标记
        /// </summary>
        protected bool isFinished = false;

        /// <summary>
        /// 获取或设置绑定的状态机处理器ID
        /// </summary>
        protected int bindingExecutorID = 0;

        /// <summary>
        /// 任务是否已经被强制终止
        /// </summary>
        protected bool isAborted = false;
    }
}
