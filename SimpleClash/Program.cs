using SimpleClash.API;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleClash
{
    public class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            SimpleClash.App app = new SimpleClash.App();
            app.InitializeComponent();

            //加载程序的配置文件
            AppConfig.LoadConfig();
            //启动启动Clash
            LunchClashCore();

            //程序退出动作
            app.Exit += App_Exit;

            app.Run();
        }

        private static void App_Exit(object sender, System.Windows.ExitEventArgs e)
        {
            AppConfig.SaveConfig();
            var por = Process.GetProcessesByName("clash-windows-amd64");
            if (por.Length > 0)
                por[0].Kill();
        }

        private static void LunchClashCore()
        {
            var test = AppConfig.Instance.GetClashConfigArg();
            var pInfo = new ProcessStartInfo
            {
                FileName = $"{Environment.CurrentDirectory}/ClashCore/clash-windows-amd64.exe",
                Arguments = AppConfig.Instance.GetClashConfigArg(),
                WindowStyle = ProcessWindowStyle.Normal
            };
            Process.Start(pInfo);
            Task.Run(() => { Thread.Sleep(1000); ClashAPI.ReloadConfig(); });
        }
    }
}
