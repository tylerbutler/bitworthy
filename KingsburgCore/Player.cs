using System;
using System.Collections.Generic;
using TylerButler.GameToolkit;

namespace TylerButler.Kingsburg.Core
{
    public class Player : Character
    {
        private Inventory inventory = new Inventory();
        private BuildingCollection buildings = new BuildingCollection();
        public bool Envoy = false, WasVictorious = false;
        public int VictoryPoints = 0, Soldiers = 0, PlusTwoTokens = 0;
        private DiceCollection dice = new DiceCollection( 3, 6 );
        internal DiceCollection MostRecentDiceRoll;//,UsedDice,RemainingDice;
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

        private DiceCollection Dice
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
                foreach( KingsburgDie d in this.MostRecentDiceRoll )
                {
                    sum += d.Value;
                }
                return sum;
            }
        }

        internal void AddDie()
        {
            Dice.Add( new KingsburgDie( KingsburgDie.DieTypes.White ) );
        }

        /// <summary>
        /// Adds a good to the player.
        /// </summary>
        /// <param name="good">The type of good to add to the player.</param>
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

        /// <summary>
        /// Removes a good from the player.
        /// </summary>
        /// <param name="good">The type of good to remove from the player.</param>
        internal void RemoveGood( GoodsChoiceOptions good )
        {
            switch( good )
            {
                case GoodsChoiceOptions.Gold:
                    this.Goods["Gold"]--;
                    break;
                case GoodsChoiceOptions.Wood:
                    this.Goods["Wood"]--;
                    break;
                case GoodsChoiceOptions.Stone:
                    this.Goods["Stone"]--;
                    break;
                case GoodsChoiceOptions.GoldAndStone:
                    this.Goods["Gold"]--;
                    this.Goods["Stone"]--;
                    break;
                case GoodsChoiceOptions.GoldAndWood:
                    this.Goods["Gold"]--;
                    this.Goods["Wood"]--;
                    break;
                case GoodsChoiceOptions.WoodAndStone:
                    this.Goods["Wood"]--;
                    this.Goods["Stone"]--;
                    break;
                case GoodsChoiceOptions.None:
                    throw new ArgumentException( "Why are you trying to remove no goods from a player?" );
                default:
                    throw new ArgumentException( "Arguments passed into RemoveGood method were not valid." );
            }
        }

        internal void RollDice()
        {
            Dice.RollAllDice();
            MostRecentDiceRoll = (DiceCollection)Dice.Clone();
        }

        internal DiceCollection RemainingDice
        {
            get
            {
                DiceCollection dv = new DiceCollection();
                foreach( KingsburgDie die in this.Dice )
                {
                    if( !die.IsUsed )
                    {
                        dv.Add( (KingsburgDie)die );
                    }
                }
                return dv;
            }
        }

        internal DiceCollection UsedDice
        {
            get
            {
                DiceCollection dv = new DiceCollection();
                foreach( KingsburgDie die in this.Dice )
                {
                    if( die.IsUsed )
                    {
                        dv.Add( (KingsburgDie)die );
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

        internal void AllocateDie( KingsburgDie die )
        {
            //RemainingDice.Remove( die );
            //UsedDice.Add( die );
            if( die.IsUsed == true )
            {
                throw new Exception( "A used die was tried to be used. Something went wrong." );
            }
            else
            {
                die.IsUsed = true;
            }
        }

        internal void AllocateDice( DiceCollection db )
        {
            foreach( KingsburgDie d in db )
            {
                this.AllocateDie( d );
            }
        }

        public override string ToString()
        {
            return this.Name;
        }

        internal void AllocateAllDice()
        {
            foreach( KingsburgDie d in RemainingDice )
            {
                AllocateDie( d );
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
            for( int c = b.Column - 1; c >= 1; c-- )
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

        /// <summary>
        /// The number of soldiers a player can recruit in phase 8, based on the number of goods he has and his barracks, if applicable.
        /// </summary>
        internal int RecruitableSoldiers
        {
            get
            {
                // TODO: make sure this does the right thing and rounds down
                return this.GoodsCount / this.SoldierCost;
            }
        }

        /// <summary>
        /// The cost of a soldier for the player, tyaking into account whether or not he has a Barracks.
        /// </summary>
        internal int SoldierCost
        {
            get
            {
                int cost = this.HasBuilding( GameManager.Instance.GetBuilding( "Barracks" ) ) ? 1 : 2;
                return cost;
            }
        }

        /// <summary>
        /// The current military strength the player has due to buildings.
        /// </summary>
        /// <remarks>Does not take bonuses that only apply to certain enemy types into account.</remarks>
        /// <example>For example, if the player owns a Guard Tower and a Barricade, this method will return 1, not 2.</example>
        internal int CurrentBuildingStrength
        {
            get
            {
                int toReturn = 0;
                foreach( Building b in this.Buildings )
                {
                    toReturn += b.BattleValue;
                }
                return toReturn;
            }
        }

        /// <summary>
        /// The total military strength of the player, including recruited soldiers and Buildings.
        /// </summary>
        internal int TotalStrength
        {
            get
            {
                return Soldiers + CurrentBuildingStrength;
            }
        }

        /// <summary>
        /// The bonus strength a player receives against an enemy due to buildings he has.
        /// </summary>
        /// <param name="enemy">The enemy the player is battling.</param>
        /// <returns>The bonus strength the player receives.</returns>
        internal int BonusStrengthAgainstEnemy( Enemy enemy )
        {
            int bonus = 0;
            GameManager gm = GameManager.Instance;
            switch( enemy.Type )
            {
                case EnemyType.Zombies:
                    if( this.HasBuilding( gm.GetBuilding( "Palisade" ) ) )
                    {
                        bonus += 2;
                    }
                    break;
                case EnemyType.Goblins:
                    if( this.HasBuilding( gm.GetBuilding( "Barricade" ) ) )
                    {
                        bonus += 1;
                    }
                    break;
                case EnemyType.Demons:
                    if( this.HasBuilding( gm.GetBuilding( "Church" ) ) )
                    {
                        bonus += 1;
                    }
                    break;
            }
            return bonus;
        }

        /// <summary>
        /// Removes all goods (gold, stone, and wood) from the player.
        /// </summary>
        internal void RemoveAllGoods()
        {
            this.Goods["Gold"] = 0;
            this.Goods["Wood"] = 0;
            this.Goods["Stone"] = 0;
        }

        /// <summary>
        /// Returns a list of the TYPES of goods (gold, wood, stone) a player has.
        /// </summary>
        internal List<GoodsChoiceOptions> GoodTypesPlayerHas
        {
            get
            {
                List<GoodsChoiceOptions> toReturn = new List<GoodsChoiceOptions>();
                if( this.Goods["Gold"] > 0 )
                {
                    toReturn.Add( GoodsChoiceOptions.Gold );
                }

                if( this.Goods["Wood"] > 0 )
                {
                    toReturn.Add( GoodsChoiceOptions.Wood );
                }

                if( this.Goods["Stone"] > 0 )
                {
                    toReturn.Add( GoodsChoiceOptions.Stone );
                }

                return toReturn;
            }

        }

        /// <summary>
        /// Removes a number of buildings from the player, rightmost uppermost buildings first
        /// </summary>
        /// <param name="num">The number of buildings to destroy.</param>
        internal void DestroyBuildings( int num )
        {
            foreach( int i in new Range( 1, num ) )
            {
                this.Buildings.Remove( this.Buildings.RightmostUpperBuilding );
            }
        }
    }

    internal class PlayerDiceRollComparer : IComparer<Player>
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
