using System;
using TylerButler.GameToolkit;
using TylerButler.Kingsburg.Core.UI;

namespace TylerButler.Kingsburg.Core
{
    internal class StartPhase : Phase
    {
        public StartPhase()
        {
            this.Title = "START GAME - Choose Players";
            this.Description = "Choose the players to play the game.";
        }

        public override Phase Execute()
        {
            // Add Players
            GameManager.Instance.AllPlayers = UIManager.Instance.DisplayGetPlayers();
            return new Phase1();
        }
    }

    internal class Phase1 : Phase
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
            UIManager.Instance.DisplayYearInfo();
            PlayerCollection LeastBuildingPlayers = k.PlayersWithLowestBuildingCount( k.AllPlayers );
            if( LeastBuildingPlayers.Count == 1 )
            {
                LeastBuildingPlayers[0].AddDie();
            }
            else
            {
                PlayerCollection LeastGoodsPlayers = k.PlayersWithLowestGoodsCount( LeastBuildingPlayers );
                if( LeastGoodsPlayers.Count == 1 )
                {
                    LeastGoodsPlayers[0].AddDie();
                }
                else
                {
                    foreach( Player p in LeastGoodsPlayers )
                    {
                        GoodsChoiceOptions choice = UIManager.Instance.DisplayChooseAGood( p, GoodsChoiceOptions.Gold,
                            GoodsChoiceOptions.Wood, GoodsChoiceOptions.Stone );
                        p.AddGood( choice );
                    }
                }
            }

