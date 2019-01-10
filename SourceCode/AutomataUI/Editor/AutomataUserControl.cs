using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SkiaSharp;
using SkiaSharp.Views.Desktop;

namespace AutomataUI.Editor
{
    public partial class AutomataUserControl : UserControl
    {
        public SKCanvas canvas;
        public int PosX = 0;


        public AutomataUserControl()
        {
            InitializeComponent();
            
            
        }

       
        

        private void skiaView_PaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            // the the canvas and properties
            canvas = e.Surface.Canvas;

            // get the screen density for scaling
            var scale = 1f;
            var scaledSize = new SKSize(e.Info.Width / scale, e.Info.Height / scale);

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
            var coord = new SKPoint(scaledSize.Width / 2, (scaledSize.Height + paint.TextSize) / 2);
            canvas.DrawText("SkiaSharp", coord, paint);


            var paint2 = new SKPaint
            {
                Color = SKColors.Blue,
                IsAntialias = true,
                StrokeWidth = 3,
                Style = SKPaintStyle.Stroke

            };
            canvas.DrawCircle(PosX, 50, 30, paint2); //arguments are x position, y position, radius, and paint

            using (SKPath path = new SKPath())
            {
                path.MoveTo(100, 100);
                path.CubicTo(100, 200,
                             200, 300,
                             400, 100);

                canvas.DrawPath(path, paint2);

                if (path.Contains(100, 100))
                {
                    Console.WriteLine("path hit");
                }
            }
        }

    }
}
