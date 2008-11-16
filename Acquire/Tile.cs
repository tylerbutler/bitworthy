using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Bitworthy.Games.Acquire.Components
{
    public class Tile : IComparable
    {
        #region Fields
        private Player owner;
        private bool placed;
        private String value;
        private Button button = new Button();
        private StockCard.Type hotel;

        #endregion

        public Tile(Player owner, String value)
        {
            Owner = owner;
            Name = value;
            Button.Text = Name;
            Button.Size = new Size(75, 60);
            Button.Enabled = false;
            hotel = StockCard.Type.None;
        }

        #region Properties
        public Button Button
        {
            get { return button; }
            set { button = value; }
        }

        public StockCard.Type Hotel
        {
            get { return hotel; }
            set
            {
                hotel = value;

                if (value == StockCard.Type.None)
                {
                    if (Placed == true)
                    {
                        Button.BackColor = Color.BurlyWood;
                    }
                    else
                    { Button.BackColor = System.Drawing.SystemColors.Control; }
                }
                else
                {
                    Button.BackColor = StockCard.getStockColor(Hotel);
                }
            }
        }


        public String Name
        {
            get { return this.value; }
            set
            {
                // TODO: Add better range checking here
                if (value.Length > 3)
                {
                    throw new Exception("Tile value was not two characters.");
                }
                else
                {
                    this.value = value;
                }
            }
        }

        public Player Owner
        {
            get { return owner; }
            set { owner = value; }
        }

        public bool Placed
        {
            get { return placed; }
            set
            {
                placed = value;

                if (Hotel == StockCard.Type.None)
                {
                    if (value == true)
                    {
                        Button.BackColor = Color.BurlyWood;
                    }
                    else
                    { Button.BackColor = System.Drawing.SystemColors.Control; }
                }
                else
                {
                    Button.BackColor = StockCard.getStockColor(Hotel);
                }
            }
        }

        #endregion

        public override string ToString()
        {
            return Name;
        }

        public int CompareTo(object o)
        {
            if( !(o is Tile))
                throw new ArgumentException("Must be a Tile!");

            Tile inTile = (Tile)o;
            int number, inNumber;
            char letter, inLetter;

            if (Name.Length == 3)
            {
                number = Int32.Parse(Name.Substring(0, 2));
                letter = Name[2];
            }
            else //(Name.Length == 2)
            {
                number = Int32.Parse(Name[0].ToString());
                letter = Name[1];
            }

            if (inTile.Name.Length == 3)
            {
                inNumber = Int32.Parse(inTile.Name.Substring(0, 2));
                inLetter = inTile.Name[2];
            }
            else //(inTile.Name.Length == 2)
            {
                inNumber = Int32.Parse(inTile.Name[0].ToString());
                inLetter = inTile.Name[1];
            }

            // compare letters first
            if (letter < inLetter)
                return -1;
            else if (letter > inLetter)
                return 1;
            else
            {
                if (number < inNumber)
                    return -1;
                else if (number > inNumber)
                    return 1;
                else
                    return 0;
            }
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Tile))
                return false;
            
            return (this.CompareTo(obj) == 0);
        }

        public override int GetHashCode()
        {
            char[] c = this.Name.ToCharArray();
            return (int)c[0];
        }
    }
}
