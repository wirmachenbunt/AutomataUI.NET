using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutomataUI;

namespace AutomataUI
{

    public class AutomataWrapper
    {
        public bool testtest = true;
        public AutomataNode automataNode;

        public AutomataWrapper()
        {
            InitializeForm();
        }

        //public void Dispose()
        //{
        //    if (automataNode != null)
        //        automataNode.Invoke((Action)CleanupForm);
        //}

        //void CleanupForm()
        //{
        //    automataNode.Dispose();
        //}

        void InitializeForm()
        {
            automataNode = new AutomataNode();

            // Use ShowDialog as it starts its own main loop - using Show alone the window just disappears
            automataNode.ShowDialog();
        }

    }
}
