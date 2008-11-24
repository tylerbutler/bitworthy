using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using iTunesLib;
using System.Collections;

namespace iTunes_Tagger
{
    public partial class Tagger : Form
    {
        private iTunesApp iTunes = new iTunesApp();
        private TagDB db = new TagDB();

        public Tagger()
        {
            InitializeComponent();

            //Hashtable h = new Hashtable();
            //ArrayList deleteComments = new ArrayList();
            //foreach (IITTrack track in scanTracks())
            //{
            //    if (hasKnownComment(track))
            //    {
            //        //h.Add(track.Comment, track);
            //    }
            //    else
            //    {
            //        deleteComments.Add(track);
            //    }
            //}

            //iTunes.CreatePlaylist("Delete Comments");
            //IITUserPlaylist p = (IITUserPlaylist)iTunes.LibrarySource.Playlists.get_ItemByName("Delete Comments");
            //foreach (IITTrack track in deleteComments)
            //{
            //    object o = track;
            //    p.AddTrack(ref o);
            //}
        }

        private ArrayList scanTracks()
        {
            int i = 0;
            int numTracks = iTunes.LibraryPlaylist.Tracks.Count;
            Console.WriteLine("Scanning tracks for comments...");
            ArrayList commentTracks = new ArrayList();
            foreach (IITTrack track in iTunes.LibraryPlaylist.Tracks)
            {
                if (i % 100 == 0)
                {
                    Console.WriteLine("Scanning Track " + (i + 1) + " of " + numTracks + "...");
                }

                if (track.Comment != null && track.Comment != "")
                {
                    commentTracks.Add(track);
                }

                i++;
            }
            Console.WriteLine(commentTracks.Count + " tracks with comments.");
            return commentTracks;
        }

        private bool hasKnownComment(IITTrack track)
        {
            string comment = track.Comment;

            if (comment.Contains("80s") ||
                comment.Contains("Dance") ||
                comment.Contains("The Drive") ||
                comment.Contains("Instrumental") ||
                comment.Contains("Story Songs") ||
                comment.Contains("Perform") ||
                comment.Contains("Brad") ||
                comment.Contains("Cerney") ||
                comment.Contains("Dan") ||
                comment.Contains("Gunz") ||
                comment.Contains("Ricardo") ||
                comment.Contains("Chill") ||
                comment.Contains("Grind") ||
                comment.Contains("Love") ||
                comment.Contains("Memory Lane") ||
                comment.Contains("Upbeat"))
            {
                return true;
            }

            return false;
        }
    }
}