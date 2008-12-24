//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using TylerButler.Kingsburg.Core;
//using TylerButler.GameToolkit;

//namespace KingsburgXNA
//{
//    public class XNAPhase1 : Phase1
//    {
//        public override Phase Execute()
//        {
//            GameManager k = GameManager.Instance;
//            k.CurrentYear++;
//            //TODO: Pop up phase info message
//            GameManager.Instance.UI.DisplayYearInfo();
//            PlayerCollection LeastBuildingPlayers = k.PlayersWithLowestBuildingCount( k.AllPlayers );
//            if( LeastBuildingPlayers.Count == 1 )
//            {
//                //LeastBuildingPlayers[0].KingsAidDie = 
//                GameManager.Instance.UI.DisplayKingsAid( LeastBuildingPlayers[0] );
//                LeastBuildingPlayers[0].AddDie();
//            }
//            else
//            {
//                PlayerCollection LeastGoodsPlayers = k.PlayersWithLowestGoodsCount( LeastBuildingPlayers );
//                if( LeastGoodsPlayers.Count == 1 )
//                {
//                    //LeastBuildingPlayers[0].KingsAidDie = 
//                    GameManager.Instance.UI.DisplayKingsAid( LeastGoodsPlayers[0] );
//                    LeastGoodsPlayers[0].AddDie();
//                }
//                else
//                {
//                    foreach( Player p in LeastGoodsPlayers )
//                    {
//                        GoodsChoiceOptions choice = GameManager.Instance.UI.DisplayChooseAGood( p, GoodsChoiceOptions.Gold,
//                            GoodsChoiceOptions.Wood, GoodsChoiceOptions.Stone );
//                        p.AddGood( choice );
//                    }
//                }
//            }

//            return new Phase2();

            
//            return base.Execute();
//        }
//    }
//}
