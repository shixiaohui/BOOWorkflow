using System.Collections.Generic;
using BOODemo.Model;
using BOODemo.ViewModel;

namespace BOODemo.TaskWarehouse
{
    internal sealed class PaymentTaskHandler : TaskUtils.AbstractTaskHandler 
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
                var orderId = (int)paraDict["guestOrderId"];
                this.GuestOrder = RestaurantViewModel.RestaurantEntity.GuestOrderList.Find((x) => x.OrderId == orderId);
                if (this.GuestOrder == null)
                {
                    return false;
                }
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
            return true;
        }

        /// <summary>
        /// 完成付款
        /// </summary>
        /// <returns>操作是否成功</returns>
        public bool MadePayment()
        {
            try
            {
                this.GuestOrder.FinishPayment();
                return this.isFinished = true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 客户订单
        /// </summary>
        public GuestOrderEntity GuestOrder;
    }
}
