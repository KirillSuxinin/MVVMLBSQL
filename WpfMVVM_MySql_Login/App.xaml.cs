using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace WpfMVVM_MySql_Login
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnExit(ExitEventArgs e)
        {
            if(e.ApplicationExitCode == 0)
            {
                WpfMVVM_MySql_Login.Properties.Settings.Default.Save();
                base.OnExit(e);
            }
            else
            {
                System.IO.File.WriteAllText("OnExit.log", e.ApplicationExitCode.ToString());
                base.OnExit(e);
            }
        }
    }
}
