using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Helper
{
    internal class Sender : IDisposable
    {
        private string _tempDirectory;

        internal string Subject { get; set; }
        internal string Text { get; set; }
        internal Bitmap[] Screens { get; private set; }

        public Sender()
        {
            _tempDirectory = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "Hepler");
        }

        public void Dispose()
        {
            try
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(_tempDirectory);
                if (directoryInfo.Exists)
                    directoryInfo.Delete();
            }
            catch (Exception)
            {
            }
        }

        internal void GetScreens(byte count = 1)
        {
            if (count < 1)
                count = 1;

            int screenLeft = (int)SystemParameters.VirtualScreenLeft;
            int screenTop = (int)SystemParameters.VirtualScreenTop;
            int screenWidth = (int)SystemParameters.VirtualScreenWidth;
            int screenHeight = (int)SystemParameters.VirtualScreenHeight;

            List<Bitmap> listBitMap = new List<Bitmap>();

            using (Bitmap bitmap = new Bitmap(screenWidth, screenHeight))
            {
                using (Graphics graphics = Graphics.FromImage(bitmap))
                {
                    graphics.CopyFromScreen(screenLeft, screenTop, 0, 0, bitmap.Size);
                    listBitMap.Add(bitmap);                                     

                    //string filename = "screen-" + DateTime.Now.ToString("yyyyMMdd_hhmmss") + ".png";
                    //bitmap.Save(Path.Combine(_tempDirectory, filename));
                }
            }

            Screens = listBitMap.ToArray();

        }

    }
}
