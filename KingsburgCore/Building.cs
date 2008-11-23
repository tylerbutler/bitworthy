using System;
using System.Collections.Generic;
using System.Linq;

namespace TylerButler.Kingsburg.Core
{
    [Serializable]
    public class Building : GameToolkit.GameComponent, ICloneable
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

        public override string ToString()
        {
            return this.Name;
        }

        #region ICloneable Members

        public object Clone()
        {
            //Building clone = new Building( this.Name, this.Description, this.Row, this.Column );
            //clone.BattleValue = this.BattleValue;
            //clone.GoldCost = this.GoldCost;
            //clone.StoneCost = this.StoneCost;
            //clone.VictoryPointValue = this.VictoryPointValue;
            //clone.WoodCost = this.WoodCost
            return this.MemberwiseClone();
        }

        #endregion
    }

    [Serializable]
    internal class BuildingCollection : List<Building>
    {
        internal BuildingCollection()
            : base()
        {
        }

        internal BuildingCollection( IEnumerable<Building> collection )
            : base( collection )
        {
        }

        internal Building GetBuilding( int row, int column )
        {
            IEnumerable<Building> r = from b in this
                                      where b.Row == row && b.Column == column
                                      select b;
            return r.ToArray<Building>()[0];
        }

        internal Building GetBuilding( string Name )
        {
            IEnumerable<Building> r = from b in this
                                      where b.Name == Name
                                      select b;
            return r.ToArray<Building>()[0];
        }

        internal Building RightmostUpperBuilding
        {
            get
            {
                int maxColumn = ( from b in this
                                  select b.Column ).Max();

                IEnumerable<Building> inColumn = from b in this
                                                 where b.Column == maxColumn
                                                 select b;

                int row = ( from b in this
                            select b.Row ).Min();

                return this.GetBuilding( row, maxColumn );
            }
        }
    }
}
