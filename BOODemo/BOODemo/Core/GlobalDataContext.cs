using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace BOODemo.Core
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
    }
}
