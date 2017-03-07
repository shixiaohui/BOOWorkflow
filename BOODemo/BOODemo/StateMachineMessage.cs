using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BOODemo
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
        /// 获取或设置Agent名称
        /// </summary>
        public string AgentName
        {
            get;
            set;
        }
    }
}
