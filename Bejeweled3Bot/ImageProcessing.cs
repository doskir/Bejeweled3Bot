using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Emgu.CV;
using Emgu.CV.Structure;

namespace Bejeweled3Bot
{
    class ImageProcessing
    {

        public Image<Bgr,byte>[,] ExtractGemSlots(Image<Bgr,byte> screenshot)
        {
            Image<Bgr, byte>[,] gemSlots = new Image<Bgr, byte>[8,8];
            //values are for 1024 by 768 pixels
            //each gem slot is 82x82 pixels
            //the top left gem slots top left point is at 334,47
            Point baseOffset = new Point(334, 47);
            Size gemAreaSize = new Size(82, 82);
            for(int row = 0;row < 8;row++)
            {
                for(int column = 0;column < 8;column++)
                {
                    Point offset = new Point(column*82, row*82);
                    Point topLeft = new Point(baseOffset.X + offset.X, baseOffset.Y + offset.Y);
                    Rectangle gemSlotRectangle = new Rectangle(topLeft, gemAreaSize);
                    gemSlots[row, column] = screenshot.Copy(gemSlotRectangle);
                }
            }
            return gemSlots;
        }
    }
}
