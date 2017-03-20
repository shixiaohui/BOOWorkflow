using System.Threading;

namespace BOODemo
{
    /// <summary>
    /// Provides access to the global context for the application
    /// </summary>
    internal static class GlobalDataContext
    {
        /// <summary>
        /// The global console displays mutexes
        /// </summary>
        public static Mutex ConsolePrintMutex = new Mutex();

        /// <summary>
        /// Kitchen bill counter mutex
        /// </summary>
        public static Mutex KitchenOrderCounterMutex = new Mutex();

        /// <summary>
        /// Kitchen menu ID counter
        /// </summary>
        public static int KitchenOrderIdCounter = 0;

        /// <summary>
        /// Task Solver Namespace
        /// </summary>
        public static readonly string TaskHandlerWarehouseNamespace = "BOODemo.TaskWarehouse.";

        /// <summary>
        /// The entry XML descriptor gets the protocol name of the URL
        /// </summary>
        public static readonly string EntryPointXMLDescriptorURLProtocol = "file";

        /// <summary>
        /// The name of the entry XML description file
        /// </summary>
        public static readonly string EntryPointXMLDescriptorFileName = "GuestOrder.xml";
    }
}
