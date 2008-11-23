using System.Collections.Generic;

namespace TylerButler.GameToolkit
{
    public abstract class Game
    {
        private int numPlayersMax;
        private int numPlayersMin;
        private List<Phase> phases = new List<Phase>(),
            gameStart = new List<Phase>(), 
            gameEnd = new List<Phase>();

        public int NumPlayersMax
        {
            get
            {
                return this.numPlayersMax;
            }
            set
            {
                this.numPlayersMax = value;
            }
        }

        public int NumPlayersMin
        {
            get
            {
                return this.numPlayersMin;
            }
            set
            {
                this.numPlayersMin = value;
            }
        }

        public List<Phase> Phases
        {
            get
            {
                return this.phases;
            }
            set
            {
                this.phases = value;
            }
        }

        public List<Phase> GameStart
        {
            get
            {
                return this.gameStart;
            }
            set
            {
                this.gameStart = value;
            }
        }

        public List<Phase> GameEnd
        {
            get
            {
                return this.gameEnd;
            }
            set
            {
                this.gameEnd = value;
            }
        }

        public abstract bool IsGameOver
        {
            get;
            set;
        }
    }
}
