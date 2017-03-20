using System;
using System.Collections.Generic;
using BOODemo.View;

namespace BOODemo.TaskWarehouse
{
    /// <summary>
    /// Task Solver: Add guest orders
    /// </summary>
    internal sealed class AddItemTaskHandler : TaskUtils.AbstractTaskHandler
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
                this.GuestOrderId = (int)paraDict["guestOrderId"];
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
        /// Finish the order
        /// </summary>
        public void Submit()
        {
            this.isFinished = true;
        }

        /// <summary>
        /// Guest Order ID
        /// </summary>
        public int GuestOrderId;
    }
}
