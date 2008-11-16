using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using gmit;
using System.Windows.Forms;
using System.Drawing;
using System.Runtime.Serialization;

namespace Bitworthy.Games.Acquire.Components
{
    public class Board
    {
        private Tile[,] tiles = new Tile[12, 9];
        private ArrayList placedTiles = new ArrayList();
        private ArrayList availableTiles = new ArrayList();
        private enum ColumnLetters { A = 0, B = 1, C = 2, D = 3, E = 4, F = 5, G = 6, H = 7, I = 8 };
        //internal enum TileBorders { Top, Bottom, Right, Left };

        public Board()
        {
            foreach (int j in new Range(0, 8))
            {
                foreach (int i in new Range(0, 11))
                {
                    Tile t = new Tile(null, (i + 1).ToString() + Enum.GetName(typeof(ColumnLetters), j));
                    tiles[i, j] = t;
                    AvailableTiles.Add(tiles[i, j]);
                }
            }
        }

        public Tile[,] Tiles
        {
            get { return tiles; }
            set { tiles = value; }
        }

        public int getHotelChainLength(StockCard.Type type)
        {
            int toReturn = 0;

            foreach (Tile t in Tiles)
            {
                if (t.Hotel == type)
                {
                    toReturn++;
                }
            }
            return toReturn;
        }

        public ArrayList PlacedTiles
        {
            get { return placedTiles; }
            set { placedTiles = value; }
        }
        public ArrayList AvailableTiles
        {
            get { return availableTiles; }
            set { availableTiles = value; }
        }

        public Tile getTileByName(String name)
        {
            int i = Int32.Parse(name.Substring(0, name.Length - 1).ToString()) - 1;
            int j = (int)Enum.Parse(typeof(ColumnLetters), name.Substring(name.Length - 1, 1).ToString(), false);
            return Tiles[i, j];
        }

        public void disableAllTiles()
        {
            //Disable all tiles
            foreach (Tile t in GameManager.Instance.Board.Tiles)
            {
                t.Button.Enabled = false;
            }
        }

        internal ArrayList GetNeighborsforTile(String name)
        {
            ArrayList toReturn = new ArrayList();

            int i = Int32.Parse(name.Substring(0, name.Length - 1).ToString()) - 1;
            int j = (int)Enum.Parse(typeof(ColumnLetters), name.Substring(name.Length - 1, 1).ToString(), false);

            try
            {
                Tile left = Tiles[i - 1, j];
                if (PlacedTiles.Contains(left))
                {
                    toReturn.Add(left);
                }
            }
            catch (IndexOutOfRangeException e)
            {
                //catch border condition exceptions and ignore
                e.ToString();
            }

            try
            {
                Tile right = Tiles[i + 1, j];
                if (PlacedTiles.Contains(right))
                {
                    toReturn.Add(right);
                }
            }
            catch (IndexOutOfRangeException e)
            {
                //catch border condition exceptions and ignore
                e.ToString();
            }

            try
            {
                Tile top = Tiles[i, j - 1];
                if (PlacedTiles.Contains(top))
                {
                    toReturn.Add(top);
                }
            }
            catch (IndexOutOfRangeException e)
            {
                //catch border condition exceptions and ignore
                e.ToString();
            }

            try
            {
                Tile bottom = Tiles[i, j + 1];
                if (PlacedTiles.Contains(bottom))
                {
                    toReturn.Add(bottom);
                }
            }
            catch (IndexOutOfRangeException e)
            {
                //catch border condition exceptions and ignore
                e.ToString();
            }

            return toReturn;
        }

        internal ArrayList GetNeighborsforTile(Tile t)
        {
            return GetNeighborsforTile(t.Name);
        }

        internal ArrayList GetHotelsOnBoard()
        {
            ArrayList toReturn = new ArrayList();

            foreach (Tile t in PlacedTiles)
            {
                if (t.Hotel != StockCard.Type.None && !toReturn.Contains(t.Hotel))
                {
                    toReturn.Add(t.Hotel);
                }
            }
            return toReturn;
        }

        internal bool isHotelSafe(StockCard.Type hotel)
        {
            if (getHotelChainLength(hotel) >= 11)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}