            return new Phase2();
        }
    }

    internal class Phase2 : Phase
    {
        public Phase2()
        {
            this.Title = "Phase 2 - Spring";
            this.Description = "All players roll dice, influence advisors, receive help and construct buildings.";
        }

        public override Phase Execute()
        {
            GameManager gm = GameManager.Instance;

            // Reset advisor state just in case
            gm.ClearInfluencedAdvisors();

            foreach( Player p in gm.AllPlayers )
            {
                p.RollDice();
                UIManager.Instance.DisplayDiceRoll( p );
            }
            gm.DeterminePlayerOrder();
            while( DiceAllocationManager.Instance.PlayersHaveDiceToAllocate() )
            {
                foreach( Player p in gm.AllPlayers )
                {
                    if( !p.HasUsedAllDice )
                    {
                        Advisor influenced = UIManager.Instance.DisplayChooseAdvisorToInfluence( p );
                        if( influenced != null ) // player has passed if influenced==null
                        {
                            influenced.InfluencingPlayers.Add( p );
                            DiceCollection spent = UIManager.Instance.DisplayChooseDice( p, influenced );
                            p.AllocateDice( spent );
                        }

                        // do we need this line? 
                        //p.InfluencedAdvisors.Add( influenced );
                    }
                }
            }

            gm.InfluenceAdvisors();
            ConstructBuildings();

            // Phase is complete, reset the advisors
            gm.ClearInfluencedAdvisors();

            return new Phase3();
        }

        internal void ConstructBuildings()
        {
            foreach( Player p in GameManager.Instance.AllPlayers )
            {
                Building built = UIManager.Instance.DisplayBuildingCard( p, true /* can build */);
                if( built != null )
                {
                    p.Buildings.Add( built );
                    p.Goods["Gold"] -= built.GoldCost;
                    p.Goods["Wood"] -= built.WoodCost;
                    p.Goods["Stone"] -= built.StoneCost;
                }
            }
        }
    }

    internal class Phase3 : Phase
    {
        internal Phase3()
        {
            this.Title = "Phase 3 - The King's Reward";
            this.Description = "The player with the most buildings receives 1 Victory Point. If there is a tie, every tied player receives 1 Victory Point.";
        }

        public override Phase Execute()
        {
            /*
             * PHASE 3 - The King's Reward
                The Player with the most buildings receives 1 Victory Point. If there is a tie, every tied player receives 1 Victory Point.

                highPlayers = GetPlayersWithHighestBuildingCount( Game.Players ) returns ArrayList of Players
                foreach Player in highPlayers
                    Player.VictoryPoints++;
                UI.DisplayKingsReward( highPlayers )
                GO TO PHASE 4
             */

            GameManager gm = GameManager.Instance;
            PlayerCollection players = gm.PlayersWithHighestBuildingCount( gm.AllPlayers );
            UIManager.Instance.DisplayKingsReward( players );
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
    internal class Phase4 : Phase2
    {
        internal Phase4()
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
                    UIManager.Instance.DisplayInnReward( p );
                    p.PlusTwoTokens++;
                }
            }

            return new Phase5();
        }
    }

    internal class Phase5 : Phase
    {
        internal Phase5()
        {
            this.Title = "Phase 3 - The King's Envoy";
            this.Description = "The player with the fewest buildings receives help from the King's Envoy. If there is a tie, the player with the least number of goods. If that is also a tie, nobody receives help.";
        }

        public override Phase Execute()
        {
            /*
             * PHASE 5 - The King's Envoy
                The Player with the fewest buildings receives help from the King's Envoy. If there is a tie, the player with the least number of goods. If that is also a tie, nobody receives help.

                Pop up phase info message
                RemoveEnvoyFromPlayers()
                lowPlayers = GetPlayersWithLowestBuildingCount( Game.Players ) returns ArrayList of Players
                if LeastBuildingPlayers.Count = 1
                    LeastBuildingPlayers[0].Envoy = true
                else
                    LeastGoodsPlayers = GetPlayersWithLowestGoodsCount(LeastBuildingPlayers) returns ArrayList of Players
                    if LeastGoodsPlayers.Count = 1
                        LeastGoodsPlayers[0].Envoy = true
                    else // nothing
                UI.DisplayKingsEnvoy( Player p )
                GO TO PHASE 6
             * */
            GameManager gm = GameManager.Instance;
            gm.ClearEnvoyFromAllPlayers();
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

            UIManager.Instance.DisplayKingsEnvoy( PlayerReceivingEnvoy );
            return new Phase6();
        }
    }

    internal class Phase6 : Phase2
    {
        public Phase6()
            : base()
        {
            this.Title = "Phase 6 - Autumn";
        }

        // Don't need to implement the execute method since it's exactly the same as in phase 2
        //public override Phase Execute()
        //{
        //    return base.Execute();
        //}
    }

    internal class Phase7 : Phase
    {
        internal Phase7()
        {
            this.Title = "Phase 7 - Recruit Soldiers";
            this.Description = "Each player may pay goods to hire additional soldiers.";
        }

        public override Phase Execute()
        {
            foreach( Player p in GameManager.Instance.AllPlayers )
            {
                int numRecruited = UIManager.Instance.DisplayRecruitSoldiers( p );
                p.Soldiers += numRecruited;
            }

            return new Phase8();
        }
    }

    internal class Phase8 : Phase
    {
        internal Phase8()
        {
            this.Title = "Phase 8 - Winter - The Battle";
            this.Description = "Enemies are attacking! All players must defeat the invaders!";
        }

        public override Phase Execute()
        {
            UIManager.Instance.DisplayBattleInfo();
            Enemy e= GameManager.Instance.Enemies[GameManager.Instance.CurrentYear - 1];

            int reinforcements = GetKingsReinforcements();
            UIManager.Instance.DisplayKingsReinforcements( reinforcements );

            foreach( Player p in GameManager.Instance.AllPlayers )
            {
                DoPlayerBattle( p, e );
            }

            foreach( Player p in GameManager.Instance.PlayersWithHighestStrength( GameManager.Instance.AllPlayers ) )
            {
                if( p.WasVictorious )
                {
                    p.VictoryPoints++;
                    UIManager.Instance.DisplayMostGloriousVictory( p );
                }
            }

            // Handle the Fortress special ability
            foreach( Player p in GameManager.Instance.AllPlayers )
            {
                if( p.HasBuilding( GameManager.Instance.GetBuilding( "Fortress" ) ) )
                {
                    p.VictoryPoints++;
                    UIManager.Instance.DisplayFortressBonus( p );
                }
            }

            // Reset stuff for the next year.
            foreach( Player p in GameManager.Instance.AllPlayers )
            {
                p.Soldiers = 0;
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
        internal int GetKingsReinforcements()
        {
            return RandomNumber.GetRandom( 1, 6 );
        }

        /// <summary>
        /// Execute a battle between the player and an enemy.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="enemy"></param>
        internal void DoPlayerBattle( Player player, Enemy enemy )
        {
            int totalStrength = player.TotalStrength + player.BonusStrengthAgainstEnemy( enemy );
            if( totalStrength > enemy.Strength ) // player wins
            {
                UIManager.Instance.DisplayBattleResults( player, enemy, BattleResults.Victory );
                this.AwardPlayerAfterBattle( player, enemy );
            }
            else if( totalStrength == enemy.Strength ) // player ties
            {
                // Check if the player has the stone wall, which allows a tie to count as a victory
                if( player.HasBuilding( GameManager.Instance.GetBuilding( "Stone Wall" ) ) )
                {
                    UIManager.Instance.DisplayBattleResults( player, enemy, BattleResults.Victory );
                    this.AwardPlayerAfterBattle( player, enemy );
                }
                else
                {
                    UIManager.Instance.DisplayBattleResults( player, enemy, BattleResults.Tie );
                }
            }
            else // player loses
            {
                UIManager.Instance.DisplayBattleResults( player, enemy, BattleResults.Loss );
            }
        }

        /// <summary>
        /// Awards the player after a victory against the enemy.
        /// </summary>
        /// <param name="player">The player</param>
        /// <param name="enemy">The enemy</param>
        internal void AwardPlayerAfterBattle( Player player, Enemy enemy )
        {
            player.Goods["Gold"] += enemy.GoldReward;
            player.Goods["Wood"] += enemy.WoodReward;
            player.Goods["Stone"] += enemy.StoneReward;
            player.VictoryPoints += enemy.VictoryPointReward;

            if( enemy.GoodReward > 0 )
            {
                player.AddGood( UIManager.Instance.DisplayChooseAGood( player, GoodsChoiceOptions.Gold, GoodsChoiceOptions.Wood, GoodsChoiceOptions.Stone ) );
            }

            player.WasVictorious = true;
        }

        /// <summary>
        /// Penalizes the player after a loss to the enemy.
        /// </summary>
        /// <param name="player">The player</param>
        /// <param name="enemy">The enemy</param>
        internal void PenalizePlayerAfterBattle( Player player, Enemy enemy )
        {
            //Bug:19
            player.Goods["Gold"] -= enemy.GoldPenalty;
            player.Goods["Wood"] -= enemy.WoodPenalty;
            player.Goods["Stone"] -= enemy.StonePenalty;
            player.VictoryPoints -= enemy.VictoryPointPenalty;

            if( enemy.GoodPenalty >= player.GoodsCount )
            {
                player.RemoveAllGoods();
            }
            else
            {
                player.RemoveGood( UIManager.Instance.DisplayChooseAGood( player, player.GoodTypesPlayerHas.ToArray() ) );
            }

            player.DestroyBuildings( enemy.BuildingPenalty );
            player.WasVictorious = false;
        }
    }

    internal class EndPhase : Phase
    {
        public override Phase Execute()
        {
            throw new NotImplementedException();
        }
    }

    internal enum BattleResults
    {
        Victory,
        Tie,
        Loss,
    }
}
