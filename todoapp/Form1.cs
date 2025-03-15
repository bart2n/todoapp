using System;
using System.Drawing;
using System.Windows.Forms;
using todoapp;
using System.IO;
using System.Linq;

namespace todoApp
{
    public partial class Form1 : Form
    {
        private TextBox txtTask;
        private Button btnAdd;
        private Button btnEdit;
        private Button btnDelete;
        private Button btnMarkAsDone;
        private ListBox lstTasks;
        private Label lblTitle;
        private Label lblTotalTasks;
        private RadioButton rdbHigh;
        private RadioButton rdbMedium;
        private RadioButton rdbLow;
        private CheckBox chkDarkMode;
        private Button btnColorPicker;
        private Label lblPriorityTitle;

        public Form1()
        {
            this.Name = "frmToDoList";
            this.Text = "To-Do List Application";
            this.Size = new Size(500, 450);
            this.BackColor = Color.LightSteelBlue;
            this.StartPosition = FormStartPosition.CenterScreen;

            lblTitle = new Label();
            lblTitle.Name = "lblTitle";
            lblTitle.Text = "To-Do List";
            lblTitle.Size = new Size(250, 30);
            lblTitle.Location = new Point(125, 10);
            lblTitle.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            lblTitle.ForeColor = Color.Navy;
            lblTitle.BackColor = Color.Transparent;

            txtTask = new TextBox();
            txtTask.Name = "txtTask";
            txtTask.Text = string.Empty;
            txtTask.Size = new Size(320, 30);
            txtTask.Location = new Point(20, 50);
            txtTask.Font = new Font("Segoe UI", 12, FontStyle.Regular);
            txtTask.BackColor = Color.White;
            txtTask.ForeColor = Color.Black;

            btnAdd = new Button();
            btnAdd.Name = "btnAdd";
            btnAdd.Text = "Add Task";
            btnAdd.Size = new Size(120, 30);
            btnAdd.Location = new Point(350, 50);
            btnAdd.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnAdd.BackColor = Color.LightGreen;
            btnAdd.Click += BtnAdd_Click;

            lstTasks = new ListBox();
            lstTasks.Name = "lstTasks";
            lstTasks.Size = new Size(450, 130);
            lstTasks.Location = new Point(20, 90);
            lstTasks.Font = new Font("Segoe UI", 12, FontStyle.Regular);
            lstTasks.BackColor = Color.Gray;

            btnEdit = new Button();
            btnEdit.Name = "btnEdit";
            btnEdit.Text = "Edit Task";
            btnEdit.Size = new Size(140, 30);
            btnEdit.Location = new Point(20, 300);
            btnEdit.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnEdit.BackColor = Color.LightBlue;
            btnEdit.Click += BtnEdit_Click;

            btnDelete = new Button();
            btnDelete.Name = "btnDelete";
            btnDelete.Text = "Delete Task";
            btnDelete.Size = new Size(140, 30);
            btnDelete.Location = new Point(180, 300);
            btnDelete.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnDelete.BackColor = Color.LightCoral;
            btnDelete.Click += BtnDelete_Click;

            btnMarkAsDone = new Button();
            btnMarkAsDone.Name = "btnMarkComplete";
            btnMarkAsDone.Text = "Mark Completed";
            btnMarkAsDone.Size = new Size(180, 30);
            btnMarkAsDone.Location = new Point(340, 300);
            btnMarkAsDone.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnMarkAsDone.BackColor = Color.Gray;
            btnMarkAsDone.Click += BtnMarkAsDone_Click;

            lblTotalTasks = new Label();
            lblTotalTasks.Name = "lblTotalTasks";
            lblTotalTasks.Text = "Total Tasks: 0";
            lblTotalTasks.Size = new Size(250, 30);
            lblTotalTasks.Location = new Point(20, 340);
            lblTotalTasks.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            lblTotalTasks.ForeColor = Color.Purple;
            lblTotalTasks.BackColor = Color.DarkBlue;

            this.Controls.Add(lblTitle);
            this.Controls.Add(txtTask);
            this.Controls.Add(btnAdd);
            this.Controls.Add(lstTasks);
            this.Controls.Add(btnEdit);
            this.Controls.Add(btnDelete);
            this.Controls.Add(btnMarkAsDone);
            this.Controls.Add(lblTotalTasks);

            lblPriorityTitle = new Label();
            lblPriorityTitle.Text = "Priority:";
            lblPriorityTitle.Size = new Size(80, 20);
            lblPriorityTitle.Location = new Point(20, 230);
            lblPriorityTitle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            lblPriorityTitle.ForeColor = Color.Black;
            this.Controls.Add(lblPriorityTitle);

            rdbHigh = new RadioButton();
            rdbHigh.Text = "High";
            rdbHigh.Location = new Point(20, 260);
            rdbHigh.ForeColor = Color.Black;
            rdbHigh.Checked = true;
            rdbHigh.CheckedChanged += PriorityRadio_CheckedChanged;

            rdbMedium = new RadioButton();
            rdbMedium.Text = "Medium";
            rdbMedium.Location = new Point(200, 260);
            rdbMedium.ForeColor = Color.Black;
            rdbMedium.CheckedChanged += PriorityRadio_CheckedChanged;

            rdbLow = new RadioButton();
            rdbLow.Text = "Low";
            rdbLow.Location = new Point(400, 260);
            rdbLow.ForeColor = Color.Black;
            rdbLow.CheckedChanged += PriorityRadio_CheckedChanged;

            this.Controls.Add(rdbHigh);
            this.Controls.Add(rdbMedium);
            this.Controls.Add(rdbLow);

            lstTasks.DrawMode = DrawMode.OwnerDrawFixed;
            lstTasks.ItemHeight = 25;
            lstTasks.DrawItem += LstTasks_DrawItem;
            lstTasks.SelectedIndexChanged += LstTasks_SelectedIndexChanged;

            chkDarkMode = new CheckBox();
            chkDarkMode.Text = "Dark Mode";
            chkDarkMode.Location = new Point(400, 10);
            chkDarkMode.CheckedChanged += ChkDarkMode_CheckedChanged;
            this.Controls.Add(chkDarkMode);

            btnColorPicker = new Button();
            btnColorPicker.Text = "Customize Background";
            btnColorPicker.Size = new Size(180, 30);
            btnColorPicker.Location = new Point(300, 350);
            btnColorPicker.BackColor = Color.Red;
            btnColorPicker.Click += BtnColorPicker_Click;
            this.Controls.Add(btnColorPicker);

            this.Load += MainForm_Load;
            this.FormClosing += Form1_FormClosing;
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtTask.Text))
            {
                string prefix = "L|";
                if (rdbHigh.Checked) prefix = "H|";
                else if (rdbMedium.Checked) prefix = "M|";
                string itemStr = prefix + txtTask.Text.Trim();
                lstTasks.Items.Add(itemStr);
                txtTask.Clear();
                UpdateTaskCount();
            }
            else
            {
                MessageBox.Show("Please enter a task.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (lstTasks.SelectedItem != null)
            {
                string currentItem = lstTasks.SelectedItem.ToString();
                bool isCompleted = false;
                if (currentItem.StartsWith("✓ "))
                {
                    isCompleted = true;
                    currentItem = currentItem.Substring(2);
                }
                string priority = "L";
                if (currentItem.Length >= 2 && currentItem[1] == '|')
                {
                    priority = currentItem[0].ToString();
                    currentItem = currentItem.Substring(2);
                }
                using (InputDialog dialog = new InputDialog("Edit Task", "Edit task:", currentItem))
                {
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        string newTask = dialog.InputText;
                        if (!string.IsNullOrWhiteSpace(newTask))
                        {
                            int index = lstTasks.SelectedIndex;
                            string newItem = priority + "|" + newTask.Trim();
                            if (isCompleted)
                            {
                                newItem = "✓ " + newItem;
                            }
                            lstTasks.Items[index] = newItem;
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a task to edit.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (lstTasks.SelectedItem != null)
            {
                lstTasks.Items.Remove(lstTasks.SelectedItem);
                UpdateTaskCount();
            }
            else
            {
                MessageBox.Show("Please select a task to delete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnMarkAsDone_Click(object sender, EventArgs e)
        {
            if (lstTasks.SelectedItem != null)
            {
                int index = lstTasks.SelectedIndex;
                string task = lstTasks.Items[index].ToString();
                if (!task.StartsWith("✓ "))
                {
                    lstTasks.Items[index] = "✓ " + task;
                }
            }
            else
            {
                MessageBox.Show("Please select a task to mark as done.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateTaskCount()
        {
            lblTotalTasks.Text = "Total Tasks: " + lstTasks.Items.Count;
        }

        private void LstTasks_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;
            string itemText = lstTasks.Items[e.Index].ToString();
            e.DrawBackground();
            bool isCompleted = false;
            if (itemText.StartsWith("✓ "))
            {
                isCompleted = true;
                itemText = itemText.Substring(2);
            }
            string priority = "L";
            if (itemText.Length >= 2 && itemText[1] == '|')
            {
                priority = itemText[0].ToString();
                itemText = itemText.Substring(2);
            }
            Color textColor = Color.Green;
            if (priority == "H") textColor = Color.Red;
            else if (priority == "M") textColor = Color.Blue;
            if (isCompleted)
            {
                itemText = "✓ " + itemText;
            }
            using (Brush brush = new SolidBrush(textColor))
            {
                e.Graphics.DrawString(itemText, lstTasks.Font, brush, e.Bounds);
            }
            e.DrawFocusRectangle();
        }

        private void ChkDarkMode_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDarkMode.Checked)
            {
                this.BackColor = Color.Black;
                lblTitle.ForeColor = Color.White;
                lblTotalTasks.ForeColor = Color.White;
                txtTask.BackColor = Color.DimGray;
                txtTask.ForeColor = Color.White;
                lblPriorityTitle.ForeColor = Color.White;
                rdbHigh.ForeColor = Color.White;
                rdbMedium.ForeColor = Color.White;
                rdbLow.ForeColor = Color.White;
                lstTasks.BackColor = Color.Black;
            }
            else
            {
                this.BackColor = Color.LightSteelBlue;
                lblTitle.ForeColor = Color.Navy;
                lblTotalTasks.ForeColor = Color.Purple;
                txtTask.BackColor = Color.White;
                txtTask.ForeColor = Color.Black;
                lblPriorityTitle.ForeColor = Color.Black;
                rdbHigh.ForeColor = Color.Black;
                rdbMedium.ForeColor = Color.Black;
                rdbLow.ForeColor = Color.Black;
                lstTasks.BackColor = Color.Gray;
            }
        }

        private void BtnColorPicker_Click(object sender, EventArgs e)
        {
            using (ColorDialog cd = new ColorDialog())
            {
                if (cd.ShowDialog() == DialogResult.OK)
                {
                    this.BackColor = cd.Color;
                }
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (File.Exists("tasks.txt"))
            {
                string[] lines = File.ReadAllLines("tasks.txt");
                foreach (string line in lines)
                {
                    lstTasks.Items.Add(line);
                }
                UpdateTaskCount();
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            File.WriteAllLines("tasks.txt", lstTasks.Items.Cast<string>());
        }

        private void LstTasks_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstTasks.SelectedItem == null) return;
            string currentItem = lstTasks.SelectedItem.ToString();
            bool isCompleted = false;
            if (currentItem.StartsWith("✓ "))
            {
                isCompleted = true;
                currentItem = currentItem.Substring(2);
            }
            string priority = "L";
            if (currentItem.Length >= 2 && currentItem[1] == '|')
            {
                priority = currentItem[0].ToString();
                currentItem = currentItem.Substring(2);
            }
            if (priority == "H") rdbHigh.Checked = true;
            else if (priority == "M") rdbMedium.Checked = true;
            else rdbLow.Checked = true;
        }

        private void PriorityRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (lstTasks.SelectedItem == null) return;
            int index = lstTasks.SelectedIndex;
            if (index < 0) return;
            string currentItem = lstTasks.Items[index].ToString();
            bool isCompleted = false;
            if (currentItem.StartsWith("✓ "))
            {
                isCompleted = true;
                currentItem = currentItem.Substring(2);
            }
            string oldPriority = "L";
            if (currentItem.Length >= 2 && currentItem[1] == '|')
            {
                oldPriority = currentItem[0].ToString();
                currentItem = currentItem.Substring(2);
            }
            string newPriority = "L";
            if (rdbHigh.Checked) newPriority = "H";
            else if (rdbMedium.Checked) newPriority = "M";
            else if (rdbLow.Checked) newPriority = "L";
            string newItem = newPriority + "|" + currentItem;
            if (isCompleted) newItem = "✓ " + newItem;
            lstTasks.Items[index] = newItem;
            lstTasks.Refresh();
        }

        private ListBox listBox1;
        private Button button1;
        private TextBox textBox1;
        private Button button2;
        private Button button3;
        private Button button4;
    }
}
