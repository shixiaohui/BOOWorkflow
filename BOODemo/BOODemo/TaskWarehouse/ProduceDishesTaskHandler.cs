using System;
using System.Collections.Generic;
using BOODemo.ViewModel;

namespace BOODemo.TaskWarehouse
{
    /// <summary>
    /// Task Finder: The kitchen produces meals according to the order
    /// </summary>
    internal sealed class ProduceDishesTaskHandler : TaskUtils.AbstractTaskHandler 
    {
        /// <summary>
        /// Initialize the task processor
        /// </summary>
        /// <param name="paraDict">Parameter dictionary, key is a parameter, value is a real object</param>
        /// <returns>Whether the task initialized successfully</returns>
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
        /// Start processing tasks
        /// </summary>
        /// <returns>Whether the task started successfully</returns>
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
        /// Set the production dishes finished
        /// </summary>
        public void Produced()
        {
            this.isFinished = true;
        }

        /// <summary>
        /// Kitchen Order Id
        /// </summary>
        public int KitchenOrderId = -1;

        /// <summary>
        /// Binding Guest Order Id
        /// </summary>
        private int bindingGuestOrderId = -1;
    }
}
