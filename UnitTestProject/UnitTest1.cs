﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Converters;
using System.Xml;
using System.Collections.Generic;
using SimpleClash.Models;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        private string FilePath = @"D:\test\clash-win64.exe";
        private string ConfigPath = @"D:\test\config.yaml";
        [TestMethod]
        public void TestMethod1()
        {
            var pInfo = new ProcessStartInfo
            {
                FileName = FilePath,
                Arguments = $"-f {ConfigPath}",
                WindowStyle = ProcessWindowStyle.Hidden,
            };
            Process proc = Process.Start(pInfo);

            //proc.WaitForExit();

            Thread.Sleep(1000);
            var por = Process.GetProcessesByName("clash-win64").ToArray();
            por?.First()?.Kill();
        }

        [TestMethod]
        public void GetProcess()
        {
            var str = "{\"proxies\":{\"DIRECT\":{\"history\":[],\"name\":\"DIRECT\",\"type\":\"Direct\",\"udp\":true},\"GLOBAL\":{\"all\":[\"DIRECT\",\"REJECT\",\"SS美国1\",\"SS美国2\",\"V2Ray美国\",\"V2Ray美国0.1倍流量\",\"V2Ray日本\",\"V2Ray荷兰\",\"全部节点\",\"自动选择快速节点\",\"国际流媒体\"],\"history\":[],\"name\":\"GLOBAL\",\"now\":\"DIRECT\",\"type\":\"Selector\",\"udp\":true},\"REJECT\":{\"history\":[],\"name\":\"REJECT\",\"type\":\"Reject\",\"udp\":true},\"SS美国1\":{\"history\":[{\"time\":\"2022-06-25T20:49:27.8411796+08:00\",\"delay\":458}],\"name\":\"SS美国1\",\"type\":\"Shadowsocks\",\"udp\":true},\"SS美国2\":{\"history\":[{\"time\":\"2022-06-25T20:49:27.7836839+08:00\",\"delay\":400}],\"name\":\"SS美国2\",\"type\":\"Shadowsocks\",\"udp\":true},\"V2Ray日本\":{\"history\":[{\"time\":\"2022-06-25T20:49:27.6804244+08:00\",\"delay\":297}],\"name\":\"V2Ray日本\",\"type\":\"Vmess\",\"udp\":true},\"V2Ray美国\":{\"history\":[{\"time\":\"2022-06-25T20:49:28.0129247+08:00\",\"delay\":630}],\"name\":\"V2Ray美国\",\"type\":\"Vmess\",\"udp\":true},\"V2Ray美国0.1倍流量\":{\"history\":[{\"time\":\"2022-06-25T20:49:27.9904892+08:00\",\"delay\":607}],\"name\":\"V2Ray美国0.1倍流量\",\"type\":\"Vmess\",\"udp\":true},\"V2Ray荷兰\":{\"history\":[{\"time\":\"2022-06-25T20:49:27.9256148+08:00\",\"delay\":542}],\"name\":\"V2Ray荷兰\",\"type\":\"Vmess\",\"udp\":true},\"全部节点\":{\"all\":[\"自动选择快速节点\",\"V2Ray美国\",\"SS美国1\",\"SS美国2\",\"V2Ray日本\",\"V2Ray荷兰\",\"V2Ray美国0.1倍流量\"],\"history\":[{\"time\":\"2022-06-25T22:04:40.0239838+08:00\",\"delay\":0},{\"time\":\"2022-06-25T22:05:39.2179147+08:00\",\"delay\":0},{\"time\":\"2022-06-25T22:06:04.7950376+08:00\",\"delay\":455}],\"name\":\"全部节点\",\"now\":\"SS美国2\",\"type\":\"Selector\",\"udp\":true},\"国际流媒体\":{\"all\":[\"V2Ray美国0.1倍流量\",\"自动选择快速节点\",\"V2Ray美国\",\"SS美国1\",\"SS美国2\",\"V2Ray日本\",\"V2Ray荷兰\"],\"history\":[],\"name\":\"国际流媒体\",\"now\":\"V2Ray美国0.1倍流量\",\"type\":\"Selector\",\"udp\":true},\"自动选择快速节点\":{\"all\":[\"V2Ray美国\",\"SS美国1\",\"SS美国2\",\"V2Ray日本\",\"V2Ray荷兰\",\"V2Ray美国0.1倍流量\"],\"history\":[],\"name\":\"自动选择快速节点\",\"now\":\"V2Ray日本\",\"type\":\"URLTest\",\"udp\":true}}}";

            var a = JObject.Parse(str);
            var proList = new List<ProxyInfo>();
            var test = a["proxies"].ToList();
            foreach (var item in test)
            {
                var data = item.Children().First();
                var info = new ProxyInfo
                {
                    Name = (string)data["name"],
                    Type = (string)data["type"],
                    Udp = Convert.ToString(data["udp"]) == "true",
                    Now = (string)data["now"]
                };

                if (data["all"] != null)
                {
                    var proxies = data["all"]?.Children().Values().ToList();
                    info.All = proxies.Select(t => (string)t).ToList();
                }

                if (data["history"] != null)
                {
                    info.History = data["history"].Select(t => new History { Time = (DateTime)t["time"], Delay = (int)t["delay"] }).ToList();
                }

                proList.Add(info);
            }

            //XmlDocument doc = (XmlDocument)JsonConvert.DeserializeXmlNode(str);
            //doc.Save(@"D:\2.xml");
            //
            //var providers = doc.ChildNodes[0].ChildNodes;
            //for (int i = 0; i < providers.Count; i++)
            //{
            //    var test = providers[i].SelectSingleNode("type").FirstChild.Value;
            //    if (providers[i]["now"] != "Selector")
            //        continue;
            //    var info = new ProxyInfo
            //    {
            //        Name = providers[i]["name"].Value,
            //        Type = providers[i]["type"].Value,
            //        Now = providers[i]["now"].Value,
            //        Udp = providers[i]["udp"].Value == "true"
            //    };
            //    proList.Add(info);
            //}
            //foreach(var item in providers)
            //{
            //    var info = new ProxyInfo
            //    {
            //        Name = item.
            //    }
            //}
        }
    }
}