using System.Collections.Generic;
using BOODemo.View;

namespace BOODemo.TaskWarehouse
{
    /// <summary>
    /// 任务解决器：添加客户订单
    /// </summary>
    internal sealed class AddItemTaskHandler : TaskUtils.AbstractTaskHandler
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
            try
            {
                OrderingForm of = new OrderingForm();
                of.BindingGuestOrderId = this.bindingExecutorID;
                of.Show();
                return this.isFinished = true;
            }
            catch
            {
                return false;
            }
        }
        
        /// <summary>
        /// 客户订单号
        /// </summary>
        private int guestOrderId;
    }
}
