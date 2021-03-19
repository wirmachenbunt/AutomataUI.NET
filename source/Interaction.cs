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
        Dialogs Dialogs;

        public Interaction(AutomataView ViewInput, AutomataModel AutomataDataInput, Dialogs DialogInput)
        {
            AutomataView = ViewInput; // reference to skia drawing
            AutomataData = AutomataDataInput; // reference to Automata Data
            Dialogs = DialogInput;
            ViewInput.skiaView.MouseMove += DoMouseMove;
            ViewInput.skiaView.MouseWheel += DoMouseWheel;
            ViewInput.skiaView.MouseDoubleClick += DoDoubleClick;
        }

        private void DoDoubleClick(object sender, MouseEventArgs e)
        {
            int frames = 0;
            string name = "new state";
            int size = 20;

            
            Dialogs.TestMyForm();

            //if (Dialogs.AddState(ref name, ref frames, "Add State") == DialogResult.OK)
            //{
            //    //AutomataData.AddState(name, frames, e.Location.ToSKPoint());
            //}

            //if (path.Contains(PointToClient(MousePosition).X, PointToClient(MousePosition).Y))
            //{
            //    // Console.WriteLine("path hit");
            //}
        }
        private void DoMouseMove(object sender, MouseEventArgs e)
        {
            DragWorld(e);

            HitTest(e);
            //debug mouse coords
            AutomataView.mousePos = e.Location.ToSKPoint();
            AutomataView.skiaView.Invalidate();
        }
        private void DoMouseWheel(object sender, MouseEventArgs e)
        {
            ZoomWorld(e);
        }

        private void HitTest(MouseEventArgs e)
        {
            
            SKPoint worldMousePos = Tools.ToWorldSpace(e.Location.ToSKPoint(), AutomataView.worldOffset, AutomataView.worldScale);

          
            
            foreach (var item in AutomataData.states)
            {
                if (item.Bounds.Contains(worldMousePos))
                {
                    Console.WriteLine(e.Location);
                }
            }

        }


        public void DragWorld(MouseEventArgs e)
        {
            // drag position
            SKPoint mousePos = e.Location.ToSKPoint();
            if (e.Button == MouseButtons.Right)
            {
                // Console.WriteLine("left click");
                AutomataView.worldOffset.X += (e.X - previousMousePosition.X) / AutomataView.worldScale;
                AutomataView.worldOffset.Y += (e.Y - previousMousePosition.Y) / AutomataView.worldScale;
                AutomataView.skiaView.Invalidate();
            }
            previousMousePosition = mousePos;
        }
        public void ZoomWorld(MouseEventArgs e)
        {
            float worldScalePre = AutomataView.worldScale;
            Console.WriteLine(AutomataView.worldScale);

            if (e.Delta > 0 && AutomataView.worldScale < 1.2f)
            {
                AutomataView.worldScale *= 1.08f;
            }

            if (e.Delta < 0 && AutomataView.worldScale > 0.2f)
            {
                AutomataView.worldScale *= 0.92f;
            }

            SKPoint preZoomPos = new SKPoint(e.X / worldScalePre, e.Y / worldScalePre);
            SKPoint postZoomPos = new SKPoint(e.X / AutomataView.worldScale, e.Y / AutomataView.worldScale);

            AutomataView.worldOffset.X = postZoomPos.X - preZoomPos.X + AutomataView.worldOffset.X;
            AutomataView.worldOffset.Y = postZoomPos.Y - preZoomPos.Y + AutomataView.worldOffset.Y;

            AutomataView.skiaView.Invalidate();
        }
       

    }
}
