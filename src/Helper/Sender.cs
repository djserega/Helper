using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper
{
    internal class Sender
    {
        internal SenderInfo SenderInfo { get; set; }

        internal void SendMessage()
        {
            if (SenderInfo == null)
                throw new NullReferenceException("Не заполнен объект отправки сообщения.");


        }
    }
}
