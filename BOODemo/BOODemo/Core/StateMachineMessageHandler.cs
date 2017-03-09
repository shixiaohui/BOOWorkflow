using System;
using System.Threading;
using System.Collections.Generic;
using com.sysu.workflow;
using com.sysu.workflow.entity;
using BOODemo.TaskUtils;

namespace BOODemo.Core
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
                    var parasList = bom.GetParamsList();
                    var callbackList = bom.GetCallbackList();
                    for (int i = 0; i < taskList.size(); i++)
                    {
                        var tAgent = agentList.get(i);
                        var tParas = parasList.get(i);
                        StateMachineMessage smm = new StateMachineMessage()
                        {
                            TaskName = taskList.get(i).ToString(),
                            CallbackEvent = callbackList.get(i).ToString(),
                            Paras = tParas != null ? tParas.ToString() : String.Empty,
                            AgentName = tAgent != null ? tAgent.ToString() : String.Empty
                        };
                        dealingQueue.Enqueue(smm);
                    }
                }
            }
            // 执行具体动作
            while (dealingQueue.Count != 0)
            {
                var dealingItem = dealingQueue.Dequeue();
                var tHandler = TaskFactory.GetTaskHandlerByTaskName(dealingItem.TaskName);
                Dictionary<string, object> paraDict = new Dictionary<string, object>();
                if (dealingItem.Paras != String.Empty)
                {
                    var paraItems = dealingItem.Paras.Split(',');
                    // TODO: 参数列表转义
                    foreach (var paraPairs in paraItems)
                    {
                        var pairItems = paraPairs.Split(':');
                        paraDict[pairItems[0]] = pairItems[1];
                    }
                }
                // 初始化人任务处理器
                if (tHandler.Init(paraDict) == false)
                {
                    throw new Exception(String.Format("Init handler for {0} failed.", dealingItem.TaskName));
                }
                // 执行任务
                // TODO: 异步执行
                if (tHandler.Begin() == false)
                {
                    throw new Exception(String.Format("Process {0} failed.", dealingItem.TaskName));
                }
                // 获取解决的结果包装
                object resultPackage;
                if (tHandler.GetResult(out resultPackage) == false)
                {
                    throw new Exception(String.Format("Get result package of {0} failed.", dealingItem.TaskName));
                }
                // 反馈给状态机
                if (tHandler.IsFinished())
                {
                    this.engineBridge.SendEventAndTrigger(dealingItem.CallbackEvent, resultPackage);
                }
            }
        }

        /// <summary>
        /// 桥接器的引用
        /// </summary>
        private EngineBridge engineBridge = EngineBridge.GetInstance();
    }
}
