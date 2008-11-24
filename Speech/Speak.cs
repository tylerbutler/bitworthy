using System;
using SpeechLib;
using System.Threading;

namespace Speak
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	class Speak
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			SpeechVoiceSpeakFlags flags = SpeechVoiceSpeakFlags.SVSFlagsAsync;
			SpVoice v = new SpVoice();
			
			if( args.Length > 0 )
			{
				v.Speak(args[0], flags);
			}
			else
			{
				v.Speak("Please specify what you would like me to say.", flags );
			}

			v.WaitUntilDone(Timeout.Infinite);
		}
	}
}