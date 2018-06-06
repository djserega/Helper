using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WF = System.Windows.Forms;
using System.Reflection;

namespace Helper
{
    internal class Notify : IDisposable
    {
        private WF.NotifyIcon _notifyIcon;
        private NotifyIconEvents _notifyIconEvents;

        internal Notify(NotifyIconEvents notifyIconEvents)
        {
            _notifyIconEvents = notifyIconEvents;

            _notifyIcon = new WF.NotifyIcon()
            {
                BalloonTipIcon = WF.ToolTipIcon.Info,
                ContextMenu = CreateContextMenu(),
                Icon = Icon.ExtractAssociatedIcon(Assembly.GetExecutingAssembly().Location)
            };
            _notifyIcon.MouseDoubleClick += _notifyIcon_MouseDoubleClick;

            _notifyIcon.Visible = true;
        }

        public void Dispose()
        {
            _notifyIcon.Visible = false;
        }

        private void _notifyIcon_MouseDoubleClick(object sender, WF.MouseEventArgs e)
        {
            _notifyIconEvents.EvokeNotifyIconShowFormMessageEvent();
        }

        private WF.ContextMenu CreateContextMenu()
        {
            WF.ContextMenu ContextMenuHelper = new WF.ContextMenu();

            var HelperMenu = ContextMenuHelper.MenuItems;

            WF.MenuItem HelperElementSend = HelperMenu.Add("Отправить ошибку");
            HelperElementSend.Click += HelperElement_Send_Click;
            HelperElementSend.DefaultItem = true;
            HelperElementSend.Name = "SendError";

            WF.MenuItem HelperElementOpen = HelperMenu.Add("Открыть");
            HelperElementOpen.Click += HelperElementOpen_Click;
            HelperElementOpen.Name = "OpenForm";

            HelperMenu.Add("-");

            WF.MenuItem HelperElementExit = HelperMenu.Add("Выйти");
            HelperElementExit.Click += HelperElementExit_Click;
            HelperElementExit.Name = "ExitHelper";

            return ContextMenuHelper;
        }

        private void HelperElementOpen_Click(object sender, EventArgs e)
        {
            _notifyIconEvents.EvokeNotifyIconOpenFormEvent();
        }

        private void HelperElementExit_Click(object sender, EventArgs e)
        {
            _notifyIconEvents.EvokeNotifyIconExitAppEvent();
        }

        private void HelperElement_Send_Click(object sender, EventArgs e)
        {
            _notifyIconEvents.EvokeNotifyIconShowFormMessageEvent();
        }
    }
}
