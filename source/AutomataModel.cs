using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkiaSharp;

namespace AutomataUI
{
    class AutomataModel
    {
        public List<State> states { get; set; }
        public List<Transition> transitions { get; set; }

        public AutomataModel()
        {
            states = new List<State>();
            transitions = new List<Transition>();
        }
    }

    public abstract class UIelement //all UI Elements should be based upon this to make the hittest work
    {
        public SKRect Bounds { get; set; }
        public string Name { get; set; }
    }

    public class State : UIelement
    {
        public string ID { get; set; }

        public bool Active { get; set; }

        public int Duration { get; set; } //how many frames is the state locked

    }
    public class Transition : UIelement
    {
        public State startState { get; set; }

        public State endState { get; set; }

        public int Duration { get; set; }

        public bool IsPingPong { get; set; }

    }
}




