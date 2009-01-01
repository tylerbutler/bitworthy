using System.Collections.Generic;
using TylerButler.Kingsburg.Utilities;
using System;

namespace TylerButler.Kingsburg.Core
{
    public sealed class DiceAllocationManager
    {
        private static DiceAllocationManager instance = null;
        static GameManager gm;

        private DiceAllocationManager()
        {
            // Singleton
        }

        public static DiceAllocationManager Instance
        {
            get
            {
                if( instance == null )
                {
                    throw new Exception( "DiceAllocationManager was not initialized." );
                }
                return instance;
            }
        }

        public static void Initialize( GameManager gameManager )
        {
            instance = new DiceAllocationManager();
            gm = gameManager;
        }

        private AdvisorCollection InfluencedAdvisors
        {
            get
            {
                AdvisorCollection toReturn = new AdvisorCollection();
                foreach( Advisor a in gm.Advisors )
                {
                    if( a.IsInfluenced )
                    {
                        toReturn.Add( a );
                    }
                }
                return toReturn;
            }
        }

        public bool PlayersHaveDiceToAllocate()
        {
            foreach( Player p in gm.AllPlayers )
            {
                if( !p.HasUsedAllDice )
                {
                    return true;
                }
            }
            return false;
        }

        public AdvisorCollection InfluenceableAdvisors( Player p )
        {
            AdvisorCollection toReturn = new AdvisorCollection( gm.Advisors );
            if( !p.Envoy )
            {
                foreach( Advisor a in InfluencedAdvisors )
                {
                    toReturn.Remove( a );
                }
            }

            HashSet<int> sums = SumComboFinder.Sums( p.RemainingDice );

            AdvisorCollection copy = new AdvisorCollection( toReturn );
            foreach( Advisor a in copy )
            {
                if( !sums.Contains( a.Order ) )
                {
                    toReturn.Remove( a );
                }
            }

            return toReturn;
        }
    }
}
