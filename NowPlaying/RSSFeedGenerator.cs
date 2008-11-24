//  This file was found at http://www.developerfusion.com/show/4597/
// No changes have been made.

using System;
using System.Xml;
/// <summary>
/// Enables the generation of an RSS feed
/// </summary>
public class RSSFeedGenerator
{
	XmlTextWriter writer;
	public RSSFeedGenerator( System.IO.Stream stream, System.Text.Encoding encoding )
	{
		writer = new XmlTextWriter(stream, encoding);
		writer.Formatting = Formatting.Indented;
	}
	public RSSFeedGenerator( System.IO.TextWriter w )
	{
		writer = new XmlTextWriter(w);
		writer.Formatting = Formatting.Indented;
	}
	/// <summary>
	/// Writes the beginning of the RSS document
	/// </summary>
	public void WriteStartDocument()
	{
		writer.WriteStartDocument();
		writer.WriteStartElement("rss");
		writer.WriteAttributeString("version","2.0");
	}
	/// <summary>
	/// Writes the end of the RSS document
	/// </summary>
	public void WriteEndDocument()
	{
		writer.WriteEndElement(); //rss
		writer.WriteEndDocument();
	}
	/// <summary>
	/// Closes this stream and the underlying stream
	/// </summary>
	public void Close()
	{
		writer.Flush();
		writer.Close();
	}
	/// <summary>
	/// Begins a new channel in the RSS document
	/// </summary>
	/// <param name="title"></param>
	/// <param name="link"></param>
	/// <param name="description"></param>
	public void WriteStartChannel(string title, string link, string description, string copyright, string webMaster)
	{
		writer.WriteStartElement("channel");
		writer.WriteElementString("title",title);
		writer.WriteElementString("link",link);
		writer.WriteElementString("description",description);
		writer.WriteElementString("language","en-gb");
		writer.WriteElementString("copyright",copyright);
		writer.WriteElementString("generator","Developer Fusion RSS Feed Generator v1.0");
		writer.WriteElementString("webMaster",webMaster);
		writer.WriteElementString("lastBuildDate",DateTime.Now.ToString("r"));
		writer.WriteElementString("ttl","20");
       
	}
	/// <summary>
	/// Writes the end of a channel in the RSS document
	/// </summary>
	public void WriteEndChannel()
	{
		writer.WriteEndElement(); //channel
	}
	/// <summary>
	/// Writes an item to a channel in the RSS document
	/// </summary>
	/// <param name="title"></param>
	/// <param name="link"></param>
	/// <param name="description"></param>
	/// <param name="author"></param>
	/// <param name="publishedDate"></param>
	/// <param name="category"></param>
	public void WriteItem(string title, string link, string description, string author, DateTime publishedDate, string subject)
	{
		writer.WriteStartElement("item");
		writer.WriteElementString("title",title);
		writer.WriteElementString("link",link);
		writer.WriteElementString("description",description);
		writer.WriteElementString("author",author);
		writer.WriteElementString("pubDate",publishedDate.ToString("r"));
		//writer.WriteElementString("category",category);
		writer.WriteElementString("subject",subject);
		writer.WriteEndElement();
	}
}