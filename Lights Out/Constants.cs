using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using System.Xml;

namespace Lights_Out
{
    public static class Constants
    {
        #region Texture Names
        public const string litBlockName = "Dirt Block";
        public const string unlitBlockName = "Grass Block";
        public const string selectorName = "Selector";
        public const string backgroundName = "background";
        #endregion

        #region Game Constants
        public const int boardWidth = 5;
        public const int boardHeight = 5;
        public const float scaleFactor = .85f;
        public const int tileWidth = 101;
        public const int tileHeight = 83;//122;
        public const int selectorOffsetTop = -35;
        public const int tileOffsetTop = -35;
        public static Vector2 boardStartPosition = new Vector2(120, 120);
        #endregion
    }

    public static class Utilities
    {
        public static int[,] LoadPuzzle(string filename, int puzzlenumber)
        {
            //XMLTextReader reader = new XMLTextReader();
            return null;

            XmlReader rdr;
            XmlReaderSettings settings = new XmlReaderSettings();

            settings.ConformanceLevel = ConformanceLevel.Fragment;
            settings.IgnoreWhitespace = true;
            settings.IgnoreComments = true;

            rdr = XmlReader.Create("test.xml", settings);

            rdr.Read();
            rdr.ReadStartElement();
            rdr.MoveToAttribute(0); 
        }
    }

    public struct Dimensions
    {
        public int height;
        public int width;
    }
}