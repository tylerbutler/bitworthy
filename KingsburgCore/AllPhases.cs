using System;
using TylerButler.GameToolkit;
using TylerButler.Kingsburg.Core.UI;

namespace TylerButler.Kingsburg.Core
{
    public class StartPhase : Phase
    {
        public StartPhase()
        {
            this.Title = "START GAME - Choose Players";
            this.Description = "Choose the players to play the game.";
        }

        public override Phase Execute()
        {
            // Add Players
            GameManager.Instance.AllPlayers = GameManager.Instance.UI.DisplayGetPlayers();
            GameManager.DebugSetup();
            return new Phase1();
        }
    }

    public class Phase1 : Phase
    {
        public Phase1()
        {
            this.Title = "PHASE 1 - Aid from the King";
            this.Description = "The player with the fewest buildings receives one additional die. If there is a tie, the player with the least number of goods. If that is also a tie, each tied player takes one good of choice.";
        }

        public override Phase Execute()
        {
            GameManager k = GameManager.Instance;
            k.CurrentYear++;
            //TODO: Pop up phase info message
            GameManager.Instance.UI.DisplayYearInfo();
            PlayerCollection LeastBuildingPlayers = k.PlayersWithLowestBuildingCount( k.AllPlayers );
            if( LeastBuildingPlayers.Count == 1 )
            {
                //LeastBuildingPlayers[0].KingsAidDie = 
                GameManager.Instance.UI.DisplayKingsAid( LeastBuildingPlayers[0] );
                LeastBuildingPlayers[0].AddDie();
            }
            else
            {
                PlayerCollection LeastGoodsPlayers = k.PlayersWithLowestGoodsCount( LeastBuildingPlayers );
                if( LeastGoodsPlayers.Count == 1 )
                {
                    //LeastBuildingPlayers[0].KingsAidDie = 
                    GameManager.Instance.UI.DisplayKingsAid( LeastGoodsPlayers[0] );
                    LeastGoodsPlayers[0].AddDie();
                }
                else
                {
                    foreach( Player p in LeastGoodsPlayers )
                    {
                        GoodsChoiceOptions choice = GameManager.Instance.UI.DisplayChooseAGood( p, GoodsChoiceOptions.Gold,
                            GoodsChoiceOptions.Wood, GoodsChoiceOptions.Stone );
                        p.AddGood( choice );
                    }
                }
            }

            return new Phase2();
        }
    }

    /// <summary>
    /// Phase 2 is the first productive phase of the year, and this class serves as a model for all productive seasons.
    /// The following actions are taken in phase 2:
    /// a) Roll Dice and determine the player order
    /// b) Influence Advisors
    /// c) Receive the rewards from the advisors
    /// d) Construct buildings
    /// </summary>
    public class Phase2 : Phase
    {
        public Phase2()
        {
            this.Title = "Phase 2 - Spring";
            this.Description = "All players roll dice, influence advisors, receive help and construct buildings.";
        }

        public override Phase Execute()
        {
            // Reset advisor state just in case
            ClearInfluencedAdvisors();

            foreach( Player p in GameManager.Instance.AllPlayers )
            {
                HandlePlusTwo( p );
                HandleMerchantsGuild( p );
                HandleFarms( p );
                HandleMarket( p );
                p.RollDice();
                GameManager.Instance.UI.DisplayDiceRoll( p );
                HandleStatueAction( p );
                HandleChapelAction( p );
            }
            DeterminePlayerOrder();
            while( DiceAllocationManager.Instance.PlayersHaveDiceToAllocate() )
            {
                foreach( Player p in GameManager.Instance.AllPlayers )
                {
                    if( !p.HasUsedAllDice )
                    {
                        Advisor influenced = GameManager.Instance.UI.DisplayChooseAdvisorToInfluence( p );
                        if( influenced != null ) // player has passed if influenced==null
                        {
                            influenced.InfluencingPlayers.Add( p );
                            DiceCollection spent = GameManager.Instance.UI.DisplayChooseDice( p, influenced );
                            p.AllocateDice( spent );
                            if( !p.HasUsedPlusTwo && spent.GetAllDiceOfType( KingsburgDie.DieTypes.PlusTwo ).Count > 0 )
                            {
                                p.HasUsedPlusTwo = false;
                            }
                        }
                    }
                }
            }

            InfluenceAdvisors();
            ConstructBuildings();

            // Phase is complete, reset the advisors and the players
            ClearInfluencedAdvisors();

            foreach( Player p in GameManager.Instance.AllPlayers )
            {
                p.RemoveNonRegularDice(); //They'll be added back to appropriate players before the next production phase

                HandleTownHall( p );
                HandleEmbassy( p );

                p.HasUsedMarket = false;
                p.HasUsedPlusTwo = false;
            }

            return new Phase3();
        }

