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
    /// ViewModel class: Provide interaction logic between front-end and back-end
    /// </summary>
    internal static class RestaurantViewModel
    {
        /// <summary>
        /// Static constructor
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
        /// Start a new business object instance
        /// </summary>
        /// <returns>Change the index id of the business object</returns>
        public static int CreateBussinessObjectInstance()
        {
            try
            {
                var scxml = RestaurantViewModel.GetEntryPointSCXML();
                Evaluator ev = EvaluatorFactory.getEvaluator(scxml);
                SCXMLExecutor executor = new SCXMLExecutor(ev, new MulitStateMachineDispatcher(), new SimpleErrorReporter());
                executor.setStateMachine(scxml);
                RestaurantViewModel.engineBridge.SetExecutorReference(RestaurantViewModel.executorCounter.ToString(), executor);
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
        /// Send an event to the specified state machine
        /// </summary>
        /// <param name="execId">state machine id</param>
        /// <param name="eventName">event name</param>
        /// <param name="payload">package of event added value</param>
        public static void Send(string execId, string eventName, object payload = null)
        {
            RestaurantViewModel.engineBridge.SendEventAndTrigger(execId, eventName, payload);
        }

        /// <summary>
        /// Gets the entry SCXML object and loads it if it has never been loaded
        /// </summary>
        /// <returns>A reference of a SCXML object</returns>
        private static SCXML GetEntryPointSCXML()
        {
            if (RestaurantViewModel.EntryPointSCXML == null)
            {
                try
                {
                    var url = new OpenJDKCore.java.net.URL(
                       GlobalDataContext.EntryPointXMLDescriptorURLProtocol, String.Empty,
                       GlobalDataContext.EntryPointXMLDescriptorFileName);
                    RestaurantViewModel.EntryPointSCXML = SCXMLReader.read(url);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    throw new Exception(e.ToString());
                }
            }
            return RestaurantViewModel.EntryPointSCXML;
        }

        /// <summary>
        /// Get the order form reference dictionary
        /// </summary>
        public static Dictionary<int, OrderingForm> OrderingFormDict
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the waiter form reference
        /// </summary>
        public static WaiterForm WaiterFormReference
        {
            get;
            set;
        }

        /// <summary>
        /// Get or set the kitchen form reference
        /// </summary>
        public static KitchenForm KitchenFormReference
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the checkbox form reference
        /// </summary>
        public static GuestCheckForm GuestCheckFormReference
        {
            get;
            set;
        }

        /// <summary>
        /// Get or set restaurant entities
        /// </summary>
        public static Restaurant RestaurantEntity
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the currently active task handler vector
        /// </summary>
        public static List<ITaskHandler> ActiveTaskHandlerList
        {
            get;
            private set;
        }

        /// <summary>
        /// Get the state machine actuator dictionary
        /// </summary>
        public static Dictionary<int, SCXMLExecutor> executorDict
        {
            get;
            private set;
        }

        /// <summary>
        /// The reference to the entry SCXML object
        /// </summary>
        private static SCXML EntryPointSCXML = null;

        /// <summary>
        /// State machine actuator count
        /// </summary>
        private static int executorCounter = 0;

        /// <summary>
        /// Engine Bridge
        /// </summary>
        private static EngineBridge engineBridge;

        /// <summary>
        /// Message handler
        /// </summary>
        private static StateMachineMessageHandler messageHandler;
    }
}
