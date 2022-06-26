using SimpleClash.Helpers;
using SimpleClash.Models;
using System;

namespace SimpleClash.API
{
    internal static class ClashBaseAPI
    {
        private const string Url = "http://localhost:8909/";

        /// <summary>
        /// 获取Clash版本
        /// </summary>
        /// <returns></returns>
        internal static Result<string> GetVersion()
        {
            return HttpHelper.Get(Url, "version");
        }

        /// <summary>
        /// 获取所有代理
        /// </summary>
        /// <returns></returns>
        internal static Result<string> GetProxies()
        {
            return HttpHelper.Get(Url, "proxies");
        }

        /// <summary>
        /// 获取实时流量
        /// </summary>
        /// <param name="action"></param>
        internal static void GetTraffic(Action<string> action)
        {
            HttpHelper.Get_Async(Url, "traffic", action);
        }

        /// <summary>
        /// 获取实时日志
        /// </summary>
        /// <param name="action"></param>
        internal static void GetLogs(Action<string> action)
        {
            HttpHelper.Get_Async(Url, "logs", action);
        }

        /// <summary>
        /// 获取代理信息
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        internal static Result<string> GetProxyInfo(string name)
        {
            return HttpHelper.Get(Url, "proxies", name);
        }

        /// <summary>
        /// 获取代理延迟
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        internal static Result<string> GetProxyDelay(string name, string url, int timeout)
        {
            return HttpHelper.Get(Url, "proxies", $"{name}/delay?url={url}&timeout={timeout}");
        }

        /// <summary>
        /// 切换选择器中的代理
        /// </summary>
        /// <param name="selectorName"></param>
        /// <param name="proxyName"></param>
        /// <returns></returns>
        internal static Result<string> SwitchSelectorProxy(string selectorName, string proxyName)
        {
            return HttpHelper.Put(Url, "proxies", selectorName, $"{{\"Name\":\"{proxyName}\"}}");
        }

        /// <summary>
        /// 获取当前的基础设置
        /// </summary>
        /// <returns></returns>
        internal static Result<string> GetConfig()
        {
            return HttpHelper.Get(Url, "configs");
        }

        /// <summary>
        /// 增量修改配置
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        internal static Result<string> SetConfig(string config)
        {
            return HttpHelper.Patch(Url, "configs", config);
        }

        /// <summary>
        /// 重载配置
        /// </summary>
        /// <param name="overridePorts"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        internal static Result<string> ReloadConfig(bool overridePorts, string filePath)
        {
            return HttpHelper.Put(Url, "configs", $"?force={overridePorts}", $"{{\"path\":\"{filePath}\"}}");
        }
    }
}