        private void ConstructBuildings()
        {
            foreach( Player p in GameManager.Instance.AllPlayers )
            {
                Building built = GameManager.Instance.UI.DisplayBuildingCard( p, true /* can build */);
                if( built != null )
                {
                    p.Buildings.Add( built );
                    p.Wood -= built.WoodCost;
                    p.Stone -= built.StoneCost;

                    if( HandleCrane( p, built ) )
                    {
                        p.Gold -= ( built.GoldCost - 1 );
                    }
                    else
                    {
                        p.Gold -= built.GoldCost;
                    }

                    // Give VP to player
                    p.VictoryPoints += built.VictoryPointValue;
                }

                // If player has the Envoy, he can choose to build a second time
                if( p.Envoy )
                {
                    built = null;
                    built = GameManager.Instance.UI.DisplayUseEnvoyToBuild( p );
                }
            }
        }

        private void DeterminePlayerOrder()
        {
            // SORT THE PLAYERS BASED ON MostRecentDiceRollValue()
            GameManager.Instance.AllPlayers.Sort( new PlayerDiceRollComparer() );
            GameManager.Instance.UI.DisplayPlayerOrder( GameManager.Instance.AllPlayers );
        }

        private void InfluenceAdvisors()
        {
            // Walk through all the advisors, and do their actions
            foreach( Advisor a in GameManager.Instance.Advisors )
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

        /// <summary>
        /// Mark all the advisors as uninfluenced in preparation for a new round.
        /// </summary>
        private void ClearInfluencedAdvisors()
        {
            foreach( Advisor a in GameManager.Instance.Advisors )
            {
                a.InfluencingPlayers.Clear();
            }
        }

        /// <summary>
        /// Allow the player to use his statue if possible.
        /// </summary>
        /// <param name="p">The player.</param>
        private void HandleStatueAction( Player p )
        {
            if( !p.HasBuilding( GameManager.Instance.Buildings.GetBuilding( "Statue" ) ) )
            {
                return;
            }
            else if( !p.MostRecentDiceRoll.AllSameRoll() )
            {
                return;
            }
            else
            {
                GameManager.Instance.UI.DisplayUseStatue( p );
            }
        }

        private void HandleChapelAction( Player p )
        {
            if( !p.HasBuilding( GameManager.Instance.Buildings.GetBuilding( "Chapel" ) ) )
            {
                return;
            }
            else if( p.MostRecentDiceRollTotalValue > 7 )
            {
                return;
            }
            else
            {
                GameManager.Instance.UI.DisplayUseChapel( p );
            }
        }

        private void HandleFarms( Player p )
        {
            if( p.HasBuilding( GameManager.Instance.Buildings.GetBuilding( "Farms" ) ) )
            {
                GameManager.Instance.UI.DisplayFarmBonus( p );
                p.AddDie();
            }
        }

        private void HandleMerchantsGuild( Player p )
        {
            if( p.HasBuilding( GameManager.Instance.Buildings.GetBuilding( "Merchants' Guild" ) ) )
            {
                GameManager.Instance.UI.DisplayMerchantsGuildBonus( p );
                p.Gold++;
            }
        }

        private void HandleTownHall( Player p )
        {
            if( p.HasBuilding( GameManager.Instance.Buildings.GetBuilding( "Town Hall" ) ) )
            {
                GoodsChoiceOptions choice = GameManager.Instance.UI.DisplayGetTownHallChoice( p );

                if( choice == GoodsChoiceOptions.None ) // player chose not to use their town hall
                {
                    return;
                }
                else if( choice == GoodsChoiceOptions.PlusTwoToken ) // player chose a plus two token
                {
                    p.PlusTwoTokens--;
                    p.VictoryPoints++;
                }
                else
                {
                    p.RemoveGood( choice );
                    p.VictoryPoints++;
                }
            }
        }

        private void HandleEmbassy( Player p )
        {
            if( p.HasBuilding( GameManager.Instance.Buildings.GetBuilding( "Embassy" ) ) )
            {
                GameManager.Instance.UI.DisplayEmbassyBonus( p );
                p.VictoryPoints++;
            }
        }

        private bool HandleCrane( Player p, Building built )
        {
            if( p.HasBuilding( GameManager.Instance.Buildings.GetBuilding( "Crane" ) ) )
            {
                if( built.Column == 3 || built.Column == 4 )
                {
                    GameManager.Instance.UI.DisplayUseCrane( p );
                    return true;
                }
            }
            return false;
        }

        private void HandleMarket( Player p )
        {
            if( p.HasBuilding( GameManager.Instance.Buildings.GetBuilding( "Market" ) ) )
            {
                p.AddDie( new KingsburgDie( KingsburgDie.DieTypes.MarketPositive ) );
                p.AddDie( new KingsburgDie( KingsburgDie.DieTypes.MarketNegative ) );
                p.HasUsedMarket = false;
            }
        }

        private void HandlePlusTwo( Player p )
        {
            if( p.PlusTwoTokens > 0 )
            {
                p.AddDie( new KingsburgDie( KingsburgDie.DieTypes.PlusTwo ) );
                p.HasUsedPlusTwo = false;
            }
        }
    }

