using System;
using System.Diagnostics;
using System.Runtime.InteropServices.JavaScript;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using messengerProject = Encrypted_Messaging_C_.Network;

namespace Encrypted_Message_UI
{
    /// <summary>
    /// Interaction logic for TextPage.xaml
    /// </summary>
    public partial class TextPage : Page
    {
        private messengerProject.Messenger _user;
        public TextPage(messengerProject.Messenger user)
        {
            InitializeComponent();
            _user = user;
            Run();
        }

        private async Task Run() {
            var receiveTask = ReceiveMessagesAsync();
            await Task.WhenAny(receiveTask);
        }
        private async void SendButton_Click(object sender, RoutedEventArgs e)
        {
            string message = MessageInputTextBox.Text;
            SendMessagesAsync(message);
            MessageInputTextBox.Clear();
        }

        private async void SendMessagesAsync(string messageToSend)
        {
            await _user.GetMessageHandler().SendMessageWithLengthAsync(messageToSend);
        }

        private async Task ReceiveMessagesAsync()
        {
            while (true)
            {
                string receivedMessage = await _user.GetMessageHandler().ReceiveMessageWithLengthAsync();
                ReceivedMessagesTextBox.Text += $"~{receivedMessage}\n";
            }
        }
    }
}
