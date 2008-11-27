using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TylerButler.GameToolkit;

namespace TylerButler.Kingsburg.Core.UI
{
    public abstract class UIManagerBase
    {
        private graphicsMode mode;
        internal graphicsMode Mode
        {
            get
            {
                return mode;
            }
            set
            {
                mode = value;
            }
        }

        abstract internal void DisplayDiceRoll(Player p, DiceCollection roll);

        internal void DisplayDiceRoll(Player p)
        {
            DisplayDiceRoll(p, p.MostRecentDiceRoll);
        }

        abstract internal void DisplayPlayerInfo(Player p);
        abstract internal Advisor DisplayChooseAdvisorToInfluence(Player p);
        abstract internal void DisplayPlayerOrder(PlayerCollection order);
        abstract internal Building DisplayBuildingCard(Player p, bool canBuild);

        internal void DisplayBuildingCard(Player p)
        {
            this.DisplayBuildingCard(p, false);
        }
        
        abstract internal void DisplayKingsReward(PlayerCollection players);
        abstract internal void DisplayPhaseInfo(Phase phase);

        internal void DisplayInfluenceAdvisor(Advisor a, Player p)
        {
            List<object> dataWillBeDiscarded;
            DisplayInfluenceAdvisor(a, p, out dataWillBeDiscarded);
        }

        abstract internal void DisplayInfluenceAdvisor(Advisor a, Player p, out List<object> returnData);
        abstract internal void DisplayPeekAtEnemy(Player p);
        abstract internal void DisplayEnemyInfo(Enemy enemy);
        abstract internal void DisplayKingsEnvoy(Player p);
        abstract internal void DisplaySoldierRecruitment(Player p);
        abstract internal GoodsChoiceOptions DisplayChooseAGood(Player p, params GoodsChoiceOptions[] available);
        abstract internal PlayerCollection DisplayGetPlayers();
        abstract internal DiceCollection DisplayChooseDice(Player p, Advisor a);
        abstract internal void DisplayInnReward(Player p);
        abstract internal void DisplayYearInfo();
        abstract internal int DisplayRecruitSoldiers(Player p);
        abstract internal void DisplayBattleInfo();
        abstract internal void DisplayKingsReinforcements(int reinforcements);
        abstract internal void DisplayBattleResults(Player player, Enemy enemy, BattleResults battleResults);
        abstract internal void DisplayMostGloriousVictory(Player p);
        abstract internal void DisplayFortressBonus(Player player);
        abstract internal void DisplayUseStatue(Player p);
        abstract internal void DisplayUseChapel(Player p);
        abstract internal void DisplayCathedralBonus(Player p, int VPEarned);
        abstract internal void DisplayFarmBonus(Player p);
        abstract internal void DisplayMerchantsGuildBonus(Player p);
        abstract internal void DisplayStableBonus(Player p);
        abstract internal GoodsChoiceOptions DisplayGetTownHallChoice(Player p);
        abstract internal void DisplayEmbassyBonus(Player p);
        abstract internal void DisplayUseCrane(Player p);
        abstract internal void DisplayKingsAid(Player player);
    }

    public enum graphicsMode
    {
        CLI,
        GUI,
    }
}
