using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using TylerButler.Kingsburg.Core;

namespace KingsburgWinForms
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            GameManager.Instance.UI = new UIManagerWinForms();
            GameManager.Instance.MainExecutionMethod();

            //Application.Run(new Form1());
        }
    }
}
