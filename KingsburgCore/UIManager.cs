using System;
using System.Collections.Generic;
using TylerButler.GameToolkit;

namespace TylerButler.Kingsburg.Core.UI
{
    public sealed class UIManager
    {
        private readonly static UIManager instance = new UIManager( Properties.Settings.Default.UIMode );
        private graphicsMode mode;
        private enum graphicsMode
        {
            CLI,
            GUI,
        }

        static UIManager()
        {
            // Explicit static constructor to tell C# compiler
            // not to mark type as beforefieldinit
        }

        UIManager()
        {
        }

        UIManager( string uiMode )
        {
            if( uiMode.Equals( "CLI", StringComparison.OrdinalIgnoreCase ) )
            {
                Mode = graphicsMode.CLI;
            }
            else if( uiMode.Equals( "GUI", StringComparison.OrdinalIgnoreCase ) )
            {
                Mode = graphicsMode.GUI;
            }
            else
            {
                throw new Exception( "UIMode Setting invalid." );
            }
        }

        public static UIManager Instance
        {
            get
            {
                return instance;
            }
        }

        private graphicsMode Mode
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

        #region Methods
        public void DisplayDiceRoll( Player p )
        {
            DisplayDiceRoll( p, p.MostRecentDiceRoll );
        }

        public void DisplayDiceRoll( Player p, List<int> roll )
        {
            switch( this.Mode )
            {
                case graphicsMode.CLI:
                    Console.Write( "{0} rolled ", p.Name );
                    foreach( int i in roll )
                    {
                        Console.Write( i + " " );
                    }
                    Console.WriteLine();
                    break;
                case graphicsMode.GUI:
                    throw new NotImplementedException();
            }
        }

        internal Advisor DisplayAllocateDice( Player p )
        {
            // case:4
            Advisor chosenAdvisor = null;
            switch( this.Mode )
            {
                case graphicsMode.CLI:
                    AdvisorCollection CanBeInfluenced = DiceAllocationManager.Instance.InfluenceableAdvisors( p );
                    Console.WriteLine( "{0}, choose an advisor to influence.", p.Name );
                    Console.WriteLine( "You may influence:" );
                    foreach( Advisor a in CanBeInfluenced )
                    {
                        Console.WriteLine( "{0}. {1} - {2}", a.Order, a.Name, a.Description );
                    }

                    bool exitLoop = false;
                    do
                    {
                        string choice =  Console.ReadLine();
                        if( choice.Equals( "p", StringComparison.OrdinalIgnoreCase ) )
                        {
                            Console.WriteLine( "{0} passed." );
                            p.AllocateAllDice();
                        }
                        // Ticket:3 Need to add a check for this value (should be > 0 and < 18 )
                        //
                        chosenAdvisor = GameManager.Instance.Advisors[int.Parse( choice ) - 1];
                        if( CanBeInfluenced.Contains( chosenAdvisor ) )
                        {
                            exitLoop = true;
                        }
                        else
                        {
                            exitLoop = false;
                            Console.WriteLine( "Invalid Selection!" );
                        }
                    }
                    while( !exitLoop );
                    Console.WriteLine( "{0} chose to influence the {1}.", p.Name, chosenAdvisor.Name );
                    break;
            }
            return chosenAdvisor;
        }

        //private List<Advisor> InfluenceableAdvisors( Player p, List<Advisor> unavailableAdvisors )
        //{
        //    List<Advisor> toReturn = new List<Advisor>(GameManager.Instance.Advisors);
        //    foreach( Advisor a in unavailableAdvisors )
        //    {
        //        toReturn.Remove( a );
        //    }

        //    List<Advisor>copy = new List<Advisor>(toReturn);
        //    foreach( Advisor a in copy )
        //    {
        //        if( !p.Sums.Contains( a.Order ) )
        //        {
        //            toReturn.Remove( a );
        //        }
        //    }

        //    return toReturn;
        //}

        //private List<Advisor> InfluenceableAdvisors( Player p )
        //{
        //    return InfluenceableAdvisors( p, new List<Advisor>() );
        //}

