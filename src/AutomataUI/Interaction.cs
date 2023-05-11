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
        Object selectedItem; //hittest


        SKPoint previousMousePosition; //for mousedelta

        public Interaction(AutomataView ViewInput, AutomataModel AutomataDataInput, Dialogs DialogInput, Form FormInput)
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
                if (Dialogs.Dialog(ref stateName, ref frames, "Add State") == DialogResult.OK)
                {
                    AutomataData.AddState(stateName, frames, Tools.ToWorldSpace(e.Location.ToSKPoint(), AutomataData.worldOffset, AutomataData.worldScale));
                    AutomataView.skiaView.Invalidate();
                }
            }

            // edit state
            if (item is State && (item as State).Name != "Init") //not safe in case someone names a state also Init
            {
                stateName = (item as State).Name;
                frames = (item as State).Duration;

                if (Dialogs.Dialog(ref stateName, ref frames, "Edit State") == DialogResult.OK)
                {
                    (item as State).Name = stateName;
                    (item as State).Duration = frames;
                    AutomataView.skiaView.Invalidate();
                }
            }

            //edit Transition
            if (item is Transition)
            {
                var transitionName = (item as Transition).Name;
                frames = (item as Transition).Duration;

                if (Dialogs.Dialog(ref transitionName, ref frames, "Edit Transition") == DialogResult.OK)
                {
                    (item as Transition).Name = transitionName;
                    (item as Transition).Duration = frames;
                    AutomataView.skiaView.Invalidate();
                }
            }

        }

        private void DoMouseDown(object sender, MouseEventArgs e)
        {
            //global hittest
            selectedItem = HitTest(e);
            Console.WriteLine(selectedItem);

            //reset things
            if (e.Button == MouseButtons.Left && selectedItem is World)
            {
                AutomataView.startTransitionState = null;
                AutomataView.endTransitionState = null;
            }  

            //bring state to front
            if (selectedItem is State)
            {
                BringStateToFront((State)selectedItem, AutomataData.states);
            }

            //set active state
            if (selectedItem is State && Form.ModifierKeys == Keys.Control)
            {
                AutomataData.activeState = (State)selectedItem;
                AutomataData.elapsedStateTime = 0;
                AutomataData.elapsedTransitionTime = 0;
                AutomataData.targetState = (State)selectedItem;
            }

            //override active transition
            if (selectedItem is Transition && Form.ModifierKeys == Keys.Control)
            {
                AutomataData.activeTransition = (Transition)selectedItem;
                AutomataData.targetState = AutomataData.activeTransition.EndState;
                AutomataData.activeState = AutomataData.activeTransition.StartState;

                AutomataData.elapsedTransitionTime = AutomataData.activeTransition.Duration;
                AutomataData.elapsedStateTime = 0;

                AutomataView.skiaView.Invalidate();
            }


            ////Remove Transition
            if (selectedItem is Transition && e.Button == MouseButtons.Middle)
            {
                AutomataData.RemoveTransition((Transition)selectedItem);
            }

            ////Remove State
            if (selectedItem is State && e.Button == MouseButtons.Middle)
            {
                AutomataData.RemoveState((State)selectedItem);
            }


            //add transition
            if (selectedItem is State)
            {
                if (e.Button == MouseButtons.Right)
                {
                    AutomataView.startTransitionState = (State)selectedItem;
                    Console.WriteLine(AutomataView.startTransitionState.Name);
                }

                //create Transition
                if (e.Button == MouseButtons.Left &&
                    AutomataView.startTransitionState != null &&
                    AutomataView.endTransitionState != null &&
                    AutomataView.startTransitionState != AutomataView.endTransitionState
                    && !AutomataData.TransitionExists(AutomataView.startTransitionState, AutomataView.endTransitionState))
                {
                    //hier fehlt noch der test ob es die transition geben darf
                    //&&!AutomataData.TransitionExists(AutomataView.startTransitionState, AutomataView.endTransitionState
                    string transName = "to" + AutomataView.endTransitionState.Name;
                    int frames = 1;

                    if (Dialogs.Dialog(ref transName, ref frames, "New Transition") == DialogResult.OK)
                    {
                        //Console.WriteLine("Make new transition");
                        AutomataData.AddTransition(transName, frames, AutomataView.startTransitionState, AutomataView.endTransitionState);

                        AutomataView.startTransitionState = null;
                        AutomataView.endTransitionState = null;

                    }
                }
            }

            

            AutomataView.skiaView.Invalidate();
        }

        private void DoMouseMove(object sender, MouseEventArgs e)
        {
            // mousedelta for all sorts of dragging
            SKPoint mouseDelta = e.Location.ToSKPoint() - previousMousePosition;
            
            //do a hittest
            var selecteditem = HitTest(e);

            //drag the editor world
            DragWorld(e, mouseDelta,selectedItem);
            
            //drag State 
            DragState(e, mouseDelta, selectedItem);
      
            //add transition snappy line
            if (AutomataView.startTransitionState != null)
            {
                //find target state for new transition
                if (selecteditem is State && selecteditem != AutomataView.startTransitionState)
                {
                    AutomataView.endTransitionState = (State)selecteditem;
                    
                    Console.WriteLine(AutomataView.endTransitionState.Name);
                }
                else AutomataView.endTransitionState = null;
                
                //do redraw when we try to make a new transition
                AutomataView.skiaView.Invalidate();
            }

            

            //Mouse stuff like mouse delta and pos 2 view
            previousMousePosition = e.Location.ToSKPoint(); 
            AutomataView.mousePosition = e.Location.ToSKPoint();
        }
        private void DoMouseWheel(object sender, MouseEventArgs e)
        {
            ZoomWorld(e);
        }
        private Object HitTest(MouseEventArgs e)
        {
            //transform mouse to world space for hit testing
            SKPoint worldMousePos = Tools.ToWorldSpace(e.Location.ToSKPoint(), AutomataData.worldOffset, AutomataData.worldScale);


            //who wants to be hit tested
            List<UIelement> hitTestList = new List<UIelement>();

            //states UI element
            foreach (var item in AutomataData.states)
            {         
                hitTestList.Add(item);
            }

            //transitions UI element
            foreach (var item in AutomataData.transitions)
            {
                hitTestList.Add(item);
                //Console.WriteLine(item.Bounds);
            }
            
            //background UI element
            hitTestList.Add(AutomataData.world);
            
            // HIT TEST
            var hooverObject = hitTestList.FirstOrDefault(x => x.Bounds.Contains(worldMousePos));

            if (hooverObject is State || hooverObject is Transition)
            {
                parentForm.Cursor = Cursors.Hand;
            }
            else
            {
                parentForm.Cursor = Cursors.Default;
            }
            return hooverObject;
        }
        public void DragWorld(MouseEventArgs e, SKPoint mouseDelta, Object selectedItem)
        {
            // drag position      
            if (e.Button == MouseButtons.Right && selectedItem is World)
            {
                AutomataData.worldOffset.X += (mouseDelta.X) / AutomataData.worldScale;
                AutomataData.worldOffset.Y += (mouseDelta.Y) / AutomataData.worldScale;
                AutomataView.skiaView.Invalidate();
            }

        }
        public void DragState(MouseEventArgs e, SKPoint mouseDelta, Object selectedState)
        {
            if (selectedState != null && selectedState is State && e.Button == MouseButtons.Left)
            {
                var state = (State)selectedState;

                state.Bounds = new SKRect(
                    state.Bounds.Left + mouseDelta.X / AutomataData.worldScale,
                    state.Bounds.Top + mouseDelta.Y / AutomataData.worldScale,
                    state.Bounds.Right + mouseDelta.X / AutomataData.worldScale,
                    state.Bounds.Bottom + mouseDelta.Y / AutomataData.worldScale);

                AutomataView.skiaView.Invalidate();
            }
        }
        public void ZoomWorld(MouseEventArgs e)
        {
            float worldScalePre = AutomataData.worldScale;
            //Console.WriteLine(AutomataView.worldScale);

            if (e.Delta > 0 && AutomataData.worldScale < 2.2f)
            {
                AutomataData.worldScale *= 1.08f;
            }

            if (e.Delta < 0 && AutomataData.worldScale > 0.1f)
            {
                AutomataData.worldScale *= 0.92f;
            }

            SKPoint preZoomPos = new SKPoint(e.X / worldScalePre, e.Y / worldScalePre);
            SKPoint postZoomPos = new SKPoint(e.X / AutomataData.worldScale, e.Y / AutomataData.worldScale);

            AutomataData.worldOffset.X = postZoomPos.X - preZoomPos.X + AutomataData.worldOffset.X;
            AutomataData.worldOffset.Y = postZoomPos.Y - preZoomPos.Y + AutomataData.worldOffset.Y;

            AutomataView.skiaView.Invalidate();
        }

        public enum Statemachine
        {
            world,
            state,
            transition,
            connect,
            dragState
        } // do we need this ???
        public void BringStateToFront(Object selectedState, List<State> states)
        {
            if (selectedState != null && selectedState is State)
            {
                //bring to front
                var idx = states.IndexOf((State)selectedState);
                var item = (State)selectedState;
                states.RemoveAt(idx);
                states.Insert(states.Count, item);
            }
        }
    }
}
