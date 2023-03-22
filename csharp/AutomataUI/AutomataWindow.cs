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
        public AutomataView AutomataView; //UI Rendering
        public Interaction AutomataInteraction; //User Input Management
        public AutomataModel AutomataData; //contains Automata Structure and Methods
        Dialogs AutomataDialogs; //Winforms Dialogs



        //debug stuff
        public bool loopAsTask = false;

        public AutomataWindow()
        {

            InitializeAutomata();

            // run automataloop as task when in VS
            if (loopAsTask)
            {
                //automata renderloop

                //Action<AutomataModel> firstAction = AutomataLoop;
                //firstAction(AutomataData); 

                //Action actionDelegate = new Action(AutomataLoop);
                //Task task1 = new Task(actionDelegate);
                //task1.Start();




                Task.Run(() => AutomataLoop(AutomataData));
            }


        }

        public void InitializeAutomata()
        {

            AutomataData = new AutomataModel();
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