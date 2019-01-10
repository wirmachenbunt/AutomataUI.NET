#region usings
using System;
using System.ComponentModel.Composition;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing;

using VVVV.PluginInterfaces.V1;
using VVVV.PluginInterfaces.V2;
using VVVV.Utils.VColor;
using VVVV.Utils.VMath;
using AutomataUI.Editor;

using VVVV.Core.Logging;
#endregion usings

namespace AutomataUI.VVVV
{
    #region PluginInfo
    [PluginInfo(Name = "Template",
                Category = "GUI",
                Help = "Template with some gui elements",
                Tags = "c#",
                AutoEvaluate = true)]
    #endregion PluginInfo
    public class GUITemplateNode : UserControl, IPluginEvaluate
    {
        #region fields & pins

        [Import()]
        public ILogger FLogger;

        //gui controls   
        AutomataUserControl AutomataEditor = new AutomataUserControl();


        #endregion fields & pins

        #region constructor and init

        public GUITemplateNode()
        {
            //setup the gui        
            InitializeComponent();        
        }

       

        void InitializeComponent()
        {
            //clear controls in case init is called multiple times
            Controls.Clear();       
            Controls.Add(AutomataEditor);
        }


        #endregion constructor and init

        //called when data for any output pin is requested
        public void Evaluate(int SpreadMax)
        {
            
        }
    }
}