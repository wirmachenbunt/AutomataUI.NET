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
    class MouseKeyboard
    {
        Drawing drawing;
        Point previousMousePosition;

        public MouseKeyboard(Drawing drawingInput)
        {
            drawing = drawingInput; // reference to skia drawing
            drawingInput.skiaView.MouseMove += DoMouseMove;
            drawingInput.skiaView.MouseWheel += DoMouseWheel;
        }


        private void DoMouseMove(object sender, MouseEventArgs e)
        {
            // drag position
            Point mousePos = e.Location;
            if (e.Button == MouseButtons.Left)
            {
                Console.WriteLine("left click");
                drawing.worldOffset.X += (e.X - previousMousePosition.X) / drawing.worldScale;
                drawing.worldOffset.Y += (e.Y - previousMousePosition.Y) / drawing.worldScale;
                drawing.skiaView.Invalidate();
            }
            previousMousePosition = mousePos;

        }
        private void DoMouseWheel(object sender, MouseEventArgs e)
        {
            float worldScalePre = drawing.worldScale;

            if (e.Delta > 0)
            {
                drawing.worldScale *= 1.03f;
            }

            if (e.Delta < 0)
            {
                drawing.worldScale *= 0.97f;
            }

            SKPoint preZoomPos = new SKPoint(e.X / worldScalePre, e.Y / worldScalePre);
            SKPoint postZoomPos = new SKPoint(e.X / drawing.worldScale, e.Y / drawing.worldScale);

            drawing.worldOffset.X = postZoomPos.X - preZoomPos.X + drawing.worldOffset.X;
            drawing.worldOffset.Y = postZoomPos.Y - preZoomPos.Y + drawing.worldOffset.Y;

            drawing.skiaView.Invalidate();
        }
    }
}
