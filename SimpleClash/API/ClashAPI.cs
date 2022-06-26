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
        public static ClashVersion GetVersion()
        {
            var res = ClashBaseAPI.GetVersion();
            return JsonConvert.DeserializeObject<ClashVersion>(res.Data);
        }

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

        public static Latency GetLatency(string name)
        {
            var res = ClashBaseAPI.GetProxyDelay(name, "http://www.gstatic.com/generate_204", 5000);
            return JsonConvert.DeserializeObject<Latency>(res.Data);
        }

        public static ProxyInfo GetProxy(string name)
        {
            var res = ClashBaseAPI.GetProxyInfo(name);
            return JsonConvert.DeserializeObject<ProxyInfo>(res.Data);
        }

        public static ClashBaseConfig GetCurrentConfig()
        {
            var res = ClashBaseAPI.GetConfig();
            return JsonConvert.DeserializeObject<ClashBaseConfig>(res.Data);
        }

        public static void SetConfig(string config)
        {
            //string config = string.Empty;
            ClashBaseAPI.SetConfig(config);
        }
    }
}
