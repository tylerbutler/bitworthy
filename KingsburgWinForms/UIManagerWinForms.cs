using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TylerButler.Kingsburg.Core.UI;
using TylerButler.Kingsburg.Core;

namespace KingsburgWinForms
{
    class UIManagerWinForms : UIManagerBase, UIManagerInterface
    {
        #region UIManagerInterface Members

        override public void DisplayDiceRoll(TylerButler.Kingsburg.Core.Player p, TylerButler.Kingsburg.Core.DiceCollection roll)
        {
            throw new NotImplementedException();
        }

        override public void DisplayPlayerInfo(TylerButler.Kingsburg.Core.Player p)
        {
            throw new NotImplementedException();
        }

        override public Advisor DisplayChooseAdvisorToInfluence(TylerButler.Kingsburg.Core.Player p)
        {
            throw new NotImplementedException();
        }

        override public void DisplayPlayerOrder(TylerButler.Kingsburg.Core.PlayerCollection order)
        {
            throw new NotImplementedException();
        }

        override public Building DisplayBuildingCard(TylerButler.Kingsburg.Core.Player p, bool canBuild)
        {
            throw new NotImplementedException();
        }

        override public void DisplayKingsReward(TylerButler.Kingsburg.Core.PlayerCollection players)
        {
            throw new NotImplementedException();
        }

        override public void DisplayPhaseInfo(TylerButler.GameToolkit.Phase phase)
        {
            throw new NotImplementedException();
        }

        override public void DisplayInfluenceAdvisor(TylerButler.Kingsburg.Core.Advisor a, TylerButler.Kingsburg.Core.Player p, out List<object> returnData)
        {
            throw new NotImplementedException();
        }

        override public void DisplayPeekAtEnemy(TylerButler.Kingsburg.Core.Player p)
        {
            throw new NotImplementedException();
        }

        override public void DisplayEnemyInfo(TylerButler.Kingsburg.Core.Enemy enemy)
        {
            throw new NotImplementedException();
        }

        override public void DisplayKingsEnvoy(TylerButler.Kingsburg.Core.Player p)
        {
            throw new NotImplementedException();
        }

        override public void DisplaySoldierRecruitment(TylerButler.Kingsburg.Core.Player p)
        {
            throw new NotImplementedException();
        }

        override public GoodsChoiceOptions DisplayChooseAGood(Player p, params GoodsChoiceOptions[] available)
        {
            throw new NotImplementedException();
        }

        override public PlayerCollection DisplayGetPlayers()
        {
            throw new NotImplementedException();
        }

        override public DiceCollection DisplayChooseDice(TylerButler.Kingsburg.Core.Player p, TylerButler.Kingsburg.Core.Advisor a)
        {
            throw new NotImplementedException();
        }

        override public void DisplayInnReward(TylerButler.Kingsburg.Core.Player p)
        {
            throw new NotImplementedException();
        }

        override public void DisplayYearInfo()
        {
            throw new NotImplementedException();
        }

        override public int DisplayRecruitSoldiers(TylerButler.Kingsburg.Core.Player p)
        {
            throw new NotImplementedException();
        }

        override public void DisplayBattleInfo()
        {
            throw new NotImplementedException();
        }

        override public void DisplayKingsReinforcements(int reinforcements)
        {
            throw new NotImplementedException();
        }

        override public void DisplayBattleResults(TylerButler.Kingsburg.Core.Player player, TylerButler.Kingsburg.Core.Enemy enemy, TylerButler.Kingsburg.Core.BattleResults battleResults)
        {
            throw new NotImplementedException();
        }

        override public void DisplayMostGloriousVictory(TylerButler.Kingsburg.Core.Player p)
        {
            throw new NotImplementedException();
        }

        override public void DisplayFortressBonus(TylerButler.Kingsburg.Core.Player player)
        {
            throw new NotImplementedException();
        }

        override public void DisplayUseStatue(TylerButler.Kingsburg.Core.Player p)
        {
            throw new NotImplementedException();
        }

        override public void DisplayUseChapel(TylerButler.Kingsburg.Core.Player p)
        {
            throw new NotImplementedException();
        }

        override public void DisplayCathedralBonus(TylerButler.Kingsburg.Core.Player p, int VPEarned)
        {
            throw new NotImplementedException();
        }

        override public void DisplayFarmBonus(TylerButler.Kingsburg.Core.Player p)
        {
            throw new NotImplementedException();
        }

        override public void DisplayMerchantsGuildBonus(TylerButler.Kingsburg.Core.Player p)
        {
            throw new NotImplementedException();
        }

        override public void DisplayStableBonus(TylerButler.Kingsburg.Core.Player p)
        {
            throw new NotImplementedException();
        }

        override public GoodsChoiceOptions DisplayGetTownHallChoice(TylerButler.Kingsburg.Core.Player p)
        {
            throw new NotImplementedException();
        }

        override public void DisplayEmbassyBonus(TylerButler.Kingsburg.Core.Player p)
        {
            throw new NotImplementedException();
        }

        override public void DisplayUseCrane(TylerButler.Kingsburg.Core.Player p)
        {
            throw new NotImplementedException();
        }

        override public void DisplayKingsAid(TylerButler.Kingsburg.Core.Player player)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