        internal void DisplayPlayerOrder( PlayerCollection order )
        {
            switch( this.Mode )
            {
                case graphicsMode.CLI:
                    Console.Write( "Turn order is: " );
                    foreach( Player p in order )
                    {
                        Console.Write( p.Name + ", " );
                    }
                    Console.WriteLine();
                    break;
            }
        }

        internal void DisplayBuildingCard( Player p, bool canBuild )
        {
            //displays the building card for a given player. The boolean argument specifies whether or not the player can build a building. When false, the player can only see what he's built.
            throw new NotImplementedException();
        }

        internal void DisplayKingsReward( PlayerCollection players )
        {
            //Displays the confirmation that the players in the playerlist have received a 1VP bonus.
        }

        internal void DisplayPhaseInfo( Phase phase )
        {
            //Displays info about a phase that is about to start.
            switch( this.Mode )
            {
                case graphicsMode.CLI:
                    Console.WriteLine( "+++++++++++++++++++++++++++++++++++" );
                    Console.WriteLine( phase.Title + "\n" );
                    Console.WriteLine( phase.Description );
                    Console.WriteLine( "+++++++++++++++++++++++++++++++++++" + "\n" );
                    break;
            }
        }

        public void DisplayInfluenceAdvisor( Advisor a, Player p )
        {
            List<object> dataWillBeDiscarded;
            DisplayInfluenceAdvisor( a, p, out dataWillBeDiscarded );
        }

