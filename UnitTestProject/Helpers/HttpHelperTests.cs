using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleClash.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleClash.Helpers.Tests
{
    [TestClass()]
    public class HttpHelperTests
    {
        [TestMethod()]
        public void GetTest()
        {
            var res = HttpHelper.Get("http://localhost:8080/", "/version");
        }

        [TestMethod()]
        public void PutTest()
        {
            var res = HttpHelper.Put("http://localhost:8080", "/proxies", "全部节点", "{\"Name\":\"SS美国2\"}");
        }

        [TestMethod()]
        public void Get_AsyncTest()
        {
            Debug.Write("\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\");
            HttpHelper.Get_Async("http://localhost:8080/", "/traffic", delegate (string str) { Debug.Write(str); });
            Thread.Sleep(100000);
        }
    }
}