using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BOODemo.Model
{
    /// <summary>
    /// Entity Class: Guest Order
    /// </summary>
    [Serializable]
    internal sealed class GuestOrderEntity
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">Order id</param>
        public GuestOrderEntity(int id)
        {
            this.OrderId = id;
            this.IsRequestPayment = false;
            this.CreateTimeStamp = DateTime.Now;
            this.PaidTimeStamp = null;
            this.orderedList = new List<DishEntity>();
        }

        /// <summary>
        /// Add a dish for the order
        /// </summary>
        /// <param name="dish">Dish entity</param>
        public void AddDish(DishEntity dish)
        {
            this.orderedList.Add(dish);
        }

        /// <summary>
        /// Remove a dish for the order
        /// </summary>
        /// <param name="index">The index on the order vector of the dish that will be remove</param>
        public void RemoveDish(int index)
        {
            this.orderedList.RemoveAt(index);
        }

        /// <summary>
        /// Get all the already ordered items in the guest order
        /// </summary>
        /// <returns>The vector of dish that was odered</returns>
        public List<DishEntity> GetOrderedList()
        {
            return this.orderedList;
        }

        /// <summary>
        /// The order has been paid
        /// </summary>
        public void FinishPayment()
        {
            this.PaidTimeStamp = DateTime.Now;
        }

        /// <summary>
        /// Convert an order into a description text
        /// </summary>
        /// <returns>Order details</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(String.Format("ID: {0}", this.OrderId));
            sb.AppendLine(String.Format("Order Time: {0}", this.CreateTimeStamp));
            sb.AppendLine("----------");
            foreach (var item in this.orderedList)
            {
                sb.AppendLine(String.Format(" {0} : ${1}", item.Name, item.Price));
            }
            return sb.ToString();
        }

        /// <summary>
        /// Get the unique id of the order
        /// </summary>
        public int OrderId
        {
            get;
            private set;
        }

        /// <summary>
        /// Get the timestamp when the order was created
        /// </summary>
        public DateTime CreateTimeStamp
        {
            get;
            private set;
        }

        /// <summary>
        /// Get the timestamp when the order was paid
        /// </summary>
        public DateTime? PaidTimeStamp
        {
            get;
            private set;
        }

        /// <summary>
        /// Get or set whether the order request to pay or not
        /// </summary>
        public bool IsRequestPayment
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the total price of the order
        /// </summary>
        public double TotalPrice
        {
            get
            {
                return this.orderedList.Aggregate(0.0, (acct, t) => acct + t.Price);
            }
        }

        /// <summary>
        /// Get whether the order has been paid or not
        /// </summary>
        public bool IsPaid
        {
            get
            {
                return this.PaidTimeStamp != null;
            }
        }
        
        /// <summary>
        /// The dish that has been ordered by guest
        /// </summary>
        /// <remarks>
        /// If the same dish has been ordered more than one time, 
        /// that was equal that the guest orders one dish many times
        /// </remarks>
        private List<DishEntity> orderedList;
    }
}
