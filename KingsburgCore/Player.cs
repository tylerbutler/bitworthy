using System;
using System.Collections.Generic;
using TylerButler.GameToolkit;

namespace TylerButler.Kingsburg.Core
{
    public class Player : Character
    {
        private Inventory inventory = new Inventory();
        private BuildingCollection buildings = new BuildingCollection();
        public bool Envoy = false;
        public int VictoryPoints = 0, Soldiers = 0, PlusTwoTokens = 0;
        private DiceBag<KingsburgDie> dice = new DiceBag<KingsburgDie>( 3, 6 );
        public DiceValues MostRecentDiceRoll;//,UsedDice,RemainingDice;
        //private bool hasUsedAllDice = false;
        HashSet<Advisor> influencedAdvisors = new HashSet<Advisor>();

        public Player( string nameIn, string descriptionIn )
            : base( nameIn, descriptionIn )
        {
        }

        public bool HasUsedAllDice
        {
            get
            {
                if( RemainingDice.Count == 0 )
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public Inventory Goods
        {
            get
            {
                return this.inventory;
            }
            set
            {
                this.inventory = value;
            }
        }

        internal BuildingCollection Buildings
        {
            get
            {
                return this.buildings;
            }
            set
            {
                this.buildings = value;
            }
        }

        private DiceBag<KingsburgDie> Dice
        {
            get
            {
                return this.dice;
            }
        }

        internal int GoodsCount
        {
            get
            {
                return Goods["Gold"] + Goods["Wood"] + Goods["Stone"];
            }
        }

        public int MostRecentDiceRollTotalValue
        {
            get
            {
                int sum = 0;
                foreach( int i in this.MostRecentDiceRoll )
                {
                    sum += i;
                }
                return sum;
            }
        }

        internal void AddDie()
        {
            Dice.AddDie( new KingsburgDie( KingsburgDie.DieTypes.White ) );
        }

        internal void AddGood( GoodsChoiceOptions good )
        {
            switch( good )
            {
                case GoodsChoiceOptions.Gold:
                    this.Goods["Gold"]++;
                    break;
                case GoodsChoiceOptions.Wood:
                    this.Goods["Wood"]++;
                    break;
                case GoodsChoiceOptions.Stone:
                    this.Goods["Stone"]++;
                    break;
                case GoodsChoiceOptions.GoldAndStone:
                    this.Goods["Gold"]++;
                    this.Goods["Stone"]++;
                    break;
                case GoodsChoiceOptions.GoldAndWood:
                    this.Goods["Gold"]++;
                    this.Goods["Wood"]++;
                    break;
                case GoodsChoiceOptions.WoodAndStone:
                    this.Goods["Wood"]++;
                    this.Goods["Stone"]++;
                    break;
                case GoodsChoiceOptions.None:
                    throw new ArgumentException( "Why are you trying to add no goods to a player?" );
                default:
                    throw new ArgumentException( "Arguments passed into AddGood method were not valid." );
            }
        }

        internal void RollDice()
        {
            Dice.RollAllDice();
            MostRecentDiceRoll = Dice.Values;
        }

        internal DiceValues RemainingDice
        {
            get
            {
                DiceValues dv = new DiceValues();
                foreach( KingsburgDie die in this.Dice )
                {
                    if( !die.IsUsed )
                    {
                        dv.Add( die.Value );
                    }
                }
                return dv;
            }
        }

        internal DiceValues UsedDice
        {
            get
            {
                DiceValues dv = new DiceValues();
                foreach( KingsburgDie die in this.Dice )
                {
                    if( die.IsUsed )
                    {
                        dv.Add( die.Value );
                    }
                }
                return dv;
            }
        }

        public HashSet<Advisor> InfluencedAdvisors
        {
            get
            {
                return influencedAdvisors;
            }
        }

        //internal HashSet<int> Sums
        //{
        //    get
        //    {
        //        // returns all the sums of the players dice
        //        HashSet<int> toReturn = new HashSet<int>();
        //        for( int i = 0; i < this.MostRecentDiceRoll.Count; i++ )
        //        {
        //            toReturn.Add( this.MostRecentDiceRoll[i] );
        //            for( int j = i + 1; j < this.MostRecentDiceRoll.Count; j++ )
        //            {
        //                toReturn.Add( this.MostRecentDiceRoll[i] + this.MostRecentDiceRoll[j] );
        //            }
        //        }
        //        toReturn.Add( MostRecentDiceRollTotalValue );
        //        return toReturn;
        //    }
        //}

        public bool CanBuild( Building b )
        {
            if( this.Goods["Gold"] >= b.GoldCost &&
                this.Goods["Wood"] >= b.WoodCost &&
                this.Goods["Stone"] >= b.StoneCost &&
                this.HasPrerequisiteBuildings( b ) )
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        internal void AllocateDie( int die )
        {
            RemainingDice.Remove( die );
            UsedDice.Add( die );
        }

        public override string ToString()
        {
            return this.Name;
        }

        internal void AllocateAllDice()
        {
            foreach( int i in RemainingDice )
            {
                AllocateDie( i );
            }
        }

        internal List<Building> BuildableBuildings
        {
            get
            {
                List<Building> toReturn = new List<Building>();
                foreach( Building b in GameManager.Instance.Buildings )
                {
                    if( this.CanBuild( b ) )
                    {
                        toReturn.Add( b );
                    }
                }
                return toReturn;
            }
        }

        internal bool HasPrerequisiteBuildings( Building b )
        {
            for( int c = b.Column - 1; c >= 0; c-- )
            {
                if( !this.Buildings.Contains( GameManager.Instance.Buildings.GetBuilding( b.Row, c ) ) )
                {
                    return false;
                }
            }
            return true;
        }

        internal int NumBuildings
        {
            get
            {
                int toReturn = 0;
                foreach( Building b in this.Buildings )
                {
                    if( b != null )
                    {
                        toReturn++;
                    }
                }
                return toReturn;
            }
        }

        internal bool HasBuilding( Building b )
        {
            if( this.Buildings.Contains( b ) )
                return true;
            else
                return false;
        }
    }

    public class PlayerDiceRollComparer : IComparer<Player>
    {

        #region IComparer<Player> Members

        public int Compare( Player x, Player y )
        {
            return x.MostRecentDiceRollTotalValue.CompareTo( y.MostRecentDiceRollTotalValue );
        }

        #endregion
    }

    internal enum GoodsChoiceOptions
    {
        Gold = 1,
        Wood,
        Stone,
        GoldAndWood,
        GoldAndStone,
        WoodAndStone,
        None,
    }

    internal class PlayerCollection : List<Player>
    {

        internal PlayerCollection()
            : base()
        {
        }

        internal PlayerCollection( IEnumerable<Player> collection )
            : base( collection )
        {
        }
    }
}
