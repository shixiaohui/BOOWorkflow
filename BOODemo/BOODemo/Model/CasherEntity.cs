using System;
using System.Collections.Generic;
using System.Linq;

namespace BOODemo.Model
{
    /// <summary>
    /// Entity class:cashier
    /// </summary>
    [Serializable]
    internal sealed class CasherEntity
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public CasherEntity(Restaurant rest)
        {
            this.restaurant = rest;
        }

        /// <summary>
        /// Get the vector of pending order 
        /// </summary>
        public List<GuestOrderEntity> WaitForPaymentOrderList
        {
            get
            {
                var wpc = from goe in this.restaurant.GuestOrderList
                          where goe.IsRequestPayment && !goe.IsPaid
                          select goe;
                return wpc.ToList();
            }
        }

        /// <summary>
        /// Pay orders
        /// </summary>
        /// <param name="oe">The quote for the order to be paid</param>
        public void PayFor(GuestOrderEntity oe)
        {
            oe.FinishPayment();
        }

        /// <summary>
        /// Reference of restaurant entities
        /// </summary>
        private Restaurant restaurant;
    }
}
