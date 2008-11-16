using System;
using System.Collections.Generic;
using System.Text;

namespace TylerButler.Kingsburg.Core
{
    [Serializable]
    public class Building : GameToolkit.GameComponent
    {
        private int goldCost=0, woodCost=0, stoneCost=0, battleValue=0, vpValue=0;
        private int row=1, column=1;

        public Building()
            : this( "Name of Building", "Description of building", 1, 1 )
        {
        }

        public Building( string name, string description, int rowIn, int columnIn )
        {
            this.Name = name;
            this.Description = description;
            this.Row = rowIn;
            this.Column = columnIn;
            this.GoldCost = this.WoodCost = this.StoneCost = 0;
        }

        public int GoldCost
        {
            get
            {
                return this.goldCost;
            }
            set
            {
                if( value >= 0 )
                {
                    this.goldCost = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
        }

        public int WoodCost
        {
            get
            {
                return this.woodCost;
            }
            set
            {
                if( value >= 0 )
                {
                    this.woodCost = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
        }

        public int StoneCost
        {
            get
            {
                return this.stoneCost;
            }
            set
            {
                if( value >= 0 )
                {
                    this.stoneCost = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
        }

        public int BattleValue
        {
            get
            {
                return this.battleValue;
            }
            set
            {
                this.battleValue = value;
            }
        }

        public int VictoryPointValue
        {
            get
            {
                return vpValue;
            }
            set
            {
                vpValue = value;
            }
        }

        public int Row
        {
            get
            {
                return this.row;
            }
            set
            {
                if( value >= 0 && value <= 5 )
                {
                    this.row = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
        }

        public int Column
        {
            get
            {
                return this.column;
            }
            set
            {
                if( value >= 0 && value <= 4 )
                {
                    this.column = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}
