using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Emgu.CV;
using Emgu.CV.Structure;

namespace Bejeweled3Bot
{
    class PlayArea
    {
        public Gem[,] Gems;
        public PlayArea(Image<Bgr, byte> screenshot)
        {
            Gems = new Gem[8, 8];
            UpdateWithScreenshot(screenshot);
        }
        public PlayArea(Gem[,] gems)
        {
            Gems = gems;
        }

        /// <summary>
        /// Update the current list of gems by detecting them from a screenshot
        /// </summary>
        /// <param name="screenshot">An image of the current playing area (without windowborders)</param>
        public void UpdateWithScreenshot(Image<Bgr,byte> screenshot)
        {
            ImageProcessing imageProcessing = new ImageProcessing();
            Image<Bgr, byte>[,] gemSlotImages = imageProcessing.ExtractGemSlots(screenshot);
            for(int row = 0;row < 8;row++)
            {
                for(int column = 0;column < 8;column++)
                {
                    Gems[row, column] = new Gem(gemSlotImages[row, column]);
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
