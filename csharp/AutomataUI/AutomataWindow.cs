using SkiaSharp;
using SkiaSharp.Views.Desktop;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutomataUI
{

/// <summary>
/// Main window providing home for automata view rendering, data and control
/// </summary>

    public class AutomataWindow : Form
    {
        AutomataView AutomataView; //UI Rendering
        Interaction AutomataInteraction; //User Input Management
        AutomataModel AutomataData; //contains Automata Structure and Methods
        Dialogs AutomataDialogs; //Winforms Dialogs

        //results if statemachine     
        public State activeState;
        public Transition activeTransition;

        //debug stuff
        public bool loopAsTask = false;


        public AutomataWindow()
        {
            InitializeAutomata();

            // run automataloop as task when in VS
            if (loopAsTask)
            {
                //automata renderloop
                Action actionDelegate = new Action(AutomataLoop);
                Task task1 = new Task(actionDelegate);
                task1.Start();
            }
            

        } 
         
        private void InitializeAutomata()
        {
           
            AutomataData = new AutomataModel();
            AutomataView = new AutomataView(AutomataData);
            AutomataDialogs = new Dialogs();
            // create mousekeyboard control for drawing
            AutomataInteraction = new Interaction(AutomataView, AutomataData, AutomataDialogs,this);

            this.SuspendLayout();
            //AutoScaleDimensions = new System.Drawing.SizeF(192F, 192F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            ClientSize = new System.Drawing.Size(1000, 500);
            Controls.Add(AutomataView.skiaView);
            Name = "AutomataUI";
            Text = "AutomataUI";
            //note! USING JUST AUTOSCALEMODE WILL NOT SOLVE ISSUE. MUST USE BOTH!
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F); //IMPORTANT
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;   //IMPORTANT

            ResumeLayout(false);
        }


        //evluation loop to find out which state is active
        static void AutomataLoop()
        {
            int counter = 0;
            

            while (true)
            {
                Thread.Sleep(10);
                counter++;
                Console.WriteLine("counter " + counter);
            }
                        
        }
    }
}