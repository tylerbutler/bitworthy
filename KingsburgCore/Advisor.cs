using System;
using System.Collections.Generic;
using System.Text;
using TylerButler.Kingsburg.Utilities;
using TylerButler.GameToolkit;

namespace TylerButler.Kingsburg.Core
{
    [Serializable]
    public class Advisor : Character
    {
        private Advisors advisorNameEnum;

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

        public bool isInfluenced( List<Player> players, out Player player )
        {
            foreach( Player p in players )
            {
                if( p.InfluencedAdvisors.Contains( this ) )
                {
                    player = p;
                    return true;
                }
            }
            player = null;
            return false;
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
}
