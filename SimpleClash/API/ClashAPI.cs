using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SimpleClash.Models;
using System.Collections.Generic;
using System.Linq;

namespace SimpleClash.API
{
    public class ClashAPI
    {
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

    }
}
