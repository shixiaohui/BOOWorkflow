using System.Collections.Generic;
using BOODemo.ViewModel;

namespace BOODemo.TaskWarehouse
{
    /// <summary>
    /// Task solver: kitchen dish delivery
    /// </summary>
    internal sealed class DeliverTaskHandler : TaskUtils.AbstractTaskHandler
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
                var ko = RestaurantViewModel.RestaurantEntity.KitchenOrderList.Find((x) => x.Id == this.KitchenOrderId);
                for (int i = 0; i < ko.QTList.Count; i++)
                {
                    if (ko.QTList[i].PassedQT)
                    {
                        ko.PassQTDish(i);
                    }
                }
                ko.QTList.RemoveAll((x) => x.PassedQT);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// The tag has been delivered
        /// </summary>
        public void Arrived()
        {
            var ko = RestaurantViewModel.RestaurantEntity.KitchenOrderList.Find((x) => x.Id == this.KitchenOrderId);
            for (int i = 0; i < ko.DeliveringList.Count; i++)
            {
                ko.ArriveDish(i);
            }
            ko.DeliveringList.Clear();
            ko.Finish();
            RestaurantViewModel.KitchenFormReference.RefreshKitchenOrder();
            this.isFinished = true;
        }

        /// <summary>
        /// Kitchen Order Id
        /// </summary>
        public int KitchenOrderId = -1;

        /// <summary>
        /// Binding Guest OrderId
        /// </summary>
        private int bindingGuestOrderId = -1;
    }
}
