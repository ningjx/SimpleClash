using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleClash.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleClash.Helpers.Tests
{
    [TestClass()]
    public class HttpHelperTests
    {
        [TestMethod()]
        public void PutTest()
        {
            var res = HttpHelper.Get("http://localhost:8080/", "/proxies");
        }

        [TestMethod()]
        public void PutTest1()
        {
            var res = HttpHelper.Put("http://localhost:8080", "/proxies", "全部节点", "{\"Name\":\"SS美国2\"}");
        }
    }
}