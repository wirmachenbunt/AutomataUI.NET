namespace AutomataUI.Editor
{
    partial class AutomataUserControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Dock = System.Windows.Forms.DockStyle.Fill;

            this.skiaView = new SkiaSharp.Views.Desktop.SKControl();
            this.SuspendLayout();

            
            // 
            // skiaView
            // 
            this.skiaView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.skiaView.Location = new System.Drawing.Point(0, 0);
            this.skiaView.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.skiaView.Name = "skiaView";
            this.skiaView.Size = new System.Drawing.Size(1886, 1045);
            this.skiaView.TabIndex = 0;
            this.skiaView.Text = "skControl1";
            this.skiaView.PaintSurface += new System.EventHandler<SkiaSharp.Views.Desktop.SKPaintSurfaceEventArgs>(this.skiaView_PaintSurface);
            // 
            // AutomataUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.skiaView);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "AutomataUserControl";
            this.Size = new System.Drawing.Size(1886, 1045);
            this.ResumeLayout(false);

            this.skiaView.MouseMove += SkiaView_MouseMove1;

            
        }

        private void SkiaView_MouseMove1(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            //
            PosX = e.X;
            skiaView.Invalidate();
        }

        #endregion

        public SkiaSharp.Views.Desktop.SKControl skiaView;


        
    }
}
