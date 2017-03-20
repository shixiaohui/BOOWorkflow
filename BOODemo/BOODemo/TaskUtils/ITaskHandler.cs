using System.Collections.Generic;

namespace BOODemo.TaskUtils
{
    /// <summary>
    /// Task Processor Interface Class: Provides a common method interface for the task processor
    /// All of the task processor need to implement this interface
    /// </summary>
    internal interface ITaskHandler
    {
        /// <summary>
        /// Initialize the task processor
        /// </summary>
        /// <param name="paraDict">Parameter dictionary, key is a parameter, value is a real object</param>
        /// <returns>Whether the task initialized successfully</returns>
        bool Init(Dictionary<string, object> paraDict);

        /// <summary>
        /// Start processing tasks
        /// </summary>
        /// <returns>Whether the task started successfully</returns>
        bool Begin();

        /// <summary>
        /// Forced to end the task
        /// </summary>
        /// <returns>Whether the task has been successfully forced to end</returns>
        bool Terminate();

        /// <summary>
        /// Query whether the task has been completed
        /// </summary>
        /// <returns>Whether the task finished successfully</returns>
        bool IsFinished();

        /// <summary>
        /// Gets the return result of the task
        /// </summary>
        /// <param name="result">[out] Package of the result</param>
        /// <returns>Whether to successfully get to the implementation of the results to be returned</returns>
        bool GetResult(out object result);

        /// <summary>
        /// Gets the state machine ID of the processor binding
        /// </summary>
        /// <returns>State machine ID</returns>
        string GetBindingExecutorId();

        /// <summary>
        /// Gets whether the processor has been forcibly aborted
        /// </summary>
        /// <returns>whether the processor has been forcibly aborted</returns>
        bool IsAbort();
    }
}
