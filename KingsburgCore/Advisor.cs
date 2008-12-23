using System;
using System.Collections.Generic;
using TylerButler.GameToolkit;
using TylerButler.Kingsburg.Core.UI;
using TylerButler.Kingsburg.Utilities;
using System.Text;

namespace TylerButler.Kingsburg.Core
{
    [Serializable]
    public class Advisor : Character
    {
        private Advisors advisorNameEnum;
        //private bool isInfluenced = false;
        [NonSerialized] private PlayerCollection influencingPlayers = new PlayerCollection();

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
                base.Name = ProperSpace( value.ToString() );
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

        public bool IsInfluenced
        {
            get
            {
                return ( InfluencingPlayers.Count > 0 );
            }
        }

        public PlayerCollection InfluencingPlayers
        {
            get
            {
                return influencingPlayers;
            }
        }

        public void Influence( Player p )
        {
            this.InfluencingPlayers.Add( p );
        }

        public void Reset()
        {
            this.InfluencingPlayers.Clear();
        }

        public void DoAction( Player p )
        {
            // For actions that require return data from the UI
            List<object> returnData;
            GoodsChoiceOptions choice;

            switch( this.AdvisorNameEnum )
            {
                case Advisors.Jester:
                    // Player gains 1 VP
                    p.BoardVictoryPoints++;
                    GameManager.Instance.UI.DisplayInfluenceAdvisor( this, p );
                    break;
                case Advisors.Squire:
                    // Take 1 gold from the supply.
                    p.Gold++;
                    GameManager.Instance.UI.DisplayInfluenceAdvisor( this, p );
                    break;
                case Advisors.Architect:
                    // Take 1 wood from the supply.
                    p.Wood++;
                    GameManager.Instance.UI.DisplayInfluenceAdvisor( this, p );
                    break;
                case Advisors.Merchant:
                    // Take 1 wood OR 1 gold from the supply.
                    GameManager.Instance.UI.DisplayInfluenceAdvisor( this, p, out returnData );
                    choice = (GoodsChoiceOptions)returnData[0];
                    p.AddGood( choice );
                    break;
                case Advisors.Sergeant:
                    // Recruit 1 soldier.
                    GameManager.Instance.UI.DisplayInfluenceAdvisor( this, p );
                    p.Soldiers++;
                    HandleStable( p );
                    break;
                case Advisors.Alchemist:
                    //trade a single good for one of each of the other two goods
                    GameManager.Instance.UI.DisplayInfluenceAdvisor( this, p, out returnData );
                    choice = (GoodsChoiceOptions)returnData[0];
                    switch( choice )
                    {
                        case GoodsChoiceOptions.Gold:
                            p.Gold--;
                            p.Wood++;
                            p.Stone++;
                            break;
                        case GoodsChoiceOptions.Wood:
                            p.Wood--;
                            p.Gold++;
                            p.Stone++;
                            break;
                        case GoodsChoiceOptions.Stone:
                            p.Stone--;
                            p.Gold++;
                            p.Wood++;
                            break;
                    }
                    break;
                case Advisors.Astronomer:
                    // Receive a good of choice and a +2 token
                    GameManager.Instance.UI.DisplayInfluenceAdvisor( this, p, out returnData );
                    choice = (GoodsChoiceOptions)returnData[0];
                    p.AddGood( choice );
                    p.PlusTwoTokens++;
                    break;
                case Advisors.Treasurer:
                    // Receive 2 gold
                    GameManager.Instance.UI.DisplayInfluenceAdvisor( this, p );
                    p.Gold += 2;
                    break;
                case Advisors.MasterHunter:
                    // Take a wood and a gold, or a wood and a stone
                    GameManager.Instance.UI.DisplayInfluenceAdvisor( this, p, out returnData );
                    choice = (GoodsChoiceOptions)returnData[0];
                    p.AddGood( choice );
                    break;
                case Advisors.General:
                    // recruit two soliders and secretly look at the enemy card
                    GameManager.Instance.UI.DisplayInfluenceAdvisor( this, p );
                    p.Soldiers += 2;
                    HandleStable( p );
                    break;
                case Advisors.Swordsmith:
                    // receive a stone and a wood or a stone and a gold
                    GameManager.Instance.UI.DisplayInfluenceAdvisor( this, p, out returnData );
                    choice = (GoodsChoiceOptions)returnData[0];
                    p.AddGood( choice );
                    break;
                case Advisors.Duchess:
                    // Take 2 goods of choice and a "+2" token
                    GameManager.Instance.UI.DisplayInfluenceAdvisor( this, p, out returnData );
                    p.AddGood( (GoodsChoiceOptions)returnData[0] );
                    p.AddGood( (GoodsChoiceOptions)returnData[1] );
                    p.PlusTwoTokens++;
                    break;
                case Advisors.Champion:
                    // Take 3 stone
                    GameManager.Instance.UI.DisplayInfluenceAdvisor( this, p );
                    p.Stone += 3;
                    break;
                case Advisors.Smuggler:
                    // Pay a VP to take 3 goods of choice.
                    GameManager.Instance.UI.DisplayInfluenceAdvisor( this, p, out returnData );
                    p.BoardVictoryPoints--;
                    p.AddGood( (GoodsChoiceOptions)returnData[0] );
                    p.AddGood( (GoodsChoiceOptions)returnData[1] );
                    p.AddGood( (GoodsChoiceOptions)returnData[2] );
                    break;
                case Advisors.Inventor:
                    // receive 1 of each good
                    GameManager.Instance.UI.DisplayInfluenceAdvisor( this, p );
                    p.Gold++;
                    p.Wood++;
                    p.Stone++;
                    break;
                case Advisors.Wizard:
                    // Take 4 gold
                    GameManager.Instance.UI.DisplayInfluenceAdvisor( this, p );
                    p.Gold += 4;
                    break;
                case Advisors.Queen:
                    // Take 2 goods of choice, spy on the enemy, and get 3 VP
                    GameManager.Instance.UI.DisplayInfluenceAdvisor( this, p, out returnData );
                    p.AddGood( (GoodsChoiceOptions)returnData[0] );
                    p.AddGood( (GoodsChoiceOptions)returnData[1] );
                    p.BoardVictoryPoints += 3;
                    break;
                case Advisors.King:
                    // take one of each good and a soldier
                    GameManager.Instance.UI.DisplayInfluenceAdvisor( this, p );
                    p.Gold++;
                    p.Wood++;
                    p.Stone++;
                    p.Soldiers++;
                    HandleStable( p );
                    break;
                default:
                    throw new Exception( "Something went wrong when running the DoAction method. Advisor={0}, Player={1}" );
            }
        }

        private void HandleStable( Player p )
        {
            if( p.HasBuilding( GameManager.Instance.Buildings.GetBuilding( "Stable" ) ) )
            {
                GameManager.Instance.UI.DisplayStableBonus( p );
                p.Soldiers++;
            }
        }

        public override string ToString()
        {
            return this.Order + ". " + this.Name;
        }

        //Code for this method came from http://www.csharper.net/blog/getting_the_text_name_of_an_enum_value_with_spaces.aspx
        private string ProperSpace( string text )
        {
            StringBuilder sb = new StringBuilder();
            string lowered = text.ToLower();

            for( int i = 0; i < text.Length; i++ )
            {
                string a = text.Substring( i, 1 );
                string b = lowered.Substring( i, 1 );
                if( a != b )
                    sb.Append( " " );

                sb.Append( a );
            }

            return sb.ToString().Trim();
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

    public class AdvisorCollection : HashSet<Advisor>
    {
        public AdvisorCollection()
            : base()
        {
        }

        public AdvisorCollection( IEnumerable<Advisor> collection )
            : base( collection )
        {
        }

        public Advisor this[int advisorNumber]
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

