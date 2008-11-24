using System;
using System.Collections.Generic;
using System.Text;
using mshtml;
using System.Net;
using System.IO;
using System.Xml;
using System.Collections;

namespace PodcastMaker
{
    class Program
    {
        static void Main(string[] args)
        {
            string filename = @"C:\Users\tyler\desktop\test.xml";
            string[] toDownload = {
                "mondayam", "mondaypm",
                "tuesdayam", "tuesdaypm",
                "wednesdayam", "wednesdaypm",
                "thursdayam", "thursdaypm",
                "fridayam", "fridaypm",
                "saturday",
            };
            //string[] toDownload = { "mondayam", "tuesdaypm", "saturday" };
            Hashtable files = new Hashtable();

            foreach (string s in toDownload)
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(ReadFromURL("http://www.paulharvey.com/mac/" + s + ".qtl"));
                string url = doc["embed"].Attributes["href"].Value;
                files.Add(s, url);
            }

            // Write out core RSS XML
            XmlWriter temp = new XmlTextWriter(filename, Encoding.UTF8);
            temp.WriteStartDocument();
            temp.WriteStartElement("rss");
            temp.WriteStartElement("channel");
            temp.Close();

            XmlDocument podcast = new XmlDocument();
            podcast.Load(filename);
            XmlNode channel = podcast["rss"]["channel"];

            XmlAttribute attribute = podcast.CreateAttribute("version");
            attribute.Value = "2.0";
            podcast["rss"].Attributes.Append(attribute);

            XmlElement child = podcast.CreateElement("title");
            child.InnerText = "Paul Harvey Podcast";
            channel.AppendChild(child);

            (child = podcast.CreateElement("link")).InnerText = "http://www.paulharvey.com";
            channel.AppendChild(child);

            (child = podcast.CreateElement("description")).InnerText = "Paul harvey's daily shows in podcast form. Brought to you by http://www.tylerbutler.com.";
            channel.AppendChild(child);

            (child = podcast.CreateElement("language")).InnerText = "en";
            channel.AppendChild(child);

            (child = podcast.CreateElement("pubDate")).InnerText = System.DateTime.Now.ToString("ddd, dd MMM yyyy hh:mm:ss PST");
            channel.AppendChild(child);

            (child = podcast.CreateElement("webMaster")).InnerText = "tyler@tylerbutler.com";
            channel.AppendChild(child);

            (child = podcast.CreateElement("ttl")).InnerText = "1440";
            channel.AppendChild(child);

            child = podcast.CreateElement("image");
            channel.AppendChild(child);

            (child = podcast.CreateElement("url")).InnerText = "http://www.paulharvey.com/graphics/splash/harvey_home_color.gif";
            channel["image"].AppendChild(child);

            (child = podcast.CreateElement("title")).InnerText = "Paul Harvey";
            channel["image"].AppendChild(child);

            (child = podcast.CreateElement("link")).InnerText = "http://www.paulharvey.com/";
            channel["image"].AppendChild(child);

            // now write out each element
            foreach (string s in files.Keys)
            {
                string url = (string)files[s];
                string date = DetermineDate(s);

                child = podcast.CreateElement("item");
                XmlNode item = channel.AppendChild(child);

                (child = podcast.CreateElement("title")).InnerText = date;
                item.AppendChild(child);

                (child = podcast.CreateElement("author")).InnerText = "Paul Harvey";
                item.AppendChild(child);

                (child = podcast.CreateElement("pubDate")).InnerText = date;
                item.AppendChild(child);

                child = podcast.CreateElement("enclosure");
                (attribute = podcast.CreateAttribute("url")).Value = url;
                child.Attributes.Append(attribute);
                (attribute = podcast.CreateAttribute("length")).Value = "0";
                child.Attributes.Append(attribute);
                (attribute = podcast.CreateAttribute("type")).Value = "application/octet-stream";
                child.Attributes.Append(attribute);
                item.AppendChild(child);
            }

            podcast.Save(filename);
        }

        static string ReadFromURL(string url)
        {
            byte[] buf = new byte[8192];
            StringBuilder sb = new StringBuilder();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream s = response.GetResponseStream();

            string tempString = null;
            int count = 0;
            do
            {
                // fill the buffer with data
                count = s.Read(buf, 0, buf.Length);

                // make sure we read some data
                if (count != 0)
                {
                    // translate from bytes to ASCII text
                    tempString = Encoding.ASCII.GetString(buf, 0, count);

                    // continue building the string
                    sb.Append(tempString);
                }
            }
            while (count > 0); // any more data to read?
            return sb.ToString();
        }

        static string DetermineDate(string file)
        {
            string day;
            bool morning = false;

            if (!file.Equals("saturday", StringComparison.OrdinalIgnoreCase))
            {
                day = file.Substring(0, file.Length - 2);
                string time = file.Substring(file.Length - 2, 2);
                morning = time == "am" ? true : false;
            }
            else
            {
                day = "saturday";
            }

            DateTime date = System.DateTime.Today;
            while (!day.Equals(date.ToString("dddd"), StringComparison.OrdinalIgnoreCase))
            {
                date = date.AddDays(-1);
            }

            string toReturn = date.ToString("ddd, dd MMM yyyy ");

            if (morning)
            {
                return toReturn + "09:15:00 EST";
            }
            else // saturday
            {
                return toReturn + "13:15:00 EST";
            }
        }
    }
}
