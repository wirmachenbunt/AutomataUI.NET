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

/// <summary>
/// Main window providing home for automata view rendering, data and control
/// </summary>

    public class Window : Form
    {
        AutomataView AutomataView; //UI Rendering
        Interaction AutomataInteraction; //User Input Management
        AutomataModel AutomataData; //contains Automata Structure and Methods
        Dialogs AutomataDialogs; //Winforms Dialogs
        
        public Window()
        {
            InitializeAutomata();
        } 
         
        private void InitializeAutomata()
        {
           
            AutomataData = new AutomataModel();
            AutomataView = new AutomataView(AutomataData);
            AutomataDialogs = new Dialogs();
            // create mousekeyboard control for drawing
            AutomataInteraction = new Interaction(AutomataView, AutomataData, AutomataDialogs,this);

            this.SuspendLayout();
            AutoScaleDimensions = new System.Drawing.SizeF(192F, 192F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            ClientSize = new System.Drawing.Size(774, 529);
            Controls.Add(AutomataView.skiaView);
            Name = "AutomataUI";
            Text = "AutomataUI";
            ResumeLayout(false);
        }
    }
}