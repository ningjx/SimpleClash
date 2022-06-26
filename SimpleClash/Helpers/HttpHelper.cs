using SimpleClash.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace SimpleClash.Helpers
{
    public static class HttpHelper
    {
        public static Result<string> Get(string url, string api)
        {
            url = url.TrimEnd('/');

            var request = WebRequest.Create($"{url}/{api}");
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

        public static Result<Stream> GetStream(string url)
        {
            url = url.TrimEnd('/');

            var request = WebRequest.Create(url);
            request.Method = "GET";

            using (var response = request.GetResponse() as HttpWebResponse)
            {
                //if (resp.StatusCode != HttpStatusCode.OK)
                //    return Result<string>.Error();

                return new Result<Stream>
                {
                    Code = response.StatusCode,
                    Message = "",
                    Data = response.GetResponseStream()
                };
            }
        }

        public static Result<string> Get(string url, string api, string query)
        {
            url = url.TrimEnd('/');

            var request = WebRequest.Create($"{url}/{api}/{query}");
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

        public static async void Get_Async(string url, string api, Action<string> action)
        {
            url = url.TrimEnd('/');

            await Task.Run(() =>
            {
                var request = WebRequest.Create($"{url}/{api}");
                request.Method = "GET";
                using (var response = request.GetResponse() as HttpWebResponse)
                {
                    var stream = response.GetResponseStream();
                    var bytes = new List<byte>();
                    int len = 0;
                    while (true)
                    {
                        var b = stream.ReadByte();
                        bytes.Add((byte)b);

                        len = CheckLength(b);
                        if (len > 1)
                        {
                            while (len > 0)
                            {
                                var nb = stream.ReadByte();
                                bytes.Add((byte)nb);
                                len--;
                            }
                        }
                        //decode
                        var str = Encoding.UTF8.GetString(bytes.ToArray());
                        bytes.Clear();
                        //invoke
                        action?.Invoke(str);
                        Thread.Sleep(0);
                    }
                }
            });
        }

        public static Result<string> Put(string url, string api, string query, string body)
        {
            url = url.TrimEnd('/');

            var bytes = Encoding.UTF8.GetBytes(body);

            var request = WebRequest.Create($"{url}/{api}/{query}");
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

        public static Result<string> Patch(string url, string api, string body)
        {
            url = url.TrimEnd('/');

            var bytes = Encoding.UTF8.GetBytes(body);

            var request = WebRequest.Create($"{url}/{api}");
            request.Method = "PATCH";
            request.ContentType = "text/plain";
            request.ContentLength = bytes.Length;
            using (var stream = request.GetRequestStream())
            {
                stream.Write(bytes, 0, bytes.Length);
            }

            using (var response = request.GetResponse() as HttpWebResponse)
            {
                return new Result<string>
                {
                    Code = response.StatusCode,
                    Message = "",
                    Data = null
                };
            }
        }

        private static int CheckLength(int b)
        {
            if (b < 0x80)
            {
                return 1;//占一个字节
            }
            else if (b < 0xe0)
            {
                return 2;//占2个字节
            }
            else if (b < 0xf0)
            {
                return 3;
            }
            else if (b < 0xf8)
            {
                return 4;
            }
            else if (b < 0xfc)
            {
                return 5;
            }
            else if (b < 0xfe)
            {
                return 6;
            }
            return -1;
        }
    }
}

