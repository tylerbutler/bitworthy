﻿using System.Collections.Generic;
using TylerButler.Kingsburg.Utilities;

namespace TylerButler.Kingsburg.Core
{
    public sealed class DiceAllocationManager
    {
        private static DiceAllocationManager instance = new DiceAllocationManager();

        private DiceAllocationManager()
        {
            // Singleton
        }

        public static DiceAllocationManager Instance
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
                foreach( Advisor a in GameManager.Instance.Advisors )
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
            foreach( Player p in GameManager.Instance.AllPlayers )
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
            AdvisorCollection toReturn = new AdvisorCollection( GameManager.Instance.Advisors );
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
