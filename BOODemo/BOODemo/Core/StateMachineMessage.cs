namespace BOODemo.Core
{
    /// <summary>
    /// 状态机消息类
    /// </summary>
    internal class StateMachineMessage
    {
        /// <summary>
        /// 获取或设置任务名称
        /// </summary>
        public string TaskName
        {
            get;
            set;
        }

        /// <summary>
        /// 获取或设置发出该消息的状态机执行器id
        /// </summary>
        public string BindingExecutorId
        {
            get;
            set;
        }

        /// <summary>
        /// 获取或设置Agent名称
        /// </summary>
        public string AgentName
        {
            get;
            set;
        }

        /// <summary>
        /// 获取或设置完成事件名
        /// </summary>
        public string CallbackEvent
        {
            get;
            set;
        }

        /// <summary>
        /// 获取或设置参数字符串
        /// </summary>
        public string Paras
        {
            get;
            set;
        }
    }
}
