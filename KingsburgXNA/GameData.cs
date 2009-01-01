using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TylerButler.Kingsburg.Core;

namespace KingsburgXNA
{
    public class GameData : GameManager
    {
        #region Fields

        public PhasesEnum CurrentPhase = PhasesEnum.Start;
        public XNAPlayer CurrentPlayer;
        public XNAPlayer Player1;
        //public XNAPlayer Player2 = new XNAPlayer();
        private Game1 game;

        #endregion

        public GameData( Game1 game )
            : base()
        {
            this.game = game;
            Player1 = new XNAPlayer( this );
            AddPlayer( Player1 );
            //AddPlayer( Player2 );
        }

        public void StartGame()
        {
            CurrentPhase = PhasesEnum.Phase1;
        }
    }
}
