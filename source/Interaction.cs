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
        AutomataModel AutomataData;
        Dialogs Dialogs;
        Form parentForm;    

        SKPoint previousMousePosition;

        public Interaction(AutomataView ViewInput, AutomataModel AutomataDataInput, Dialogs DialogInput,Form FormInput)
        {
            AutomataView = ViewInput; // reference to skia drawing
            AutomataData = AutomataDataInput; // reference to Automata Data
            Dialogs = DialogInput;
            parentForm = FormInput; // access parentform to change mouse cursor
            ViewInput.skiaView.MouseMove += DoMouseMove;
            ViewInput.skiaView.MouseWheel += DoMouseWheel;
            ViewInput.skiaView.MouseDoubleClick += DoDoubleClick;
        }
        private void DoDoubleClick(object sender, MouseEventArgs e)
        {

            string stateName = "empty";
            int frames = 1;

           // Dialogs.TestMyForm();
            

            if (Dialogs.AddState(ref stateName, ref frames, "Add State") == DialogResult.OK)
            {
                // AutomataData.AddState(name, frames, e.Location.ToSKPoint());
                Console.WriteLine("Add State");
            }

            //if (path.Contains(PointToClient(MousePosition).X, PointToClient(MousePosition).Y))
            //{
            //    // Console.WriteLine("path hit");
            //}
        }
        private void DoMouseMove(object sender, MouseEventArgs e)
        {
            DragWorld(e);

            //HitTest(e);

            var thing = HitTest(e);

            //debug mouse coords
            AutomataView.mousePos = e.Location.ToSKPoint();
            AutomataView.skiaView.Invalidate();
        }
        private void DoMouseWheel(object sender, MouseEventArgs e)
        {
            ZoomWorld(e);
        }   
        private Object HitTest(MouseEventArgs e)
        {
            //transform mouse to world space for hit testing
            SKPoint worldMousePos = Tools.ToWorldSpace(e.Location.ToSKPoint(), AutomataView.worldOffset, AutomataView.worldScale);
            
            //who wants to be hit tested
            List<UIelement> hitTestList = new List<UIelement>();

            //states UI element
            foreach (var item in AutomataData.states)
            {
                hitTestList.Add(item);
            }

            //background UI element
            hitTestList.Add(AutomataData.world);

            // actual hittest
            var hooverObject = hitTestList.FirstOrDefault(x => x.Bounds.Contains(worldMousePos));
            //Console.WriteLine(hooverObject);

            if (hooverObject is State)
            {
                
                parentForm.Cursor = Cursors.Hand;
            }
            else
            {
                parentForm.Cursor = Cursors.Default;
            }

            return hooverObject;
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
        public enum Statemachine
        {
            world,
            state,
            transition,
            connect,
            dragState
        }
    }
}
