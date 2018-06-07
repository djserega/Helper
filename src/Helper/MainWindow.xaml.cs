using System;
using System.Windows;

namespace Helper
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string MessageSubject { get; set; }
        public string MessageText { get; set; }
        private SenderInfo _senderInfo = new SenderInfo();

        public MainWindow()
        {
            InitializeComponent();

            string textLogo = new DefaultSettings(true).TextLogo;
            Title += textLogo;
            LabelTextLogo.Content = textLogo;

            _senderInfo.GetScreens();

            DataContext = this;
        }

        private void ButtonSendMessage_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(MessageSubject))
            {
                MessageBox.Show("Не заполнена тема.");
                TextBoxMessageSubject.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(MessageText))
            {
                MessageBox.Show("Не заполнено описание.");
                TextBoxMessageText.Focus();
                return;
            }

            _senderInfo.Subject = MessageSubject;
            _senderInfo.Text = MessageText;

            Sender senderMail = new Sender() { SenderInfo = _senderInfo };
            if (senderMail.SendMessage())
            {
                MessageBox.Show("Сообщение успешно отправлено.");
                Close();
            }
            else
            {
                MessageBox.Show("Ошибка отправки сообщения.");
            }
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void FormMainWindow_Closed(object sender, EventArgs e)
        {
            _senderInfo.Dispose();
        }
    }
}
