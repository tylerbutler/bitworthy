using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using iTunesLib;
using System.Windows.Forms;

namespace iTunes_Tagger
{
    class TagDB
    {
        private iTunesApp iTunes = new iTunesApp();
        private Dictionary<Tag, ArrayListOne> tags2tracks = new Dictionary<Tag, ArrayListOne>(new TagComparer());
        private Dictionary<IITTrack, ArrayListOne> tracks2tags = new Dictionary<IITTrack, ArrayListOne>();
        private Dictionary<int, IITTrack> trackIDMap = new Dictionary<int, IITTrack>();

        public void AddTagToTrack(Tag tag, IITTrack track)
        {
            if (!tracks2tags.ContainsKey(track))
            {
                tracks2tags.Add(track, new ArrayListOne());
            }

            if (!tags2tracks.ContainsKey(tag))
            {
                tags2tracks[tag] = new ArrayListOne();
            }
            tracks2tags[track].Add(tag);
            tags2tracks[tag].Add(track);
            trackIDMap[track.TrackDatabaseID] = track;
        }

        public void AddTagToTrack(string tag, IITTrack track)
        {
            AddTagToTrack(new Tag(tag), track);
        }

        public void AddTag(Tag tag)
        {
            //TODO: Implement this
        }

        public void RemoveTagFromTrack(Tag tag, IITTrack track)
        {
            //TODO: Implement this
        }

        private void WriteOutTagsToTrack(IITTrack track)
        {

        }

        public TagDB RebuildLibrary()
        {
            iTunesApp iTunes = new iTunesApp();
            return RebuildLibrary(iTunes.LibraryPlaylist, null);
        }

        public TagDB RebuildLibrary(IITPlaylist playlist, Label progress)
        {
            if (progress == null)
            {
                progress = new Label();
            }

            tracks2tags.Clear();
            tags2tracks.Clear();

            //Scan for tracks w/ comments
            int i = 0;
            int numTracks = playlist.Tracks.Count;
            Console.WriteLine("Scanning tracks for comments...");
            ArrayList commentTracks = new ArrayList();
            foreach (IITTrack track in playlist.Tracks)
            {
                if (i % 100 == 0)
                {
                    progress.Text = "Progress: Scanned " + i + " tracks out of " + numTracks + ". " + commentTracks.Count + " tagged tracks found.";
                }

                if (track.Comment != null && track.Comment != "")
                {
                    commentTracks.Add(track);
                }

                i++;
            }

            foreach (IITTrack track in commentTracks)
            {
                //get tags
                foreach (Tag tag in GetTagsFromTrack(track))
                {
                    AddTagToTrack(tag, track);
                }
            }

            progress.Text = "Progress: Found " + commentTracks.Count + " tracks using " + this.GetTags().Count + " tags.";

            return this;
        }

        private ArrayList GetTagsFromTrack(IITTrack track)
        {
            ArrayList tags = new ArrayList();
            char[] splitter = { ';' };
            string[] stringTags = track.Comment.Split(splitter, StringSplitOptions.RemoveEmptyEntries);
            foreach (string s in stringTags)
            {
                tags.Add(new Tag(s));
            }

            return tags;
        }

        public Dictionary<Tag, ArrayListOne>.KeyCollection GetTags()
        {
            return tags2tracks.Keys;
        }

        public ArrayList GetTracksForTag(Tag tag)
        {
            return tags2tracks[tag];
        }

        public ArrayList GetTagsForTrack(IITTrack track)
        {
            return tracks2tags[track];
        }

        public IITTrack GetTrackByID(int id)
        {
            return trackIDMap[id];
        }
    }

    public class Tag : IEquatable<Tag>
    {
        private string name;

        public Tag(string nameIn)
        {
            this.Name = nameIn;
        }

        public string Name
        {
            get { return name; }
            set { name = value.Trim().ToLower(); }
        }

        public override string ToString()
        {
            return this.Name;
        }

        #region IEquatable<Tag> Members

        public bool Equals(Tag other)
        {
            if (this.Name == other.Name)
            {
                return true;
            }
            else return false;
        }

        #endregion
    }

    class TagComparer : IEqualityComparer<Tag>
    {

        #region IEqualityComparer<Tag> Members

        public bool Equals(Tag x, Tag y)
        {
            return x.Equals(y);
        }

        public int GetHashCode(Tag obj)
        {
            int code = obj.Name.GetHashCode();
            return code;
        }

        #endregion
    }

    class TagCollection : ICollection
    {
        public TagCollection()
        { }

        #region ICollection Members

        public void CopyTo(Array array, int index)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int Count
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        public bool IsSynchronized
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        public object SyncRoot
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        #endregion

        #region IEnumerable Members

        public IEnumerator GetEnumerator()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion

    }

    class ArrayListOne : ArrayList
    {
        public override int Add(object value)
        {
            if (!base.Contains(value))
            {
                return base.Add(value);
            }
            else { return 0; } // do nothing
        }
    }
}
