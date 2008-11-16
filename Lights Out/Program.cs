using System;

namespace Lights_Out
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (LightsOutGame game = new LightsOutGame())
            {
                game.Run();
            }
        }
    }
}