    public class Phase3 : Phase
    {
        public Phase3()
        {
            this.Title = "Phase 3 - The King's Reward";
            this.Description = "The player with the most buildings receives 1 Victory Point. If there is a tie, every tied player receives 1 Victory Point.";
        }

        public override Phase Execute()
        {
            GameManager gm = GameManager.Instance;
            PlayerCollection players = gm.PlayersWithHighestBuildingCount( gm.AllPlayers );
            GameManager.Instance.UI.DisplayKingsReward( players );
            foreach( Player p in players )
            {
                p.VictoryPoints++;
            }

            return new Phase4();
        }
    }

    /// <summary>
    /// Phase 4 is the same as phase 2.
    /// </summary>
    public class Phase4 : Phase2
    {
        public Phase4()
            : base()
        {
            this.Title = "Phase 4 - Summer";
        }

        public override Phase Execute()
        {
            // Do all of phase 2's actions, then finish it up by calculating the tokens for players with inns
            base.Execute();

            foreach( Player p in GameManager.Instance.AllPlayers )
            {
                if( p.HasBuilding( GameManager.Instance.GetBuilding( "Inn" ) ) )
                {
                    GameManager.Instance.UI.DisplayInnReward( p );
                    p.PlusTwoTokens++;
                }
            }

            return new Phase5();
        }
    }

    public class Phase5 : Phase
    {
        public Phase5()
        {
            this.Title = "Phase 5 - The King's Envoy";
            this.Description = "The player with the fewest buildings receives help from the King's Envoy. If there is a tie, the player with the least number of goods. If that is also a tie, nobody receives help.";
        }

