using System.Collections.Generic;
using System.IO;
using System.Linq;
using TylerButler.GameToolkit;
using TylerButler.Kingsburg.Core.UI;
using TylerButler.Kingsburg.Utilities;
using System;

namespace TylerButler.Kingsburg.Core
{
    public sealed class GameManager : Game
    {
        private static GameManager instance = null;
        private PlayerCollection playerOrderPrimary;
        private PlayerCollection playerOrderSecondary;
        private PlayerCollection allPlayers;
        private readonly BuildingCollection buildings = DataLoader.LoadBuildings( Path.Combine( Properties.Settings.Default.DataPath, "Buildings.xml" ) );
        private readonly AdvisorCollection advisors = DataLoader.LoadAdvisors( Path.Combine( Properties.Settings.Default.DataPath, "Advisors.xml" ) );
        private readonly EnemyCollection enemies = DataLoader.LoadEnemies( Path.Combine( Properties.Settings.Default.DataPath, "Enemies.xml" ) );
        private Enemy[] enemiesForGame = new Enemy[5];
        private int currentYear = 0;
        private bool isGameOver = false;

        static GameManager()
        {
            // Explicit static constructor to tell C# compiler
            // not to mark type as beforefieldinit
        }

        GameManager()
            : base()
        {

        }

        public static GameManager Instance
        {
            get
            {
                if( instance == null )
                {
                    instance = new GameManager();
                    instance.InitializeData();
                }
                return instance;
            }
        }

        internal EnemyCollection Enemies
        {
            get
            {
                return enemies;
            }
        }

        internal AdvisorCollection Advisors
        {
            get
            {
                return advisors;
            }
        }

        internal BuildingCollection Buildings
        {
            get
            {
                return buildings;
            }
        }

        internal PlayerCollection PlayerOrderPrimary
        {
            get
            {
                return playerOrderPrimary;
            }
            set
            {
                playerOrderPrimary = value;
            }
        }

        internal PlayerCollection PlayerOrderSecondary
        {
            get
            {
                return playerOrderSecondary;
            }
            set
            {
                playerOrderSecondary = value;
            }
        }

        internal PlayerCollection AllPlayers
        {
            get
            {
                return this.allPlayers;
            }
            set
            {
                this.allPlayers = value;
            }
        }

        public Enemy[] EnemiesForGame
        {
            get
            {
                return enemiesForGame;
            }
            set
            {
                enemiesForGame = value;
            }
        }

        public int CurrentYear
        {
            get
            {
                return currentYear;
            }
            set
            {
                currentYear = value;
            }
        }


        public override bool IsGameOver
        {
            get
            {
                return isGameOver;
            }

            set
            {
                isGameOver = value;
            }
        }

        private void InitializeData()
        {
            // Pick the enemies for the game
            Instance.SelectEnemies();

            // Add Phases
            Instance.GameStart.Add( new StartPhase() );
        }

        private void SelectEnemies()
        {
            for( int i = 0; i < 5; i++ )
            {
                IEnumerable<Enemy> queryResult = from e in Enemies
                                                 where e.Level == ( i + 1 )
                                                 select e;
                Enemy[] enemiesForYear = queryResult.ToArray<Enemy>();
                //int roll = new Die( 0, queryResult.Count ).Roll();
                int roll = RandomNumber.GetRandom( 0, queryResult.Count<Enemy>() );
                this.EnemiesForGame[i] = enemiesForYear[roll];
            }
        }

        internal PlayerCollection PlayersWithLowestBuildingCount( PlayerCollection PlayersToCheck )
        {
            PlayerCollection toReturn = new PlayerCollection();
            toReturn.Add( PlayersToCheck[0] );
            for( int i = 1; /* skipping the first player in the list */ i < PlayersToCheck.Count; i++ )
            {
                if( PlayersToCheck[i].NumBuildings < toReturn[0].NumBuildings )
                {
                    toReturn.Clear();
                    toReturn.Add( PlayersToCheck[i] );
                }
                else if( PlayersToCheck[i].NumBuildings == toReturn[0].NumBuildings )
                {
                    toReturn.Add( PlayersToCheck[i] );
                }
                else
                {
                } // do nothing
            }
            return toReturn;
        }

