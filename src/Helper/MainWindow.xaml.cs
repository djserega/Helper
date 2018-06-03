using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Helper
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string MessageSubject { get; set; }
        public string MessageText { get; set; }
        private SenderInfo _senderInfo;

        public MainWindow()
        {
            InitializeComponent();

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