        public override Phase Execute()
        {
            GameManager gm = GameManager.Instance;
            RemoveEnvoyFromPlayers();
            Player PlayerReceivingEnvoy;
            PlayerCollection LeastBuildingsPlayers = gm.PlayersWithLowestBuildingCount( gm.AllPlayers );

            if( LeastBuildingsPlayers.Count == 1 )
            {
                PlayerReceivingEnvoy = LeastBuildingsPlayers[0];
                PlayerReceivingEnvoy.Envoy = true;
            }
            else
            {
                PlayerCollection LeastGoodsPlayers = gm.PlayersWithLowestGoodsCount( LeastBuildingsPlayers );
                if( LeastGoodsPlayers.Count == 1 )
                {
                    PlayerReceivingEnvoy = LeastGoodsPlayers[0];
                    PlayerReceivingEnvoy.Envoy = true;
                }
                else
                {
                    PlayerReceivingEnvoy = null;
                }
            }

            GameManager.Instance.UI.DisplayKingsEnvoy( PlayerReceivingEnvoy );
            return new Phase6();
        }

        /// <summary>
        /// Remove the envoy from all players.
        /// </summary>
        public void RemoveEnvoyFromPlayers()
        {
            foreach( Player p in GameManager.Instance.AllPlayers )
            {
                p.Envoy = false;
            }
        }
    }

    public class Phase6 : Phase2
    {
        public Phase6()
            : base()
        {
            this.Title = "Phase 6 - Autumn";
        }

        public override Phase Execute()
        {
            base.Execute();
            return new Phase7();
        }
    }

    public class Phase7 : Phase
    {
        public Phase7()
        {
            this.Title = "Phase 7 - Recruit Soldiers";
            this.Description = "Each player may pay goods to hire additional soldiers.";
        }

        public override Phase Execute()
        {
            foreach( Player p in GameManager.Instance.AllPlayers )
            {
                int numRecruited = GameManager.Instance.UI.DisplayRecruitSoldiers( p );
                p.Soldiers += numRecruited;
            }

            return new Phase8();
        }
    }

    public class Phase8 : Phase
    {
        public Phase8()
        {
            this.Title = "Phase 8 - Winter - The Battle";
            this.Description = "Enemies are attacking! All players must defeat the invaders!";
        }

        public override Phase Execute()
        {
            GameManager.Instance.UI.DisplayBattleInfo();
            Enemy e= GameManager.Instance.EnemiesForGame[GameManager.Instance.CurrentYear - 1];

            int reinforcements = GetKingsReinforcements();
            GameManager.Instance.UI.DisplayKingsReinforcements( reinforcements );

            foreach( Player p in GameManager.Instance.AllPlayers )
            {
                DoPlayerBattle( p, e, reinforcements );
            }

            foreach( Player p in GameManager.Instance.PlayersWithHighestStrength( GameManager.Instance.AllPlayers ) )
            {
                if( p.WasVictorious )
                {
                    p.VictoryPoints++;
                    GameManager.Instance.UI.DisplayMostGloriousVictory( p );
                }
            }

            // Handle the Fortress special ability
            foreach( Player p in GameManager.Instance.AllPlayers )
            {
                if( p.HasBuilding( GameManager.Instance.GetBuilding( "Fortress" ) ) )
                {
                    p.VictoryPoints++;
                    GameManager.Instance.UI.DisplayFortressBonus( p );
                }
            }

            // Reset stuff for the next year.
            foreach( Player p in GameManager.Instance.AllPlayers )
            {
                p.Soldiers = 0;
                p.WasVictorious = false;
            }

            if( GameManager.Instance.CurrentYear == 5 )
            {
                GameManager.Instance.IsGameOver = true;
                return new EndPhase();
            }
            else
            {
                return new Phase1();
            }
        }

