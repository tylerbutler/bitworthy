using System;
using System.IO;

namespace Bitworthy.Utilities
{
    public class Logger
    {
        #region Fields
        private static bool debug = true;
        private static bool error = true;
        private static bool info = false;
        private static bool output = true;
        private static string filepath;
        #endregion

        #region Properties

        private static string GenerateDefaultLogFileName( string BaseFileName )
        {
            return AppDomain.CurrentDomain.BaseDirectory + "\\" + BaseFileName + "_" + DateTime.Now.Month + "_" + DateTime.Now.Day + "_"
                + DateTime.Now.Year + ".log";
        }

        public static bool Debug
        {
            get
            {
                return Logger.debug;
            }
            set
            {
                Logger.debug = value;
            }
        }

        public static bool Error
        {
            get
            {
                return Logger.error;
            }
            set
            {
                Logger.error = value;
            }
        }

        public static bool Info
        {
            get
            {
                return Logger.info;
            }
            set
            {
                Logger.info = value;
            }
        }

        public static bool Output
        {
            get
            {
                return Logger.output;
            }
            set
            {
                Logger.output = value;
            }
        }
        #endregion

        #region Constructors
        public Logger( string basefilename )
        {
            Logger.filepath = Logger.GenerateDefaultLogFileName(basefilename);
        }

        public Logger() : this( Logger.GenerateDefaultLogFileName( "Logs" ) )
        {
        }
        #endregion

        public static void Log( string msg, LogLevels lvl )
        {
            // TODO Add logic to log different lvl errors differently
            StreamWriter writer = File.AppendText( Logger.filepath );
            writer.WriteLine( msg );
            throw new System.NotImplementedException();
        }

    }

    public enum LogLevels
    {
        Debug = 0,
        Error,
        Info,
        Output
    }
}
