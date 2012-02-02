using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Emgu.CV;
using Emgu.CV.Structure;

namespace Bejeweled3Bot
{
    public enum GemColor
    {
        Unknown,
        Any,
        Blue,
        Green,
        Orange,
        Red,
        Violet,
        White,
        Yellow

    };

    public enum GemType
    {
        Unknown,Normal,Flame,Star,Hypercube,Supernova
    }

    class Gem
    {
        public GemColor Color;
        public GemType Type;
        /// <summary>
        /// Update the color and type of the this object by using machine learning to detect its type and color
        /// </summary>
        /// <param name="gemSlotImage">The image of the gem slot</param>
        /// <returns>True if the gem was successfully detected, False if the Color or Type are unknown</returns>
        public bool UpdateFromImage(Image<Bgr,byte> gemSlotImage)
        {
            //update the color and type here
            return false;
        }
        /// <summary>
        /// Create a new Gem by detecting the color and type of the gem using the provided image
        /// </summary>
        /// <param name="gemslotImage">Image of the gems slot</param>
        public Gem(Image<Bgr,byte> gemslotImage)
        {
            
        }
        public Gem(GemColor color,GemType type)
        {
            Color = color;
            Type = type;
        }

    }
}
