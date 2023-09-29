using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

namespace Encrypted_Message_UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            NavigationWindow window = new NavigationWindow
            {
                ShowsNavigationUI = false,
                Height = 320,
                Width = 320,
        };
            window.Content = new ClientOrListener();
            window.ResizeMode = ResizeMode.NoResize;
            window.Show();
            base.OnStartup(e);
        }
    }
}
