using System.Collections.Generic;
using TylerButler.Kingsburg.Utilities;

namespace TylerButler.Kingsburg.Core
{
    internal sealed class DiceAllocationManager
    {
        private static DiceAllocationManager instance = new DiceAllocationManager();
        //private Dictionary<Advisor, PlayerCollection> AdvisorPlayerMap = new Dictionary<Advisor,PlayerCollection>();

        private DiceAllocationManager()
        {
            // Singleton
            //foreach( Advisor a in GameManager.Instance.Advisors)
            //{
            //    AdvisorPlayerMap[a] = a.InfluencingPlayers;
            //}
        }

        internal static DiceAllocationManager Instance
        {
            get
            {
                return instance;
            }
        }

        private AdvisorCollection InfluencedAdvisors
        {
            get
            {
                AdvisorCollection toReturn = new AdvisorCollection();
                foreach( Advisor a in GameManager.Instance.Advisors)
                {
                    if(a.IsInfluenced)
                    {
                        toReturn.Add(a);
                    }
                }
                return toReturn;
            }
        }

        internal bool PlayersHaveDiceToAllocate()
        {
            foreach( Player p in GameManager.Instance.AllPlayers )
            {
                if( !p.HasUsedAllDice )
                {
                    return true;
                }
            }
            return false;
        }

        internal AdvisorCollection InfluenceableAdvisors( Player p )
        {
            AdvisorCollection toReturn = new AdvisorCollection( GameManager.Instance.Advisors );
            if( !p.Envoy )
            {
                foreach( Advisor a in InfluencedAdvisors )
                {
                    toReturn.Remove( a );
                }
            }

            AdvisorCollection copy = new AdvisorCollection( toReturn );
            foreach( Advisor a in copy )
            {
                HashSet<int> sums = Helpers.Sums( p.RemainingDice );
                if( !sums.Contains( a.Order ) )
                {
                    toReturn.Remove( a );
                }
            }

            return toReturn;
        }
    }
}
