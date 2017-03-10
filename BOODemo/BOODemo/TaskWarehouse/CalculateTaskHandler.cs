using System;
using System.Collections.Generic;
using BOODemo.ViewModel;

namespace BOODemo.TaskWarehouse
{
    internal sealed class CalculateTaskHandler : TaskUtils.AbstractTaskHandler
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
                var gOrder = RestaurantViewModel.RestaurantEntity.GuestOrderList.Find((x) => x.OrderId == this.guestOrderId);
                gOrder.IsRequestPayment = true;
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
