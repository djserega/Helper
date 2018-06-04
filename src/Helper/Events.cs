using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper
{
    internal delegate void GlobalHotKeyEvent();

    internal class GlobalHotKeyEvents : EventArgs
    {
        internal event GlobalHotKeyEvent GlobalHotKeyOpenFormMessage;

        internal void EvokeOpenFormMessageEvent()
        {
            if (GlobalHotKeyOpenFormMessage == null)
                return;

            GlobalHotKeyOpenFormMessage();
        }
    }


    internal delegate void NotifyIconEvent();

    internal class NotifyIconEvents : EventArgs
    {
        internal event NotifyIconEvent NotifyIconEvent;

        internal void EvokeNotifyIconEvent()
        {
            if (NotifyIconEvent == null)
                return;

            NotifyIconEvent();
        }
    }
}
