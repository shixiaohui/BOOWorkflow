using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace BOODemo
{
    /// <summary>
    /// 为应用程序提供全局上下文的访问
    /// </summary>
    internal static class GlobalDataContext
    {
        /// <summary>
        /// 全局控制台显示互斥量
        /// </summary>
        public static Mutex ConsolePrintMutex = new Mutex();

        /// <summary>
        /// 订单计数器互斥量
        /// </summary>
        public static Mutex OrderCounterMutex = new Mutex();

        /// <summary>
        /// 订单ID计数器
        /// </summary>
        public static int OrderIdCounter = 0;

        /// <summary>
        /// 厨房餐单计数器互斥量
        /// </summary>
        public static Mutex KitchenOrderCounterMutex = new Mutex();

        /// <summary>
        /// 厨房餐单ID计数器
        /// </summary>
        public static int KitchenOrderIdCounter = 0;

        /// <summary>
        /// 任务解决器命名空间
        /// </summary>
        public static readonly string TaskHandlerWarehouseNamespace = "BOODemo.TaskWarehouse.";

        /// <summary>
        /// 入口XML描述文件获取URL的协议名
        /// </summary>
        public static readonly string EntryPointXMLDescriptorURLProtocol = "file";

        /// <summary>
        /// 入口XML描述文件的文件名
        /// </summary>
        public static readonly string EntryPointXMLDescriptorFileName = "GuestOrder.xml";
    }
}
