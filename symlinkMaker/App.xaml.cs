using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Versioning;
using System.Threading.Tasks;
using System.Windows;

namespace symlinkMaker
{
    /// <summary>
    /// App.xaml の相互作用ロジック
    /// </summary>
    public partial class App : Application
    {
        
        [SupportedOSPlatform("windows")]
        [STAThread]
        public static void Main()
        {
            //管理者権限で起動するとD&Dが機能しなくなるという

            System.Security.Principal.WindowsIdentity windowsIdentity = System.Security.Principal.WindowsIdentity.GetCurrent();
            System.Security.Principal.WindowsPrincipal windowsPrincipal = new System.Security.Principal.WindowsPrincipal(windowsIdentity);
            if (!windowsPrincipal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator))
            {
                MessageBox.Show("管理者権限で再起動します", "", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);

                var proc = new Process()
                {
                    StartInfo =
                     {
                        //FileName = Path.Combine(AppContext.BaseDirectory, Environment.GetCommandLineArgs()[0]),
                        FileName = Process.GetCurrentProcess().MainModule?.FileName,
                        UseShellExecute = true,
                        Verb = "runas"
                     }
                };

                //https://qiita.com/tada0724/items/645616a5d1deb55bfdb8
                //https://stackoverflow.com/questions/70252093/implement-restart-as-administrator-in-c-sharp-console-app
                //https://qiita.com/karuakun/items/4af234b5fd5ef4951b5e
                //https://kuttsun.blogspot.com/2019/12/net-core-publishsinglefile.html


                //var proc = new ProcessStartInfo()
                //{
                //    //WorkingDirectory = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule?.FileName),
                //    FileName = Process.GetCurrentProcess().MainModule?.FileName,
                //    Verb = "runas"
                //};

                proc.Start();
                //Process.Start(proc);

                return;

            }
            else
            {
            }


            App app = new App();
            app.InitializeComponent();
            app.Run();
        }
    }
}