        public void DisplayInfluenceAdvisor( Advisor a, Player p, out List<object> returnData )
        {
            // Displays info about the Advisor that is being influenced
            returnData = new List<object>();
            GoodsChoiceOptions choice;

            switch( this.Mode )
            {
                case graphicsMode.CLI:
                    switch( a.AdvisorNameEnum )
                    {
                        case Advisors.Jester:
                            Console.WriteLine( "{0} influences the Jester, and receives 1 Victory Point.", p.Name );
                            break;
                        case Advisors.Squire:
                            Console.WriteLine( "{0} influences the Squire and receives 1 Gold.", p.Name );
                            break;
                        case Advisors.Architect:
                            Console.WriteLine( "{0} influences the Architect and receives 1 Wood.", p.Name );
                            break;
                        case Advisors.Merchant:
                            Console.WriteLine( "{0} influences the Merchant and receives 1 Wood OR 1 Gold.", p.Name );
                            choice = DisplayChooseAGood( p, GoodsChoiceOptions.Wood, GoodsChoiceOptions.Gold );
                            returnData.Add( choice );
                            break;
                        case Advisors.Sergeant:
                            Console.WriteLine( "{0} influences the Sergeant and recruits 1 Soldier.", p.Name );
                            break;
                        case Advisors.Alchemist:
                            Console.WriteLine( "{0} influences the Alchemist and can transmute 1 good.", p.Name );
                            GoodsChoiceOptions[] availableGoods = new GoodsChoiceOptions[3];
                            availableGoods[0] = p.Goods["Gold"] > 0 ? GoodsChoiceOptions.Gold : GoodsChoiceOptions.None;
                            availableGoods[1] = p.Goods["Wood"] > 0 ? GoodsChoiceOptions.Wood : GoodsChoiceOptions.None;
                            availableGoods[2] = p.Goods["Stone"] > 0 ? GoodsChoiceOptions.Stone : GoodsChoiceOptions.None;
                            choice = DisplayChooseAGood( p, availableGoods );
                            returnData.Add( choice );
                            break;
                        case Advisors.Astronomer:
                            Console.WriteLine( "{0} influences the Astronomer and receives 1 good of choice and a \"+2\" token.", p.Name );
                            choice = DisplayChooseAGood( p, GoodsChoiceOptions.Gold, GoodsChoiceOptions.Wood, GoodsChoiceOptions.Stone );
                            returnData.Add( choice );
                            break;
                        case Advisors.Treasurer:
                            Console.WriteLine( "{0} influences the Treasurer and receives 2 Gold.", p.Name );
                            break;
                        case Advisors.MasterHunter:
                            Console.WriteLine( "{0} influences the Master Hunter and receives either 1 Wood and 1 Stone, or 1 Wood and 1 Gold.", p.Name );
                            choice = DisplayChooseAGood( p, GoodsChoiceOptions.WoodAndStone, GoodsChoiceOptions.GoldAndWood );
                            returnData.Add( choice );
                            break;
                        case Advisors.General:
                            Console.WriteLine( "{0} influences the General and recruits 2 soldiers and may spy on the enemy.", p.Name );
                            UIManager.Instance.DisplayPeekAtEnemy( p );
                            break;
                        case Advisors.Swordsmith:
                            Console.WriteLine( "{0} influences the Swordsmith and receives either 1 Stone and 1 Wood, or 1 Stone and 1 Gold.", p.Name );
                            choice = DisplayChooseAGood( p, GoodsChoiceOptions.WoodAndStone, GoodsChoiceOptions.GoldAndStone );
                            returnData.Add( choice );
                            break;
                        case Advisors.Duchess:
                            Console.WriteLine( "{0} influences the Duchess and receives 2 goods of choice and a \"+2\" token.", p.Name );
                            choice = DisplayChooseAGood( p, GoodsChoiceOptions.Gold, GoodsChoiceOptions.Wood, GoodsChoiceOptions.Stone );
                            returnData.Add( choice );
                            choice = DisplayChooseAGood( p, GoodsChoiceOptions.Gold, GoodsChoiceOptions.Wood, GoodsChoiceOptions.Stone );
                            returnData.Add( choice );
                            break;
                        case Advisors.Champion:
                            Console.WriteLine( "{0} influences the Champion and receives 3 Stone.", p.Name );
                            break;
                        case Advisors.Smuggler:
                            Console.WriteLine( "{0} influences the Smuggler and pays 1 Victory Point to receive 3 goods of choice.", p.Name );
                            choice = DisplayChooseAGood( p, GoodsChoiceOptions.Gold, GoodsChoiceOptions.Wood, GoodsChoiceOptions.Stone );
                            returnData.Add( choice );
                            choice = DisplayChooseAGood( p, GoodsChoiceOptions.Gold, GoodsChoiceOptions.Wood, GoodsChoiceOptions.Stone );
                            returnData.Add( choice );
                            choice = DisplayChooseAGood( p, GoodsChoiceOptions.Gold, GoodsChoiceOptions.Wood, GoodsChoiceOptions.Stone );
                            returnData.Add( choice );
                            break;
                        case Advisors.Inventor:
                            Console.WriteLine( "{0} influences the Inventor and receives 1 Gold, 1 Wood, and 1 Stone.", p.Name );
                            break;
                        case Advisors.Wizard:
                            Console.WriteLine( "{0} influences the Wizard and receives 4 Gold.", p.Name );
                            break;
                        case Advisors.Queen:
                            Console.WriteLine( "{0} influences the Queen and receives 2 goods of choice, 3 Victory Points, and may spy on the enemy.", p.Name );
                            choice = DisplayChooseAGood( p, GoodsChoiceOptions.Gold, GoodsChoiceOptions.Wood, GoodsChoiceOptions.Stone );
                            returnData.Add( choice );
                            choice = DisplayChooseAGood( p, GoodsChoiceOptions.Gold, GoodsChoiceOptions.Wood, GoodsChoiceOptions.Stone );
                            returnData.Add( choice );
                            this.DisplayPeekAtEnemy( p );
                            break;
                        case Advisors.King:
                            Console.WriteLine( "{0} influences the King and receives 1 Gold, 1 Wood, and 1 Stone, and recruits 1 Soldier.", p.Name );
                            break;
                        default:
                            throw new Exception( "Something went wrong when running the DisplayInfluenceAdvisor method. Advisor={0}, Player={1}" );
                    }
                    break;
            }
        }

