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
        Object selectedItem;

        SKPoint previousMousePosition; //for mousedelta

        public Interaction(AutomataView ViewInput, AutomataModel AutomataDataInput, Dialogs DialogInput,Form FormInput)
        {
            AutomataView = ViewInput; // reference to skia drawing
            AutomataData = AutomataDataInput; // reference to Automata Data
            Dialogs = DialogInput;
            parentForm = FormInput; // access parentform to change mouse cursor
            ViewInput.skiaView.MouseMove += DoMouseMove;
            ViewInput.skiaView.MouseWheel += DoMouseWheel;
            ViewInput.skiaView.MouseDoubleClick += DoDoubleClick;
            ViewInput.skiaView.MouseDown += DoMouseDown;
            ViewInput.skiaView.MouseUp += DoMouseUp;
        }

        private void DoMouseUp(object sender, MouseEventArgs e)
        {
            selectedItem = null;
        }

        private void DoDoubleClick(object sender, MouseEventArgs e)
        {
            string stateName = "empty";
            int frames = 1;

            var item = HitTest(e); // do hit test

            // add new state
            if (item is World)
            {
                if (Dialogs.StateDialog(ref stateName, ref frames, "Add State") == DialogResult.OK)
                {
                    AutomataData.AddState(stateName, frames, Tools.ToWorldSpace(e.Location.ToSKPoint(), AutomataView.worldOffset, AutomataView.worldScale));
                    AutomataView.skiaView.Invalidate();
                }
            }

            // edit state
            if (item is State && (item as State).Name != "Init") //not safe in case someone names a state also Init
            {
                stateName = (item as State).Name;
                frames = (item as State).Duration;

                if (Dialogs.StateDialog(ref stateName, ref frames, "Edit State") == DialogResult.OK)
                {
                    (item as State).Name = stateName;
                    (item as State).Duration = frames;
                    AutomataView.skiaView.Invalidate();
                }
            }
        }

        private void DoMouseDown(object sender,MouseEventArgs e)
        {
            selectedItem = HitTest(e);
        }

        private void DoMouseMove(object sender, MouseEventArgs e)
        {
            SKPoint mouseDelta = e.Location.ToSKPoint() - previousMousePosition;
            var temp = HitTest(e);


            DragWorld(e,mouseDelta);
           
            if (selectedItem is State)
            {          
                DragState(e, mouseDelta,selectedItem as State);
            }


           
            //debug mouse coords
            //AutomataView.mousePos = e.Location.ToSKPoint();
            //AutomataView.skiaView.Invalidate();
            previousMousePosition = e.Location.ToSKPoint(); //needed for mouseDelta
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
        public void DragWorld(MouseEventArgs e, SKPoint mouseDelta)
        {
            // drag position      
            if (e.Button == MouseButtons.Right)
            {
                // Console.WriteLine("left click");
                AutomataView.worldOffset.X += (mouseDelta.X) / AutomataView.worldScale;
                AutomataView.worldOffset.Y += (mouseDelta.Y) / AutomataView.worldScale;
                AutomataView.skiaView.Invalidate();
            }
           
        }
        public void DragState(MouseEventArgs e,SKPoint mouseDelta, State state)
        {

            if (e.Button == MouseButtons.Left && state != null)
            {
                Console.WriteLine(state.Name);
                Console.WriteLine(state.Bounds);

                state.Bounds = new SKRect(
                    state.Bounds.Left + mouseDelta.X / AutomataView.worldScale,
                    state.Bounds.Top + mouseDelta.Y / AutomataView.worldScale,
                    state.Bounds.Right + mouseDelta.X / AutomataView.worldScale,
                    state.Bounds.Bottom + mouseDelta.Y / AutomataView.worldScale);

                //AutomataView.worldOffset.X += (e.X - previousMousePosition.X) / AutomataView.worldScale;
                //AutomataView.worldOffset.Y += (e.Y - previousMousePosition.Y) / AutomataView.worldScale;
                AutomataView.skiaView.Invalidate();
            }

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
