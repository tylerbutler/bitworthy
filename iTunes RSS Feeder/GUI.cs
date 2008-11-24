using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using iTunesLib;


namespace iTunes_XML_Feeder
{
    public partial class GUI : Form
    {
        iTunesXMLWriter writer = new iTunesXMLWriter(@"\\mediabox\C$\www\backend");
        iTunesApp iTunes = new iTunesAppClass();

        public GUI()
        {
            InitializeComponent();

            IITUserPlaylist p2;

            /*if ((p2 = (IITUserPlaylist)iTunes.LibrarySource.Playlists.get_ItemByName("Need Album Art")) != null)
            {
                p2.Delete();
            }

            p2 = (IITUserPlaylist)iTunes.CreatePlaylist("Need Album Art");

            foreach (IITTrack t in iTunes.LibraryPlaylist.Tracks)
            {
                if (t.Artwork.Count == 0)
                {
                    object o = (object)t;
                    p2.AddTrack(ref o);
                    Console.WriteLine("Adding " + t.Name);
                }
            }*/


            foreach (IITPlaylist playlist in iTunes.LibrarySource.Playlists)
            {
                playlistBox.Items.Add(playlist.Name);
            }

            foreach( IITWindow window in iTunes.Windows )
            {
                int i = 0;
            }

            IITPlaylist p = iTunes.LibrarySource.Playlists.get_ItemByName("Recently Played");
            writer.writeTracks(p, 10);

            //iTunes.OnPlayerPlayEvent += new _IiTunesEvents_OnPlayerPlayEventEventHandler(iTunes_OnPlayerPlayEvent);
            //iTunes.OnPlayerStopEvent += new _IiTunesEvents_OnPlayerStopEventEventHandler(iTunes_OnPlayerStopEvent);

            //testMethod();
        }

        void iTunes_OnPlayerStopEvent(object iTrack)
        {
            IITTrack t = (IITTrack)iTrack;
            Console.WriteLine(iTunes.PlayerPosition);
        }

        void iTunes_OnPlayerPlayEvent(object iTrack)
        {
            //message.Text = "Event fired.";
            Console.WriteLine("Event fired.");
            IITPlaylist p = iTunes.LibrarySource.Playlists.get_ItemByName("Recently Played");
            IITTrack current = iTunes.CurrentTrack;
            Console.WriteLine(current.Name);
            //writer.writeTracks(p);
        }

        void testMethod()
        {
            string plPending = "(pending rating)";
            string plRecent = "Recently Played";
            int nrUp = 20;
            int nrDown = -20;

            if (iTunes.LibrarySource.Playlists.get_ItemByName(plPending) == null)
            {
                iTunes.CreatePlaylist(plPending);
            }
            IITUserPlaylist playlistPending = (IITUserPlaylist)iTunes.LibrarySource.Playlists.get_ItemByName(plPending);
            IITTrack thisPending = playlistPending.Tracks[playlistPending.Tracks.Count-1];
            IITUserPlaylist playlistRecent = (IITUserPlaylist)iTunes.LibrarySource.Playlists.get_ItemByName(plRecent);
            IITTrack thisRecent = playlistRecent.Tracks[playlistRecent.Tracks.Count-1];
            IITTrack thisTrack = null;
            bool isCurrent = false, alreadyPending = false;

            if (iTunes.CurrentTrack != null)
            {
                thisTrack = iTunes.CurrentTrack;
                isCurrent = true;
            }

            if (thisPending.TrackDatabaseID == thisRecent.TrackDatabaseID)
            {
                thisPending.Rating = newRating(thisPending.Rating, nrUp);
                thisPending.Comment = thisPending.Comment + "AutoRated";
                thisPending = null;
            }
            else
            {
                if (isCurrent)
                {
                    if (thisPending.TrackDatabaseID == thisTrack.TrackDatabaseID)
                    {
                        alreadyPending = true;
                    }
                    else
                    {
                        thisPending.Rating = newRating(thisPending.Rating, nrDown);
                        thisPending.Comment = thisPending.Comment + "AutoRated";
                        thisPending = null;
                    }
                }
                else
                {
                    thisPending.Rating = newRating(thisPending.Rating, nrDown);
                    thisPending.Comment = thisPending.Comment + "AutoRated";
                    thisPending = null;
                }
            }

            if (isCurrent && !alreadyPending)
            {
                object o = (object)thisTrack;
                playlistPending.AddTrack(ref o);
            }
        }

        private int newRating(int old, int direction)
        {
            int rating = old + direction;
            if (rating > 100)
                rating = 100;
            if (rating < 40 && direction > 0)
                rating = 40;
            if (rating < 20)
                rating = 20;
            if (old == 0 && direction < 0)
                rating = 0;
            return rating;
        }

    }
}
