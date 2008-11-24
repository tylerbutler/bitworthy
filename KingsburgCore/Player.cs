using System;
using System.Collections.Generic;
using System.Linq;
using TylerButler.GameToolkit;

namespace TylerButler.Kingsburg.Core
{
    public class Player : Character
    {
        //private Inventory inventory = new Inventory();
        private BuildingCollection buildings = new BuildingCollection();
        private int gold=0,wood=0,stone=0;
        public bool Envoy = false, WasVictorious = false;
        public int VictoryPoints = 0, Soldiers = 0, PlusTwoTokens = 0;
        private DiceCollection dice = new DiceCollection( 3, 6 );
        internal DiceCollection MostRecentDiceRoll;
        HashSet<Advisor> influencedAdvisors = new HashSet<Advisor>();
        private bool hasUsedMarket = false;

        public Player( string nameIn, string descriptionIn )
            : base( nameIn, descriptionIn )
        {
        }

        // DEBUG: take this out after debugging is over
        //Bug:22
        public new string Name
        {
            get
            {
                string toReturn = String.Format( "{0} ({1}G, {2}W, {3}S, {4}T, {7}VP - Soldiers: {5} - Buildings: {6}, {10} - Dice Roll: {8} {9})",
                    /*0*/ base.Name,
                    /*1*/ this.Gold,
                    /*2*/ this.Wood,
                    /*3*/ this.Stone,
                    /*4*/ this.PlusTwoTokens,
                    /*5*/ this.Soldiers,
                    /*6*/ this.NumBuildings,
                    /*7*/ this.VictoryPoints,
                    /*8*/ this.MostRecentDiceRoll == null ? "NONE" : this.MostRecentDiceRoll.ToString(),
                    /*9*/ this.Envoy == true ? "*E*" : "",
                    /*10*/ this.CurrentBuildingStrength );
                return toReturn;
            }
            set
            {
                base.Name = value;
            }
        }

        public int Stone
        {
            get
            {
                return stone;
            }
            set
            {
                stone = value;
                if( stone < 0 )
                {
                    stone = 0;
                }
            }
        }

        public int Wood
        {
            get
            {
                return wood;
            }
            set
            {
                wood = value;
                if( wood < 0 )
                {
                    wood = 0;
                }
            }
        }

        public int Gold
        {
            get
            {
                return gold;
            }
            set
            {
                gold = value;
                if( gold < 0 )
                {
                    gold = 0;
                }
            }
        }

        public bool HasUsedMarket
        {
            get
            {
                return hasUsedMarket;
            }
            set
            {
                hasUsedMarket = value;
            }
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

        //public Inventory Goods
        //{
        //    get
        //    {
        //        return this.inventory;
        //    }
        //    set
        //    {
        //        this.inventory = value;
        //    }
        //}

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
                return this.Gold + this.Wood + this.Stone;
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

        internal KingsburgDie AddDie()
        {
            KingsburgDie die = new KingsburgDie( KingsburgDie.DieTypes.White );
            this.AddDie( die );
            return die;
        }

        internal void AddDie( KingsburgDie die )
        {
            Dice.Add( die );
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
                    this.Gold++;
                    break;
                case GoodsChoiceOptions.Wood:
                    this.Wood++;
                    break;
                case GoodsChoiceOptions.Stone:
                    this.Stone++;
                    break;
                case GoodsChoiceOptions.GoldAndStone:
                    this.Gold++;
                    this.Stone++;
                    break;
                case GoodsChoiceOptions.GoldAndWood:
                    this.Gold++;
                    this.Wood++;
                    break;
                case GoodsChoiceOptions.WoodAndStone:
                    this.Wood++;
                    this.Stone++;
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
                    this.Gold--;
                    break;
                case GoodsChoiceOptions.Wood:
                    this.Wood--;
                    break;
                case GoodsChoiceOptions.Stone:
                    this.Stone--;
                    break;
                case GoodsChoiceOptions.GoldAndStone:
                    this.Gold--;
                    this.Stone--;
                    break;
                case GoodsChoiceOptions.GoldAndWood:
                    this.Gold--;
                    this.Wood--;
                    break;
                case GoodsChoiceOptions.WoodAndStone:
                    this.Wood--;
                    this.Stone--;
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
            Dice.ResetDiceUsage();
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

        public bool CanBuild( Building b )
        {
            int goldCost = b.GoldCost;

            // Adjust gold cost if player has the crane
            if( this.HasBuilding( GameManager.Instance.Buildings.GetBuilding( "Crane" ) ) )
            {
                goldCost--;
            }

            if( this.Gold >= goldCost &&
                this.Wood >= b.WoodCost &&
                this.Stone >= b.StoneCost &&
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
            if( die.IsUsed == true )
            {
                throw new Exception( "A used die was tried to be used. Something went wrong." );
            }
            else
            {
                die.IsUsed = true;
            }

            if( die.Type == KingsburgDie.DieTypes.MarketPositive || die.Type == KingsburgDie.DieTypes.MarketNegative )
            {
                IEnumerable<KingsburgDie> dc = from r in this.Dice
                                               where r.Type == KingsburgDie.DieTypes.MarketPositive || r.Type == KingsburgDie.DieTypes.MarketNegative
                                               select r;
                foreach( KingsburgDie d in dc )
                {
                    d.IsUsed = true;
                }

                this.HasUsedMarket = true;
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
                        if( !this.HasBuilding( b ) )
                            toReturn.Add( b );
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
                return this.GoodsCount / this.SoldierCost;
            }
        }

        /// <summary>
        /// The cost of a soldier for the player, taking into account whether or not he has a Barracks.
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
            this.Gold = 0;
            this.Wood = 0;
            this.Stone = 0;
        }

        /// <summary>
        /// Returns a list of the TYPES of goods (gold, wood, stone) a player has.
        /// </summary>
        internal List<GoodsChoiceOptions> GoodTypesPlayerHas
        {
            get
            {
                List<GoodsChoiceOptions> toReturn = new List<GoodsChoiceOptions>();
                if( this.Gold > 0 )
                {
                    toReturn.Add( GoodsChoiceOptions.Gold );
                }

                if( this.Wood > 0 )
                {
                    toReturn.Add( GoodsChoiceOptions.Wood );
                }

                if( this.Stone > 0 )
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
                Building toDestroy = this.Buildings.RightmostUpperBuilding;
                this.Buildings.Remove( toDestroy );
                this.VictoryPoints -= toDestroy.VictoryPointValue;
            }
        }

        internal void RemoveDie( KingsburgDie die )
        {
            this.Dice.Remove( die );
        }

        internal void RemoveNonRegularDice()
        {
            this.Dice.RemoveNonRegularDice();
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
        PlusTwoToken,
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
