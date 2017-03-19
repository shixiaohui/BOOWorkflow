using System.Collections.Generic;
using BOODemo.ViewModel;

namespace BOODemo.TaskWarehouse
{
    internal sealed class CalculateTaskHandler : TaskUtils.AbstractTaskHandler
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
                this.guestOrderId = (int)paraDict["guestOrderId"];
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
        /// Guest Order ID
        /// </summary>
        private int guestOrderId;
    }
}
