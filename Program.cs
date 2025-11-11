using System;
using System.Windows.Forms;

namespace InterestCalculatorWinForms
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Enable visual styles and start the main form
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(new Form1());
        }
    }
}
