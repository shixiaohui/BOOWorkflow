namespace BOODemo.Core
{
    /// <summary>
    /// State machine message class
    /// </summary>
    internal class StateMachineMessage
    {
        /// <summary>
        /// Gets or sets the task name
        /// </summary>
        public string TaskName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the id of the state machine executor that send the message
        /// </summary>
        public string BindingExecutorId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the name of the Agent
        /// </summary>
        public string AgentName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the name of the completed event
        /// </summary>
        public string CallbackEvent
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the parameter string
        /// </summary>
        public string Paras
        {
            get;
            set;
        }
    }
}
