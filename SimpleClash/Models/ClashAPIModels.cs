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

    public class DelayInfo
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
}
