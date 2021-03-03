using SkiaSharp;
using SkiaSharp.Views.Desktop;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void skiaView_PaintSurface(object sender, SKPaintGLSurfaceEventArgs e)
        {
            // the the canvas and properties
            var canvas = e.Surface.Canvas;

            // get the screen density for scaling



            // handle the device screen density
            canvas.Scale(worldScale);
            canvas.Translate(worldOffset);


            // make sure the canvas is blank
            canvas.Clear(SKColors.Coral);


            // draw some text
            var paint = new SKPaint
            {
                Color = SKColors.Black,
                IsAntialias = true,
                Style = SKPaintStyle.Fill,
                TextAlign = SKTextAlign.Right,
                Typeface = SKTypeface.FromFamilyName("CoText_Bd"),
                TextSize = 24
            };
            var coord = new SKPoint(100, 100);
            canvas.DrawText("SkiaSharp", coord, paint);


            var paint2 = new SKPaint
            {
                Color = SKColors.Blue,
                IsAntialias = true,
                StrokeWidth = 3,
                Style = SKPaintStyle.Stroke

            };
            //SKPoint circlePos = new SKPoint(
            //    (PointToClient(MousePosition).X - worldOffset.X) / worldScale,
            //    (PointToClient(MousePosition).Y - worldOffset.Y) / worldScale);
            canvas.DrawCircle(new SKPoint(10, 10), 30, paint2); //arguments are x position, y position, radius, and paint

            using (SKPath path = new SKPath())
            {
                path.MoveTo(100, 100);
                path.CubicTo(100, 200,
                             200, 300,
                             400, 100);

                canvas.DrawPath(path, paint2);


                if (path.Contains(PointToClient(MousePosition).X, PointToClient(MousePosition).Y))
                {
                    // Console.WriteLine("path hit");
                }
            }
        }

        Point previousMousePosition;
        SKPoint worldOffset;
        float worldScale = 1;
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {

            // drag position
            Point mousePos = MousePosition;
            if (e.Button == MouseButtons.Left)
            {
                Console.WriteLine("left click");
                worldOffset.X += (MousePosition.X - previousMousePosition.X) / worldScale;
                worldOffset.Y += (MousePosition.Y - previousMousePosition.Y) / worldScale;
            }

            previousMousePosition = mousePos;

            skiaView.Invalidate();

        }
        private void Form1_MouseWheel(object sender, MouseEventArgs e)
        {

            Point mousePos = MousePosition;
           

            if (e.Delta > 0)
            {
                worldScale *= 1.03f;
            }

            if (e.Delta < 0)
            {
                worldScale *= 0.97f;
            }
            worldOffset.X -= (MousePosition.X - previousMousePosition.X) / worldScale;
            worldOffset.Y -= (MousePosition.Y - previousMousePosition.Y) / worldScale;
            previousMousePosition = mousePos;

            skiaView.Invalidate();
            
        }
    }
}