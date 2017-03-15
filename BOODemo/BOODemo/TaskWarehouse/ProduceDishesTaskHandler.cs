using System;
using System.Collections.Generic;
using BOODemo.ViewModel;

namespace BOODemo.TaskWarehouse
{
    /// <summary>
    /// 任务解决器：厨房餐单生产餐品
    /// </summary>
    internal sealed class ProduceDishesTaskHandler : TaskUtils.AbstractTaskHandler 
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
                this.KitchenOrderId = (int)paraDict["kitchenOrderId"];
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
                var gOrderList = RestaurantViewModel.RestaurantEntity.GuestOrderList.Find((x) => x.OrderId == this.bindingGuestOrderId).GetOrderedList();
                var gOrderListUnhandle = gOrderList.FindAll((x) => x.KitchenOrderId == -1);
                gOrderListUnhandle.ForEach((x) => x.KitchenOrderId = this.KitchenOrderId);
                var kOrder = RestaurantViewModel.RestaurantEntity.KitchenOrderList.Find((x) => x.Id == this.KitchenOrderId);
                var gOrderListExceptInKO = gOrderListUnhandle.FindAll(
                    (x) => kOrder.QTList.Contains(x) == false &&
                    kOrder.DeliveringList.Contains(x) == false &&
                    kOrder.ArrivedList.Contains(x) == false &&
                    kOrder.PendingList.Contains(x) == false);
                for (int i = 0; i < gOrderListExceptInKO.Count; i++)
                {
                    kOrder.PendingList.Add(gOrderListExceptInKO[i]);
                }
                RestaurantViewModel.KitchenFormReference.RefreshKitchenOrder();
                return true;
            }
            catch (Exception e)
            {
                lock (GlobalDataContext.ConsolePrintMutex)
                {
                    Console.WriteLine(e.ToString());
                }
                return false;
            }
        }

        /// <summary>
        /// 设置生产菜品完毕
        /// </summary>
        public void Produced()
        {
            this.isFinished = true;
        }
        
        /// <summary>
        /// 厨房餐单id
        /// </summary>
        public int KitchenOrderId = -1;

        /// <summary>
        /// 绑定的客户订单id
        /// </summary>
        private int bindingGuestOrderId = -1;
    }
}