        internal PlayerCollection PlayersWithHighestBuildingCount( PlayerCollection PlayersToCheck )
        {
            PlayerCollection toReturn = new PlayerCollection();
            toReturn.Add( PlayersToCheck[0] );
            for( int i = 1; i < PlayersToCheck.Count; i++ )
            {
                if( PlayersToCheck[i].NumBuildings > toReturn[0].NumBuildings )
                {
                    toReturn.Clear();
                    toReturn.Add( PlayersToCheck[i] );
                }
                else if( PlayersToCheck[i].NumBuildings == toReturn[0].NumBuildings )
                {
                    toReturn.Add( PlayersToCheck[i] );
                }
                else
                {
                } // do nothing
            }
            return toReturn;
        }

        internal PlayerCollection PlayersWithLowestGoodsCount( PlayerCollection PlayersToCheck )
        {
            PlayerCollection toReturn = new PlayerCollection();
            toReturn.Add( PlayersToCheck[0] );
            for( int i = 1; /* skipping the first player in the list */ i < PlayersToCheck.Count; i++ )
            {
                if( PlayersToCheck[i].GoodsCount < toReturn[0].GoodsCount )
                {
                    toReturn.Clear();
                    toReturn.Add( PlayersToCheck[i] );
                }
                else if( PlayersToCheck[i].GoodsCount == toReturn[0].GoodsCount )
                {
                    toReturn.Add( PlayersToCheck[i] );
                }
                else // do nothing
                {
                }
            }
            return toReturn;
        }

        internal PlayerCollection PlayersWithHighestStrength( PlayerCollection PlayersToCheck )
        {
            PlayerCollection toReturn = new PlayerCollection();
            toReturn.Add( PlayersToCheck[0] );
            for( int i = 1; i < PlayersToCheck.Count; i++ )
            {

                if( PlayersToCheck[i].TotalStrength > toReturn[0].TotalStrength )
                {
                    toReturn.Clear();
                    toReturn.Add( PlayersToCheck[i] );
                }
                else if( PlayersToCheck[i].NumBuildings == toReturn[0].NumBuildings )
                {
                    toReturn.Add( PlayersToCheck[i] );
                }
                else
                {
                } // do nothing
            }
            return toReturn;
        }

        internal void DeterminePlayerOrder()
        {
            // int[] toReturn = new int[Players.Count]
            // toReturn[0] = Players[0]
            // SORT THE PLAYERS BASED ON MostRecentDiceRollValue()

            this.AllPlayers.Sort( new PlayerDiceRollComparer() );
        }

        public void InfluenceAdvisors()
        {
            // Walk through all the advisors, and do their actions
            foreach( Advisor a in Advisors )
            {
                if( a.IsInfluenced )
                {
                    foreach( Player p in a.InfluencingPlayers )
                    {
                        a.DoAction( p );
                    }
                }
            }
        }

        public void RemoveEnvoyFromPlayers()
        {
            foreach( Player p in this.AllPlayers )
            {
                p.Envoy = false;
            }
        }

        internal void ConstructBuildings()
        {
            foreach( Player p in Instance.AllPlayers )
            {
                UIManager.Instance.DisplayBuildingCard( p, true /* can build */);
            }
        }

        /// <summary>
        /// A convenience method to get a building reference from the name of the building.
        /// </summary>
        /// <param name="name">Name of the building.</param>
        /// <returns>A Building object representing that building.</returns>
        internal Building GetBuilding( string name )
        {
            return this.Buildings.GetBuilding( name );
        }

        public void MainExecutionMethod()
        {
            Phase next = ( this.GameStart.Count > 0 ? this.GameStart[0] : this.Phases[0] );

            do
            {
                UIManager.Instance.DisplayPhaseInfo( next );
                next = next.Execute();
            }
            while( !this.IsGameOver );
        }

        /// <summary>
        /// Mark all the advisors as uninfluenced in preparation for a new round.
        /// </summary>
        internal void ClearInfluencedAdvisors()
        {
            foreach( Advisor a in this.Advisors )
            {
                a.InfluencingPlayers.Clear();
            }
        }

        /// <summary>
        /// Remove the envoy from all players.
        /// </summary>
        internal void ClearEnvoyFromAllPlayers()
        {
            foreach( Player p in this.AllPlayers )
            {
                p.Envoy = false;
            }
        }
    }
}
