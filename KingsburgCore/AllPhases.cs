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
            DiceAllocationManager dam = DiceAllocationManager.Instance;
            //LoadAdvisors();
            foreach( Player p in gm.AllPlayers )
            {
                p.RollDice();
                UIManager.Instance.DisplayDiceRoll( p );
            }
            gm.DeterminePlayerOrder();
            while( dam.PlayersHaveDiceToAllocate() )
            {
                foreach( Player p in gm.AllPlayers )
                {
                    if( !p.HasUsedAllDice )
                    {
                        Advisor influenced = UIManager.Instance.DisplayAllocateDice(p);
                        
                        // do we need this line? 
                        //p.InfluencedAdvisors.Add( influenced );
                    }
                }
            }

            gm.InfluenceAdvisors();
            gm.ConstructBuildings();


            /*All players roll dice, influence advisors, receive help and construct buildings.

Pop up phase info message
foreach Player in Game
    UI.DisplayDiceRoll(Player.RollDice())
DeterminePlayerOrder()
while Players have dice to allocate
    for each player in the player order
        allocate dice or pass
InfluenceAdvisors()
ConstructBuildings()
    UI.DisplayBuildingCard(Player, canBuild=true)
GO TO PHASE 3*/
            throw new NotImplementedException();
        }

        //private void LoadAdvisors()
        //{
        //    foreach( Advisor a in GameManager.Instance.Advisors )
        //    {
        //        AvailableAdvisors.Add( a, null );
        //    }
        //}

        //private List<Advisor> TakenAdvisors()
        //{
        //    List<Advisor> toReturn = new List<Advisor>();
        //    foreach( Advisor a in this.AvailableAdvisors.Keys )
        //    {
        //        if( AvailableAdvisors[a] != null )
        //        {
        //            toReturn.Add( a );
        //        }
        //    }
        //    return toReturn;
        //}
    }
}
