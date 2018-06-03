using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Helper
{
    internal class Sender
    {
        internal SenderInfo SenderInfo { get; set; }

        internal bool SendMessage()
        {
            if (SenderInfo == null)
                throw new NullReferenceException("Не заполнен объект отправки сообщения.");

            DefaultSettings settings = new DefaultSettings();

            try
            {
                settings.GetDefaultSettings();
            }
            catch (Exception)
            {
                try
                {
                    settings.SetDefaultSettings();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                return false;
            }
            

            return true;
        }
    }
}
