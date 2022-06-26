using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using SimpleClash.API;
using SimpleClash.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleClash.API.Tests
{
    [TestClass()]
    public class ClashAPITests
    {
        [TestMethod()]
        public void GetProxiesTest()
        {
            var res = ClashAPI.GetProxies();
        }

        [TestMethod()]
        public void GetLatencyTest()
        {
            var res = ClashAPI.GetLatency("全部节点");
        }

        [TestMethod()]
        public void GetProxyTest()
        {
            var res = ClashAPI.GetProxy("全部节点");
        }

        [TestMethod()]
        public void GetTrafficTest()
        {
            ClashAPI.GetTraffic(delegate (Traffic t) { Debug.WriteLine(JsonConvert.SerializeObject(t)); });
            Thread.Sleep(100000);
        }

        [TestMethod()]
        public void GetCurrentConfigTest()
        {
            var res = ClashAPI.GetCurrentConfig();
        }
    }
}