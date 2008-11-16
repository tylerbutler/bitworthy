using System;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using System.IO;

namespace Lights_Out
{
    [Serializable]
    class Gameboard : GameObject
    {
        public LightButton[,] tiles;
        public int sizeAcross, sizeDown;

        public Gameboard(int numTilesAcross, int numTilesDown,
            Texture2D unlitTexture, Texture2D litTexture, ContentManager Content)
            : base(unlitTexture)
        {
            sizeAcross = numTilesAcross;
            sizeDown = numTilesDown;
            tiles = new LightButton[sizeAcross, sizeDown];
            for (int i = 0; i < sizeAcross; i++)
            {
                for (int j = 0; j < sizeDown; j++)
                {
                    tiles[i, j] = new LightButton(Content.Load<Texture2D>(Constants.unlitBlockName),
                        Content.Load<Texture2D>(Constants.litBlockName));
                }
            }
        }

        public void Update(int i, int j)
        {
            tiles[i, j].toggleLight();
            try
            {
                tiles[i + 1, j].toggleLight();
            }
            catch
            {
                // do nothing
            }
            try
            {
                tiles[i - 1, j].toggleLight();
            }
            catch
            {
                // do nothing
            }
            try
            {
                tiles[i, j + 1].toggleLight();
            }
            catch
            {
                // do nothing
            }
            try
            {
                tiles[i, j - 1].toggleLight();
            }
            catch
            {
                // do nothing
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < sizeAcross; i++)
            {
                for (int j = 0; j < sizeDown; j++)
                {
                    LightButton tile = tiles[i, j];
                    spriteBatch.Draw(tile.Sprite, tile.Position, null, Color.White,
                        tile.Rotation, tile.Center, Constants.scaleFactor, SpriteEffects.None, 0);
                }
            }
        }

        private Dimensions getBoardDimensionsInPixels()
        {
            Dimensions toReturn = new Dimensions();
            toReturn.width = tiles[0, 0].Sprite.Width * sizeAcross;
            toReturn.height = tiles[0, 0].Sprite.Height * sizeDown;
            return toReturn;
        }

        public void LoadBoard(string FilePath, int desiredPuzzle)
        {
            if (desiredPuzzle > 0)
            {
                int puzzleNum = desiredPuzzle;
                XmlReader reader = new XmlTextReader(FilePath);
                while (reader.Read())
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element:
                            switch (reader.Name)
                            {
                                case "Puzzle":
                                    puzzleNum = int.Parse(reader.GetAttribute("Number"));
                                    break;
                                case "Size":
                                    int across = int.Parse(reader.GetAttribute("Across"));
                                    int down = int.Parse(reader.GetAttribute("Down"));
                                    //TODO: Make the loading take arbitrary sizes
                                    //this.tiles = new LightButton[across, down];
                                    break;
                                case "Light":
                                    if (puzzleNum == desiredPuzzle)
                                    {
                                        int x = int.Parse(reader.GetAttribute("X"));
                                        int y = int.Parse(reader.GetAttribute("Y"));
                                        this.tiles[x, y].IsLit = true;
                                    }
                                    break;
                            } break;
                    }
                }
            }
            else // XML is a deserialized gameboard
            {
                XmlSerializer s = new XmlSerializer(typeof(Gameboard));
                TextReader reader = new StreamReader(FilePath);
                Gameboard g = (Gameboard)s.Deserialize(reader);
                this.tiles = g.tiles;
                this.sizeAcross = g.sizeAcross;
                this.sizeDown = g.sizeDown;
            }
        }

        public void SaveBoard(string filename)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Gameboard));
            TextWriter writer = new StreamWriter(filename);
            serializer.Serialize(writer, this);
        }
    }

    //[Serializable]
    //class SimpleBoard
    //{
    //    LightButton[,] board;
    //}

    //public class BoardContentReader : ContentTypeReader<Gameboard>
    //{
    //    protected override Gameboard Read(ContentReader input, Gameboard existingInstance)
    //    {
    //        Gameboard board = new Gameboard();
    //        board.sizeAcross = input.
    //    }
    //}
}