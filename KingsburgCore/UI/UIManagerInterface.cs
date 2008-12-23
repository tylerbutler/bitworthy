using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TylerButler.GameToolkit;

namespace TylerButler.Kingsburg.Core.UI
{
    public abstract class UIManagerBase : UIManagerInterface
    {
        private graphicsMode mode;
        public graphicsMode Mode
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

        abstract public void DisplayDiceRoll( Player p, DiceCollection roll );

        public void DisplayDiceRoll( Player p )
        {
            DisplayDiceRoll( p, p.MostRecentDiceRoll );
        }

        abstract public void DisplayPlayerInfo( Player p );
        abstract public Advisor DisplayChooseAdvisorToInfluence( Player p );
        abstract public void DisplayPlayerOrder( PlayerCollection order );
        abstract public Building DisplayBuildingCard( Player p, bool canBuild );

        public void DisplayBuildingCard( Player p )
        {
            this.DisplayBuildingCard( p, false );
        }

        abstract public void DisplayKingsReward( PlayerCollection players );
        abstract public void DisplayPhaseInfo( Phase phase );

        public void DisplayInfluenceAdvisor( Advisor a, Player p )
        {
            List<object> dataWillBeDiscarded;
            DisplayInfluenceAdvisor( a, p, out dataWillBeDiscarded );
        }

        abstract public void DisplayInfluenceAdvisor( Advisor a, Player p, out List<object> returnData );
        abstract public void DisplayPeekAtEnemy( Player p );
        abstract public void DisplayEnemyInfo( Enemy enemy );
        abstract public void DisplayKingsEnvoy( Player p );
        abstract public void DisplaySoldierRecruitment( Player p );
        abstract public GoodsChoiceOptions DisplayChooseAGood( Player p, params GoodsChoiceOptions[] available );
        abstract public PlayerCollection DisplayGetPlayers();
        abstract public DiceCollection DisplayChooseDice( Player p, Advisor a );
        abstract public void DisplayInnReward( Player p );
        abstract public Building DisplayUseEnvoyToBuild( Player p );
        abstract public void DisplayYearInfo();
        abstract public int DisplayRecruitSoldiers( Player p );
        abstract public void DisplayBattleInfo();
        abstract public void DisplayKingsReinforcements( int reinforcements );
        abstract public void DisplayBattleResults( Player player, Enemy enemy, BattleResults battleResults );
        abstract public void DisplayMostGloriousVictory( Player p );
        abstract public void DisplayFortressBonus( Player player );
        abstract public void DisplayUseStatue( Player p );
        abstract public void DisplayUseChapel( Player p );
        abstract public void DisplayCathedralBonus( Player p, int VPEarned );
        abstract public void DisplayFarmBonus( Player p );
        abstract public void DisplayMerchantsGuildBonus( Player p );
        abstract public void DisplayStableBonus( Player p );
        abstract public GoodsChoiceOptions DisplayGetTownHallChoice( Player p );
        abstract public void DisplayEmbassyBonus( Player p );
        abstract public void DisplayUseCrane( Player p );
        abstract public void DisplayKingsAid( Player player );
    }

    public interface UIManagerInterface
    {
        void DisplayDiceRoll( Player p, DiceCollection roll );
        void DisplayPlayerInfo( Player p );
        Advisor DisplayChooseAdvisorToInfluence( Player p );
        void DisplayPlayerOrder( PlayerCollection order );

        /// <summary>
        /// Displays the building card for a given player. The boolean argument specifies whether or not the player can build a building. When false, the player can only see what he's built.
        /// </summary>
        /// <param name="p">The player whose building card should be displayed.</param>
        /// <param name="canBuild">Whether or not the player can actually build or not.</param>
        /// <returns>The building the player chooses to build.</returns>
        Building DisplayBuildingCard( Player p, bool canBuild );

        void DisplayKingsReward( PlayerCollection players );
        void DisplayPhaseInfo( Phase phase );
        void DisplayInfluenceAdvisor( Advisor a, Player p, out List<object> returnData );
        void DisplayPeekAtEnemy( Player p );

        /// <summary>
        /// Displays information about an enemy.
        /// </summary>
        /// <param name="enemy">The enemy to display.</param>
        void DisplayEnemyInfo( Enemy enemy );

        /// <summary>
        /// Displays a report that the kings envoy was rewarded to a specific player
        /// </summary>
        /// <param name="p">The player receiving the envoy. If null, no one is receiving the envoy.</param>
        void DisplayKingsEnvoy( Player p );

        void DisplaySoldierRecruitment( Player p );

        GoodsChoiceOptions DisplayChooseAGood( Player p, params GoodsChoiceOptions[] available );

        PlayerCollection DisplayGetPlayers();

        DiceCollection DisplayChooseDice( Player p, Advisor a );

        void DisplayInnReward( Player p );

        Building DisplayUseEnvoyToBuild( Player p );

        /// <summary>
        /// Displays information on the current year in the game.
        /// </summary>
        void DisplayYearInfo();

        /// <summary>
        /// Allows the player to recruit soldiers.
        /// </summary>
        /// <param name="p">The player recruiting soldiers.</param>
        /// <returns>The number of soldiers recruited.</returns>
        int DisplayRecruitSoldiers( Player p );

        /// <summary>
        /// Displays information about the attacker that is attacking during phase 8.
        /// </summary>
        void DisplayBattleInfo();

        /// <summary>
        /// Displays the number of reinforcements the king has sent.
        /// </summary>
        /// <param name="reinforcements">The number of reinforcements sent.</param>
        void DisplayKingsReinforcements( int reinforcements );

        /// <summary>
        /// Displays the results of a battle.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <param name="enemy">The enemy.</param>
        /// <param name="battleResults">Whether the player won, lost or tied the battle.</param>
        void DisplayBattleResults( Player player, Enemy enemy, BattleResults battleResults );

        /// <summary>
        /// Displays information that a player was awarded a VP for being the most glorious player in battle.
        /// </summary>
        /// <param name="p">The player.</param>
        void DisplayMostGloriousVictory( Player p );

        /// <summary>
        /// Displays info that a player is receiving a victory point from his fortress.
        /// </summary>
        /// <param name="player">The player.</param>
        void DisplayFortressBonus( Player player );

        /// <summary>
        /// Lets the player use the ability of their Statue to reroll.
        /// </summary>
        /// <param name="p">The player.</param>
        void DisplayUseStatue( Player p );

        /// <summary>
        /// Lets the player use the ability of their Chapel to reroll.
        /// </summary>
        /// <param name="p">The player.</param>
        void DisplayUseChapel( Player p );

        /// <summary>
        /// Displays info that a player is receiving victory points from his cathedral.
        /// </summary>
        /// <param name="p">The player.</param>
        /// <param name="VPEarned">The number of victory points earned.</param>
        void DisplayCathedralBonus( Player p, int VPEarned );

        /// <summary>
        /// Displays info that a player is receiving an extra die from his farms.
        /// </summary>
        /// <param name="p">The player.</param>
        void DisplayFarmBonus( Player p );

        void DisplayMerchantsGuildBonus( Player p );
        void DisplayStableBonus( Player p );
        GoodsChoiceOptions DisplayGetTownHallChoice( Player p );
        void DisplayEmbassyBonus( Player p );
        void DisplayUseCrane( Player p );
        void DisplayKingsAid( Player player );
    }

    public enum graphicsMode
    {
        CLI,
        GUI,
    }
}
