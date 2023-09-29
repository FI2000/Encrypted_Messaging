using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Diagnostics;
namespace Encrypted_Message_UI
{
    /// <summary>
    /// Interaction logic for ClientOrListener.xaml
    /// </summary>
    public partial class ClientOrListener : Page
    {
        public ClientOrListener()
        {
            InitializeComponent();
        }

        private void ClientButton_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to Input.xaml as Client
            Debug.WriteLine("Clicked Client");
            NavigationService.Navigate(new Input("Client")); // Need a Frame or NavigationWindow for this to not be null
        }

        private void ListenerButton_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to Input.xaml as Listener
            Debug.WriteLine("Clicked Listener");
            NavigationService.Navigate(new Input("Listener"));
        }
    }
}
