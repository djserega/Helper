using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Security.Principal;
using System.Text;
using System.Windows;

namespace Helper
{
    internal class Permission
    {
        private readonly string _fullPathApplication = Assembly.GetExecutingAssembly().Location;
        private readonly string _nameApplication;
        private readonly string _subKeyAutorun = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run\";

        internal Permission()
        {
            _nameApplication = Path.GetFileNameWithoutExtension(new FileInfo(_fullPathApplication).Name);
        }

        internal bool GetStatusAutostart()
        {
            try
            {
                return StatusAutostart();
            }
            catch (Exception ex)
            {
                Messages.Show($"Не удалось получить статус автозапуска.\nПричина: {ex.Message}");
                return false;
            }
        }

        internal void RunApplicationWithAdministrator() => RunWithAdministrator();

        internal bool SetRemoveAutostart(bool status)
        {
            try
            {
                if (!new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator))
                {
                    RunWithAdministrator();
                    return false;
                };

                if (status)
                    SetAutostart();
                else
                    RemoveAutostart();

                return true;
            }
            catch (Exception ex)
            {
                if (status)
                    Messages.Show($"Не удалось подключить автозапуск.\nПричина: {ex.Message}");
                else
                    Messages.Show($"Не удалось отключить автозапуск.\nПричина: {ex.Message}");

                return false;
            }
        }

        private bool StatusAutostart()
        {
            using (RegistryKey key = GetRegistryKey())
            {
                object status = key.GetValue(_nameApplication);
                return status != null;
            };
        }

        private void SetAutostart()
        {
            using (RegistryKey key = GetRegistryKey(true))
            {
                StringBuilder stringBuilder = new StringBuilder(_fullPathApplication);
                stringBuilder.Append(" ");
                stringBuilder.Append("/hidetotray");
                key.SetValue(_nameApplication, stringBuilder.ToString());
            };
        }

        private void RemoveAutostart()
        {
            using (RegistryKey key = GetRegistryKey(true))
            {
                key.DeleteValue(_nameApplication);
            };
        }

        private RegistryKey GetRegistryKey(bool writable = false)
        {
            return Registry.CurrentUser.OpenSubKey(_subKeyAutorun, writable);
        }

        private void RunWithAdministrator()
        {
            try
            {
                Process.Start(new ProcessStartInfo(_fullPathApplication, "/\"run from administrator\"")
                {
                    Verb = "runas"
                });
                Application.Current.Shutdown();
            }
            catch (Win32Exception) { }
            catch (Exception)
            {
                Messages.Show("Ошибка повышения прав.");
            }
        }
    }
}
