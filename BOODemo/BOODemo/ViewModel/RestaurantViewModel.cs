using System;
using System.Collections.Generic;
using com.sysu.workflow;
using com.sysu.workflow.io;
using com.sysu.workflow.env;
using com.sysu.workflow.model;
using com.sysu.workflow.env.jexl;
using BOODemo.Core;
using BOODemo.View;
using BOODemo.Model;
using BOODemo.TaskUtils;

namespace BOODemo.ViewModel
{
    extern alias OpenJDKCore;
    /// <summary>
    /// ViewModel类
    /// </summary>
    internal static class RestaurantViewModel
    {
        /// <summary>
        /// 静态构造函数
        /// </summary>
        static RestaurantViewModel()
        {
            RestaurantViewModel.engineBridge = EngineBridge.GetInstance();
            RestaurantViewModel.messageHandler = new StateMachineMessageHandler();
            RestaurantViewModel.engineBridge.Init(messageHandler);
            RestaurantViewModel.executorDict = new Dictionary<int, SCXMLExecutor>();
            RestaurantViewModel.OrderingFormDict = new Dictionary<int, View.OrderingForm>();
            RestaurantViewModel.RestaurantEntity = new Restaurant();
            RestaurantViewModel.ActiveTaskHandlerList = new List<ITaskHandler>();
        }

        /// <summary>
        /// 开始一个新的业务对象实例
        /// </summary>
        /// <returns>改业务对象的索引id</returns>
        public static int CreateBussinessObjectInstance()
        {
            try
            {
                StateMachineMessageHandler MsgHandler = new StateMachineMessageHandler();
                var url = new OpenJDKCore.java.net.URL(
                    GlobalDataContext.EntryPointXMLDescriptorURLProtocol, String.Empty,
                    GlobalDataContext.EntryPointXMLDescriptorFileName);
                SCXML scxml = SCXMLReader.read(url);
                Evaluator ev = new JexlEvaluator();
                SCXMLExecutor executor = new SCXMLExecutor(ev, new MulitStateMachineDispatcher(), new SimpleErrorReporter());
                executor.setStateMachine(scxml);
                RestaurantViewModel.engineBridge.SetExecutorReference(RestaurantViewModel.executorCounter, executor);
                RestaurantViewModel.executorDict[RestaurantViewModel.executorCounter] = executor;
                return RestaurantViewModel.executorCounter++;
            }
            catch (Exception e)
            {
                lock (GlobalDataContext.ConsolePrintMutex)
                {
                    Console.WriteLine(e.ToString());
                }
                throw new ModelException();
            }
        }

        /// <summary>
        /// 向指定状态机发送事件
        /// </summary>
        /// <param name="execId">状态机编号</param>
        /// <param name="eventName">事件名称</param>
        /// <param name="payload">事件附加值的包装</param>
        public static void Send(int execId, string eventName, object payload = null)
        {
            RestaurantViewModel.engineBridge.SendEventAndTrigger(execId, eventName, payload);
        }

        /// <summary>
        /// 获取点餐窗体引用字典
        /// </summary>
        public static Dictionary<int, OrderingForm> OrderingFormDict
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取或设置服务员窗体引用
        /// </summary>
        public static WaiterForm WaiterFormReference
        {
            get;
            set;
        }

        /// <summary>
        /// 获取或设置厨房窗体引用
        /// </summary>
        public static KitchenForm KitchenFormReference
        {
            get;
            set;
        }

        /// <summary>
        /// 获取或设置收银台窗体引用
        /// </summary>
        public static GuestCheckForm GuestCheckFormReference
        {
            get;
            set;
        }

        /// <summary>
        /// 获取或设置餐厅实体
        /// </summary>
        public static Restaurant RestaurantEntity
        {
            get;
            set;
        }
        
        /// <summary>
        /// 获取当前活跃的任务处理器向量
        /// </summary>
        public static List<ITaskHandler> ActiveTaskHandlerList
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取状态机执行器字典
        /// </summary>
        public static Dictionary<int, SCXMLExecutor> executorDict
        {
            get;
            private set;
        }

        /// <summary>
        /// 状态机执行器计数
        /// </summary>
        private static int executorCounter = 0;
        
        /// <summary>
        /// 桥接器
        /// </summary>
        private static EngineBridge engineBridge;

        /// <summary>
        /// 消息处理器
        /// </summary>
        private static StateMachineMessageHandler messageHandler;
    }
}
