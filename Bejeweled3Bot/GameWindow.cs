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
    internal class GameWindow
    {
        private const string WINDOWNAME = "Bejeweled 3";
        private const string WINDOWCLASS = "MainWindow";



        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("User32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool PrintWindow(IntPtr hwnd, IntPtr hDC, uint nFlags);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);

        public enum PostMessageFlags
        {
            MK_LBUTTON = 0x0001,
            WM_LBUTTONDOWN = 0x0201,
            WM_LBUTTONUP = 0x0202,
            WM_MOUSEMOVE = 0x0200,
            WM_ACTIVATEAPP = 0x001C
        }

        private IntPtr GetGameWindowHandle()
        {
            return FindWindow(WINDOWCLASS, WINDOWNAME);
        }

        public Image<Bgr, byte> TakeScreenshot()
        {
            Bitmap frame = new Bitmap(1024, 768);
            using (Graphics g = Graphics.FromImage(frame))
            {
                IntPtr deviceContextHandle = g.GetHdc();
                PrintWindow(GetGameWindowHandle(), deviceContextHandle, 1);
                g.ReleaseHdc();
            }
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
            image.Save("screenshot.png");

            return image;
        }

        public void Click(int x, int y)
        {
            IntPtr windowHandle = GetGameWindowHandle();
            var pos = new IntPtr(y*0x10000 + x);
            SendMessage(windowHandle, (uint) PostMessageFlags.WM_LBUTTONDOWN,
                        new IntPtr((int) PostMessageFlags.MK_LBUTTON), pos);
            System.Threading.Thread.Sleep(10);
            SendMessage(windowHandle, (int) PostMessageFlags.WM_LBUTTONUP, new IntPtr((int) PostMessageFlags.MK_LBUTTON),
                        pos);
            System.Threading.Thread.Sleep(10);
        }
        public void UnpauseWindow()
        {
            SendMessage(GetGameWindowHandle(), 0x001c, (IntPtr) 1, (IntPtr) 0);
        }
    }
}
