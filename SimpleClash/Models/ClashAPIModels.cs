using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SimpleClash.Models
{
    public class ClashVersion
    {
        [JsonProperty("premium")]
        public bool Premium { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }
    }

    /// <summary>
    /// 单位Byte
    /// </summary>
    public class Traffic
    {
        [JsonProperty("up")]
        public int Up { get; set; }
        [JsonProperty("down")]
        public int Down { get; set; }
    }

    /// <summary>
    /// 单个代理信息
    /// </summary>
    public class ProxyInfo
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("all")]
        public List<string> All { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("history")]
        public List<History> History { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("now")]
        public string Now { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("udp")]
        public bool Udp { get; set; }

    }

    public class Latency
    {
        [JsonProperty("delay")]
        public int Delay { get; set; }
    }

    public class History
    {
        [JsonProperty("time")]
        public DateTime Time { get; set; }

        /// <summary>
        /// ms
        /// </summary>
        [JsonProperty("delay")]
        public int Delay { get; set; }
    }

    public class ClashBaseConfig
    {
        [JsonProperty("port")]
        public int Port { get; set; }

        [JsonProperty("socks-port")]
        public int SocksPort { get; set; }

        [JsonProperty("redir-port")]
        public int RedirPort { get; set; }

        [JsonProperty("tproxy-port")]
        public int TproxyPort { get; set; }

        [JsonProperty("mixed-port")]
        public int MixedPort { get; set; }

        [JsonProperty("allow-lan")]
        public bool AllowLan { get; set; }

        [JsonProperty("bind-address")]
        public string BindAddress { get; set; }

        [JsonProperty("mode")]
        public string Mode { get; set; }

        [JsonProperty("log-level")]
        public string LogLevel { get; set; }

        [JsonProperty("ipv6")]
        public bool IPv6 { get; set; }
    }
}
