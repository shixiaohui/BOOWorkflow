using System;
using System.Threading;
using System.Collections.Generic;
using com.sysu.workflow;
using com.sysu.workflow.entity;
using BOODemo.ViewModel;
using BOODemo.TaskUtils;

namespace BOODemo.Core
{
    /// <summary>
    /// State machine message processing class:
    ///     Accepts messages from the state machine and executes the actual business logic
    /// </summary>
    internal class StateMachineMessageHandler : EngineBridgeAppHandler
    {
        /// <summary>
        /// Notification function: when a state machine is notified of a new message
        /// </summary>
        public void WasNotified()
        {
            Thread consumerThread = new Thread(new ThreadStart(this.ConsumerHandler));
            consumerThread.IsBackground = true;
            consumerThread.Start();
        }

        /// <summary>
        /// Asynchronously get and process the message that Sent by the state machine
        /// </summary>
        private void ConsumerHandler()
        {
            // get the message
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
                            BindingExecutorId = bom.GetExecutorIndex(),
                            CallbackEvent = callbackList.get(i).ToString(),
                            Paras = tParas != null ? tParas.ToString() : String.Empty,
                            AgentName = tAgent != null ? tAgent.ToString() : String.Empty
                        };
                        dealingQueue.Enqueue(smm);
                    }
                }
            }
            // Perform specific actions
            while (dealingQueue.Count != 0)
            {
                var dealingItem = dealingQueue.Dequeue();
                var tHandler = TaskFactory.GetTaskHandlerByTaskName(dealingItem.TaskName);
                Dictionary<string, object> paraDict = new Dictionary<string, object>();
                // Parameters from the state machine
                if (dealingItem.Paras != String.Empty)
                {
                    var paraItems = dealingItem.Paras.Split(',');
                    // TODO: Parameter list escapes
                    foreach (var paraPairs in paraItems)
                    {
                        var pairItems = paraPairs.Split(':');
                        paraDict[pairItems[0]] = pairItems[1];
                    }
                }
                // Parameters from the application
                switch (dealingItem.TaskName)
                {
                    case "addItemTask":
                    case "calculateTask":
                    case "paymentTask":
                    case "updateDeliTimeTask":
                        paraDict["guestOrderId"] = Convert.ToInt32(dealingItem.BindingExecutorId);
                        break;
                    case "deliverTask":
                    case "produceDishesTask":
                    case "testQualityTask":
                        paraDict["guestOrderId"] = Convert.ToInt32(dealingItem.BindingExecutorId);
                        paraDict["kitchenOrderId"] = RestaurantViewModel.RestaurantEntity.KitchenOrderList.Find(
                            (t) => t.GuestOrderId.ToString() == dealingItem.BindingExecutorId && t.IsFinish == false).Id;
                        break;
                        
                }
                // Bind the processor
                ((AbstractTaskHandler)tHandler).Binding(dealingItem.BindingExecutorId);
                // Initialize the task processor
                if (tHandler.Init(paraDict) == false)
                {
                    throw new Exception(String.Format("Init handler for {0} failed.", dealingItem.TaskName));
                }
                // Join the active processor vector
                lock (RestaurantViewModel.ActiveTaskHandlerList)
                {
                    RestaurantViewModel.ActiveTaskHandlerList.Add(tHandler);
                }
                // Perform the task
                // TODO: Asynchronous
                if (tHandler.Begin() == false)
                {
                    throw new Exception(String.Format("Process {0} failed.", dealingItem.TaskName));
                }
                // Waiting for the task to complete
                while (tHandler.IsFinished() == false || tHandler.IsAbort())
                {
                    Thread.Sleep(TimeSpan.FromTicks(100));
                }
                // Get the solution to the results
                object resultPackage;
                if (tHandler.GetResult(out resultPackage) == false)
                {
                    throw new Exception(String.Format("Get result package of {0} failed.", dealingItem.TaskName));
                }
                // Feedback to the state machine
                if (tHandler.IsFinished())
                {
                    this.engineBridge.SendEventAndTrigger(tHandler.GetBindingExecutorId(),
                        dealingItem.CallbackEvent, resultPackage);
                }
                // Remove from active processor vector
                lock (RestaurantViewModel.ActiveTaskHandlerList)
                {
                    RestaurantViewModel.ActiveTaskHandlerList.Remove(tHandler);
                }
            }
        }

        /// <summary>
        /// reference of engineBridge
        /// </summary>
        private EngineBridge engineBridge = EngineBridge.GetInstance();
    }
}
