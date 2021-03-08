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
    class Interaction
    {
        AutomataView AutomataView;
        SKPoint previousMousePosition;
        AutomataModel AutomataData;

        public Interaction(AutomataView ViewInput, AutomataModel ModelInput)
        {
            AutomataView = ViewInput; // reference to skia drawing
            AutomataData = ModelInput; // reference to Automata Data

            ViewInput.skiaView.MouseMove += DoMouseMove;
            ViewInput.skiaView.MouseWheel += DoMouseWheel;
            ViewInput.skiaView.MouseDoubleClick += DoDoubleClick;
        }

        private void DoDoubleClick(object sender, MouseEventArgs e)
        {

            //if (path.Contains(PointToClient(MousePosition).X, PointToClient(MousePosition).Y))
            //{
            //    // Console.WriteLine("path hit");
            //}
        }

        private void DoMouseMove(object sender, MouseEventArgs e)
        {
            // drag position
            SKPoint mousePos = e.Location.ToSKPoint();
            if (e.Button == MouseButtons.Left)
            {
               // Console.WriteLine("left click");
                AutomataView.worldOffset.X += (e.X - previousMousePosition.X) / AutomataView.worldScale;
                AutomataView.worldOffset.Y += (e.Y - previousMousePosition.Y) / AutomataView.worldScale;
                AutomataView.skiaView.Invalidate();
            }
            previousMousePosition = mousePos;

        }
        private void DoMouseWheel(object sender, MouseEventArgs e)
        {
            float worldScalePre = AutomataView.worldScale;
            Console.WriteLine(e.Delta);

            if (e.Delta > 0)
            {
                AutomataView.worldScale *= 1.03f;
            }

            if (e.Delta < 0)
            {
                AutomataView.worldScale *= 0.97f;
            }

            SKPoint preZoomPos = new SKPoint(e.X / worldScalePre, e.Y / worldScalePre);
            SKPoint postZoomPos = new SKPoint(e.X / AutomataView.worldScale, e.Y / AutomataView.worldScale);

            AutomataView.worldOffset.X = postZoomPos.X - preZoomPos.X + AutomataView.worldOffset.X;
            AutomataView.worldOffset.Y = postZoomPos.Y - preZoomPos.Y + AutomataView.worldOffset.Y;

            AutomataView.skiaView.Invalidate();
        }
    }
}
