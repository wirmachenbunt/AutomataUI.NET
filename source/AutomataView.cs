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
        AutomataModel AutomataData;

        public SkiaSharp.Views.Desktop.SKGLControl skiaView;
        public SKPoint worldOffset;
        public float worldScale = 1;
        public SKPoint mousePos;

        //colors
        SKPaint stateInitPaint;
        SKPaint stateDefaultPaint;
        SKPaint textPaint;

        SkiaTextRenderer.Font font;

        //Initialize
        public AutomataView(AutomataModel AutomataDataInput)
        {
            AutomataData = AutomataDataInput; // reference to parent class data

            skiaView = new SkiaSharp.Views.Desktop.SKGLControl();
            skiaView.Dock = System.Windows.Forms.DockStyle.Fill;
            skiaView.Location = new System.Drawing.Point(0, 0);
            skiaView.Name = "skiaView";
            skiaView.Size = new System.Drawing.Size(774, 529);
            skiaView.TabIndex = 0;
            skiaView.Text = "skControl1";
            skiaView.PaintSurface += new System.EventHandler<SkiaSharp.Views.Desktop.SKPaintGLSurfaceEventArgs>(UpdateSkiaView);

            SetupPaints();
            font = new SkiaTextRenderer.Font(SKTypeface.Default, 15);

         

        }

        public void SetupPaints()
        {

            stateInitPaint = new SKPaint
            {
                IsAntialias = true,
                Style = SKPaintStyle.Fill,
                Color = SKColor.Parse("#00ffea")
            };

            stateDefaultPaint = new SKPaint
            {
                IsAntialias = true,
                Style = SKPaintStyle.Fill,
                Color = SKColor.Parse("#323232")
            };

            textPaint = new SKPaint
            {
                Color = SKColor.Parse("#adadad"),
                IsAntialias = true,
                Style = SKPaintStyle.Fill,
                TextAlign = SKTextAlign.Center,
                Typeface = SKTypeface.FromFamilyName("CoText_Bd"),
                TextSize = 24
            };

        }
        public void DrawStates(SKCanvas canvas)
        {

            if (AutomataData != null)
            {
                foreach (var item in AutomataData.states)
                {
                    canvas.DrawRect(item.Bounds, stateDefaultPaint);

                    

                    if (item.Name == "Init")
                    {
                        canvas.DrawCircle(item.Bounds.MidX, item.Bounds.MidY, 50, stateInitPaint);
                    }
                    else
                    {
                        canvas.DrawCircle(item.Bounds.MidX, item.Bounds.MidY, 50, stateInitPaint);
                    }

                    var size = TextRendererSk.MeasureText(item.Name, font);

                    TextRendererSk.DrawText(canvas,
                                            item.Name,
                                            font,
                                            SKRect.Create(-40, -40, 80, 80),
                                            SKColor.Parse("#adadad"), 
                                            SkiaTextRenderer.TextFormatFlags.WordBreak | 
                                            SkiaTextRenderer.TextFormatFlags.VerticalCenter | 
                                            SkiaTextRenderer.TextFormatFlags.HorizontalCenter);
                }
            }






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


            //debug mouse method
            canvas.DrawCircle(ToWorldSpace(mousePos,worldOffset,worldScale), 10, stateDefaultPaint);


        }

        private SKPoint ToWorldSpace (SKPoint position, SKPoint worldOffset,float worldScale)
        {
            return new SKPoint(position.X / worldScale, position.Y / worldScale) - worldOffset;
        }
    }
}
