using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BOORouter
{
    internal static class BOORouterTable
    {
        /// <summary>
        /// 指定下一路由的名字，获得路由的网关
        /// </summary>
        /// <param name="routeableName">路由的名字</param>
        /// <param name="url">[out] 网关地址</param>
        /// <returns>操作是否成功</returns>
        public static bool GetRouterToURL(string routeableName, out string url)
        {
            if (BOORouterTable.routerTable.ContainsKey(routeableName))
            {
                url = BOORouterTable.routerTable[routeableName];
                return true;
            }
            else
            {
                url = String.Empty;
                return false;
            }
        }

        /// <summary>
        /// 添加一条路由项
        /// </summary>
        /// <param name="routeableName">路由名字</param>
        /// <param name="gatewayUrl">网关URL</param>
        /// <returns>是否为替换操作</returns>
        public static bool AddRouteable(string routeableName, string gatewayUrl)
        {
            if (BOORouterTable.routerTable.ContainsKey(routeableName))
            {
                BOORouterTable.routerTable[routeableName] = gatewayUrl;
                return true;
            }
            else
            {
                BOORouterTable.routerTable[routeableName] = gatewayUrl;
                return false;
            }
        }
        
        /// <summary>
        /// 移除一条路由记录
        /// </summary>
        /// <param name="routeableName">路由名字</param>
        /// <returns>是否存在并成功移除</returns>
        public static bool RemoveRouteable(string routeableName)
        {
            return BOORouterTable.routerTable.Remove(routeableName);
        }

        /// <summary>
        /// 路由表字典
        /// </summary>
        private static readonly Dictionary<string, string> routerTable = new Dictionary<string, string>();
    }
}
