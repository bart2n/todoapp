using System;
using System.Drawing;
using System.Windows.Forms;

namespace todoapp
{
    public class InputDialog : Form
    {
        private TextBox txtInput;
        private Button btnOk;
        private Button btnCancel;

        public string InputText { get; private set; }

        public InputDialog(string title, string promptText, string defaultValue = "")
        {
            this.Text = title;
            this.Size = new Size(300, 150);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            Label lblPrompt = new Label() { Left = 10, Top = 10, Text = promptText, AutoSize = true };
            txtInput = new TextBox() { Left = 10, Top = 35, Width = 260, Text = defaultValue };

            btnOk = new Button() { Text = "OK", Left = 110, Width = 75, Top = 70, DialogResult = DialogResult.OK };
            btnCancel = new Button() { Text = "Cancel", Left = 190, Width = 75, Top = 70, DialogResult = DialogResult.Cancel };

            btnOk.Click += (sender, e) =>
            {
                InputText = txtInput.Text;
                this.Close();
            };

            btnCancel.Click += (sender, e) =>
            {
                this.Close();
            };

            this.Controls.Add(lblPrompt);
            this.Controls.Add(txtInput);
            this.Controls.Add(btnOk);
            this.Controls.Add(btnCancel);

            this.AcceptButton = btnOk;
            this.CancelButton = btnCancel;
        }
    }
}
