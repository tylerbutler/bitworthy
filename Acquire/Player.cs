using System;
using System.Text;
using System.Collections.Generic;
using System.Collections;
using System.Windows.Forms;

namespace Bitworthy.Games.Acquire.Components
{
    public class Player
    {
        private String name;
        private Dictionary<StockCard.Type, int> stock = new Dictionary<StockCard.Type, int>();
        private ArrayList tiles = new ArrayList();
        private ArrayList special = new ArrayList();
        private int money = 0;
        private Label label = new Label();
        private int tilesLeftToPlay = 1;
        private int stockLeftToBuy = 3;
        private int maxTiles = 6;
        private int numTurns = 0; // the number of tunrs that the player has had
        private bool hasPlayedSpecial = false;
        private bool played3Free = false;

        private Random rand = new Random(DateTime.Now.Millisecond);

        public Player(String name)
        {
            // Initialize name and label
            Name = name;

            Label.AutoSize = true;
            Label.Name = Name + "Label";
            Label.Size = new System.Drawing.Size(35, 13);
            Label.Text = Name;

            // Give players starting money, tiles, and cards
            giveMoney(6000);
            foreach (SpecialCard.Type t in Enum.GetValues(typeof(SpecialCard.Type)))
            {
                giveSpecial(t);
            }

            // initialize stock to 0
            foreach (StockCard.Type t in Enum.GetValues(typeof(StockCard.Type)))
            {
                stock.Add(t, 0);
            }
            
            // draw tiles
            while (Tiles.Count < maxTiles)
            {
                drawRandomTile();
            }
        }

        public ArrayList Special
        {
            get { return special; }
            set { special = value; }
        }

        public bool Played3Free
        {
            get { return played3Free; }
            set { played3Free = value; }
        }

        public String Name
        {
            get { return name; }
            set { name = value; }
        }

        public int NumTurns
        {
            get { return numTurns; }
            set { numTurns = value; }
        }

        public Label Label
        {
            get { return label; }
            set { label = value; }
        }

        public Dictionary<StockCard.Type, int> Stock
        {
            get { return stock; }
            set { stock = value; }
        }

        public ArrayList Tiles
        {
            get { return tiles; }
            set { tiles = value; }
        }

        public int Money
        {
            get { return money; }
            set { money = value; }
        }

        public int StockLeftToBuy
        {
            get { return stockLeftToBuy; }
            set { stockLeftToBuy = value; }
        }

        public void giveMoney(int m)
        {
            Money += m;
        }

        public void takeMoney(int m)
        {
            Money -= m;
        }

        public int TilesLeftToPlay
        {
            get { return tilesLeftToPlay; }
            set { tilesLeftToPlay = value; }
        }

        public bool HasPlayedSpecial
        {
            get { return hasPlayedSpecial; }
            set { hasPlayedSpecial = value; }
        }

        public void giveTile(Tile t)
        {
            if (Tiles.Count == 6 && hasSpecial(SpecialCard.Type.Pick5))
            {
                throw new Exception("Tried to add too many tiles.");
            }
            else
            {
                Tiles.Add(t);
                t.Owner = this;
            }
        }

        public Tile takeTile(Tile t)
        {
            if (!Tiles.Contains(t))
            {
                throw new Exception("Couldn't find tile: " + t + " in player's tiles.");
            }
            else
            {
                Tiles.Remove(t);
                return t;
            }
        }

        public bool hasSpecial(SpecialCard.Type special)
        {
            if (Special.Contains(special))
            { return true; }
            else { return false; }
        }

        public void giveSpecial(SpecialCard.Type special)
        {
            Special.Add(special);
        }

        public Tile drawRandomTile()
        {
            ArrayList availableTiles = GameManager.Instance.Board.AvailableTiles;

            int i = rand.Next(0, availableTiles.Count - 1);

            Tile toReturn = (Tile)availableTiles[i];
            availableTiles.Remove(toReturn);
            giveTile(toReturn);
            return toReturn;
        }

        public int numStock(StockCard.Type type)
        {
            return Stock[type];
        }

        public void giveStock(int num, StockCard.Type type)
        {
            if (Stock.ContainsKey(type))
            {
                Stock[type] += num;
            }
            else
            {
                Stock.Add(type, num);
            }
        }

        public override string ToString()
        {
            return Name;
        }

        internal void takeStock(int num, StockCard.Type type)
        {
            Stock[type] -= num;

            if (Stock[type] < 0)
            {
                throw new Exception("Removed more stock than the player has.");
            }
        }
    }
}