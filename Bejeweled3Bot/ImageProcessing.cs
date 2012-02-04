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
        /// <summary>
        /// Extracts each gems rectangle in the image
        /// </summary>
        /// <param name="screenshot">An image of the current playing area (without windowborders)</param>
        /// <returns>A two-dimensional array of images in the [row,column] format</returns>
        public Image<Bgr,byte>[,] ExtractGemSlots(Image<Bgr,byte> screenshot,GemSlot[,] gemSlots)
        {
            Image<Bgr, byte>[,] gemSlotImages = new Image<Bgr, byte>[8,8];
            //values are for 1024 by 768 pixels
            //each gem slot is 82x82 pixels
            //the top left gem slots top left point is at 334,47
            Point baseOffset = new Point(334, 47);
            Size gemAreaSize = new Size(82, 82);
            for(int row = 0;row < 8;row++)
            {
                for(int column = 0;column < 8;column++)
                {
                    Rectangle gemSlotRectangle = gemSlots[row, column].Rectangle;
                    gemSlotImages[row, column] = screenshot.Copy(gemSlotRectangle);
                }
            }
            return gemSlotImages;
        }
    }
}
