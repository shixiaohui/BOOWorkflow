using System;
using System.Collections.Generic;
using BOODemo.ViewModel;

namespace BOODemo.TaskWarehouse
{
    /// <summary>
    /// 任务解决器：厨房餐单递送
    /// </summary>
    internal sealed class DeliverTaskHandler : TaskUtils.AbstractTaskHandler
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
                this.kitchenOrderId = (int)paraDict["kitchenOrderId"];
                this.bindingGuestOrderId = (int)paraDict["guestOrderId"];
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
                var ko = RestaurantViewModel.RestaurantEntity.KitchenOrderList.Find((x) => x.Id == this.kitchenOrderId);
                for (int i = 0; i < ko.QTList.Count; i++)
                {
                    if (ko.QTList[i].PassedQT)
                    {
                        ko.PassQTDish(i);
                    }
                    ko.QTList.RemoveAll((x) => x.PassedQT);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 标记已经送达
        /// </summary>
        public void Arrived()
        {
            var ko = RestaurantViewModel.RestaurantEntity.KitchenOrderList.Find((x) => x.Id == this.kitchenOrderId);
            for (int i = 0; i < ko.DeliveringList.Count; i++)
            {
                ko.ArriveDish(i);
            }
            ko.DeliveringList.Clear();
            this.isFinished = true;
        }

        /// <summary>
        /// 厨房餐单id
        /// </summary>
        private int kitchenOrderId = -1;

        /// <summary>
        /// 绑定的客户订单id
        /// </summary>
        private int bindingGuestOrderId = -1;
    }
}
