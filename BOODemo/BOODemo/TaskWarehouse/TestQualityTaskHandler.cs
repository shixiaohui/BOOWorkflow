using System;
using System.Collections.Generic;
using BOODemo.ViewModel;

namespace BOODemo.TaskWarehouse
{
    /// <summary>
    /// Task Solver:dish test quality
    /// </summary>
    internal sealed class TestQualityTaskHandler : TaskUtils.AbstractTaskHandler  
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
            var kOrder = RestaurantViewModel.RestaurantEntity.KitchenOrderList.Find((x) => x.Id == this.KitchenOrderId);
            for (int i = 0; i < kOrder.PendingList.Count; i++)
            {
                kOrder.ProducedDish(i);
            }
            kOrder.PendingList.Clear();
            return true;
        }

        /// <summary>
        /// Gets the return result of the task
        /// </summary>
        /// <param name="result">[out] Package of the result</param>
        /// <returns>Whether to successfully get to the implementation of the results to be returned</returns>
        public override bool GetResult(out object result)
        {
            if (this.isPass == null)
            {
                result = null;
                return false;
            }
            var resPack = new TestQualityTaskResultPackage();
            resPack.passed = (bool)this.isPass ? "1" : "0";
            result = resPack;
            return true;
        }

        /// <summary>
        /// Set whether the test quality is passed
        /// </summary>
        public bool IsQualityTestPass
        {
            get
            {
                if (this.isPass == null)
                {
                    throw new FieldAccessException();
                }
                return (bool)this.isPass;
            }
            set
            {
                this.isPass = value;
                var ko = RestaurantViewModel.RestaurantEntity.KitchenOrderList.Find((x) => x.Id == this.KitchenOrderId);
                if ((bool)this.isPass)
                {
                    for (int i = 0; i < ko.QTList.Count; i++)
                    {
                        ko.QTList[i].PassedQT = true;
                    }
                }
                else
                {
                    for (int i = 0; i < ko.QTList.Count; i++)
                    {
                        ko.NotpassQTDish(i);
                    }
                    ko.QTList.Clear();
                }
                this.isFinished = true;
            }
        }

        /// <summary>
        /// Kitchen Order Id
        /// </summary>
        public int KitchenOrderId = -1;

        /// <summary>
        /// Binding Guest Order Id
        /// </summary>
        private int bindingGuestOrderId = -1;

        /// <summary>
        /// Whether the test quality passed
        /// </summary>
        private bool? isPass = null;

    }

    /// <summary>
    /// Test quality results package
    /// </summary>
    public class TestQualityTaskResultPackage
    {
        /// <summary>
        /// The mark about whether the test quality passes, 0 for failure, 1 for success
        /// </summary>
        public string passed;
    }
}
