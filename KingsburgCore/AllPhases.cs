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
            gm.ConstructBuildings();

            // Phase is complete, reset the advisors
            gm.ClearInfluencedAdvisors();

            return new Phase3();
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
            // Do all of phase 2's actions, then finish it up by calculateing the tokens for players with inns
            base.Execute();

            foreach( Player p in GameManager.Instance.AllPlayers )
            {
                if( p.HasBuilding( GameManager.Instance.Buildings.GetBuilding( "Inn" ) ) )
                {
                    UIManager.Instance.DisplayInnReward( p );
                    p.PlusTwoTokens++;
                }
            }

            //return new Phase5();

            throw new NotImplementedException();
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
            }
            else
            {
                PlayerCollection LeastGoodsPlayers = gm.PlayersWithLowestGoodsCount( LeastBuildingsPlayers );
                if( LeastGoodsPlayers.Count == 1 )
                {
                    PlayerReceivingEnvoy = LeastGoodsPlayers[0];
                }
                else
                {
                    PlayerReceivingEnvoy = null;
                }
            }

            UIManager.Instance.DisplayKingsEnvoy( PlayerReceivingEnvoy );

            throw new NotImplementedException();
        }
    }
}
