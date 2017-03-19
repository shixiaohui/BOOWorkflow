using System;
using System.Collections.Generic;

namespace BOODemo.TaskWarehouse
{
    /// <summary>
    /// Task Solver: [Test] console log output task processor
    /// </summary>
    internal sealed class ConsoleLogTaskHandler : TaskUtils.AbstractTaskHandler
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
                this.label = paraDict["label"] as string;
                this.message = paraDict["message"] as string;
            }
            catch
            {
                this.label = paraDict.ContainsKey("label") ? paraDict["label"] as string : "null";
                this.message = paraDict.ContainsKey("message") ? paraDict["message"] as string : "null";
            }
            return true;
        }

        /// <summary>
        /// Start processing tasks
        /// </summary>
        /// <returns>Whether the task started successfully</returns>
        public override bool Begin()
        {
            lock (GlobalDataContext.ConsolePrintMutex)
            {
                Console.WriteLine(String.Format("[APPLOG {0}] {1} : {2}", DateTime.Now, this.label, this.message));
            }
            return this.isFinished = true;
        }

        /// <summary>
        /// Forced to end the task
        /// </summary>
        /// <returns>Whether the task has been successfully forced to end</returns>
        public override bool Terminate()
        {
            return false;
        }

        /// <summary>
        /// Query whether the task has been completed
        /// </summary>
        /// <returns>Whether the task finished successfully</returns>
        public override bool IsFinished()
        {
            return this.isFinished;
        }

        /// <summary>
        /// Gets the return result of the task
        /// </summary>
        /// <param name="result">[out] Package of the result</param>
        /// <returns>Whether to successfully get to the implementation of the results to be returned</returns>
        public override bool GetResult(out object result)
        {
            result = "1";
            return true;
        }

        /// <summary>
        /// Gets or sets the label of the record
        /// </summary>
        private string label { get; set; }

        /// <summary>
        /// Gets or sets the contents of the record
        /// </summary>
        private string message { get; set; }
    }
}
