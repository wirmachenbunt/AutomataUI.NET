
using System;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    partial class Form1
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        /// 

        private SkiaSharp.Views.Desktop.SKGLControl skiaView;
        private void InitializeComponent()
        {
            //this.components = new System.ComponentModel.Container();
            //this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            //this.ClientSize = new System.Drawing.Size(800, 450);
            //this.Text = "Form1";


            this.skiaView = new SkiaSharp.Views.Desktop.SKGLControl();

            this.SuspendLayout();
            // 
            // skiaView
            // 
            this.skiaView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.skiaView.Location = new System.Drawing.Point(0, 0);
            this.skiaView.Name = "skiaView";
            this.skiaView.Size = new System.Drawing.Size(774, 529);
            this.skiaView.TabIndex = 0;
            this.skiaView.Text = "skControl1";
            this.skiaView.PaintSurface += new System.EventHandler<SkiaSharp.Views.Desktop.SKPaintGLSurfaceEventArgs>(this.skiaView_PaintSurface);
            // 
            // Form1
            // 
            this.skiaView.MouseMove += Form1_MouseMove;
            this.skiaView.MouseWheel += Form1_MouseWheel;

            this.AutoScaleDimensions = new System.Drawing.SizeF(192F, 192F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(774, 529);
            this.Controls.Add(this.skiaView);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
        }

       

        #endregion
    }
}

