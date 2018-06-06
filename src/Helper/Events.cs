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


    internal delegate void NotifyIconOpenFormEvent();
    internal delegate void NotifyIconShowFormMessageEvent();
    internal delegate void NotifyIconExitAppEvent();


    internal class NotifyIconEvents : EventArgs
    {
        internal event NotifyIconOpenFormEvent NotifyIconOpenFormEvent;
        internal event NotifyIconShowFormMessageEvent NotifyIconShowFormMessageEvent;
        internal event NotifyIconExitAppEvent NotifyIconExitAppEvent;

        internal void EvokeNotifyIconOpenFormEvent()
        {
            if (NotifyIconOpenFormEvent == null)
                return;

            NotifyIconOpenFormEvent();
        }

        internal void EvokeNotifyIconShowFormMessageEvent()
        {
            if (NotifyIconShowFormMessageEvent == null)
                return;

            NotifyIconShowFormMessageEvent();
        }

        internal void EvokeNotifyIconExitAppEvent()
        {
            if (NotifyIconExitAppEvent == null)
                return;

            NotifyIconExitAppEvent();
        }
    }
}
