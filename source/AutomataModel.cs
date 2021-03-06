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

        public class State
        {
            public string ID { get; set; }

            public string Name { get; set; }

            public bool Active { get; set; }

            public int Frames { get; set; }//how many frames is the state locked

            public SKRect Rectangle { get; set; }
        }
        public class Transition
        {
            public string Name
            {
                get;
                set;
            }

            public State startState
            {
                get;
                set;
            }

            public State endState
            {
                get;
                set;
            }

            public int Frames //how long does the transition take
            {
                get;
                set;
            }

            public bool IsPingPong //how long does the transition take
            {
                get;
                set;
            }
            public SKRect Rectangle { get; set; }
        }

    }
}
