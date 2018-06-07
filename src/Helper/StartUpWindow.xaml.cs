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
        private bool _runWithAdmin;
        private Notify _notify;
        private NotifyIconEvents _notifyIconEvents;
        private GlobalHotKeyEvents _globalHotKeyEvents;
        private GlobalHotKeyManager _globalHotKeyManager;

        public StartUpWindow()
        {
            InitializeComponent();

            CheckCommandLineArgs();
            
            _globalHotKeyEvents = new GlobalHotKeyEvents();
            _globalHotKeyEvents.GlobalHotKeyOpenFormMessage += _globalHotKeyEvents_GlobalHotKeyOpenFormMessage;

            _notifyIconEvents = new NotifyIconEvents();
            _notifyIconEvents.NotifyIconOpenFormEvent += _notifyIconEvents_NotifyIconOpenFormEvent;
            _notifyIconEvents.NotifyIconShowFormMessageEvent += _notifyIconEvents_NotifyIconShowFormMessageEvent;
            _notifyIconEvents.NotifyIconExitAppEvent += _notifyIconEvents_NotifyIconExitAppEvent;

            _notify = new Notify(_notifyIconEvents);

            _globalHotKeyManager = new GlobalHotKeyManager(_globalHotKeyEvents);

        }

        private void _globalHotKeyEvents_GlobalHotKeyOpenFormMessage()
        {
            ShowMainWindow();
        }

        private void _notifyIconEvents_NotifyIconExitAppEvent()
        {
            Application.Current.Shutdown();
        }

        private void _notifyIconEvents_NotifyIconShowFormMessageEvent()
        {
            ShowMainWindow();
        }

        private void _notifyIconEvents_NotifyIconOpenFormEvent()
        {
            Show();
        }

        private void ButtonHelper_Click(object sender, RoutedEventArgs e)
        {
            ShowMainWindow();
        }

        private void ShowMainWindow()
        {
            Visibility = Visibility.Collapsed;
            new MainWindow().ShowDialog();
            Hide();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            _notify.Dispose();
        }

        private void ButtonHideToTray_Click(object sender, RoutedEventArgs e)
        {
            Hide();
        }

        private void ButtonHideToTray_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (_globalHotKeyManager.PressedLeftCtrl)
            {
                if (!_runWithAdmin)
                {
                    if (Messages.Dialog("Для изменния состояния нужно запустить приложение от имени админисратора.\nЗапустить?", "Автозагрузка") == MessageBoxResult.OK)
                    {
                        new Permission().RunApplicationWithAdministrator();
                    }
                }
                else
                {
                    Permission permission = new Permission();
                    if (permission.GetStatusAutostart())
                    {
                        if (Messages.Dialog("Выключить с автозагрузки?", "Автозагрузка") == MessageBoxResult.OK)
                        {
                            permission.SetRemoveAutostart(false);
                        }
                    }
                    else
                    {
                        if (Messages.Dialog("Включить в автозагрузку?", "Автозагрузка") == MessageBoxResult.OK)
                        {
                            permission.SetRemoveAutostart(true);
                        }
                    }
                }
            }
        }

        private void CheckCommandLineArgs()
        {
            string[] commandLine = Environment.GetCommandLineArgs();

            _runWithAdmin = false;
            if (commandLine.Count() > 1)
            {
                _runWithAdmin = commandLine[1] == "/run from administrator";

                if (!_runWithAdmin)
                {
                    if (commandLine[1]  == "/hidetotray")
                    {
                        Hide();
                    }
                }
            }
        }
    }
}
