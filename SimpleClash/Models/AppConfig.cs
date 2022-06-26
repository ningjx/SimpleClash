using Newtonsoft.Json;
using SimpleClash.Helpers;
using SimpleClash.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace SimpleClash
{
    public class AppConfig
    {
        private static string ConfigPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\SimpleClash\\SimpleClashConfig.json";

        #region 程序配置文件字段
        public string CurrentConfigPath { get; set; }

        public ClashBaseConfig ClashBaseConfig { get; set; }

        public bool AutoStart { get; set; }
        public Key SystemProxyKey { get; set; }

        public List<ConfigFile> ClashConfigs { get; set; }

        public bool PortsOverride { get; set; } = false;

        [JsonIgnore]
        public string CurrentClashConfigPath
        {
            get
            {
                if (ClashConfigs != null && ClashConfigs.Count != 0)
                {
                    var currentConfig = ClashConfigs.Where(l => l.Active).FirstOrDefault();
                    if (currentConfig != null && !string.IsNullOrEmpty(currentConfig.FileFullPath))
                        return currentConfig.FileFullPath;
                    else
                        return string.Empty;
                }
                else
                    return string.Empty;
            }
        }

        #endregion

        private static AppConfig instance = null;
        public static AppConfig Instance => instance;

        private AppConfig()
        {

        }

        /// <summary>
        /// 获取默认Clash配置
        /// </summary>
        /// <returns></returns>
        public string GetClashConfigArg()
        {
            return $"-f {Environment.CurrentDirectory + "\\Resources\\config.yaml"}";
        }

        /// <summary>
        /// 加载配置文件
        /// 必须先通过该方法加载AppConfig实例
        /// </summary>
        public static void LoadConfig()
        {
            var configStr = FileHelper.Read(ConfigPath);
            if (configStr != null)
            {
                instance = JsonConvert.DeserializeObject<AppConfig>(configStr);
            }
            else
            {
                instance = new AppConfig();
                SaveConfig();
            }
        }

        /// <summary>
        /// 保存App的配置文件
        /// </summary>
        public static void SaveConfig()
        {
            if (Instance != null)
                FileHelper.Write(ConfigPath, JsonConvert.SerializeObject(Instance));
        }
    }
}
