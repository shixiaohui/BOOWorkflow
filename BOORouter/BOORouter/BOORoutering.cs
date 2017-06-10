using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BOORouter
{
    internal static class BOORoutering
    {
        public static bool DoRouter(string toRouterName, Dictionary<string, string> args, string responseRouterName)
        {
            try
            {
                if (BOORouterTable.GetRouterToURL(toRouterName, out string routerGateway))
                {
                    BOORouterClient.PostAndFetch(routerGateway, args, out string responseStr);
                    if (!String.IsNullOrEmpty(responseRouterName))
                    {
                        if (BOORouterTable.GetRouterToURL(responseRouterName, out string responseGateway))
                        {
                            if (args.ContainsKey("uuid"))
                            {
                                var responseDict = new Dictionary<string, string>(){{"uuid", args["uuid"]}};
                                BOORouterClient.PostAndFetch(responseGateway, responseDict, out string tres);
                                return true;
                            }
                            else
                            {
                                LogUtils.AsyncLogLine("Response router failed, argument uuid not exist in dictionary", "BOORoutering",
                                    LogLevel.Error);
                                return false;
                            }
                        }
                        else
                        {
                            LogUtils.AsyncLogLine("Response router failed, router not exist: " + responseRouterName, "BOORoutering",
                                LogLevel.Error);
                            return false;
                        }
                    }
                    return true;
                }
                else
                {
                    LogUtils.AsyncLogLine("Fetch router failed, router not exist: " + toRouterName, "BOORoutering",
                        LogLevel.Error);
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogUtils.AsyncLogLine("Routering failed, with CLR ERROR: " + ex, "BOORoutering",
                    LogLevel.Error);
                return false;
            }
        }
    }
}
