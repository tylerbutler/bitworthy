using System;
using System.Collections.Generic;
using TylerButler.GameToolkit;
using TylerButler.Kingsburg.Utilities;

namespace TylerButler.Kingsburg.Core.UI
{
    public sealed class UIManager : UIManagerBase
    {
        private readonly static UIManager instance = new UIManager( Properties.Settings.Default.UIMode );

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

        override internal void DisplayDiceRoll(Player p, DiceCollection roll)
        {
            switch( this.Mode )
            {
                case graphicsMode.CLI:
                    Console.Write( "{0} rolled ", p.Name );
                    foreach( KingsburgDie d in roll )
                    {
                        Console.Write( d.ToString() + " " );
                    }
                    Console.WriteLine();
                    break;
                case graphicsMode.GUI:
                    throw new NotImplementedException();
            }
        }

        override internal void DisplayPlayerInfo( Player p )
        {
            throw new NotImplementedException();
        }

        override internal Advisor DisplayChooseAdvisorToInfluence( Player p )
        {
            Advisor chosenAdvisor = null;
            switch( this.Mode )
            {
                case graphicsMode.CLI:
                    AdvisorCollection CanBeInfluenced = DiceAllocationManager.Instance.InfluenceableAdvisors( p );
                    Console.WriteLine( "\n{0}, choose an advisor to influence. (Enter 'p' to pass)", p.Name );
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
                            Console.WriteLine( "{0} passed.", p.Name );
                            p.AllocateAllDice();
                            return null;
                        }
                        // TODO: Add a check for this value (should be > 0 and < 18 )
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

                    //Bug:26 FIXED
                    // If the chosen advisor is already influenced, the player must be using the envoy, so we remove it
                    if( chosenAdvisor.IsInfluenced )
                    {
                        if( p.Envoy == true )
                        {
                            p.Envoy = false;
                        }
                        else
                        {
                            throw new Exception( "A player tried to influence an advisor that was already influenced, but he didn't have the envoy" );
                        }
                    }

                    Console.WriteLine( "{0} chose to influence the {1}.", p.Name, chosenAdvisor.Name );
                    break;
            }
            return chosenAdvisor;
        }

        override internal void DisplayPlayerOrder( PlayerCollection order )
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

        /// <summary>
        /// Displays the building card for a given player. The boolean argument specifies whether or not the player can build a building. When false, the player can only see what he's built.
        /// </summary>
        /// <param name="p">The player whose building card should be displayed.</param>
        /// <param name="canBuild">Whether or not the player can actually build or not.</param>
        /// <returns>The building the player chooses to build.</returns>
        override internal Building DisplayBuildingCard( Player p, bool canBuild )
        {
            Building toReturn=null;
            switch( this.Mode )
            {
                case graphicsMode.CLI:
                    foreach( Building b in GameManager.Instance.Buildings )
                    {
                        string ownedStar = p.HasBuilding( b ) ? "***" : string.Empty;
                        Console.WriteLine( "{5}\t{0}: {1} Gold, {2} Wood, {3} Stone, {4} VP", b.Name, b.GoldCost,
                            b.WoodCost, b.StoneCost, b.VictoryPointValue, ownedStar );
                    }
                    if( canBuild )
                    {
                        //bug:14
                        Console.WriteLine( "{0}, you may build the following buildings: ", p.Name );
                        foreach( Building bu in p.BuildableBuildings )
                        {
                            Console.WriteLine( "{0},{1}: {2}", bu.Row, bu.Column, bu.Name );
                        }
                        Console.WriteLine();
                        if( p.HasBuilding( GameManager.Instance.Buildings.GetBuilding( "Crane" ) ) )
                        {
                            Console.WriteLine( "You have a crane so gold prices of buildings in columns 3 and 4 are reduced by 1." );
                            Console.WriteLine();
                        }
                        Console.WriteLine( "Enter 'p' to pass." );
                        string choice = Console.ReadLine();

                        if( choice.Equals( "p", StringComparison.OrdinalIgnoreCase ) )
                        {
                            Console.WriteLine( "{0} passed without building a building.", p.Name );
                            return null;
                        }

                        string[] parsedChoice = choice.Split( ',' );
                        int chosenRow = int.Parse( parsedChoice[0] );
                        int chosenColumn = int.Parse( parsedChoice[1] );
                        toReturn = (Building)GameManager.Instance.Buildings.GetBuilding( chosenRow, chosenColumn ).Clone();
                    }
                    break;
                case graphicsMode.GUI:
                    throw new NotImplementedException();
            }
            return toReturn;
        }

