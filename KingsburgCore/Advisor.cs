using System;
using System.Collections.Generic;
using TylerButler.GameToolkit;
using TylerButler.Kingsburg.Utilities;

namespace TylerButler.Kingsburg.Core
{
    [Serializable]
    public class Advisor : Character
    {
        private Advisors advisorNameEnum;
        //private bool isInfluenced = false;
        private PlayerCollection influencingPlayers = new PlayerCollection();

        public Advisor( Advisors adv, string descriptionIn )
            : base( "" /*will get replaced when advisor enum is set*/, descriptionIn )
        {
            this.AdvisorNameEnum = adv;
        }

        public Advisor()
            : base( "Name", "Description" )
        {
        }

        public Advisors AdvisorNameEnum
        {
            get
            {
                return this.advisorNameEnum;
            }
            set
            {
                this.advisorNameEnum = value;
                base.Name = Helpers.ProperSpace( value.ToString() );
            }
        }

        public int Order
        {
            get
            {
                return (int)advisorNameEnum;
            }
        }

        public new string Name
        {
            get
            {
                return base.Name;
            }
            set
            {
                /* no op */
            }
        }

        //public bool isInfluenced( List<Player> players, out Player player )
        //{
        //    foreach( Player p in players )
        //    {
        //        if( p.InfluencedAdvisors.Contains( this ) )
        //        {
        //            player = p;
        //            return true;
        //        }
        //    }
        //    player = null;
        //    return false;
        //}

        internal bool IsInfluenced
        {
            get
            {
                return (InfluencingPlayers.Count > 0 );
            }
        }

        internal PlayerCollection InfluencingPlayers
        {
            get
            {
                return influencingPlayers;
            }
            //set
            //{
            //    influencingPlayer = value;
            //    if( value == null )
            //    {
            //        isInfluenced = false;
            //    }
            //    else
            //    {
            //        isInfluenced = true;
            //    }
            //}
        }

        internal void Influence( Player p )
        {
            this.InfluencingPlayers.Add( p );
        }

        internal void Reset()
        {
            this.InfluencingPlayers.Clear();
        }

        public void DoAction( Player p )
        {
            throw new System.NotImplementedException();
        }

        public override string ToString()
        {
            return this.Order + ". " + this.Name;
        }
    }

    public class AdvisorOrderComparer : IComparer<Advisor>
    {
        #region IComparer<Advisor> Members

        public int Compare( Advisor x, Advisor y )
        {
            return x.Order.CompareTo( y.Order );
        }

        #endregion
    }

    public enum Advisors
    {
        Jester = 1,
        Squire,
        Architect,
        Merchant,
        Sergeant,
        Alchemist,
        Astronomer,
        Treasurer,
        MasterHunter,
        General,
        Swordsmith,
        Duchess,
        Champion,
        Smuggler,
        Inventor,
        Wizard,
        Queen,
        King,
    }

    internal class AdvisorCollection : HashSet<Advisor>
    {
        internal AdvisorCollection() : base()
        {
        }

        internal AdvisorCollection( IEnumerable<Advisor> collection )
            : base( collection )
        {
        }

        internal Advisor this[int advisorNumber]
        {
            get
            {
                List<Advisor> l = new List<Advisor>( this );
                l.Sort( new AdvisorOrderComparer() );
                return l[advisorNumber];
            }
        }
    }
}
