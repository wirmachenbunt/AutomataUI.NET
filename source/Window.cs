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
    public class Window : Form
    {
        AutomataView drawing;
        Interaction automataInteraction;
        AutomataModel automataData;
        
        public Window()
        {
            InitializeComponent();
        } 
         
        private void InitializeComponent()
        {          
            this.SuspendLayout();

            // create skia drawing
            drawing = new AutomataView();

            // create mousekeyboard control for drawing
            automataInteraction = new Interaction(drawing);

            AutoScaleDimensions = new System.Drawing.SizeF(192F, 192F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            ClientSize = new System.Drawing.Size(774, 529);
            Controls.Add(drawing.skiaView);
            Name = "AutomataUI";
            Text = "AutomataUI";
            ResumeLayout(false);
        }
    }
}