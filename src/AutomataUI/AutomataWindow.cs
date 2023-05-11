using SkiaSharp;
using SkiaSharp.Views.Desktop;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace AutomataUI
{

    /// <summary>
    /// Main window providing home for automata view rendering, data and control
    /// </summary>

    public class AutomataWindow : Form
    {
        AutomataView AutomataView; //UI Rendering
        Interaction AutomataInteraction; //User Input Management
        public AutomataModel AutomataData; //contains Automata Structure and Methods
        Dialogs AutomataDialogs; //Winforms Dialogs

        //debug stuff
        public bool loopAsTask = false;

        // disable Close button
        private const int CP_NOCLOSE_BUTTON = 0x200;
        private const int WS_CAPTION = 0x00C00000;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCp = base.CreateParams;               
                myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
                return myCp;
            }
        }

        public AutomataWindow()
        {

            InitializeAutomata();

            // run automataloop as task when in VS
            if (loopAsTask)
            {
                Task.Run(() => AutomataLoop(AutomataData));
            }
        }

      


        public void InitializeAutomata()
        {

            AutomataData = new AutomataModel();

            //initialize with test data when there is no data setup
            AutomataData.states = new List<State>();
            AutomataData.transitions = new List<Transition>();
            AutomataData.AddState("Init", 0, new SKPoint(0, 0)); // add default state
            
            AutomataData.states[0].ID = "init"; //making init unique

            AutomataData.AddState("Start", 0, new SKPoint(500, 50));
            AutomataData.AddTransition("Start", 0, AutomataData.states[0], AutomataData.states[1]);

            AutomataData.activeState = AutomataData.states[0]; //set activestate to init
            AutomataData.targetState = AutomataData.states[0]; // set targetstate also to init

            //UI background aka desktop element
            AutomataData.world = new World()
            {
                Bounds = new SKRect(-100000, -100000, 100000, 100000),
                Name = "World"
            };


            AutomataView = new AutomataView(AutomataData);
            AutomataDialogs = new Dialogs();
            // create mousekeyboard control for drawing
            AutomataInteraction = new Interaction(AutomataView, AutomataData, AutomataDialogs, this);

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
            this.ControlBox = true;
            ResumeLayout(false);
        }


        //evluation loop to find out which state is active
        static void AutomataLoop(AutomataModel data)
        {
            while (true)
            {
                Thread.Sleep(60);
                data.UpdateAutomata();
            }

        }

        public void Serialize(string path)
        {
            Tools.WriteToBinaryFile(path, AutomataData, false);
        }

        public String SerializeData()
        {
            string serializedData = string.Empty;
            XmlSerializer serializer = new XmlSerializer(AutomataData.GetType());
            using (StringWriter sw = new StringWriter())
            {
                serializer.Serialize(sw, AutomataData);
                serializedData = sw.ToString();
            }

            return serializedData;
        }

    }
}