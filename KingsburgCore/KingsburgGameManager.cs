using System.Collections.Generic;
using System.IO;
using System.Linq;
using TylerButler.GameToolkit;
using TylerButler.Kingsburg.Core.UI;
using TylerButler.Kingsburg.Utilities;
using System;
using System.Diagnostics;

namespace TylerButler.Kingsburg.Core
{
    public class GameManager : Game
    {
        private static GameManager instance = null;
        private PlayerCollection playerOrderPrimary;
        private PlayerCollection playerOrderSecondary;
        private PlayerCollection allPlayers;
        private readonly BuildingCollection buildings = DataLoader.LoadBuildings(Path.Combine(Properties.Settings.Default.DataPath, "Buildings.xml"));
        private readonly AdvisorCollection advisors = DataLoader.LoadAdvisors(Path.Combine(Properties.Settings.Default.DataPath, "Advisors.xml"));
        private readonly EnemyCollection enemies = DataLoader.LoadEnemies(Path.Combine(Properties.Settings.Default.DataPath, "Enemies.xml"));
        private Enemy[] enemiesForGame = new Enemy[5];
        private int currentYear = 0;
        private bool isGameOver = false;
        private UIManagerBase ui;

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
                if (instance == null)
                {
                    instance = new GameManager();
                    instance.InitializeData();
                }
                return instance;
            }
        }

        public UIManagerBase UI
        {
            get { return ui; }
            set { ui = value; }
        }

        public EnemyCollection Enemies
        {
            get
            {
                return enemies;
            }
        }

        public AdvisorCollection Advisors
        {
            get
            {
                return advisors;
            }
        }

        public BuildingCollection Buildings
        {
            get
            {
                return buildings;
            }
        }

        public PlayerCollection PlayerOrderPrimary
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

        public PlayerCollection PlayerOrderSecondary
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

        public PlayerCollection AllPlayers
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

            // Load the CLI UI Manager by default
            this.UI = new UIManager();

            // Add Phases
            Instance.GameStart.Add(new StartPhase());
        }

        private void SelectEnemies()
        {
            for (int i = 0; i < 5; i++)
            {
                IEnumerable<Enemy> queryResult = from e in Enemies
                                                 where e.Level == (i + 1)
                                                 select e;
                Enemy[] enemiesForYear = queryResult.ToArray<Enemy>();
                //int roll = new Die( 0, queryResult.Count ).Roll();
                int roll = RandomNumber.GetRandom(0, queryResult.Count<Enemy>());
                this.EnemiesForGame[i] = enemiesForYear[roll];
            }
        }

        public PlayerCollection PlayersWithLowestBuildingCount(PlayerCollection PlayersToCheck)
        {
            PlayerCollection toReturn = new PlayerCollection();
            toReturn.Add(PlayersToCheck[0]);
            for (int i = 1; /* skipping the first player in the list */ i < PlayersToCheck.Count; i++)
            {
                if (PlayersToCheck[i].NumBuildings < toReturn[0].NumBuildings)
                {
                    toReturn.Clear();
                    toReturn.Add(PlayersToCheck[i]);
                }
                else if (PlayersToCheck[i].NumBuildings == toReturn[0].NumBuildings)
                {
                    toReturn.Add(PlayersToCheck[i]);
                }
                else
                {
                } // do nothing
            }
            return toReturn;
        }

        public PlayerCollection PlayersWithHighestBuildingCount(PlayerCollection PlayersToCheck)
        {
            PlayerCollection toReturn = new PlayerCollection();
            toReturn.Add(PlayersToCheck[0]);
            for (int i = 1; i < PlayersToCheck.Count; i++)
            {
                if (PlayersToCheck[i].NumBuildings > toReturn[0].NumBuildings)
                {
                    toReturn.Clear();
                    toReturn.Add(PlayersToCheck[i]);
                }
                else if (PlayersToCheck[i].NumBuildings == toReturn[0].NumBuildings)
                {
                    toReturn.Add(PlayersToCheck[i]);
                }
                else
                {
                } // do nothing
            }
            return toReturn;
        }

        public PlayerCollection PlayersWithLowestGoodsCount(PlayerCollection PlayersToCheck)
        {
            PlayerCollection toReturn = new PlayerCollection();
            toReturn.Add(PlayersToCheck[0]);
            for (int i = 1; /* skipping the first player in the list */ i < PlayersToCheck.Count; i++)
            {
                if (PlayersToCheck[i].GoodsCount < toReturn[0].GoodsCount)
                {
                    toReturn.Clear();
                    toReturn.Add(PlayersToCheck[i]);
                }
                else if (PlayersToCheck[i].GoodsCount == toReturn[0].GoodsCount)
                {
                    toReturn.Add(PlayersToCheck[i]);
                }
                else // do nothing
                {
                }
            }
            return toReturn;
        }

        public PlayerCollection PlayersWithHighestStrength(PlayerCollection PlayersToCheck)
        {
            PlayerCollection toReturn = new PlayerCollection();
            toReturn.Add(PlayersToCheck[0]);
            for (int i = 1; i < PlayersToCheck.Count; i++)
            {

                if (PlayersToCheck[i].TotalStrength > toReturn[0].TotalStrength)
                {
                    toReturn.Clear();
                    toReturn.Add(PlayersToCheck[i]);
                }
                else if (PlayersToCheck[i].NumBuildings == toReturn[0].NumBuildings)
                {
                    toReturn.Add(PlayersToCheck[i]);
                }
                else
                {
                } // do nothing
            }
            return toReturn;
        }

        /// <summary>
        /// A convenience method to get a building reference from the name of the building.
        /// </summary>
        /// <param name="name">Name of the building.</param>
        /// <returns>A Building object representing that building.</returns>
        public Building GetBuilding(string name)
        {
            return this.Buildings.GetBuilding(name);
        }

        public override void MainExecutionMethod()
        {
            Phase next = (this.GameStart.Count > 0 ? this.GameStart[0] : this.Phases[0]);

            do
            {
                GameManager.Instance.UI.DisplayPhaseInfo(next);
                next = next.Execute();
            }
            while (!this.IsGameOver);
        }

        [Conditional("DEBUG")]
        public static void DebugSetup()
        {
            //GameManager.Instance.AllPlayers[0].AddDie( new KingsburgDie( KingsburgDie.DieTypes.MarketPositive ) );
            //GameManager.Instance.AllPlayers[0].AddDie( new KingsburgDie( KingsburgDie.DieTypes.MarketNegative ) );

            //SumComboFinder s = new SumComboFinder();
            //DiceCollection bag = new DiceCollection();
            //bag.Add( new KingsburgDie( KingsburgDie.DieTypes.MarketNegative ) );
            //bag.Add( new KingsburgDie( KingsburgDie.DieTypes.MarketPositive ) );
            //KingsburgDie d = new KingsburgDie();
            //d.Value = 3;
            //bag.Add( d );
            //d = new KingsburgDie();
            //d.Value = 1;
            //bag.Add( d );
            //KingsburgDie w = new KingsburgDie( KingsburgDie.DieTypes.White );
            //w.Value = 4;
            //List<List<KingsburgDie>> results = s.Find( 3, bag );
            //int i =0;
        }
    }
}
