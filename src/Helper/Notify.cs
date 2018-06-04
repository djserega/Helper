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
                //ContextMenu = ,
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
            _notifyIconEvents.EvokeNotifyIconEvent();
        }
    }
}
