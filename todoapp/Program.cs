using System;
using System.Windows.Forms;
using todoApp;

namespace ToDoApp
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1()); // Changed from MainForm to Form1
        }
    }
}
