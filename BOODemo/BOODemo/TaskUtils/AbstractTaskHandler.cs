using System.Collections.Generic;

namespace BOODemo.TaskUtils
{
    /// <summary>
    /// Abstract Task Solver Class: Provides a common field wrapper for task solvers
    /// </summary>
    internal abstract class AbstractTaskHandler : ITaskHandler
    {
        /// <summary>
        /// Start processing tasks
        /// </summary>
        /// <returns>Whether the task started successfully</returns>
        abstract public bool Begin();

        /// <summary>
        /// Initialize the task processor
        /// </summary>
        /// <param name="paraDict">Parameter dictionary, key is a parameter, value is a real object</param>
        /// <returns>Whether the task initialized successfully</returns>
        abstract public bool Init(Dictionary<string, object> paraDict);

        /// <summary>
        /// Forced to end the task
        /// </summary>
        /// <returns>Whether the task has been successfully forced to end</returns>
        virtual public bool Terminate()
        {
            return this.isAborted = true;
        }

        /// <summary>
        /// Query whether the task has been completed
        /// </summary>
        /// <returns>Whether the task finished successfully</returns>
        virtual public bool IsFinished()
        {
            return this.isFinished;
        }

        /// <summary>
        /// Gets the return result of the task
        /// </summary>
        /// <param name="result">[out] Package of the result</param>
        /// <returns>Whether to successfully get to the implementation of the results to be returned</returns>
        virtual public bool GetResult(out object result)
        {
            result = null;
            return true;
        }

        /// <summary>
        /// Gets the state machine ID of the processor binding
        /// </summary>
        /// <returns>State machine ID</returns>
        virtual public string GetBindingExecutorId()
        {
            return this.bindingExecutorID;
        }

        /// <summary>
        /// Sets the state machine ID of the processor binding
        /// </summary>
        /// <param name="execId">要绑定的状态机的ID</param>
        virtual public void Binding(string execId)
        {
            this.bindingExecutorID = execId;
        }

        /// <summary>
        /// Gets whether the processor has been forcibly aborted
        /// </summary>
        /// <returns>whether the processor has been forcibly aborted</returns>
        virtual public bool IsAbort()
        {
            return this.isAborted;
        }

        /// <summary>
        /// Whether the task is finished
        /// </summary>
        protected bool isFinished = false;

        /// <summary>
        /// Gets or sets the bound state machine processor ID
        /// </summary>
        protected string bindingExecutorID = "0";

        /// <summary>
        /// Whether the task has been forcibly aborted
        /// </summary>
        protected bool isAborted = false;
    }
}
