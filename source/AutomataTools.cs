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
        public static SKPoint3 CartesianVVVV(double pitch, double yaw, double length)
        {
            double cosp = -length * Math.Cos(pitch);

            return new SKPoint3((float)(cosp * Math.Sin(yaw)), (float)(length * Math.Sin(pitch)),(float) (cosp * Math.Cos(yaw)));
        }

        public static SKPoint3 PolarVVVV(SKPoint3 xyz)
        {
            float x = xyz.X;
            float y = xyz.Y;
            float z = xyz.Z;

            double length = x * x + y * y + z * z;

            if (length > 0)
            {
                length = Math.Sqrt(length);
                var pitch = Math.Asin(y / length);
                var yaw = 0.0;
                if (z != 0)
                    yaw = Math.Atan2(-x, -z);
                else if (x > 0)
                    yaw = -Math.PI / 2;
                else
                    yaw = Math.PI / 2;

                return new SKPoint3((float)pitch, (float)yaw, (float)length);
            }
            else
            {
                return new SKPoint3();
            }
        }




        public class EdgePoints
        {
            public SKPoint A
            {
                get;
                set;
            }

            public SKPoint B
            {
                get;
                set;
            }

            public SKPoint Center
            {
                get;
                set;
            }
        }

        //calculate edgepoints from state position and radius
        public static EdgePoints GetEdgePoints(SKPoint A, SKPoint B, int Radius, int Radius2, float winkel)
        {

            SKPoint3 PointA = new SKPoint3(A.X, A.Y, 0);
            SKPoint3 PointB = new SKPoint3(B.X, B.Y, 0);

            SKPoint3 TempA;
            SKPoint3 TempB;
            SKPoint3 TempC;

            SKPoint3 tempVector = PolarVVVV(PointA - PointB); //get Polar Values
            Console.WriteLine(tempVector);

            if (tempVector.Y > 0) // depending which quadrant of rotation
            {
                TempA = CartesianVVVV(tempVector.X + winkel, tempVector.Y, 0 - Radius) + PointA; //minus Radius from Length > into Cartesian
                TempB = CartesianVVVV(tempVector.X - winkel, tempVector.Y, Radius2) + PointB; //Radius is Length > into Cartesian
            }
            else
            {
                TempA = CartesianVVVV(tempVector.X - winkel, tempVector.Y, 0 - Radius) + PointA; //minus Radius from Length > into Cartesian
                TempB = CartesianVVVV(tempVector.X + winkel, tempVector.Y, Radius2) + PointB; //Radius is Length > into Cartesian
            }

            TempC = CartesianVVVV(PolarVVVV(TempA - TempB).X, PolarVVVV(TempA - TempB).Y, 0 - PolarVVVV(TempA - TempB).Z / 2.75f) + TempA; // calculate center

            EdgePoints myEdgeCoords = new EdgePoints(); // edgepoint definition

            myEdgeCoords.A = new SKPoint(TempA.X, TempA.Y); // create Point from Vector
            myEdgeCoords.B = new SKPoint(TempB.X,TempB.Y); // create Point from Vector
            myEdgeCoords.Center = new SKPoint(TempC.X, TempC.Y);

            return myEdgeCoords;
        }

    }


}
