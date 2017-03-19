using System.Collections.Generic;
using BOODemo.Model;
using BOODemo.ViewModel;

namespace BOODemo.TaskWarehouse
{
    internal sealed class PaymentTaskHandler : TaskUtils.AbstractTaskHandler 
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
                var orderId = (int)paraDict["guestOrderId"];
                this.GuestOrder = RestaurantViewModel.RestaurantEntity.GuestOrderList.Find((x) => x.OrderId == orderId);
                if (this.GuestOrder == null)
                {
                    return false;
                }
                RestaurantViewModel.GuestCheckFormReference.RefreshCheckOrderList();
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
            return true;
        }

        /// <summary>
        /// Payment completed
        /// </summary>
        /// <returns>Whether the operation is successful</returns>
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
        /// Guest Order
        /// </summary>
        public GuestOrderEntity GuestOrder;
    }
}
