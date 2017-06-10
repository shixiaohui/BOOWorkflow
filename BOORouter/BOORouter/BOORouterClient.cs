using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace BOORouter
{
    internal class BOORouterClient
    {
        /// <summary>
        /// 对指定的URL做POST动作
        /// </summary>
        /// <param name="url">要访问的URL</param>
        /// <param name="argsDict">POST的参数字典</param>
        /// <param name="result">[out] URL的反馈</param>
        /// <param name="encoding">编码器</param>
        /// <returns>操作是否成功</returns>
        public static bool PostData(string url, Dictionary<string, string> argsDict, out string result, Encoding encoding = null)
        {
            try
            {
                var client = new WebClient();
                // 提交的内容
                StringBuilder sb = new StringBuilder();
                if (argsDict != null)
                {
                    foreach (var arg in argsDict)
                    {
                        sb.Append("&" + arg.Key + "=" + arg.Value);
                    }
                }
                if (sb.Length > 0)
                {
                    sb = sb.Remove(0, 1);
                }
                // 编码
                if (encoding == null)
                {
                    encoding = Encoding.UTF8;
                }
                byte[] postData = encoding.GetBytes(sb.ToString());
                // POST
                client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                client.Headers.Add("ContentLength", postData.Length.ToString());
                byte[] respondData = client.UploadData(url, "POST", postData);
                result = encoding.GetString(respondData);
                return true;
            }
            catch (Exception ex)
            {
                LogUtils.LogLine("Post Data to URL " + url + " failed." + Environment.NewLine + ex,
                    "YuriHttpClient", LogLevel.Error);
                result = null;
                return false;
            }
        }

        /// <summary>
        /// 访问指定的URL并下载页面中的内容为字符串
        /// </summary>
        /// <param name="url">要访问的URL</param>
        /// <param name="argsDict">POST的参数字典</param>
        /// <param name="result">[out] 获得的字符串</param>
        /// <returns>操作是否成功</returns>
        public static bool PostAndFetch(string url, Dictionary<string, string> argsDict, out string result)
        {
            try
            {
                // 提交的内容
                StringBuilder sb = new StringBuilder();
                if (argsDict != null)
                {
                    foreach (var arg in argsDict)
                    {
                        sb.Append("&" + arg.Key + "=" + arg.Value);
                    }
                }
                if (sb.Length > 0)
                {
                    sb[0] = '?';
                }
                var wb = new WebClient();
                result = wb.DownloadString(url + sb);
                return true;
            }
            catch (Exception ex)
            {
                LogUtils.LogLine("Fetch String from URL " + url + " failed." + Environment.NewLine + ex,
                    "YuriHttpClient", LogLevel.Error);
                result = null;
                return false;
            }
        }

        /// <summary>
        /// 访问指定的URL并下载页面中的内容为字符串
        /// </summary>
        /// <param name="url">要访问的URL</param>
        /// <param name="result">[out] 获得的字符串</param>
        /// <returns>操作是否成功</returns>
        public static bool FetchString(string url, out string result)
        {
            try
            {
                var wb = new WebClient();
                result = wb.DownloadString(url);
                return true;
            }
            catch (Exception ex)
            {
                LogUtils.LogLine("Fetch String from URL " + url + " failed." + Environment.NewLine + ex,
                    "YuriHttpClient", LogLevel.Error);
                result = null;
                return false;
            }
        }
    }
}
