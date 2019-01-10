using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomataUI.Library
{
    public class Statemachine
    {
        public List<State> States
        {
            get;
            set;
        }

        public List<Transition> Transitions
        {
            get;
            set;
        }

        public List<Statemachine> Statemachines
        {
            get;
            set;
        }

        public bool Active
        {
            get;
            set;
        }
    }


    public class State
    {
        public string ID
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public bool Active
        {
            get;
            set;
        }

        public int Frames //how many frames is the state locked
        {
            get;
            set;
        }

        //public Rectangle Bounds
        //{
        //    get;
        //    set;
        //}
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

        //public Point startBezierPoint //bezierControlSetting Angle and Lenght
        //{
        //    get;
        //    set;
        //}

        //public Point endBezierPoint  //bezierControlSetting Angle and Lenght
        //{
        //    get;
        //    set;
        //}


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

        //public Rectangle Bounds //size of transition
        //{
        //    get;
        //    set;
        //}
    }




}
