using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomataUI
{
    public class Tools
    {
        // static functions
        public static SKPoint ToWorldSpace(SKPoint position, SKPoint worldOffset, float worldScale)
        {
            return new SKPoint(position.X / worldScale, position.Y / worldScale) - worldOffset;
        }
    }


}
