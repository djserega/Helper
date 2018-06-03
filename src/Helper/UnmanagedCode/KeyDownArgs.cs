using System;
using System.Windows.Input;

namespace Helper.UnmanagedCode
{
    internal class KeyDownArgs : EventArgs
    {
        public Key KeyDown { get; private set; }

        internal KeyDownArgs(Key key)
        {
            KeyDown = key;
        }
    }
}
