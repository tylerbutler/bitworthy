using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Bitworthy.Games.Acquire;
using Bitworthy.Games.Acquire.Components;

namespace Acquire
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
            
            GameManager gm = GameManager.Instance;
            //Application.Run(PostMergerChoice);

            Application.Run(GameWindow.Instance);

            //gm.StartGame();
            
        }
    }
}