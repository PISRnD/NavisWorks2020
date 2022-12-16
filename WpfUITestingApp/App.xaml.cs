using DevExpress.Xpf.Core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace WpfUITestingApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
           ApplicationThemeHelper.ApplicationThemeName = PredefinedThemePalettes.Turquoise.Name + Theme.Office2019Colorful.Name;
           base.OnStartup(e);
        }
    }
}
