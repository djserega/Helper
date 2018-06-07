using System.Threading.Tasks;
using System.Windows;

namespace Helper
{
    internal static class Messages
    {
        internal static MessageBoxResult Dialog(string text, string caption)
        {
            return MessageBox.Show(text, caption, MessageBoxButton.OKCancel, MessageBoxImage.Warning, MessageBoxResult.Cancel);
        }
        internal static void Show(string text)
        {
            Task.Run(() => MessageBox.Show(text));
        }
    }
}