        override internal void DisplayKingsReward( PlayerCollection players )
        {
            //Displays the confirmation that the players in the playerlist have received a 1VP bonus.
            switch( this.Mode )
            {
                case graphicsMode.CLI:
                    foreach( Player p in players )
                    {
                        Console.WriteLine( "{0} receives the King's reward of 1 Victory Point.", p.Name );
                    }
                    break;
                case graphicsMode.GUI:
                    throw new NotImplementedException();
            }
        }

        override internal void DisplayPhaseInfo( Phase phase )
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

        override internal void DisplayInfluenceAdvisor( Advisor a, Player p, out List<object> returnData )
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
                            availableGoods[0] = p.Gold > 0 ? GoodsChoiceOptions.Gold : GoodsChoiceOptions.None;
                            availableGoods[1] = p.Wood > 0 ? GoodsChoiceOptions.Wood : GoodsChoiceOptions.None;
                            availableGoods[2] = p.Stone > 0 ? GoodsChoiceOptions.Stone : GoodsChoiceOptions.None;
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

        override internal void DisplayPeekAtEnemy( Player p )
        {
            switch( this.Mode )
            {
                case graphicsMode.CLI:
                    Console.WriteLine( "{0} may now spy on the enemy. All other players should look away. Press any key when ready.", p.Name );
                    Console.ReadLine();
                    Enemy e = GameManager.Instance.EnemiesForGame[GameManager.Instance.CurrentYear - 1];
                    this.DisplayEnemyInfo( e );
                    Console.WriteLine( "\nPress any key to continue." );
                    Console.ReadLine();
                    break;
                case graphicsMode.GUI:
                    throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Displays information about an enemy.
        /// </summary>
        /// <param name="enemy">The enemy to display.</param>
        override internal void DisplayEnemyInfo( Enemy enemy )
        {
            switch( this.Mode )
            {
                case graphicsMode.CLI:
                    Console.WriteLine( "Enemy Name: {0}", enemy.Name );
                    Console.WriteLine( "Strength: {0}", enemy.Strength );
                    Console.WriteLine( "Penalties:" );
                    Console.WriteLine( "Goods of choice: {0}, Gold: {1}, Wood: {2}, Stone: {3}, VP: {4}, Buildings: {5}", enemy.GoodPenalty, enemy.GoldPenalty, enemy.WoodPenalty, enemy.StonePenalty, enemy.VictoryPointPenalty, enemy.BuildingPenalty );
                    Console.WriteLine( "Rewards:" );
                    Console.WriteLine( "Gold: {0}, Wood: {1}, Stone: {2}, VP: {3}\n", enemy.GoldReward, enemy.WoodReward, enemy.StoneReward, enemy.VictoryPointReward );
                    break;
                case graphicsMode.GUI:
                    throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Displays a report that the kings envoy was rewarded to a specific player
        /// </summary>
        /// <param name="p">The player receiving the envoy. If null, no one is receiving the envoy.</param>
        override internal void DisplayKingsEnvoy( Player p )
        {
            switch( this.Mode )
            {
                case graphicsMode.CLI:
                    if( p != null )
                    {
                        Console.WriteLine( "{0} receives the King's Envoy.", p.Name );
                    }
                    else
                    {
                        Console.WriteLine( "No player receives the King's Envoy." );
                    }
                    break;
                case graphicsMode.GUI:
                    throw new NotImplementedException();
            }
        }

        override internal void DisplaySoldierRecruitment( Player p )
        {
            //Displays the soldier recruitment UI for a given player
            throw new NotImplementedException();
        }

        override internal GoodsChoiceOptions DisplayChooseAGood( Player p, params GoodsChoiceOptions[] available )
        {
            //pops up dialog to select a good
            GoodsChoiceOptions toReturn = GoodsChoiceOptions.None;
            List<GoodsChoiceOptions> validOptions = new List<GoodsChoiceOptions>( available );
            validOptions.RemoveAll( delegate( GoodsChoiceOptions none )
            {
                return none.Equals( GoodsChoiceOptions.None );
            }
                );
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

        override internal PlayerCollection DisplayGetPlayers()
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

        override internal DiceCollection DisplayChooseDice( Player p, Advisor a )
        {
            switch( this.Mode )
            {
                case graphicsMode.CLI:
                    DiceCollection toReturn;
                    SumComboFinder sc = new SumComboFinder();
                    List<List<KingsburgDie>> combos = sc.Find( a.Order, p.RemainingDice );

                    // Return the only combo if there is only one
                    if( combos.Count == 1 )
                    {
                        toReturn = new DiceCollection( combos[0] );
                    }
                    else
                    {
                        Console.WriteLine( "\n{0}, pick which dice combo to use:", p.Name );
                        foreach( List<KingsburgDie> combo in combos )
                        {
                            Console.Write( "{0}: ", combos.IndexOf( combo ) );
                            foreach( KingsburgDie d in combo )
                            {
                                Console.Write( "{0}, ", d );
                            }
                            Console.WriteLine();
                        }
                        Console.WriteLine( "\"*\" indicates a white die." );
                        int chosenCombo = -1;
                        do
                        {
                            string input = Console.ReadLine();
                            chosenCombo = int.Parse( input );
                        }
                        while( chosenCombo == -1 || chosenCombo + 1 > combos.Count );
                        toReturn = new DiceCollection( combos[chosenCombo] );
                    }
                    return toReturn;
                case graphicsMode.GUI:
                    throw new NotImplementedException();
                default:
                    throw new Exception();
            }
        }

        override internal void DisplayInnReward( Player p )
        {
            switch( this.Mode )
            {
                case graphicsMode.CLI:
                    Console.WriteLine( "{0} receives a \"+2\" token from his Inn." );
                    break;
                case graphicsMode.GUI:
                    throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Displays information on the current year in the game.
        /// </summary>
        override internal void DisplayYearInfo()
        {
            switch( this.Mode )
            {
                case graphicsMode.CLI:
                    Console.WriteLine( "This is year {0} of 5.", GameManager.Instance.CurrentYear );
                    break;
                case graphicsMode.GUI:
                    throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Allows the player to recruit soldiers.
        /// </summary>
        /// <param name="p">The player recruiting soldiers.</param>
        /// <returns>The number of soldiers recruited.</returns>
        override internal int DisplayRecruitSoldiers( Player p )
        {
            int numToRecruit = 0;
            switch( this.Mode )
            {
                case graphicsMode.CLI:
                    //DisplayPlayerInfo( p );
                    Console.WriteLine( "{0}, you may recruit {1} soldiers. How many would you like to recruit?", p.Name, p.RecruitableSoldiers );
                    do
                    {
                        numToRecruit = int.Parse( Console.ReadLine() );
                    }
                    while( numToRecruit > p.RecruitableSoldiers );

                    if( numToRecruit == 0 )
                    {
                        Console.WriteLine( "{0} recruits no soldiers.", p.Name );
                        return 0;
                    }
                    else if( numToRecruit == p.RecruitableSoldiers && p.GoodsCount % p.SoldierCost == 0 )
                    {
                        // Bug:23 FIXED
                        p.RemoveAllGoods();
                    }
                    else
                    {
                        Console.WriteLine( "You must now choose which goods to spend to recruit your soldiers. You must choose {0} goods.", numToRecruit * p.SoldierCost );
                        foreach( int i in new Range( 1, numToRecruit * p.SoldierCost ) )
                        {
                            GoodsChoiceOptions good = this.DisplayChooseAGood( p, p.GoodTypesPlayerHas.ToArray() );
                            p.RemoveGood( good );
                        }
                    }
                    Console.WriteLine( "{0} recruits {1} soldiers.", p.Name, numToRecruit );
                    break;
                case graphicsMode.GUI:
                    throw new NotImplementedException();
            }
            return numToRecruit;
        }

        /// <summary>
        /// Displays information about the attacker that is attacking during phase 8.
        /// </summary>
        override internal void DisplayBattleInfo()
        {
            switch( this.Mode )
            {
                case graphicsMode.CLI:
                    Enemy attacker = GameManager.Instance.EnemiesForGame[GameManager.Instance.CurrentYear - 1];
                    Console.WriteLine( "We are under attack by {0}! Prepare to defend yourselves!\n", attacker.Name );
                    this.DisplayEnemyInfo( attacker );
                    break;
                case graphicsMode.GUI:
                    throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Displays the number of reinforcements the king has sent.
        /// </summary>
        /// <param name="reinforcements">The number of reinforcements sent.</param>
        override internal void DisplayKingsReinforcements( int reinforcements )
        {
            switch( this.Mode )
            {
                case graphicsMode.CLI:
                    Console.WriteLine( "The King sends {0} reinforcements.", reinforcements );
                    break;
                case graphicsMode.GUI:
                    throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Displays the results of a battle.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <param name="enemy">The enemy.</param>
        /// <param name="battleResults">Whether the player won, lost or tied the battle.</param>
        override internal void DisplayBattleResults( Player player, Enemy enemy, BattleResults battleResults )
        {
            switch( this.Mode )
            {
                case graphicsMode.CLI:
                    switch( battleResults )
                    {
                        case BattleResults.Victory:
                            Console.WriteLine( "{0} is victorious over the {1}! The kingdom is safe!", player.Name, enemy.Name );
                            break;
                        case BattleResults.Tie:
                            Console.WriteLine( "{0} has barely defeated the {1}. The battle was fierce and long, and the losses great, but the kingdom is safe.", player.Name, enemy.Name );
                            break;
                        case BattleResults.Loss:
                            Console.WriteLine( "{0} has suffered defeat at the hands of the {1}. The kingdom will suffer at the enemy's hand for another year.", player.Name, enemy.Name );
                            break;
                    }
                    break;
                case graphicsMode.GUI:
                    throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Displays information that a player was awarded a VP for being the most glorious player in battle.
        /// </summary>
        /// <param name="p">The player.</param>
        override internal void DisplayMostGloriousVictory( Player p )
        {
            switch( this.Mode )
            {
                case graphicsMode.CLI:
                    Console.WriteLine( "{0} decimated the enemy in a most glorious fashion and was awarded a Victory Point!", p.Name );
                    break;
                case graphicsMode.GUI:
                    throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Displays info that a player is receiving a victory point from his fortress.
        /// </summary>
        /// <param name="player">The player.</param>
        override internal void DisplayFortressBonus( Player player )
        {
            switch( this.Mode )
            {
                case graphicsMode.CLI:
                    Console.WriteLine( "{0} receives a Victory Point from his fortress.", player.Name );
                    break;
                case graphicsMode.GUI:
                    throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Lets the player use the ability of their Statue to reroll.
        /// </summary>
        /// <param name="p">The player.</param>
        override internal void DisplayUseStatue( Player p )
        {
            switch( this.Mode )
            {
                case graphicsMode.CLI:
                    string response;
                    Console.WriteLine( "{0}, you have rolled the same number on all your dice. Would you like to use your Statue and re-roll? (y/n)", p.Name );
                    do
                    {
                        response = Console.ReadLine();
                    }
                    while( !response.Equals( "y", StringComparison.OrdinalIgnoreCase ) &&
                        !response.Equals( "n", StringComparison.OrdinalIgnoreCase ) );

                    if( response.Equals( "y", StringComparison.OrdinalIgnoreCase ) )
                    {
                        p.RollDice();
                        this.DisplayDiceRoll( p );
                    }
                    else
                        return;
                    break;
                case graphicsMode.GUI:
                    throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Lets the player use the ability of their Chapel to reroll.
        /// </summary>
        /// <param name="p">The player.</param>
        override internal void DisplayUseChapel( Player p )
        {
            switch( this.Mode )
            {
                case graphicsMode.CLI:
                    string response;
                    Console.WriteLine( "{0}, your total dice roll is less than 7. Would you like to use your Chapel and re-roll? (y/n)", p.Name );
                    do
                    {
                        response = Console.ReadLine();
                    }
                    while( !response.Equals( "y", StringComparison.OrdinalIgnoreCase ) &&
                        !response.Equals( "n", StringComparison.OrdinalIgnoreCase ) );

                    if( response.Equals( "y", StringComparison.OrdinalIgnoreCase ) )
                    {
                        p.RollDice();
                        this.DisplayDiceRoll( p );
                    }
                    else
                        return;
                    break;
                case graphicsMode.GUI:
                    throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Displays info that a player is receiving victory points from his cathedral.
        /// </summary>
        /// <param name="p">The player.</param>
        /// <param name="VPEarned">The number of victory points earned.</param>
        override internal void DisplayCathedralBonus( Player p, int VPEarned )
        {
            switch( this.Mode )
            {
                case graphicsMode.CLI:
                    Console.WriteLine( "{0} receives {1} Victory Points from his Cathedral.", p.Name, VPEarned );
                    break;
                case graphicsMode.GUI:
                    throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Displays info that a player is receiving an extra die from his farms.
        /// </summary>
        /// <param name="p">The player.</param>
        override internal void DisplayFarmBonus( Player p )
        {
            switch( this.Mode )
            {
                case graphicsMode.CLI:
                    Console.WriteLine( "{0} receives an extra white die from his Farms.", p.Name );
                    break;
                case graphicsMode.GUI:
                    throw new NotImplementedException();
            }
        }

        override internal void DisplayMerchantsGuildBonus( Player p )
        {
            switch( this.Mode )
            {
                case graphicsMode.CLI:
                    Console.WriteLine( "{0} receives a Gold from his Merchants' Guild.", p.Name );
                    break;
                case graphicsMode.GUI:
                    throw new NotImplementedException();
            }
        }

        override internal void DisplayStableBonus( Player p )
        {
            switch( this.Mode )
            {
                case graphicsMode.CLI:
                    Console.WriteLine( "{0} receives an extra Soldier from his Stable.", p.Name );
                    break;
                case graphicsMode.GUI:
                    throw new NotImplementedException();
            }
        }

        override internal GoodsChoiceOptions DisplayGetTownHallChoice( Player p )
        {
            GoodsChoiceOptions toReturn = GoodsChoiceOptions.None;
            switch( this.Mode )
            {
                case graphicsMode.CLI:
                    if( p.GoodsCount > 0 || p.PlusTwoTokens > 0 )
                    {
                        Console.WriteLine( "{0}, you may exchange a good or a \"+2\" Token for a Victory Point. What would you like to exchange?", p.Name );
                        List<GoodsChoiceOptions> choices = p.GoodTypesPlayerHas;
                        choices.Add( GoodsChoiceOptions.None );
                        if( p.PlusTwoTokens > 0 )
                        {
                            choices.Add( GoodsChoiceOptions.PlusTwoToken );
                        }
                        this.DisplayChooseAGood( p, choices.ToArray() );
                    }
                    else
                    {
                        Console.WriteLine( "{0}, cannot use your Town Hall because you have no goods or \"+2\" Tokens to exchange.", p.Name );
                    }
                    break;
                case graphicsMode.GUI:
                    throw new NotImplementedException();
            }
            return toReturn;
        }

        override internal void DisplayEmbassyBonus( Player p )
        {
            switch( this.Mode )
            {
                case graphicsMode.CLI:
                    Console.WriteLine( "{0} receives a Victory Point from his Embassy.", p.Name );
                    break;
                case graphicsMode.GUI:
                    throw new NotImplementedException();
            }
        }

        override internal void DisplayUseCrane( Player p )
        {
            switch( this.Mode )
            {
                case graphicsMode.CLI:
                    Console.WriteLine( "{0} had the price of his building reduced by 1 Gold because he has a Crane.", p.Name );
                    break;
                case graphicsMode.GUI:
                    throw new NotImplementedException();
            }
        }

        override internal void DisplayKingsAid( Player player )
        {
            switch( this.Mode )
            {
                case graphicsMode.CLI:
                    Console.WriteLine( "{0} received a white die for the spring season because the king gave him aid.", player.Name );
                    break;
                case graphicsMode.GUI:
                    throw new NotImplementedException();
            }
        }
    }
}