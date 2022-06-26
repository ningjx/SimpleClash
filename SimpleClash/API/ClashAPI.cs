using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SimpleClash.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleClash.API
{
    public class ClashAPI
    {

        /// <summary>
        /// 获取当前Clash版本
        /// </summary>
        /// <returns></returns>
        public static ClashVersion GetVersion()
        {
            var res = ClashBaseAPI.GetVersion();
            return JsonConvert.DeserializeObject<ClashVersion>(res.Data);
        }
        
        /// <summary>
        /// 获取实时网速
        /// </summary>
        /// <param name="action"></param>
        public static void GetTraffic(Action<Traffic> action)
        {
            var strLine = string.Empty;
            ClashBaseAPI.GetTraffic(delegate (string a)
            {
                if (a == "\n" || a == "\r\n")
                    return;

                strLine += a;
                if (a == "}" && strLine.StartsWith("{"))
                {
                    var tra = JsonConvert.DeserializeObject<Traffic>(strLine);
                    strLine = string.Empty;
                    action(tra);
                }
            });
        }

        /// <summary>
        /// 获取全部代理，包括proxy-groups
        /// </summary>
        /// <returns></returns>
        public static List<ProxyInfo> GetProxies()
        {
            var proxyList = new List<ProxyInfo>();

            var apiRes = ClashBaseAPI.GetProxies().Data;
            var jsonObj = JObject.Parse(apiRes)["proxies"].ToList();

            foreach (var proxyItem in jsonObj)
            {
                var proxyStr = proxyItem.First().ToString();
                var info = JsonConvert.DeserializeObject<ProxyInfo>(proxyStr);

                //var data = proxyItem.Children().First();
                //var info = new ProxyInfo
                //{
                //    Name = (string)data["name"],
                //    Type = (string)data["type"],
                //    Udp = Convert.ToString(data["udp"]) == "true",
                //    Now = (string)data["now"]
                //};

                //if (data["all"] != null)
                //{
                //    info.All = data["all"].Select(t => (string)t).ToList();
                //}

                //if (data["history"] != null)
                //{
                //    info.History = data["history"].Select(t => new History { Time = (DateTime)t["time"], Delay = (int)t["delay"] }).ToList();
                //}

                proxyList.Add(info);
            }
            return proxyList;
        }

        /// <summary>
        /// 测试节点的延迟
        /// </summary>
        /// <param name="name">节点名称</param>
        /// <returns></returns>
        public static Latency GetLatency(string name)
        {
            var res = ClashBaseAPI.GetProxyDelay(name, "http://www.gstatic.com/generate_204", 5000);
            return JsonConvert.DeserializeObject<Latency>(res.Data);
        }

        /// <summary>
        /// 获取节点的信息
        /// </summary>
        /// <param name="name">节点名称</param>
        /// <returns></returns>
        public static ProxyInfo GetProxy(string name)
        {
            var res = ClashBaseAPI.GetProxyInfo(name);
            return JsonConvert.DeserializeObject<ProxyInfo>(res.Data);
        }

        /// <summary>
        /// 获取当前的基础配置
        /// </summary>
        /// <returns></returns>
        public static ClashBaseConfig GetCurrentConfig()
        {
            var res = ClashBaseAPI.GetConfig();
            return JsonConvert.DeserializeObject<ClashBaseConfig>(res.Data);
        }

        /// <summary>
        /// 增量修改配置
        /// </summary>
        /// <param name="config">配置信息，详见<see cref="https://clash.gitbook.io/doc/restful-api/config"/></param>
        public static void SetConfig(string config)
        {
            /*传入配置格式如下
            {"port":7889}{"socks-port":"7899"}{"allow-lan":true}{"mode":"Rule"}
            */
            ClashBaseAPI.SetConfig(config);
        }

        /// <summary>
        /// 重新加载配置
        /// </summary>
        public static void ReloadConfig()
        {
            if (!string.IsNullOrEmpty(AppConfig.Instance.CurrentClashConfigPath))
                ClashBaseAPI.ReloadConfig(AppConfig.Instance.PortsOverride, AppConfig.Instance.CurrentClashConfigPath);
        }
    }
}
