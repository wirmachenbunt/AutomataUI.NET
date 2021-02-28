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

        float mouseX;
        float mouseY;

        private void skiaView_PaintSurface(object sender, SKPaintGLSurfaceEventArgs e)
        {
            // the the canvas and properties
            var canvas = e.Surface.Canvas;

            // get the screen density for scaling
            var scale = 1f;
            //var scaledSize = new SKSize(e.Surface.Canvas. / scale, e.Info.Height / scale);

            // handle the device screen density
            canvas.Scale(scale);
            //canvas.Translate(1000, 0);

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
          //  var coord = new SKPoint(scaledSize.Width / 2, (scaledSize.Height + paint.TextSize) / 2);
            //canvas.DrawText("SkiaSharp", coord, paint);


            var paint2 = new SKPaint
            {
                Color = SKColors.Blue,
                IsAntialias = true,
                StrokeWidth = 3,
                Style = SKPaintStyle.Stroke

            };
            canvas.DrawCircle(mouseX, mouseY, 30, paint2); //arguments are x position, y position, radius, and paint

            using (SKPath path = new SKPath())
            {
                path.MoveTo(100, 100);
                path.CubicTo(100, 200,
                             200, 300,
                             400, 100);

                canvas.DrawPath(path, paint2);


                if (path.Contains(mouseX, mouseY))
                {
                    Console.WriteLine("path hit");
                }


            }

        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            //Console.WriteLine("hallo");

            mouseX = e.X;
            mouseY = e.Y;

            skiaView.Invalidate();

        }

    }
}