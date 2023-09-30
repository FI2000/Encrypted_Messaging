using Encrypted_Messaging_C_.Network;
using System;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using messengerProject = Encrypted_Messaging_C_.Network;

namespace Encrypted_Message_UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Input : Page
    {
        private readonly string _userRole;
        private messengerProject.Messenger _user;
        public Input(string role)
        {
            _userRole = role;
            InitializeComponent();
            Debug.WriteLine($"Got role {_userRole}");
            if (_userRole == "Client")
            {
                // Make both text boxes and IP label visible
                HostTextBox.Visibility = System.Windows.Visibility.Visible;
                HostLabel.Visibility = System.Windows.Visibility.Visible;
                PortTextBox.Visibility = System.Windows.Visibility.Visible;
            }
            else if (_userRole == "Listener")
            {
                // Make only the port text box visible
                HostTextBox.Visibility = System.Windows.Visibility.Hidden;
                HostLabel.Visibility = System.Windows.Visibility.Hidden;
                PortTextBox.Visibility = System.Windows.Visibility.Visible;        }
        }

        // Your Submit button click method
        private async void SubmitButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            // Extract values from text boxes
            string host = HostTextBox.Text;
            string port = PortTextBox.Text;

            // Now, you can use these values for further processing
            Debug.WriteLine($"Host: {host}, Port: {port}");

            if (_userRole == "Client")
            {
                if ((!Network.IsValidIp(host) && host != "localhost") || !Network.IsValidPort(port))
                {
                    MessageBox.Show("Invalid IP or Port. Please enter a valid IP and Port.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                MessageBox.Show("Attempting to connect. Please wait...", "Info", MessageBoxButton.OK, MessageBoxImage.Information);

                _user = new Client(host, port);

                Console.WriteLine("Connected. Enter 'exit' to exit.");

            }
            else if (_userRole == "Listener")
            {
                if (!Network.IsValidPort(port))
                {
                    MessageBox.Show("Invalid Port. Please enter a valid Port.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                MessageBox.Show("Waiting for a peer to make a connection. Please wait...", "Info", MessageBoxButton.OK, MessageBoxImage.Information);

                _user = new Listener();
                Listener listenerUser = _user as Listener;
                Task listenTask = listenerUser.ListenAsync(port);

                Debug.WriteLine("Started listening...");

                while (!Listener.clientConnected)
                {
                    await Task.Delay(500);
                }

                await listenTask;
                Debug.WriteLine("Connected. Enter 'exit' to exit.");
            }

            string key = EncryptionKeyTextBox.Text;
            string ivKey = IvKeyTextBox.Text;
            NavigationService.Navigate(new TextPage(_user, key, ivKey));
        }
    }
}
