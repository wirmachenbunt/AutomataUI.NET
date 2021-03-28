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

        //colors
        SKPaint stateInitPaint;
        SKPaint stateDefaultPaint;
        SKPaint textPaint;
        SKPaint textBlackPaint;

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
                Typeface = SKTypeface.FromFamilyName("Segoe UI"),
                TextSize = 24,
                IsStroke = false
            };

            textBlackPaint = new SKPaint
            {
                Color = SKColors.Black,
                IsAntialias = true,
                Style = SKPaintStyle.Fill,
                TextAlign = SKTextAlign.Center,
                Typeface = SKTypeface.FromFamilyName("Segoe UI"),
                TextSize = 24,
                IsStroke = false
            };

        }
        public void DrawStates(SKCanvas canvas)
        {

            if (AutomataData != null)
            {
                foreach (var item in AutomataData.states)
                {
                    var size = TextRendererSk.MeasureText(item.Name, font);
                    // canvas.DrawRect(item.Bounds, stateDefaultPaint);

                    if (item.Name == "Init")
                    {
                        canvas.DrawCircle(item.Bounds.MidX, item.Bounds.MidY, 50, stateInitPaint);
                        DrawStateText(canvas, item, SKColors.Black, new SKPoint(item.Bounds.MidX, item.Bounds.MidY));
                    }
                    else
                    {
                        canvas.DrawCircle(item.Bounds.MidX, item.Bounds.MidY, 50, stateDefaultPaint);
                        DrawStateText(canvas, item, SKColors.White, new SKPoint(item.Bounds.MidX, item.Bounds.MidY));
                    }
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

            DrawTransitions(canvas);

            //debug mouse method
            //canvas.DrawCircle(Tools.ToWorldSpace(mousePos, worldOffset, worldScale), 10, stateDefaultPaint);
        }

        private void DrawStateText(SKCanvas canvas, State state, SKColor textColor, SKPoint pos)
        {
            TextRendererSk.DrawText(canvas,
                                                        state.Name,
                                                        font,
                                                        SKRect.Create(pos.X - 40, pos.Y - 40, 80, 80),
                                                        textColor,
                                                        SkiaTextRenderer.TextFormatFlags.WordBreak |
                                                        SkiaTextRenderer.TextFormatFlags.VerticalCenter |
                                                        SkiaTextRenderer.TextFormatFlags.HorizontalCenter);
        }

        private void DrawTransitions(SKCanvas canvas)
        {        
            var transitionPaint = new SKPaint
            {
                IsAntialias = true,
                Style = SKPaintStyle.StrokeAndFill,
                Color = new SKColor(244, 0, 110, 200),
                StrokeWidth = 5
            };

            if (AutomataData != null)
            {
                foreach (var item in AutomataData.transitions)
                {
                    var start = new SKPoint(item.StartState.Bounds.MidX, item.StartState.Bounds.MidY);
                    var end = new SKPoint(item.EndState.Bounds.MidX, item.EndState.Bounds.MidY);

                    Tools.EdgePoints edgepoints = Tools.GetEdgePoints(start, end, 40, 40, 0.0f);

                    canvas.DrawLine(edgepoints.A,edgepoints.B, transitionPaint);
                }
            }

        }

        private void DrawArrow()
        {
            /* https://varun.ca/polar-coords/ */
            //var rot = new SKMatrix();
            //SKMatrix.RotateDegrees(ref rot, 45.0f);
            //path.Transform(rot);

            //var pathStroke2 = new SKPaint
            //{
            //    IsAntialias = true,
            //    Style = SKPaintStyle.StrokeAndFill,
            //    Color = new SKColor(244, 0, 110, 200),
            //    StrokeWidth = 5
            //};

            //var path2 = new SKPath { FillType = SKPathFillType.EvenOdd };
            //path2.MoveTo(0, 0);
            //path2.LineTo(0, 140);
            //path2.LineTo(140, 140);
            //path2.LineTo(0, 0);
            //path2.Close();

            ////var rot = new SKMatrix();
            ////SKMatrix.RotateDegrees(ref rot, 45.0f);

            //counter++;
            //var rot = SKMatrix.CreateRotationDegrees( counter);
            //path2.Transform(rot);

            //canvas.DrawPath(path2, pathStroke2);
        }

    }
}
