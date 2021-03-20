using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutomataUI
{
    public class Dialogs
    {
        Form inputBox;
        
        public Dialogs()
        {
            inputBox = new Form();
            inputBox.Location = new Point(Cursor.Position.X, Cursor.Position.Y);
        }

        public void TestMyForm()
        {
            
            inputBox.ShowDialog();
            inputBox.Location = new Point(Cursor.Position.X, Cursor.Position.Y);
        }

        public static DialogResult AddState(ref string input, ref int frames, string DialogName)
        {

            System.Drawing.Size size = new System.Drawing.Size(200, 100);

            Form inputBox = new Form();

            inputBox.StartPosition = FormStartPosition.Manual;
            inputBox.Location = new Point(Cursor.Position.X, Cursor.Position.Y);

            inputBox.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            inputBox.ClientSize = size;
            inputBox.Text = DialogName;

            System.Windows.Forms.TextBox textBox = new TextBox();
            textBox.Size = new System.Drawing.Size(size.Width - 10, 23);
            textBox.Location = new System.Drawing.Point(5, 5);
            textBox.Text = input;
            inputBox.Controls.Add(textBox);

            System.Windows.Forms.NumericUpDown timeUpDown = new System.Windows.Forms.NumericUpDown();
            timeUpDown.Name = "Time(f)";
            timeUpDown.Location = new System.Drawing.Point(5, 39);
            timeUpDown.Size = new System.Drawing.Size(80, 20);
            timeUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            timeUpDown.Maximum = Decimal.MaxValue;
            timeUpDown.Value = frames;
            timeUpDown.TabIndex = 0;
            inputBox.Controls.Add(timeUpDown);

            System.Windows.Forms.Label framesLabel = new System.Windows.Forms.Label();
            framesLabel.Location = new System.Drawing.Point(88, 42);
            framesLabel.Size = new System.Drawing.Size(100, 23);
            framesLabel.Text = "Locked(f)";
            inputBox.Controls.Add(framesLabel);

            Button okButton = new Button();
            okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            okButton.Name = "okButton";
            okButton.Size = new System.Drawing.Size(90, 23);
            okButton.Text = "&OK";
            okButton.Location = new System.Drawing.Point(5, 70);
            inputBox.Controls.Add(okButton);

            Button cancelButton = new Button();
            cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new System.Drawing.Size(90, 23);
            cancelButton.Text = "&Cancel";
            cancelButton.Location = new System.Drawing.Point(size.Width - 95, 70);
            inputBox.Controls.Add(cancelButton);

            inputBox.AcceptButton = okButton;
            inputBox.CancelButton = cancelButton;

           // inputBox.Scale(dpi);
            //inputBox.Size = new Size(1.0f);

            DialogResult result = inputBox.ShowDialog();
            if (textBox.Text.Length > 0) input = textBox.Text;
            else input = "empty";
            frames = Convert.ToInt16(timeUpDown.Value);
            return result;
        }

        
        public static DialogResult ShowTransitionDialog(ref string input, ref int frames, ref bool pingpong, string DialogName, float dpi)
        {
            System.Drawing.Size size = new System.Drawing.Size(200, 130);

            Form inputBox = new Form();

            inputBox.StartPosition = FormStartPosition.Manual;
            inputBox.Location = new Point(Cursor.Position.X, Cursor.Position.Y);

            inputBox.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            inputBox.ClientSize = size;
            inputBox.Text = DialogName;

            System.Windows.Forms.TextBox textBox = new TextBox();
            textBox.Size = new System.Drawing.Size(size.Width - 10, 23);
            textBox.Location = new System.Drawing.Point(5, 5);
            textBox.Text = input;
            inputBox.Controls.Add(textBox);

            System.Windows.Forms.NumericUpDown timeUpDown = new System.Windows.Forms.NumericUpDown();
            timeUpDown.Name = "Time(f)";
            timeUpDown.Location = new System.Drawing.Point(5, 39);
            timeUpDown.Size = new System.Drawing.Size(80, 20);
            timeUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            timeUpDown.Maximum = Decimal.MaxValue;
            timeUpDown.TabIndex = 0;
            timeUpDown.Value = frames;
            inputBox.Controls.Add(timeUpDown);

            System.Windows.Forms.Label framesLabel = new System.Windows.Forms.Label();
            framesLabel.Location = new System.Drawing.Point(88, 42);
            framesLabel.Size = new System.Drawing.Size(100, 23);
            framesLabel.Text = "Duration(f)";
            inputBox.Controls.Add(framesLabel);

            System.Windows.Forms.CheckBox isPingPong = new System.Windows.Forms.CheckBox();
            isPingPong.Location = new System.Drawing.Point(70, 70);
            isPingPong.Text = "PingPong";
            isPingPong.Checked = pingpong; // getting the bool from the transition object
            inputBox.Controls.Add(isPingPong);



            Button okButton = new Button();
            okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            okButton.Name = "okButton";
            okButton.Size = new System.Drawing.Size(90, 23);
            okButton.Text = "&OK";
            okButton.Location = new System.Drawing.Point(5, 99);
            inputBox.Controls.Add(okButton);

            Button cancelButton = new Button();
            cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new System.Drawing.Size(90, 23);
            cancelButton.Text = "&Cancel";
            cancelButton.Location = new System.Drawing.Point(size.Width - 95, 99);
            inputBox.Controls.Add(cancelButton);

            inputBox.AcceptButton = okButton;
            inputBox.CancelButton = cancelButton;

            inputBox.Scale(dpi);

            DialogResult result = inputBox.ShowDialog();
            if (textBox.Text.Length > 0) input = textBox.Text;
            else input = "empty";
            frames = Convert.ToInt16(timeUpDown.Value);
            pingpong = isPingPong.Checked;
            return result;
        }
    }
}
