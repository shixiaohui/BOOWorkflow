using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BOODemo.TaskWarehouse
{
    internal sealed class UpdateDeliTimeTaskHandler : TaskUtils.AbstractTaskHandler  
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
                this.guestOrderId = (int)paraDict["guestOrderId"];
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 开始处理任务
        /// </summary>
        /// <returns>任务是否成功开始</returns>
        public override bool Begin()
        {
            lock (GlobalDataContext.ConsolePrintMutex)
            {
                Console.WriteLine(String.Format("Guest Order ID: {0}, all dishes are deliverd at {1}.", this.guestOrderId, DateTime.Now));
            }
            return this.isFinished = true;
        }

        /// <summary>
        /// 客户订单号
        /// </summary>
        private int guestOrderId;
    }
}
