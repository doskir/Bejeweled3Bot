using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Emgu.CV;
using Emgu.CV.Structure;

namespace Bejeweled3Bot
{
    class GameWindow
    {
        const string WINDOWNAME = "Bejeweled 3";

        [DllImport("User32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool PrintWindow(IntPtr hwnd, IntPtr hDC, uint nFlags);

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        public Image<Bgr,byte> TakeScreenshot()
        {
            IntPtr windowHandle = FindWindow(null, WINDOWNAME);
            Bitmap frame = new Bitmap(1024, 768);
            Graphics g = Graphics.FromImage(frame);
            IntPtr dc = g.GetHdc();
            PrintWindow(windowHandle, dc,1);
            g.ReleaseHdc();
            //we have the bitmap now
            //turn it into an Image for emgu
            BitmapData bmpData = frame.LockBits(new Rectangle(0, 0, frame.Width, frame.Height), ImageLockMode.ReadWrite,
                                                PixelFormat.Format24bppRgb);

            Image<Bgr, byte> tempImage = new Image<Bgr, byte>(frame.Width, frame.Height, bmpData.Stride, bmpData.Scan0);
            //to prevent any corrupted memory errors that crop up for some reason
            Image<Bgr, byte> image = tempImage.Clone();
            frame.UnlockBits(bmpData);
            //dispose all unused image data to prevent memory leaks
            frame.Dispose();
            tempImage.Dispose();

            return image;
        }
    }
}
