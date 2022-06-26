using System;

namespace SimpleClash.Models
{
    public class ConfigFile
    {
        public string FileName { get; set; }
        public string FileFullPath { get; set; }
        public int HashCode { get; set; }
        public string Url { get; set; }
        public ConfigFileType Type { get; set; }
        public string SubLink { get; set; }
        public bool Active { get; set; }
        public DateTime CreateTime { get; set; } = DateTime.Now;
    }

    public enum ConfigFileType
    {
        Url, LocalFile, Subscription
    }
}
