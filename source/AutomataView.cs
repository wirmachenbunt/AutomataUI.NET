using SkiaSharp;
using SkiaSharp.Views.Desktop;
using SkiaTextRenderer;
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

        //colors
        SKPaint statePaint;
        SKPaint textPaint;

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

            SetupPaints();
        }

        public void SetupPaints()
        {

            statePaint = new SKPaint
            {
                IsAntialias = true,
                Style = SKPaintStyle.Fill,
                Color = SKColor.Parse("#00ffea")    
            };

            textPaint = new SKPaint
            {
                Color = SKColors.Black,
                IsAntialias = true,
                Style = SKPaintStyle.Fill,
                TextAlign = SKTextAlign.Center,
                Typeface = SKTypeface.FromFamilyName("CoText_Bd"),
                TextSize = 24
            };

        }

        public void DrawStates(SKCanvas canvas)
        {
            canvas.DrawCircle(0, 0, 50, statePaint);
            canvas.DrawText("Init", new SKPoint(0,10), textPaint);

        }

        private void UpdateSkiaView(object sender, SKPaintGLSurfaceEventArgs e)
        {
            var canvas = e.Surface.Canvas;

            // scale and translate world aka canvas
            canvas.Scale(worldScale);
            canvas.Translate(worldOffset);

            // make sure the canvas is blank
            canvas.Clear(SKColor.Parse("#141414"));

            DrawStates(canvas);

            //// draw some text
            //var paint = new SKPaint
            //{
            //    Color = SKColors.Black,
            //    IsAntialias = true,
            //    Style = SKPaintStyle.Fill,
            //    TextAlign = SKTextAlign.Right,
            //    Typeface = SKTypeface.FromFamilyName("CoText_Bd"),
            //    TextSize = 24
            //};
            //var coord = new SKPoint(100, 100);
            //canvas.DrawText("SkiaSharp", coord, paint);

            //SKPaint statePaint = new SKPaint
            //{
            //    Style = SKPaintStyle.Stroke,
            //    Color = Color.Red.ToSKColor(),
            //    StrokeWidth = 25
            //};
            //canvas.DrawCircle(0, 0 , 100, statePaint);

            //statePaint.Style = SKPaintStyle.Fill;
            //statePaint.Color = SKColors.Blue;
            //canvas.DrawCircle(0,0, 100, statePaint);

            //using (SKPath path = new SKPath())
            //{
            //    path.MoveTo(100, 100);
            //    path.CubicTo(100, 200,
            //                 200, 300,
            //                 400, 100);

            //    canvas.DrawPath(path, paint2);
            //}
        }
    }
}
