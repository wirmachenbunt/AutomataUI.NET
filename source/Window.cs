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
    public class Window : Form
    {
        public Window()
        {
            InitializeComponent();
        }

        private void UpdateSkiaView(object sender, SKPaintGLSurfaceEventArgs e)
        {
            var canvas = e.Surface.Canvas;

            // scale and translate world aka canvas
            canvas.Scale(worldScale);
            canvas.Translate(worldOffset);

            // make sure the canvas is blank
            canvas.Clear(SKColors.DarkGray);

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

                if (path.Contains(PointToClient(MousePosition).X, PointToClient(MousePosition).Y))
                {
                    // Console.WriteLine("path hit");
                }
            }
        }

        Point previousMousePosition;
        SKPoint worldOffset;
        float worldScale = 1;

        private void MouseMove(object sender, MouseEventArgs e)
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

        private void MouseWheel(object sender, MouseEventArgs e)
        {
            float worldScalePre = worldScale;

            if (e.Delta > 0)
            {
                worldScale *= 1.03f;
            }

            if (e.Delta < 0)
            {
                worldScale *= 0.97f;
            }

            SKPoint preZoomPos = new SKPoint(e.X / worldScalePre, e.Y / worldScalePre);
            SKPoint postZoomPos = new SKPoint(e.X / worldScale, e.Y / worldScale);

            worldOffset.X = postZoomPos.X - preZoomPos.X + worldOffset.X;
            worldOffset.Y = postZoomPos.Y - preZoomPos.Y + worldOffset.Y;

            skiaView.Invalidate();        
        }

        private SkiaSharp.Views.Desktop.SKGLControl skiaView;
        private void InitializeComponent()
        {
            this.skiaView = new SkiaSharp.Views.Desktop.SKGLControl();
            this.SuspendLayout();
            // 
            // skiaView
            // 
            this.skiaView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.skiaView.Location = new System.Drawing.Point(0, 0);
            this.skiaView.Name = "skiaView";
            this.skiaView.Size = new System.Drawing.Size(774, 529);
            this.skiaView.TabIndex = 0;
            this.skiaView.Text = "skControl1";
            this.skiaView.PaintSurface += new System.EventHandler<SkiaSharp.Views.Desktop.SKPaintGLSurfaceEventArgs>(UpdateSkiaView);
            // 
            // Form1
            // 
            this.skiaView.MouseMove += MouseMove;
            this.skiaView.MouseWheel += MouseWheel;

            this.AutoScaleDimensions = new System.Drawing.SizeF(192F, 192F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(774, 529);
            this.Controls.Add(this.skiaView);
            this.Name = "AutomataUI";
            this.Text = "AutomataUI";
            this.ResumeLayout(false);
        }
    }
}