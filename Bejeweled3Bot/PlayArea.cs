using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Emgu.CV;
using Emgu.CV.Structure;

namespace Bejeweled3Bot
{
    class PlayArea
    {
        public GemSlot[,] GemSlots;
        public PlayArea(Image<Bgr, byte> screenshot)
        {
            GemSlots = CreateGemslots(screenshot.Bitmap.Size);
            UpdateWithScreenshot(screenshot);
        }
        public PlayArea(GemSlot[,] gemSlots)
        {
            GemSlots = gemSlots;
        }
        /// <summary>
        /// Create the grid of gem slots
        /// </summary>
        /// <param name="playAreaResolution">The resolution of the game</param>
        /// <returns>The grid of gem slots in the [row,column] format</returns>
        public GemSlot[,] CreateGemslots(Size playAreaResolution)
        {
            GemSlot[,] gemSlots = new GemSlot[8,8];
            Point baseOffset = Point.Empty;
            Size gemAreaSize = Size.Empty;
            if (playAreaResolution.Width == 1024 && playAreaResolution.Height == 768)
            {
                baseOffset = new Point(334, 47);
                gemAreaSize = new Size(82, 82);
            }
            else
            {
                throw new Exception("Wrong resolution, only 1024x768 is accepted");
            }
            for (int row = 0; row < 8; row++)
            {
                for (int column = 0; column < 8; column++)
                {
                    Point offset = new Point(column * gemAreaSize.Width, row * gemAreaSize.Height);
                    Point topLeft = new Point(baseOffset.X + offset.X, baseOffset.Y + offset.Y);
                    gemSlots[row, column] = new GemSlot(new Rectangle(topLeft, gemAreaSize));
                }

            }
            return gemSlots;
        }
        /// <summary>
        /// Update the current list of gems by detecting them from a screenshot
        /// </summary>
        /// <param name="screenshot">An image of the current playing area (without windowborders)</param>
        public void UpdateWithScreenshot(Image<Bgr,byte> screenshot)
        {
            ImageProcessing imageProcessing = new ImageProcessing();
            Image<Bgr, byte>[,] gemSlotImages = imageProcessing.ExtractGemSlots(screenshot, GemSlots);
            for(int row = 0;row < 8;row++)
            {
                for(int column = 0;column < 8;column++)
                {
                    GemSlots[row, column].Gem = new Gem(gemSlotImages[row, column]);
                }
            }
        }
        /// <summary>
        /// Get the move that will result in the highest score after the specified number of moves
        /// </summary>
        /// <param name="moves">The number of moves to look ahead, 1 means the current move</param>
        /// <returns>The best move</returns>
        public Move GetBestMove(int moves)
        {
            if (moves < 1)
                throw new Exception();

            return new Move(0, 0, 0, 0);
        }
    }
}
