using System;
using TylerButler.GameToolkit;
using TylerButler.Kingsburg.Core.UI;

namespace TylerButler.Kingsburg.Core
{
    public class StartPhase : Phase
    {
        GameManager gm;
        public StartPhase( GameManager gm )
        {
            this.gm = gm;
            this.Title = "START GAME - Choose Players";
            this.Description = "Choose the players to play the game.";
        }

        public override Phase Execute()
        {
            // Add Players
            gm.AllPlayers = gm.UI.DisplayGetPlayers();
            gm.DebugSetup();
            return new Phase1( gm );
        }
    }

    public class Phase1 : Phase
    {
        GameManager gm;

        public Phase1( GameManager gm )
        {
            this.gm = gm;
            this.Title = "PHASE 1 - Aid from the King";
            this.Description = "The player with the fewest buildings receives one additional die. If there is a tie, the player with the least number of goods. If that is also a tie, each tied player takes one good of choice.";
        }

        public override Phase Execute()
        {
            gm.CurrentYear++;
            gm.UI.DisplayYearInfo();
            PlayerCollection kingAid = FindKingsAid();
            if( kingAid.Count > 1 )
            {
                foreach( Player p in kingAid )
                {
                    GoodsChoiceOptions choice = gm.UI.DisplayChooseAGood( p, GoodsChoiceOptions.Gold,
                        GoodsChoiceOptions.Wood, GoodsChoiceOptions.Stone );
                    p.AddGood( choice );
                }
            }
            else
            {
                gm.UI.DisplayKingsAid( kingAid[0] );
                kingAid[0].AddDie();
            }
            return new Phase2( gm );
        }

