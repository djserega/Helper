using System;
using System.Collections.Generic;
using System.Drawing;
using Imaging = System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Mail;
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

            try
            {
                using (MailMessage mailMessage = new MailMessage()
                {
                    From = new MailAddress(settings.MailFrom),
                    Subject = SenderInfo.Subject,
                    Body = SenderInfo.Text
                })
                {
                    mailMessage.To.Add(new MailAddress(settings.MailTo));
                    mailMessage.Headers.Add("SenderApplication", "helper");

                    foreach (string fullName in SenderInfo.Screens)
                    {
                        mailMessage.Attachments.Add(new Attachment(fullName));
                    }

                    using (SmtpClient client = new SmtpClient(settings.Server, settings.Port)
                    {
                        EnableSsl = true,
                        UseDefaultCredentials = true,
                        Timeout = 10 * 1000
                    })
                    {
                        client.Send(mailMessage);
                    }
                };
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

        }
    }
}
