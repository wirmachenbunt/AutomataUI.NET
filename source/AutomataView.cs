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

namespace AutomataUI
{
    class AutomataView
    {
        public SkiaSharp.Views.Desktop.SKGLControl skiaView;
        public SKPoint worldOffset;
        public float worldScale = 1;

        //Initialize
        public AutomataView()
        {
            skiaView = new SkiaSharp.Views.Desktop.SKGLControl();
            skiaView.Dock = System.Windows.Forms.DockStyle.Fill;
            skiaView.Location = new System.Drawing.Point(0, 0);
            skiaView.Name = "skiaView";
            skiaView.Size = new System.Drawing.Size(774, 529);
            skiaView.TabIndex = 0;
            skiaView.Text = "skControl1";
            skiaView.PaintSurface += new System.EventHandler<SkiaSharp.Views.Desktop.SKPaintGLSurfaceEventArgs>(UpdateSkiaView);
        }

        private void UpdateSkiaView(object sender, SKPaintGLSurfaceEventArgs e)
        {
            var canvas = e.Surface.Canvas;

            // scale and translate world aka canvas
            canvas.Scale(worldScale);
            canvas.Translate(worldOffset);

            // make sure the canvas is blank
            canvas.Clear(SKColors.Orange);

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

            canvas.DrawCircle(new SKPoint(10, 10), 30, paint2); //arguments are x position, y position, radius, and paint

            using (SKPath path = new SKPath())
            {
                path.MoveTo(100, 100);
                path.CubicTo(100, 200,
                             200, 300,
                             400, 100);

                canvas.DrawPath(path, paint2);
            }
        }
    }
}
