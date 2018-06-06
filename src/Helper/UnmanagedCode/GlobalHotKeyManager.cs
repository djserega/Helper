using Helper.UnmanagedCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Helper
{
    internal partial class GlobalHotKeyManager : IDisposable
    {
        private GlobalHotKeyEvents _globalHotKeyEvents;
        private LowLevelKeyboardListener _listener;

        internal bool PressedLeftAlt { get; private set; }
        internal bool PressedPrntScr { get; private set; }
        
        private const Key _keyLeftAlt = Key.LeftAlt;
        private const Key _keyPrntScr = Key.PrintScreen;

        internal GlobalHotKeyManager(GlobalHotKeyEvents globalHotKeyEvents)
        {
            _globalHotKeyEvents = globalHotKeyEvents;

            _listener = new LowLevelKeyboardListener();
            _listener.OnKeyDown += _listener_OnKeyDown;
            _listener.OnKeyUp += _listener_OnKeyUp;

            _listener.HookKeyboard();
        }

        void _listener_OnKeyDown(object sender, KeyDownArgs e)
        {
            switch (e.KeyDown)
            {
                case _keyLeftAlt:
                    PressedLeftAlt = true;
                    break;
                case _keyPrntScr:
                    PressedPrntScr = true;
                    break;
            }

            if (PressedLeftAlt)
            {
                if (PressedPrntScr)
                {
                    PressedLeftAlt = false;
                    PressedPrntScr = false;
                    
                    _globalHotKeyEvents.EvokeOpenFormMessageEvent();
                }
            }
        }

        void _listener_OnKeyUp(object sender, KeyUpArgs e)
        {
            switch (e.KeyUp)
            {
                case _keyLeftAlt:
                    PressedLeftAlt = false;
                    break;
                case _keyPrntScr:
                    PressedPrntScr = false;
                    break;
            }
        }

        public void Dispose()
        {
            _listener.UnHookKeyboard();
        }
    }
}
