
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SkiaSharp;
using SkiaSharp.Views.Desktop;

namespace WindowsFormsSkia
{
    public partial class Form1 : Form
    {

        SKImageInfo imageInfo;

        public Form1()
        {
            InitializeComponent();
        }

        private void skiaView_PaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            // the the canvas and properties
            var canvas = e.Surface.Canvas;

            // get the screen density for scaling
            var scale = 1f;
            var scaledSize = new SKSize(e.Info.Width / scale, e.Info.Height / scale);

            // handle the device screen density
            canvas.Scale(scale);

            // make sure the canvas is blank
            canvas.Clear(SKColors.Coral);

            // draw some text
            var paint = new SKPaint
            {
                Color = SKColors.Black,
                IsAntialias = true,
                Style = SKPaintStyle.Fill,
                TextAlign = SKTextAlign.Center,
                TextSize = 24
            };
            var coord = new SKPoint(scaledSize.Width / 2, (scaledSize.Height + paint.TextSize) / 2);
            canvas.DrawText("SkiaSharp", coord, paint);


            var paint2 = new SKPaint
            {
                Color = SKColors.Blue,
                IsAntialias = true,
                StrokeWidth = 15,
                Style = SKPaintStyle.Stroke

            };
            canvas.DrawCircle(50, 50, 30, paint); //arguments are x position, y position, radius, and paint


        }

    }
}
