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
using System.Windows.Shapes;

namespace Helper
{
    /// <summary>
    /// Логика взаимодействия для StartUpWindow.xaml
    /// </summary>
    public partial class StartUpWindow : Window
    {
        private Notify _notify;
        private NotifyIconEvents _notifyIconEvents;

        public StartUpWindow()
        {
            InitializeComponent();

            _notifyIconEvents = new NotifyIconEvents();
            _notifyIconEvents.NotifyIconEvent += _notifyIconEvents_NotifyIconEvent;

            _notify = new Notify(_notifyIconEvents);
        }

        private void _notifyIconEvents_NotifyIconEvent()
        {
            ShowMainWindow();
        }

        private void ButtonHelper_Click(object sender, RoutedEventArgs e)
        {
            ShowMainWindow();
        }

        private void ShowMainWindow()
        {
            Visibility = Visibility.Collapsed;
            new MainWindow().ShowDialog();
            Visibility = Visibility.Visible;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            _notify.Dispose();
        }
    }
}
