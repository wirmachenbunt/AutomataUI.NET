using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using SkiaSharp;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;
using System.Xml.Serialization;
using static System.TimeZoneInfo;
using System.Collections;

namespace AutomataUI
{
    public class AutomataModel
    {
        //////UI settings
        public SKPoint worldOffset;
        public float worldScale = 1;
        //////UI settings


        public List<State> states { get; set; }
        public List<Transition> transitions { get; set; }

        public State activeState;
        public State targetState;

        public Transition? activeTransition;

        public int elapsedTransitionTime;
        public int elapsedStateTime;
        

        public String output = string.Empty; //output what transition or state is currently active

        //redraw events to bubble up to AutomataView
        public delegate void RedrawEventHandler();
        public event RedrawEventHandler? Redraw;

        public World world;
        public AutomataModel()
        {
            // init things if you like
        }
        public bool TransitionExists(State startState, State endState)
        {

            bool check = false;

            //check if the transition already exists
            foreach (Transition transition in transitions) // Loop through List with foreach.
            {
                if (transition.StartState.ID == startState.ID
                && transition.EndState.ID == endState.ID)
                {
                    check = true;
                    break; // achtung test, war vorher true         
                }
                check = false;
            }
            return check;
        }
        public void AddState(String name, int frames, SKPoint point)
        {

            int size = 50;

            SKRect bounds = new SKRect(point.X - size, point.Y - size, point.X + size, point.Y + size);

            states.Add(new State()
            {
                ID = RNGCharacterMask(),
                Name = UppercaseFirst(name),
                Duration = frames,
                Bounds = bounds
            });
        }
        public void AddTransition(String name, int frames, State startState, State endState)
        {
            transitions.Add(new Transition()
            {
                Name = UppercaseFirst(name),
                Duration = frames,
                StartState = startState,
                EndState = endState
            });
        }
        public void RemoveTransition(Transition transition)
        {
            transitions.Remove(transition);
        }
        public void RemoveState(State state)
        {

            if (state.ID != "init")
            {
                states.Remove(state);

                //remove all connected transitions
                for (int i = transitions.Count - 1; i >= 0; i--)
                {
                    Transition transition = new Transition();
                    transition = transitions.ElementAt(i);

                    if (state.ID == transition.StartState.ID || state.ID == transition.EndState.ID)
                    {
                        transitions.RemoveAt(i);
                    }
                }
            }

        }
        public static string RNGCharacterMask()
        {
            int maxSize = 8;
            //int minSize = 5 ;
            char[] chars = new char[62];
            string a;
            a = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            chars = a.ToCharArray();
            int size = maxSize;
            byte[] data = new byte[1];
            RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
            crypto.GetNonZeroBytes(data);
            size = maxSize;
            data = new byte[size];
            crypto.GetNonZeroBytes(data);
            StringBuilder result = new StringBuilder(size);
            foreach (byte b in data)
            { result.Append(chars[b % (chars.Length - 1)]); }
            return result.ToString();
        }
        static string UppercaseFirst(string s)
        {
            // Check for empty string.
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            // Return char and concat substring.
            return char.ToUpper(s[0]) + s.Substring(1);
        }
        public void DeserializeData(String data)
        {
            XmlSerializer xs = new XmlSerializer(typeof(AutomataModel));
            AutomataModel loadedData = (AutomataModel)xs.Deserialize(new StringReader(data));

            elapsedStateTime = loadedData.elapsedStateTime;
            elapsedTransitionTime = loadedData.elapsedTransitionTime;

            worldOffset = loadedData.worldOffset;
            worldScale = loadedData.worldScale;

            states.Clear();
            transitions.Clear();

            states = loadedData.states;
            transitions = loadedData.transitions;

            //repair relation
            foreach (Transition transition in transitions)
            {
                transition.StartState = states.First(x => x.ID.Contains(transition.StartState.ID));
                transition.EndState = states.First(x => x.ID.Contains(transition.EndState.ID));
            }

            elapsedStateTime = loadedData.elapsedStateTime;
            elapsedTransitionTime = loadedData.elapsedTransitionTime;

            activeState = states.First(x => x.ID.Contains("init"));

            //redraw UI
            if (Redraw != null) Redraw();

        }
        public void UpdateAutomata()
        {

            if (states.Count > 0)
            {
                //transition timer
                if (activeState != targetState && elapsedTransitionTime != 0)
                {
                    elapsedTransitionTime -= 1; //counting transition down to 0       
                }
                else
                {
                    output = activeState.Name;
                }

                //wenn targetstate erreicht wurde
                if (elapsedTransitionTime == 0 && elapsedStateTime == 0) //solange transition time und elapsedtime 0 sind, setze target und active gleich
                {
                    activeState = targetState;

                    activeTransition = null; // reset active transition

                    if (Redraw != null) Redraw(); //redraw UI

                    // Debug.WriteLine("Transition Ends");
                }


                //state timer
                if (elapsedTransitionTime == 0)
                {
                    elapsedStateTime += 1; //Run State Timer when TransitionTimer is 0
                }
            }

        }

        public void TriggerTransition(String transitionNameIN)
        {

            if (elapsedStateTime >= activeState.Duration) //enable state locked time
            {
                //find transition and set targettransition
                foreach (var transition in transitions)
                {
                    if (transition.Name == transitionNameIN &&
                        transition.StartState.ID == activeState.ID &&
                        elapsedTransitionTime == 0)
                    {

                        targetState = transition.EndState;
                        elapsedTransitionTime = transition.Duration;
                        elapsedStateTime = 0;

                        activeTransition = transition;
                        //redraw UI
                        if (Redraw != null) Redraw();
                        break;

                    }
                }
            }     
        }

        public void ForceStatebyName(String name)
        {
            State foundState = states.First(s => s.Name == name);

            if (foundState != null)
            {
                activeState = foundState;
                targetState = foundState;
                elapsedStateTime = 0;
                elapsedTransitionTime = 0;
                
                //redraw UI
                if (Redraw != null) Redraw();
            }
        }

    }

    public abstract class UIelement //all UI Elements should be based upon this to make the hittest work
    {
        public SKRect Bounds { get; set; }

        //public SKPath Path { get; set; }

        public string Name { get; set; }
    }
    public class State : UIelement
    {
        public string ID { get; set; }
        public int Duration { get; set; } //how many frames is the state locked

    }
    public class Transition : UIelement
    {
        public State StartState { get; set; }

        public State EndState { get; set; }

        public int Duration { get; set; }

        //public bool IsPingPong { get; set; }

    }
    public class World : UIelement
    {

    }


}