using SimpleClash.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SimpleClash.Helpers
{
    public static class HttpHelper
    {
        public static Result<string> Get(string url, string api)
        {
            url = "http://localhost:8080/";
            url = url.TrimEnd('/');

            var request = WebRequest.Create($"{url}{api}");
            request.Method = "GET";

            using (var response = request.GetResponse() as HttpWebResponse)
            {
                //if (resp.StatusCode != HttpStatusCode.OK)
                //    return Result<string>.Error();

                using (var reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    var dataStr = reader.ReadToEnd().ToString();

                    return new Result<string>
                    {
                        Code = response.StatusCode,
                        Message = dataStr,
                        Data = dataStr
                    };
                }
            }
        }

        public static Result<string> Put(string url, string api, string query, string body)
        {
            url = "http://localhost:8080/";
            url = url.TrimEnd('/');
            query = "/" + HttpUtility.UrlEncode(query, Encoding.UTF8);

            var bytes = Encoding.UTF8.GetBytes(body);

            var request = WebRequest.Create($"{url}{api}{query}");
            request.Method = "PUT";
            request.ContentType = "text/plain";
            request.ContentLength = bytes.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(bytes, 0, bytes.Length);
            }

            using (var response = request.GetResponse() as HttpWebResponse)
            {
                using (var reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    var dataStr = reader.ReadToEnd().ToString();

                    return new Result<string>
                    {
                        Code = response.StatusCode,
                        Message = dataStr,
                        Data = dataStr
                    };
                }
            }
        }
    }
}