        private void DisplayPeekAtEnemy( Player p )
        {
            switch( this.Mode )
            {
                case graphicsMode.CLI:
                    Console.WriteLine( "{0} may now spy on the enemy. All other players should look away. Press any key when ready.", p.Name );
                    Console.ReadLine();
                    Enemy e = GameManager.Instance.EnemiesForGame[GameManager.Instance.CurrentYear - 1];
                    Console.WriteLine( "Enemy Name: {0}", e.Name );
                    Console.WriteLine( "Strength: {0}", e.Strength );
                    Console.WriteLine( "Penalties:" );
                    Console.WriteLine( "Goods of choice: {0}, Gold: {1}, Wood: {2}, Stone: {3}, VP: {4}, Buildings: {5}", e.GoodPenalty, e.GoldPenalty, e.WoodPenalty, e.StonePenalty, e.VictoryPointPenalty, e.BuildingPenalty );
                    Console.WriteLine( "Rewards:" );
                    Console.WriteLine( "Gold: {0}, Wood: {1}, Stone: {2}, VP: {3}", e.GoldReward, e.WoodReward, e.StoneReward, e.VictoryPointReward );
                    Console.WriteLine( "\nPress any key to continue." );
                    Console.ReadLine();
                    break;
                case graphicsMode.GUI:
                    throw new NotImplementedException();
            }
        }

        internal void DisplayKingsEnvoy( Player p )
        {
            //Displays a report that the kings envoy was rewarded to a specific player
            throw new NotImplementedException();
        }

        internal void DisplaySoldierRecruitment( Player p )
        {
            //Displays the soldier recruitment UI for a given player
            throw new NotImplementedException();
        }

        internal GoodsChoiceOptions DisplayChooseAGood( Player p, params GoodsChoiceOptions[] available )
        {
            //pops up dialog to select a good
            GoodsChoiceOptions toReturn = GoodsChoiceOptions.None;
            List<GoodsChoiceOptions> validOptions = new List<GoodsChoiceOptions>( available );
            switch( this.Mode )
            {
                case graphicsMode.CLI:
                    Console.WriteLine( "{0}, please choose a good.", p.Name );
                    foreach( GoodsChoiceOptions goodChoice in validOptions )
                    {
                        Console.WriteLine( "{0}, enter {1}", goodChoice, (int)goodChoice );
                    }

                    bool validSelection = false;
                    do
                    {
                        string choice = Console.ReadLine();
                        if( Enum.IsDefined( typeof( GoodsChoiceOptions ), int.Parse( choice ) ) )
                        {
                            toReturn = (GoodsChoiceOptions)Enum.Parse( typeof( GoodsChoiceOptions ), choice );
                            if( validOptions.Contains( toReturn ) )
                            {
                                validSelection = true;
                            }
                            else
                            {
                                Console.WriteLine( "Invalid selection!" );
                            }
                        }
                        else
                        {
                            Console.WriteLine( "Invalid selection!" );
                        }
                    }
                    while( !validSelection );
                    Console.WriteLine( "{0} chose {1}.", p.Name, toReturn );
                    Console.WriteLine();
                    break;
            }
            return toReturn;
        }

        internal PlayerCollection DisplayGetPlayers()
        {
            PlayerCollection toReturn = new PlayerCollection();

            switch( this.Mode )
            {
                case graphicsMode.CLI:
                    bool dbg = true;
                    string data = String.Empty;
                    string cancelChar = "d";
                    Console.WriteLine( "Enter the names of the players. Press <ENTER> after every player. Type '{0}' when done.", cancelChar );
                    do
                    {
                        data = Console.ReadLine();
                        if( ( data.Equals( cancelChar, StringComparison.OrdinalIgnoreCase ) || data.Equals( String.Empty ) ) && toReturn.Count == 0 )
                        {
                            Console.WriteLine( "You must enter at least 1 valid player name." );
                            data = cancelChar + "crap";
                        }
                        else if( !data.Equals( cancelChar, StringComparison.OrdinalIgnoreCase ) && !data.Equals( String.Empty ) )
                        {
                            toReturn.Add( new Player( data, "" ) );
                            Console.WriteLine( "{0} has joined the game!", data );
                            Console.WriteLine();
                        }
                        //bool a = !data.Equals( String.Empty );
                        bool b = !data.Equals( cancelChar, StringComparison.OrdinalIgnoreCase );
                        bool c = toReturn.Count <= 5;
                        dbg = b & c;
                    }
                    while( dbg );
                    break;
            }
            return toReturn;
        }

        #endregion

    }
}
