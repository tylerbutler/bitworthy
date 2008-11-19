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
                        chosenAdvisor = GameManager.Instance.Advisors[int.Parse( choice )-1];
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
            //Displays info about the Advisor that is being influenced
            returnData = new List<object>();
            switch( this.Mode )
            {
                case graphicsMode.CLI:
                    switch( a.AdvisorNameEnum )
                    {
                        case Advisors.Jester:
                            Console.WriteLine( "{0} influences the Jester, and receives 1 Victory Point.", p.Name );
                            Console.ReadLine();
                            break;
                        case Advisors.Squire:
                            Console.WriteLine( "{0} influences the Squire and receives 1 Gold.", p.Name );
                            Console.ReadLine();
                            break;
                        case Advisors.Architect:
                            Console.WriteLine( "{0} influences the Architect and receives 1 Wood.", p.Name );
                            Console.ReadLine();
                            break;
                        case Advisors.Merchant:
                            Console.WriteLine( "{0} influences the Merchant and receives 1 Wood OR 1 Gold.", p.Name );
                            GoodsChoiceOptions choice = DisplayChooseAGood( p, GoodsChoiceOptions.Wood, GoodsChoiceOptions.Gold );
                            returnData.Add( choice );
                            Console.WriteLine( "{0} chose {1}.", choice );
                            Console.ReadLine();
                            break;
                        case Advisors.Sergeant:
                            Console.WriteLine( "{0} influences the Sergeant and recruits 1 Soldier." );
                            Console.WriteLine();
                            break;
                    }
                    break;
            }
        }

        internal void DisplayKingsEnvoy( Player p )
        {
            //Displays a report that the kings envoy was rewarded to a specific player
        }

        internal void DisplaySoldierRecruitment( Player p )
        {
            //Displays the soldier recruitment UI for a given player
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
                        dbg =  b & c;
                    }
                    while( dbg );
                    break;
            }
            return toReturn;
        }

        #endregion

    }
}
