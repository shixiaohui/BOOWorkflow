using System;
using System.Collections.Generic;

namespace BOODemo.TaskWarehouse
{
    /// <summary>
    /// Task Solver: Update delivered time
    /// </summary>
    internal sealed class UpdateDeliTimeTaskHandler : TaskUtils.AbstractTaskHandler  
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
            lock (GlobalDataContext.ConsolePrintMutex)
            {
                Console.WriteLine(String.Format("Guest Order ID: {0}, all dishes are deliverd at {1}.", this.guestOrderId, DateTime.Now));
            }
            return this.isFinished = true;
        }

        /// <summary>
        /// Guest Order Id
        /// </summary>
        private int guestOrderId;
    }
}
