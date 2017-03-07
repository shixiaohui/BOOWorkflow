using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using com.sysu.workflow;
using com.sysu.workflow.entity;

namespace BOODemo
{
    /// <summary>
    /// 状态机消息处理类：接受来自状态机的消息并执行实际的业务逻辑
    /// </summary>
    internal class StateMachineMessageHandler : EngineBridgeAppHandler
    {
        /// <summary>
        /// 通知函数：被状态机通知有新消息时
        /// </summary>
        public void WasNotified()
        {
            Thread consumerThread = new Thread(new ThreadStart(this.ConsumerHandler));
            consumerThread.IsBackground = true;
            consumerThread.Start();
        }

        /// <summary>
        /// 异步将状态机发来的消息取出并处理
        /// </summary>
        private void ConsumerHandler()
        {
            // 取消息
            var dealingQueue = new Queue<StateMachineMessage>();
            lock (engineBridge)
            {
                BOMessage bom = null;
                while ((bom = engineBridge.GetPendingMessage()) != null)
                {
                    var taskList = bom.GetTaskList();
                    var agentList = bom.GetAgentList();
                    for (int i = 0; i < taskList.size(); i++)
                    {
                        StateMachineMessage smm = new StateMachineMessage()
                        {
                            TaskName = taskList.get(i).ToString(),
                            AgentName = agentList.get(i).ToString()
                        };
                        dealingQueue.Enqueue(smm);
                    }
                }
            }
            // 执行具体动作
            throw new NotImplementedException();
        }

        /// <summary>
        /// 桥接器的引用
        /// </summary>
        private EngineBridge engineBridge = EngineBridge.GetInstance();
    }
}
