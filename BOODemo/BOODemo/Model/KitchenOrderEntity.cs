using System;
using System.Collections.Generic;

namespace BOODemo.Model
{
    /// <summary>
    /// Entity class: kitchen order
    /// </summary>
    [Serializable]
    internal sealed class KitchenOrderEntity
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public KitchenOrderEntity(int bindingGuestOrderId)
        {
            lock (GlobalDataContext.KitchenOrderCounterMutex)
            {
                this.Id = GlobalDataContext.KitchenOrderIdCounter++;
            }
            this.GuestOrderId = bindingGuestOrderId;
            this.CreateTimeStamp = DateTime.Now;
            this.FinishedTimeStamp = null;
            this.PendingList = new List<DishEntity>();
            this.QTList = new List<DishEntity>();
            this.DeliveringList = new List<DishEntity>();
            this.ArrivedList = new List<DishEntity>();
        }

        /// <summary>
        /// Produced a dish and wait for testing quality
        /// </summary>
        /// <param name="pendingIdx">The location of the dish in the vector to be processed</param>
        public void ProducedDish(int pendingIdx)
        {
            var rObj = this.PendingList[pendingIdx];
            this.QTList.Add(rObj);
        }

        /// <summary>
        /// Return the dish to the pending queue if it didn't passed the test quality
        /// </summary>
        /// <param name="qtIdx">The location of the dish in the vector to be test</param>
        public void NotpassQTDish(int qtIdx)
        {
            var rObj = this.QTList[qtIdx];
            this.PendingList.Add(rObj);
        }

        /// <summary>
        /// Put the dish to the delivery queue if it passed the test quality
        /// </summary>
        /// <param name="qtIdx">The location of the dish in the vector to be test</param>
        public void PassQTDish(int qtIdx)
        {
            var rObj = this.QTList[qtIdx];
            this.DeliveringList.Add(rObj);
        }

        /// <summary>
        /// A dish has been delivered
        /// </summary>
        /// <param name="dIdx">The location of the dish in the vector to be reached</param>
        public void ArriveDish(int dIdx)
        {
            var rObj = this.DeliveringList[dIdx];
            this.ArrivedList.Add(rObj);
        }

        /// <summary>
        /// Complete this kitchen order
        /// </summary>
        public void Finish()
        {
            this.FinishedTimeStamp = DateTime.Now;
        }

        /// <summary>
        /// Get wether the kitchen order has been completed or not
        /// </summary>
        public bool IsFinish
        {
            get
            {
                return this.FinishedTimeStamp != null;
            }
        }

        /// <summary>
        /// Get the unique id of the kitchen order
        /// </summary>
        public int Id
        {
            get;
            private set;
        }

        /// <summary>
        /// Get the customer order ID bound to the kitchen order
        /// </summary>
        public int GuestOrderId
        {
            get;
            private set;
        }

        /// <summary>
        /// Get the timestamp when the kitchen order been created
        /// </summary>
        public DateTime CreateTimeStamp
        {
            get;
            private set;
        }

        /// <summary>
        /// Get the timestamp when the kitchen order been finished
        /// </summary>
        public DateTime? FinishedTimeStamp
        {
            get;
            private set;
        }

        /// <summary>
        /// Get the pending vector
        /// </summary>
        public List<DishEntity> PendingList
        {
            get;
            private set;
        }

        /// <summary>
        /// Get the vector that is pending to test quality
        /// </summary>
        public List<DishEntity> QTList
        {
            get;
            private set;
        }

        /// <summary>
        /// Get the vector on deliverying
        /// </summary>
        public List<DishEntity> DeliveringList
        {
            get;
            private set;
        }

        /// <summary>
        /// Get the delivery success vector
        /// </summary>
        public List<DishEntity> ArrivedList
        {
            get;
            private set;
        }
    }
}
