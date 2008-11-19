using System.Collections.Generic;
using System.IO;
using System.Linq;
using TylerButler.GameToolkit;
using TylerButler.Kingsburg.Core.UI;
using TylerButler.Kingsburg.Utilities;

namespace TylerButler.Kingsburg.Core
{
    public sealed class GameManager : Game
    {
        private static GameManager instance = null;
        private PlayerCollection playerOrderPrimary;
        private PlayerCollection playerOrderSecondary;
        private PlayerCollection allPlayers;
        private readonly List<Building> buildings = DataLoader.LoadBuildings( Path.Combine( Properties.Settings.Default.DataPath, "Buildings.xml" ) );
        private readonly AdvisorCollection advisors = DataLoader.LoadAdvisors( Path.Combine( Properties.Settings.Default.DataPath, "Advisors.xml" ) );
        private readonly Dictionary<Enemy, int> enemies = DataLoader.LoadEnemies( Path.Combine( Properties.Settings.Default.DataPath, "Enemies.xml" ) );
        private Enemy[] enemiesForGame = new Enemy[5];
        private int currentYear = 0;

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

        internal Dictionary<Enemy, int> Enemies
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

        internal List<Building> Buildings
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
                //TODO implement ticket:1
                return false;
            }
        }

        private void InitializeData()
        {
            // Pick the enemies for the game
            Instance.SelectEnemies();

            // Add Phases
            Instance.GameStart.Add( new StartPhase() );
            //Instance.Phases.Add( new Phase1() );

        }

        private void SelectEnemies()
        {
            for( int i = 0; i < 5; i++ )
            {
                Dictionary<Enemy,int> queryResult = ( from e in Enemies
                                                      where e.Value == ( i + 1 )
                                                      select e ).ToDictionary( kv => kv.Key, kv => kv.Value );
                Enemy[] enemiesForYear = queryResult.Keys.ToArray<Enemy>();
                int roll = new Die( 0, queryResult.Count ).Roll();
                this.EnemiesForGame[i] = enemiesForYear[roll];
            }
        }

        internal PlayerCollection PlayersWithLowestBuildingCount( PlayerCollection PlayersToCheck )
        {
            PlayerCollection toReturn = new PlayerCollection();
            toReturn.Add( PlayersToCheck[0] );
            for( int i = 1; /* skipping the first player in the list */ i < PlayersToCheck.Count; i++ )
            {
                if( PlayersToCheck[i].Buildings.Count < toReturn[0].Buildings.Count )
                {
                    toReturn.Clear();
                    toReturn.Add( PlayersToCheck[i] );
                }
                else if( PlayersToCheck[i].Buildings.Count == toReturn[0].Buildings.Count )
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
                if( PlayersToCheck[i].Buildings.Count > toReturn[0].Buildings.Count )
                {
                    toReturn.Clear();
                    toReturn.Add( PlayersToCheck[i] );
                }
                else if( PlayersToCheck[i].Buildings.Count == toReturn[0].Buildings.Count )
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
            //bug:3

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

            throw new System.NotImplementedException();
        }

        public void RemoveEnvoyFromPlayers()
        {
            foreach( Player p in this.AllPlayers )
            {
                p.Envoy = false;
            }
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

        internal void ConstructBuildings()
        {
            throw new System.NotImplementedException();
        }
    }
}
