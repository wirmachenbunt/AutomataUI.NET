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
        public AutomataWindow automataWindow;
        Thread FUIThread;

        public AutomataWrapper()
        {
            FUIThread = new Thread(InitializeForm);
            FUIThread.SetApartmentState(ApartmentState.STA);
            FUIThread.Priority = ThreadPriority.Lowest;
            FUIThread.Start();
        }

        void InitializeForm()
        {
            automataWindow = new AutomataWindow();
            automataWindow.ShowDialog();
        }


        public void Dispose()
        {
            if (automataWindow != null)
                automataWindow.Invoke((Action)CleanupForm);
        }

        void CleanupForm()
        {
            automataWindow.Dispose();
        }

        //public InterpoleUI()
        //{
        //    FUIThread = new Thread(InitializeForm);
        //    FUIThread.SetApartmentState(ApartmentState.STA);
        //    FUIThread.Priority = ThreadPriority.Lowest;
        //    FUIThread.Start();
        //}
    }
}
