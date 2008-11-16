using System.Collections.Generic;
using System.IO;
using TylerButler.GameToolkit;
using TylerButler.Kingsburg.Utilities;
using TylerButler.Kingsburg.Core.UI;

namespace TylerButler.Kingsburg.Core
{
    public sealed class GameManager : Game
    {
        private static GameManager instance = null;
        private List<Player> playerOrderPrimary;
        private List<Player> playerOrderSecondary;
        private List<Player> allPlayers;
        private readonly List<Building> buildings = DataLoader.LoadBuildings(Path.Combine(Properties.Settings.Default.DataPath, "Buildings.xml"));
        private readonly List<Advisor> advisors = DataLoader.LoadAdvisors(Path.Combine(Properties.Settings.Default.DataPath, "Advisors.xml"));
        private readonly Dictionary<Enemy, int> enemies = DataLoader.LoadEnemies(Path.Combine(Properties.Settings.Default.DataPath, "Enemies.xml"));

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

        public Dictionary<Enemy, int> Enemies
        {
            get
            {
                return enemies;
            }
        }

        public List<Advisor> Advisors
        {
            get
            {
                return advisors;
            }
        }

        public List<Building> Buildings
        {
            get
            {
                return buildings;
            }
        }

        public List<Player> PlayerOrderPrimary
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

        public List<Player> PlayerOrderSecondary
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

        public List<Player> AllPlayers
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
            // Add Phases
            Instance.GameStart.Add(new StartPhase());
            //Instance.Phases.Add( new Phase1() );

        }

        public List<Player> PlayersWithLowestBuildingCount(List<Player> PlayersToCheck)
        {
            List<Player> toReturn = new List<Player>();
            toReturn.Add(PlayersToCheck[0]);
            for (int i = 1; /* skipping the first player in the list */ i < PlayersToCheck.Count; i++)
            {
                if (PlayersToCheck[i].Buildings.Count < toReturn[0].Buildings.Count)
                {
                    toReturn.Clear();
                    toReturn.Add(PlayersToCheck[i]);
                }
                else if (PlayersToCheck[i].Buildings.Count == toReturn[0].Buildings.Count)
                {
                    toReturn.Add(PlayersToCheck[i]);
                }
                else
                {
                } // do nothing
            }
            return toReturn;
        }

        public List<Player> PlayersWithHighestBuildingCount(List<Player> PlayersToCheck)
        {
            List<Player> toReturn = new List<Player>();
            toReturn.Add(PlayersToCheck[0]);
            for (int i = 1; i < PlayersToCheck.Count; i++)
            {
                if (PlayersToCheck[i].Buildings.Count > toReturn[0].Buildings.Count)
                {
                    toReturn.Clear();
                    toReturn.Add(PlayersToCheck[i]);
                }
                else if (PlayersToCheck[i].Buildings.Count == toReturn[0].Buildings.Count)
                {
                    toReturn.Add(PlayersToCheck[i]);
                }
                else
                {
                } // do nothing
            }
            return toReturn;
        }

        public List<Player> PlayersWithLowestGoodsCount(List<Player> PlayersToCheck)
        {
            List<Player> toReturn = new List<Player>();
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
                else
                {
                } // do nothing
            }
            return toReturn;
        }

        public void DeterminePlayerOrder()
        {
            // int[] toReturn = new int[Players.Count]
            // toReturn[0] = Players[0]
            // SORT THE PLAYERS BASED ON MostRecentDiceRollValue()

            this.AllPlayers.Sort(new PlayerDiceRollComparer());
        }

        public void InfluenceAdvisors()
        {
            // Walk through all the advisors, and do their actions\
            //ticket:2 case:3

            foreach( Advisor a in Advisors )
            {
                Player p;
                if( a.isInfluenced(AllPlayers, out p) )
                {
                    a.DoAction( p );
                }
            }

            throw new System.NotImplementedException();
        }

        public void RemoveEnvoyFromPlayers()
        {
            foreach (Player p in this.AllPlayers)
            {
                p.Envoy = false;
            }
        }

        public void MainExecutionMethod()
        {
            Phase next = (this.GameStart.Count > 0 ? this.GameStart[0] : this.Phases[0]);

            do
            {
                UIManager.Instance.DisplayPhaseInfo(next);
                next = next.Execute();
            }
            while (!this.IsGameOver);
        }

        internal void ConstructBuildings()
        {
            throw new System.NotImplementedException();
        }
    }
}
