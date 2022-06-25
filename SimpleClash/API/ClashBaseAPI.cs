using SimpleClash.Helpers;
using SimpleClash.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SimpleClash.API
{
    internal static class ClashBaseAPI
    {
        private static string Url = "http://localhost:8080";

        internal static Result<string> GetVersion()
        {
            return HttpHelper.Get(Url, "/version");
        }

        internal static Result<string> GetProxies()
        {
            return HttpHelper.Get(Url, "/proxies");
        }
    }
}
