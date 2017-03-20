using System;
using System.Collections.Generic;

namespace BOODemo.TaskWarehouse
{
    /// <summary>
    /// Task Solver: Archiving
    /// </summary>
    internal sealed class ArchiveTaskHandler : TaskUtils.AbstractTaskHandler
    {
        /// <summary>
        /// Start processing tasks
        /// </summary>
        /// <returns>Whether the task started successfully</returns>
        public override bool Begin()
        {
            // simulate archived, in producing environment this handler always saves data to DB
            lock (GlobalDataContext.ConsolePrintMutex)
            {
                Console.WriteLine(String.Format("[{0}] Archive task called. Archive successfully.", DateTime.Now));
            }
            return this.isFinished = true;
        }

        /// <summary>
        /// Initialize the task processor
        /// </summary>
        /// <param name="paraDict">Parameter dictionary, key is a parameter, value is a real object</param>
        /// <returns>Whether the task initialized successfully</returns>
        public override bool Init(Dictionary<string, object> paraDict)
        {
            return true;
        }
    }
}
