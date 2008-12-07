using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using TylerButler.Kingsburg.Core;
using TylerButler.Kingsburg.Core.UI;

namespace TylerButler.Kingsburg.Windows
{
    class UIManagerWindows : UIManagerBase, UIManagerInterface
    {
        #region Properties
        private ScreenManager screenManager;
        #endregion

        #region Constructors
        public UIManagerWindows( Game game )
            : base()
        {
            screenManager = new ScreenManager( game );
        }
        #endregion

        #region Fields
        public ScreenManager ScreenManager
        {
            get
            {
                return screenManager;
            }
        }
        #endregion

        #region UIManagerBase
        public override void DisplayDiceRoll( Player p, DiceCollection roll )
        {
            throw new NotImplementedException();
        }

        public override void DisplayPlayerInfo( Player p )
        {
            throw new NotImplementedException();
        }

        public override Advisor DisplayChooseAdvisorToInfluence( Player p )
        {
            throw new NotImplementedException();
        }

        public override void DisplayPlayerOrder( PlayerCollection order )
        {
            throw new NotImplementedException();
        }

        public override Building DisplayBuildingCard( Player p, bool canBuild )
        {
            throw new NotImplementedException();
        }

        public override void DisplayKingsReward( PlayerCollection players )
        {
            throw new NotImplementedException();
        }

        public override void DisplayPhaseInfo( TylerButler.GameToolkit.Phase phase )
        {
            throw new NotImplementedException();
        }

        public override void DisplayInfluenceAdvisor( Advisor a, Player p, out List<object> returnData )
        {
            throw new NotImplementedException();
        }

        public override void DisplayPeekAtEnemy( Player p )
        {
            throw new NotImplementedException();
        }

        public override void DisplayEnemyInfo( Enemy enemy )
        {
            throw new NotImplementedException();
        }

        public override void DisplayKingsEnvoy( Player p )
        {
            throw new NotImplementedException();
        }

        public override void DisplaySoldierRecruitment( Player p )
        {
            throw new NotImplementedException();
        }

        public override GoodsChoiceOptions DisplayChooseAGood( Player p, params GoodsChoiceOptions[] available )
        {
            throw new NotImplementedException();
        }

        public override PlayerCollection DisplayGetPlayers()
        {
            PlayerCollection p = new PlayerCollection();
            p.Add( new Player( "Tyler", "" ) );
            return p;
        }

        public override DiceCollection DisplayChooseDice( Player p, Advisor a )
        {
            throw new NotImplementedException();
        }

        public override void DisplayInnReward( Player p )
        {
            throw new NotImplementedException();
        }

        public override void DisplayYearInfo()
        {
            throw new NotImplementedException();
        }

        public override int DisplayRecruitSoldiers( Player p )
        {
            throw new NotImplementedException();
        }

        public override void DisplayBattleInfo()
        {
            throw new NotImplementedException();
        }

        public override void DisplayKingsReinforcements( int reinforcements )
        {
            throw new NotImplementedException();
        }

        public override void DisplayBattleResults( Player player, Enemy enemy, BattleResults battleResults )
        {
            throw new NotImplementedException();
        }

        public override void DisplayMostGloriousVictory( Player p )
        {
            throw new NotImplementedException();
        }

        public override void DisplayFortressBonus( Player player )
        {
            throw new NotImplementedException();
        }

        public override void DisplayUseStatue( Player p )
        {
            throw new NotImplementedException();
        }

        public override void DisplayUseChapel( Player p )
        {
            throw new NotImplementedException();
        }

        public override void DisplayCathedralBonus( Player p, int VPEarned )
        {
            throw new NotImplementedException();
        }

        public override void DisplayFarmBonus( Player p )
        {
            throw new NotImplementedException();
        }

        public override void DisplayMerchantsGuildBonus( Player p )
        {
            throw new NotImplementedException();
        }

        public override void DisplayStableBonus( Player p )
        {
            throw new NotImplementedException();
        }

        public override GoodsChoiceOptions DisplayGetTownHallChoice( Player p )
        {
            throw new NotImplementedException();
        }

        public override void DisplayEmbassyBonus( Player p )
        {
            throw new NotImplementedException();
        }

        public override void DisplayUseCrane( Player p )
        {
            throw new NotImplementedException();
        }

        public override void DisplayKingsAid( Player player )
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
