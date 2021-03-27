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


        // https://github.com/vvvv/vvvv-sdk/blob/develop/common/src/core/Utils/Math/VMath.cs //
        /// <summary>
        /// Convert polar coordinates (pitch, yaw, lenght) in radian to cartesian coordinates (x, y, z).
        /// To convert angles from cycles to radian, multiply them with VMath.CycToDec.
        /// </summary>
        public static SKPoint3 Polar2Cartesian(float pitch, float yaw, float length)
        {
            float cosp = -length * (float)Math.Cos(pitch);
            return new SKPoint3(cosp * (float)Math.Sin(yaw), length * (float)Math.Sin(pitch), cosp * (float)Math.Cos(yaw));
        }

        /// <summary>
        /// Convert cartesian coordinates (x, y, z) to polar coordinates (pitch, yaw, lenght) in radian.
        /// To convert the angles to cycles, multiply them with VMath.DegToCyc.
        /// </summary>
        public static SKPoint3 Cartesian2Polar(float x, float y, float z)
        {
            float length = x * x + y * y + z * z;

            if (length > 0)
            {
                length = (float)Math.Sqrt(length);
                return new SKPoint3((float)Math.Acos(z / length), (float)Math.Atan2(y, x), length);
            }
            else
            {
                return new SKPoint3();
            }

        }

        public const double DegToRad = 0.0174532925199432957692;
        public const double RadToDeg = 57.2957795130823208768;

    }


}
