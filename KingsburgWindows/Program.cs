using System;

namespace TylerButler.Kingsburg.Windows
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (KingsburgWindowsGame game = new KingsburgWindowsGame())
            {
                game.Run();
            }
        }
    }
}

