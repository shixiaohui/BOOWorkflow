using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BOODemo.TaskWarehouse
{
    internal sealed class AddItemTaskHandler : TaskUtils.AbstractTaskHandler
    {
        /// <summary>
        /// 开始处理任务
        /// </summary>
        /// <returns>任务是否成功结束</returns>
        public override bool Begin()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取任务处理的返回结果
        /// </summary>
        /// <param name="result">[out] 返回结果的包装</param>
        /// <returns>是否成功获取到了要返回的执行结果</returns>
        public override bool GetResult(out object result)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 初始化任务处理器
        /// </summary>
        /// <param name="paraDict">参数字典，键是形参，键值是实参对象</param>
        /// <returns>初始化任务是否成功</returns>
        public override bool Init(Dictionary<string, object> paraDict)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 查询任务是否已经完成
        /// </summary>
        /// <returns>任务是否已经完成</returns>
        public override bool IsFinished()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 强制结束任务
        /// </summary>
        /// <returns>是否已经成功强制结束了任务</returns>
        public override bool Terminate()
        {
            throw new NotImplementedException();
        }
    }
}
