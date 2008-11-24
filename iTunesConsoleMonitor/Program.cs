using System;
using System.Collections.Generic;
using System.Text;
using iTunesLib;

namespace iTunesConsoleMonitor
{
    class Program
    {
        private static iTunesApp iTunes = new iTunesApp();
        private static iTunesXMLWriter writer = new iTunesXMLWriter(@"Z:\www\backend");
        private static bool run = true;

        static void Main(string[] args)
        {
            Console.WriteLine("Starting...");
            iTunes.OnPlayerPlayEvent += new _IiTunesEvents_OnPlayerPlayEventEventHandler(iTunes_OnPlayerPlayEvent);
            iTunes.OnQuittingEvent += new _IiTunesEvents_OnQuittingEventEventHandler(iTunes_OnQuittingEvent);
            iTunes.OnPlayerPlayingTrackChangedEvent +=new _IiTunesEvents_OnPlayerPlayingTrackChangedEventEventHandler(iTunes_OnPlayerPlayEvent);

            while (run == true)
            {
                //keep running
            }
            Console.WriteLine("iTunes has exited... Console Monitor shutting down...");
        }

        static void iTunes_OnQuittingEvent()
        {
            run = false;
        }

        static void iTunes_OnPlayerPlayEvent(object iTrack)
        {
            //IITTrack track = (IITTrack)iTrack;
            String pname = "Recently Played";
            IITPlaylist playlist = iTunes.LibrarySource.Playlists.get_ItemByName(pname);
            if (playlist != null)
            {
                writer.writeTracks(playlist);
            }
            else
            {
                Console.WriteLine("The playlist '" + pname + "' cannot be found.");
            }
        }
    }
}
