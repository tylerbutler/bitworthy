using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using iTunesLib;
using System.IO;
using System.Reflection;
using bitworthy;

namespace iTunes_XML_Feeder
{
    class iTunesXMLWriter
    {
        private XmlTextWriter writer;
        private string PathIn;

        public iTunesXMLWriter(string path)
        {
            writer = new XmlTextWriter(@path + @"\nowplaying.xml", Encoding.UTF8);
            writer.Formatting = Formatting.Indented;
            PathIn = path;
        }

        public void writeTracks(IITPlaylist playlist, int num)
        {
            deleteImages();

            writer.WriteStartDocument();
            writer.WriteProcessingInstruction("xml-stylesheet", "type=\"text/xsl\" href=\"fullview.xsl\"");
            writer.WriteStartElement("iTunesXML");

            foreach (int i in new Range(1, num))
            {
                IITTrack track = playlist.Tracks.get_ItemByPlayOrder(i);
                writeTrack(track);
            }
            writer.WriteEndElement();
            writer.WriteEndDocument();

            writer.Flush();
            writer.Close();
        }

        public void writeTracks(IITPlaylist playlist)
        {
            writeTracks(playlist, 10);
        }

        private void writeTrack(IITTrack track)
        {
            int i = track.PlayOrderIndex;

            writer.WriteStartElement("Track");
            writer.WriteAttributeString("Name", track.Name == "" ? "NO NAME" : track.Name);
            writer.WriteElementString("Number", i.ToString());
            writer.WriteElementString("Artist", track.Artist == "" ? "NO ARTIST" : track.Artist);
            writer.WriteElementString("Album", track.Album == "" ? "NO ALBUM" : track.Album);
            writer.WriteElementString("Image", exportImage(track));
            writer.WriteElementString("Rating", (track.Rating / 20).ToString());
            writer.WriteElementString("LastPlayed", getFriendlyDate(track.PlayedDate));
            writer.WriteEndElement();
        }

        private string getFriendlyDate(DateTime datetime)
        {
            string toReturn, date, time;
            /*DateTime now = DateTime.Now;

            if (datetime.Date.CompareTo(now.Date) == 0)
            {
                date = "Today";
            }
            else if (datetime.Date.CompareTo(now.Date.AddDays(-1).Date) == 0)
            {
                date = "Yesterday";
            }
            else
            {
                date = datetime.Date.ToString("D");
            }*/

            toReturn = datetime.ToShortDateString() + ", " + datetime.ToShortTimeString();
            return toReturn;
        }

        private string exportImage(IITTrack track)
        {
            string toSavePath = Path.Combine("\\iTunesImages\\", track.TrackDatabaseID + ".jpg");
            if (track.Artwork.Count > 0)
            {
                IITArtwork art = track.Artwork[1];
                string s = Path.Combine(PathIn, toSavePath);
                art.SaveArtworkToFile(PathIn + toSavePath);
                return "/backend/iTunesImages/" + track.TrackDatabaseID + ".jpg";
            }
            else
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                Stream image = assembly.GetManifestResourceStream("iTunes_XML_Feeder.Resources.NoArt.jpg");
                FileStream fs = new FileStream(PathIn + @"\iTunesImages\NoArt.jpg", FileMode.Create, FileAccess.Write);
                BinaryWriter bw = new BinaryWriter(fs);
                BinaryReader br = new BinaryReader(image);
                bw.Write(br.ReadBytes((int)image.Length));
                bw.Flush();
                bw.Close();
                br.Close();
                fs.Close();
                image.Close();
                return "iTunesImages/NoArt.jpg";
            }
        }

        private void deleteImages()
        {
            DirectoryInfo dir = new DirectoryInfo(PathIn + @"\iTunesImages\");
            try
            {
                foreach (FileInfo file in dir.GetFiles("*.jpg", SearchOption.TopDirectoryOnly))
                {
                    try
                    {
                        file.Delete();
                    }
                    catch (Exception e)
                    {
                        Console.Beep();
                        Console.Write(e);
                    }
                }
            }
            catch (DirectoryNotFoundException e)
            {
                // do nothing
            }
        }

    }
}