        /// <summary>
        /// Gets a number of reinforcements that the king sends.
        /// </summary>
        /// <returns>An int between 1 and 6 representing the number of reinforcements the king has sent.</returns>
        public int GetKingsReinforcements()
        {
            return RandomNumber.GetRandom( 1, 6 );
        }

        /// <summary>
        /// Execute a battle between the player and an enemy.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="enemy"></param>
        public void DoPlayerBattle( Player player, Enemy enemy, int reinforcements )
        {
            int totalStrength = player.TotalStrength + player.BonusStrengthAgainstEnemy( enemy ) + reinforcements;
            if( totalStrength > enemy.Strength ) // player wins
            {
                GameManager.Instance.UI.DisplayBattleResults( player, enemy, BattleResults.Victory );
                this.AwardPlayerAfterBattle( player, enemy );
            }
            else if( totalStrength == enemy.Strength ) // player ties
            {
                // Check if the player has the stone wall, which allows a tie to count as a victory
                if( player.HasBuilding( GameManager.Instance.GetBuilding( "Stone Wall" ) ) )
                {
                    this.AwardPlayerAfterBattle( player, enemy );
                    GameManager.Instance.UI.DisplayBattleResults( player, enemy, BattleResults.Victory );
                }
                else
                {
                    GameManager.Instance.UI.DisplayBattleResults( player, enemy, BattleResults.Tie );
                }
            }
            else // player loses
            {
                PenalizePlayerAfterBattle( player, enemy );
                GameManager.Instance.UI.DisplayBattleResults( player, enemy, BattleResults.Loss );
            }
        }

        /// <summary>
        /// Awards the player after a victory against the enemy.
        /// </summary>
        /// <param name="player">The player</param>
        /// <param name="enemy">The enemy</param>
        public void AwardPlayerAfterBattle( Player player, Enemy enemy )
        {
            player.Gold += enemy.GoldReward;
            player.Wood += enemy.WoodReward;
            player.Stone += enemy.StoneReward;
            player.VictoryPoints += enemy.VictoryPointReward;

            if( enemy.GoodReward > 0 )
            {
                player.AddGood( GameManager.Instance.UI.DisplayChooseAGood( player, GoodsChoiceOptions.Gold, GoodsChoiceOptions.Wood, GoodsChoiceOptions.Stone ) );
            }

            player.WasVictorious = true;
        }

        /// <summary>
        /// Penalizes the player after a loss to the enemy.
        /// </summary>
        /// <param name="player">The player</param>
        /// <param name="enemy">The enemy</param>
        public void PenalizePlayerAfterBattle( Player player, Enemy enemy )
        {
            //Bug:19
            player.Gold -= enemy.GoldPenalty;
            player.Wood -= enemy.WoodPenalty;
            player.Stone -= enemy.StonePenalty;
            player.VictoryPoints -= enemy.VictoryPointPenalty;

            if( enemy.GoodPenalty >= player.GoodsCount )
            {
                player.RemoveAllGoods();
            }
            else
            {
                player.RemoveGood( GameManager.Instance.UI.DisplayChooseAGood( player, player.GoodTypesPlayerHas.ToArray() ) );
            }

            player.DestroyBuildings( enemy.BuildingPenalty );
            player.WasVictorious = false;
        }
    }

    public class EndPhase : Phase
    {
        public EndPhase()
        {
            this.Title = "Game Over - Final Scoring";
            this.Description = "The game has come to an end! Commence the final scoring!";
        }

        public override Phase Execute()
        {
            foreach( Player p in GameManager.Instance.AllPlayers )
            {
                HandleCathedral( p );
            }

            throw new NotImplementedException();
        }

        private void HandleCathedral( Player p )
        {
            int VPEarned = p.GoodsCount / 2;
            GameManager.Instance.UI.DisplayCathedralBonus( p, VPEarned );
            p.VictoryPoints += VPEarned;
            throw new NotImplementedException();
        }
    }

    public enum BattleResults
    {
        Victory,
        Tie,
        Loss,
    }
}