        public PlayerCollection FindKingsAid()
        {
            PlayerCollection LeastBuildingPlayers = gm.PlayersWithLowestBuildingCount( gm.AllPlayers );
            if( LeastBuildingPlayers.Count == 1 )
            {
                return new PlayerCollection( LeastBuildingPlayers[0] );
            }
            else
            {
                PlayerCollection LeastGoodsPlayers = gm.PlayersWithLowestGoodsCount( LeastBuildingPlayers );
                if( LeastGoodsPlayers.Count == 1 )
                {
                    return new PlayerCollection( LeastGoodsPlayers[0] );
                }
                else
                {
                    return LeastGoodsPlayers;
                }
            }
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
        protected GameManager gm;

        public Phase2( GameManager gm )
        {
            this.gm = gm;
            this.Title = "Phase 2 - Spring";
            this.Description = "All players roll dice, influence advisors, receive help and construct buildings.";
        }

        public override Phase Execute()
        {
            // Reset advisor state just in case
            ClearInfluencedAdvisors();

            foreach( Player p in gm.AllPlayers )
            {
                HandlePlusTwo( p );
                HandleMerchantsGuild( p );
                HandleFarms( p );
                HandleMarket( p );
                p.RollDice();
                gm.UI.DisplayDiceRoll( p );
                HandleStatueAction( p );
                HandleChapelAction( p );
            }
            DeterminePlayerOrder();
            while( DiceAllocationManager.Instance.PlayersHaveDiceToAllocate() )
            {
                foreach( Player p in gm.AllPlayers )
                {
                    if( !p.HasUsedAllDice )
                    {
                        Advisor influenced = gm.UI.DisplayChooseAdvisorToInfluence( p );
                        if( influenced != null ) // player has passed if influenced==null
                        {
                            influenced.InfluencingPlayers.Add( p );
                            DiceCollection spent = gm.UI.DisplayChooseDice( p, influenced );
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

            foreach( Player p in gm.AllPlayers )
            {
                p.RemoveNonRegularDice(); //They'll be added back to appropriate players before the next production phase

                HandleTownHall( p );
                HandleEmbassy( p );

                p.HasUsedMarket = false;
                p.HasUsedPlusTwo = false;
            }

            return new Phase3( gm );
        }

        private void ConstructBuildings()
        {
            foreach( Player p in gm.AllPlayers )
            {
                Building built = gm.UI.DisplayBuildingCard( p, true /* can build */);
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

                    // REMOVED because the building VP is now tracked separately from the regular VP
                    // Give VP to player
                    //p.BoardVictoryPoints += built.VictoryPointValue;
                }

                // If player has the Envoy, he can choose to build a second time
                if( p.Envoy )
                {
                    built = null;
                    built = gm.UI.DisplayUseEnvoyToBuild( p );
                }
            }
        }

        private void DeterminePlayerOrder()
        {
            // SORT THE PLAYERS BASED ON MostRecentDiceRollValue()
            gm.AllPlayers.Sort( new PlayerDiceRollComparer() );
            gm.UI.DisplayPlayerOrder( gm.AllPlayers );
        }

        private void InfluenceAdvisors()
        {
            // Walk through all the advisors, and do their actions
            foreach( Advisor a in gm.Advisors )
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
            foreach( Advisor a in gm.Advisors )
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
            if( !p.HasBuilding( gm.Buildings.GetBuilding( "Statue" ) ) )
            {
                return;
            }
            else if( !p.MostRecentDiceRoll.AllSameRoll() )
            {
                return;
            }
            else
            {
                gm.UI.DisplayUseStatue( p );
            }
        }

        private void HandleChapelAction( Player p )
        {
            if( !p.HasBuilding( gm.Buildings.GetBuilding( "Chapel" ) ) )
            {
                return;
            }
            else if( p.MostRecentDiceRollTotalValue > 7 )
            {
                return;
            }
            else
            {
                gm.UI.DisplayUseChapel( p );
            }
        }

        private void HandleFarms( Player p )
        {
            if( p.HasBuilding( gm.Buildings.GetBuilding( "Farms" ) ) )
            {
                gm.UI.DisplayFarmBonus( p );
                p.AddDie();
            }
        }

        private void HandleMerchantsGuild( Player p )
        {
            if( p.HasBuilding( gm.Buildings.GetBuilding( "Merchants' Guild" ) ) )
            {
                gm.UI.DisplayMerchantsGuildBonus( p );
                p.Gold++;
            }
        }

        private void HandleTownHall( Player p )
        {
            if( p.HasBuilding( gm.Buildings.GetBuilding( "Town Hall" ) ) )
            {
                GoodsChoiceOptions choice = gm.UI.DisplayGetTownHallChoice( p );

                if( choice == GoodsChoiceOptions.None ) // player chose not to use their town hall
                {
                    return;
                }
                else if( choice == GoodsChoiceOptions.PlusTwoToken ) // player chose a plus two token
                {
                    p.PlusTwoTokens--;
                    p.BoardVictoryPoints++;
                }
                else
                {
                    p.RemoveGood( choice );
                    p.BoardVictoryPoints++;
                }
            }
        }

        private void HandleEmbassy( Player p )
        {
            if( p.HasBuilding( gm.Buildings.GetBuilding( "Embassy" ) ) )
            {
                gm.UI.DisplayEmbassyBonus( p );
                p.BoardVictoryPoints++;
            }
        }

        private bool HandleCrane( Player p, Building built )
        {
            if( p.HasBuilding( gm.Buildings.GetBuilding( "Crane" ) ) )
            {
                if( built.Column == 3 || built.Column == 4 )
                {
                    gm.UI.DisplayUseCrane( p );
                    return true;
                }
            }
            return false;
        }

        private void HandleMarket( Player p )
        {
            if( p.HasBuilding( gm.Buildings.GetBuilding( "Market" ) ) )
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
        GameManager gm;

        public Phase3( GameManager gm )
        {
            this.gm = gm;
            this.Title = "Phase 3 - The King's Reward";
            this.Description = "The player with the most buildings receives 1 Victory Point. If there is a tie, every tied player receives 1 Victory Point.";
        }

        public override Phase Execute()
        {
            PlayerCollection players = gm.PlayersWithHighestBuildingCount( gm.AllPlayers );
            gm.UI.DisplayKingsReward( players );
            foreach( Player p in players )
            {
                p.BoardVictoryPoints++;
            }

            return new Phase4( gm );
        }
    }

    /// <summary>
    /// Phase 4 is the same as phase 2.
    /// </summary>
    public class Phase4 : Phase2
    {
        public Phase4( GameManager gm )
            : base( gm )
        {
            this.Title = "Phase 4 - Summer";
        }

        public override Phase Execute()
        {
            // Do all of phase 2's actions, then finish it up by calculating the tokens for players with inns
            base.Execute();

            foreach( Player p in gm.AllPlayers )
            {
                if( p.HasBuilding( gm.GetBuilding( "Inn" ) ) )
                {
                    gm.UI.DisplayInnReward( p );
                    p.PlusTwoTokens++;
                }
            }

            return new Phase5( gm );
        }
    }

    public class Phase5 : Phase
    {
        GameManager gm;

        public Phase5( GameManager gm )
        {
            this.gm = gm;
            this.Title = "Phase 5 - The King's Envoy";
            this.Description = "The player with the fewest buildings receives help from the King's Envoy. If there is a tie, the player with the least number of goods. If that is also a tie, nobody receives help.";
        }

        public override Phase Execute()
        {
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

            gm.UI.DisplayKingsEnvoy( PlayerReceivingEnvoy );
            return new Phase6( gm );
        }

        /// <summary>
        /// Remove the envoy from all players.
        /// </summary>
        public void RemoveEnvoyFromPlayers()
        {
            foreach( Player p in gm.AllPlayers )
            {
                p.Envoy = false;
            }
        }
    }

    public class Phase6 : Phase2
    {
        public Phase6( GameManager gm )
            : base( gm )
        {
            this.Title = "Phase 6 - Autumn";
        }

        public override Phase Execute()
        {
            base.Execute();
            return new Phase7( gm );
        }
    }

    public class Phase7 : Phase
    {
        GameManager gm;

        public Phase7( GameManager gm )
        {
            this.gm = gm;
            this.Title = "Phase 7 - Recruit Soldiers";
            this.Description = "Each player may pay goods to hire additional soldiers.";
        }

        public override Phase Execute()
        {
            foreach( Player p in gm.AllPlayers )
            {
                int numRecruited = gm.UI.DisplayRecruitSoldiers( p );
                p.Soldiers += numRecruited;
            }

            return new Phase8( gm );
        }
    }

    public class Phase8 : Phase
    {
        GameManager gm;

        public Phase8( GameManager gm )
        {
            this.gm = gm;
            this.Title = "Phase 8 - Winter - The Battle";
            this.Description = "Enemies are attacking! All players must defeat the invaders!";
        }

        public override Phase Execute()
        {
            gm.UI.DisplayBattleInfo();
            Enemy e= gm.EnemiesForGame[gm.CurrentYear - 1];

            int reinforcements = GetKingsReinforcements();
            gm.UI.DisplayKingsReinforcements( reinforcements );

            foreach( Player p in gm.AllPlayers )
            {
                DoPlayerBattle( p, e, reinforcements );
            }

            foreach( Player p in gm.PlayersWithHighestStrength( gm.AllPlayers ) )
            {
                if( p.WasVictorious )
                {
                    p.BoardVictoryPoints++;
                    gm.UI.DisplayMostGloriousVictory( p );
                }
            }

            // Handle the Fortress special ability
            foreach( Player p in gm.AllPlayers )
            {
                if( p.HasBuilding( gm.GetBuilding( "Fortress" ) ) )
                {
                    p.BoardVictoryPoints++;
                    gm.UI.DisplayFortressBonus( p );
                }
            }

            // Reset stuff for the next year.
            foreach( Player p in gm.AllPlayers )
            {
                p.Soldiers = 0;
                p.WasVictorious = false;
            }

            if( gm.CurrentYear == 5 )
            {
                gm.IsGameOver = true;
                return new EndPhase( gm );
            }
            else
            {
                return new Phase1( gm );
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
                gm.UI.DisplayBattleResults( player, enemy, BattleResults.Victory );
                this.AwardPlayerAfterBattle( player, enemy );
            }
            else if( totalStrength == enemy.Strength ) // player ties
            {
                // Check if the player has the stone wall, which allows a tie to count as a victory
                if( player.HasBuilding( gm.GetBuilding( "Stone Wall" ) ) )
                {
                    this.AwardPlayerAfterBattle( player, enemy );
                    gm.UI.DisplayBattleResults( player, enemy, BattleResults.Victory );
                }
                else
                {
                    gm.UI.DisplayBattleResults( player, enemy, BattleResults.Tie );
                }
            }
            else // player loses
            {
                PenalizePlayerAfterBattle( player, enemy );
                gm.UI.DisplayBattleResults( player, enemy, BattleResults.Loss );
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
            player.BoardVictoryPoints += enemy.VictoryPointReward;

            if( enemy.GoodReward > 0 )
            {
                player.AddGood( gm.UI.DisplayChooseAGood( player, GoodsChoiceOptions.Gold, GoodsChoiceOptions.Wood, GoodsChoiceOptions.Stone ) );
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
            player.BoardVictoryPoints -= enemy.VictoryPointPenalty;

            if( enemy.GoodPenalty >= player.GoodsCount )
            {
                player.RemoveAllGoods();
            }
            else
            {
                player.RemoveGood( gm.UI.DisplayChooseAGood( player, player.GoodTypesPlayerHas.ToArray() ) );
            }

            player.DestroyBuildings( enemy.BuildingPenalty );
            player.WasVictorious = false;
        }
    }

    public class EndPhase : Phase
    {
        GameManager gm;

        public EndPhase( GameManager gm )
        {
            this.gm = gm;
            this.Title = "Game Over - Final Scoring";
            this.Description = "The game has come to an end! Commence the final scoring!";
        }

        public override Phase Execute()
        {
            foreach( Player p in gm.AllPlayers )
            {
                HandleCathedral( p );
            }

            throw new NotImplementedException();
        }

        private void HandleCathedral( Player p )
        {
            int VPEarned = p.GoodsCount / 2;
            gm.UI.DisplayCathedralBonus( p, VPEarned );
            p.BoardVictoryPoints += VPEarned;
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
