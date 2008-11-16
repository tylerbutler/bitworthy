using System;
using System.Collections.Generic;
using System.Collections;
using Bitworthy.Games.Acquire.Components;
using Bitworthy.Games.Acquire.Util;
using System.Windows.Forms;
using gmit;
using System.Drawing;

namespace Bitworthy.Games.Acquire
{
    public class GameManager
    {
        private Dictionary<String, Player> players = new Dictionary<String, Player>();
        private ArrayList playerOrder = new ArrayList();
        private Board board = new Board();
        private static GameManager instance = null;
        private Player currentPlayer = null;
        private bool gameInProgress = true;

        public static GameManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameManager();
                }
                return instance;
            }
        }

        private GameManager()
        {
            // no constructor logic
        }

        public Player CurrentPlayer
        {
            get { return currentPlayer; }
            set
            {
                currentPlayer = value;
                CurrentPlayer.Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            }
        }

        public Board Board
        {
            get { return board; }
            set { board = value; }
        }

        public Dictionary<String, Player> Players
        {
            get { return players; }
            set { players = value; }
        }

        public ArrayList PlayerOrder
        {
            get { return playerOrder; }
            set { playerOrder = value; }
        }

        public bool IsGameDone()
        {
            bool toReturn = true;

            if (Board.GetHotelsOnBoard().Count == 0)
            {
                return false;
            }

            // All hotels on the board must be safe
            foreach (StockCard.Type hotel in Board.GetHotelsOnBoard())
            {
                if (!Board.isHotelSafe(hotel))
                {
                    return false;
                }

                if (Board.getHotelChainLength(hotel) >= 41)
                    return true;
            }

            //TODO: There must not be any place to start a new hotel chain

            return toReturn;

        }

        public void AddPlayer(Player p)
        {
            if (Players.Count >= 6)
            {
                throw new Exception("Too many players.");
            }
            else
            {
                PlayerOrder.Add(p.Name);
                Players.Add(p.Name, p);
            }
        }

        internal void StartGame()
        {
            gameInProgress = true;

            // Add button listeners
            foreach (Tile t in Board.Tiles)
            {
                t.Button.Click += tileButton_Click;
            }
            foreach (Button b in GameWindow.Instance.Controls["stockgroupbox"].Controls)
            {
                b.Click += stockButton_Click;
            }

            // add event listeners to special card buttons
            SpecialCard.getButton(SpecialCard.Type.Buy5).Click += new EventHandler(Buy5Button_Click);
            SpecialCard.getButton(SpecialCard.Type.Free3).Click += new EventHandler(Free3Button_Click);
            SpecialCard.getButton(SpecialCard.Type.Pick5).Click += new EventHandler(Pick5Button_Click);
            SpecialCard.getButton(SpecialCard.Type.Play3).Click += new EventHandler(Play3Button_Click);
            SpecialCard.getButton(SpecialCard.Type.Trade2).Click += new EventHandler(Trade2Button_Click);

            GameWindow.Instance.Controls["doneButton"].Click += doneButton_Click;

            // Add players
            PlayerSelection playerSelection = new PlayerSelection();
            playerSelection.ShowDialog();

            // get players and have them draw a random tile
            Dictionary<Tile, Player> d = new Dictionary<Tile, Player>();
            foreach (String name in playerSelection.names)
            {
                Player p = new Player(name);
                //Just use the first tile the player drew as his pick to determine order
                d.Add((Tile)p.Tiles[0], p);
            }
            Tile[] tiles = new Tile[d.Count];
            d.Keys.CopyTo(tiles, 0);
            Array.Sort(tiles);
            //Array.Reverse(tiles);

            foreach (Tile tile in tiles)
            {
                AddPlayer(d[tile]);
                placeTile(d[tile], tile);
            }

            // add players to the UI
            foreach (int i in new Range(0, Players.Count - 1))
            {
                Player p = Players[(String)PlayerOrder[i % Players.Count]];
                p.Label.Location = new System.Drawing.Point(8, 19 + (i * 15));
                GameWindow.Instance.PlayersGroupBox.Controls.Add(p.Label);
            }

            doTurn(Players[(string)PlayerOrder[0]]);
        }

        void Free3Button_Click(object sender, EventArgs e)
        {
            //TODO: Finish implementing this
            CurrentPlayer.Special.Remove(SpecialCard.Type.Free3);
            CurrentPlayer.HasPlayedSpecial = true;
            CurrentPlayer.Played3Free = true;
            GameWindow.Instance.updateUI(CurrentPlayer);
        }

        void Buy5Button_Click(object sender, EventArgs e)
        {
            CurrentPlayer.Special.Remove(SpecialCard.Type.Buy5);
            CurrentPlayer.HasPlayedSpecial = true;
            CurrentPlayer.StockLeftToBuy += 2;
            GameWindow.Instance.updateUI(CurrentPlayer);
        }

        void Pick5Button_Click(object sender, EventArgs e)
        {
            CurrentPlayer.Special.Remove(SpecialCard.Type.Pick5);
            CurrentPlayer.HasPlayedSpecial = true;
            foreach (int i in new Range(1, 5))
            {
                CurrentPlayer.drawRandomTile();
            }
            GameWindow.Instance.updateUI(CurrentPlayer);
        }

        void Play3Button_Click(object sender, EventArgs e)
        {
            CurrentPlayer.Special.Remove(SpecialCard.Type.Play3);
            CurrentPlayer.HasPlayedSpecial = true;
            CurrentPlayer.TilesLeftToPlay += 2;
            GameWindow.Instance.updateUI(CurrentPlayer);
        }

        void Trade2Button_Click(object sender, EventArgs e)
        {
            //TODO: implement the trade 2 button
            MessageBox.Show("NYI.");
        }

        internal void doTurn(Player p)
        {
            //Clean up previous player if necessary
            if (CurrentPlayer != null)
            {
                CurrentPlayer.NumTurns++;
                CurrentPlayer.Played3Free = false;
                CurrentPlayer.HasPlayedSpecial = false;
                CurrentPlayer.Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            }

            if (!gameInProgress) // the game was declared over
            {
                FinishGame();
            }

            //set the player passed in as current player
            CurrentPlayer = p;

            // Check if game can be declared over
            if (IsGameDone())
            {
                if (MessageBox.Show("You can declare the game over. Would you like to do so?", "Declare Game?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    gameInProgress = false;
                }
            }

            //find dead tiles and play them
            while (FindDeadTiles(CurrentPlayer))
            {
                while (CurrentPlayer.Tiles.Count < 6)
                {
                    CurrentPlayer.drawRandomTile();
                }
            }

            // if it's the player's first turn, draw up to six tiles
            if (CurrentPlayer.NumTurns == 0)
            {
                while (CurrentPlayer.Tiles.Count < 6)
                {
                    CurrentPlayer.drawRandomTile();
                }
            }

            // Set current player's stock/tile allowances
            CurrentPlayer.TilesLeftToPlay = 1;
            CurrentPlayer.StockLeftToBuy = 3;

            //set up UI for player
            GameWindow.Instance.updateUI(CurrentPlayer);
        }

        private void FinishGame()
        {
            //TODO: implement end game logic
        }

        internal Player getNextPlayer()
        {
            int current = PlayerOrder.IndexOf(CurrentPlayer.Name);
            int total = Players.Count;
            return Players[(String)PlayerOrder[(current + 1) % total]];
        }

        internal int stockLeft(StockCard.Type t)
        {
            int toReturn = 25;
            foreach (Player p in Players.Values)
            {
                toReturn -= p.numStock(t);
            }
            return toReturn;
        }

        private void tileButton_Click(object sender, EventArgs e)
        {
            //Get the tile object this button represents
            Tile clickedTile = Board.getTileByName(((Button)sender).Text);

            if (CurrentPlayer.TilesLeftToPlay > 0)
            {
                // get bordering tiles
                ArrayList neighbors = Board.GetNeighborsforTile(Board.getTileByName(((Button)sender).Text));

                if (neighbors.Count > 0) // there are bordering placed tiles
                {
                    ArrayList hotels = new ArrayList();
                    //check if bordering tiles belong to hotel chain
                    foreach (Tile t in neighbors)
                    {
                        if (t.Hotel != StockCard.Type.None && !hotels.Contains(t.Hotel))
                        {
                            hotels.Add(t.Hotel);
                        }
                    }

                    if (hotels.Count > 1) // tile borders more than one hotel
                    {
                        StockCard.Type overtakingStock = doMerger(hotels);
                        placeTile(CurrentPlayer, clickedTile);
                        recursiveSetHotel((Tile)neighbors[0], overtakingStock, null);
                    }
                    else if (hotels.Count == 1) //bordering tiles belong to ONE chain
                    {
                        //Now place the tile
                        placeTile(CurrentPlayer, clickedTile);
                        recursiveSetHotel((Tile)neighbors[0], (StockCard.Type)hotels[0], null);
                    }
                    else if (hotels.Count == 0 && Board.GetHotelsOnBoard().Count < 7) // bordering tiles are not in a chain
                    {
                        StockCard.Type h = showOpenChainDialog();

                        placeTile(CurrentPlayer, clickedTile);
                        // call the recursive function on one of the tiles, and all tiles will get the correct hotel recursively
                        recursiveSetHotel((Tile)neighbors[0], h, null);

                        //Since the player founded the hotel chain, give him a free stock
                        CurrentPlayer.giveStock(1, h);
                    }
                    else //There are no bordering tiles that have been placed on the board
                    {
                        //Just set the hotel and place the tile
                        clickedTile.Hotel = StockCard.Type.None;
                        placeTile(CurrentPlayer, clickedTile);
                    }
                }
                else //There are no bordering tiles that have been placed on the board
                {
                    //Just set the hotel and place the tile
                    clickedTile.Hotel = StockCard.Type.None;
                    placeTile(CurrentPlayer, clickedTile);
                }
                GameWindow.Instance.updateUI(CurrentPlayer);
            }
            else //player has no tiles left to play
            {
                MessageBox.Show("You have already played the maximum number of tiles this turn.", "Can't Play a Tile", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void stockButton_Click(object sender, EventArgs e)
        {
            if (CurrentPlayer.TilesLeftToPlay > 0) // Have to place tiles first
            {
                MessageBox.Show("You must place all of your tiles before buying stock.\nYou have " + CurrentPlayer.TilesLeftToPlay +
                    " tiles left to play.", "Place Tiles Before Buying Stock", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else // tiles have been placed, can buy stock
            {
                if (CurrentPlayer.StockLeftToBuy > 0) // Can buy stock
                {
                    //get the stock type based on the button clicked
                    StockCard.Type stockType;
                    #region switch(((Button)sender).Name)
                    switch (((Button)sender).Name)
                    {
                        case ("phoenixButton"):
                            stockType = StockCard.Type.Phoenix;
                            break;

                        case ("quantumButton"):
                            stockType = StockCard.Type.Quantum;
                            break;

                        case ("fusionButton"):
                            stockType = StockCard.Type.Fusion;
                            break;

                        case ("hydraButton"):
                            stockType = StockCard.Type.Hydra;
                            break;

                        case ("americaButton"):
                            stockType = StockCard.Type.American;
                            break;

                        case ("sacksonButton"):
                            stockType = StockCard.Type.Sackson;
                            break;

                        case ("zetaButton"):
                            stockType = StockCard.Type.Zeta;
                            break;

                        default:
                            stockType = StockCard.Type.None;
                            break;
                    }
                    #endregion

                    if (!Board.GetHotelsOnBoard().Contains(stockType))
                    {
                        MessageBox.Show("This stock is unavailable because the hotel has not been opened yet.", "Stock Unavailable", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (getRemainingStock(stockType) == 0) // stock is sold out
                    {
                        MessageBox.Show("This stock is sold out.", "Sold Out", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else //stock is available
                    {
                        if (CurrentPlayer.Played3Free)
                        {
                            CurrentPlayer.giveStock(1, stockType);
                            CurrentPlayer.StockLeftToBuy--;
                            GameWindow.Instance.updateUI(CurrentPlayer);
                        }
                        else if (StockCard.getStockValue(stockType, Board.getHotelChainLength(stockType)) > CurrentPlayer.Money) // can't afford stock
                        {
                            MessageBox.Show("You cannot afford this stock.", "You're too poor.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else // can afford stock, so buy it
                        {
                            CurrentPlayer.takeMoney(StockCard.getStockValue(stockType, Board.getHotelChainLength(stockType)));
                            CurrentPlayer.giveStock(1, stockType);
                            CurrentPlayer.StockLeftToBuy--;
                            GameWindow.Instance.updateUI(CurrentPlayer);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("You have already bought the maximum shares of stock this turn.", "Can't Buy More Stock", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void doneButton_Click(object sender, EventArgs e)
        {
            if (CurrentPlayer.TilesLeftToPlay > 0)
            {
                MessageBox.Show("You must place all of your tiles before continuing to the next player.\nYou have " + CurrentPlayer.TilesLeftToPlay +
                    " tiles left to play.", "Place Tiles Before Continuing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {

                if (MessageBox.Show("Are you done with your turn?", "Are you done?", MessageBoxButtons.OKCancel, MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2) == DialogResult.OK)
                {
                    // draw up to six tiles
                    while (CurrentPlayer.Tiles.Count < 6)
                    {
                        CurrentPlayer.drawRandomTile();
                    }
                    doTurn(getNextPlayer());
                }
            }
        }

        private bool FindDeadTiles(Player p)
        {
            ArrayList deadTiles = new ArrayList();
            foreach (Tile t in p.Tiles)
            {
                ArrayList neighbors = Board.GetNeighborsforTile(t);
                if (neighbors.Count == 0)
                { return false; }

                bool dead = true;
                foreach (Tile t2 in neighbors)
                {
                    if (!Board.isHotelSafe(t2.Hotel))
                    {
                        dead = false;
                        break;
                    }
                }

                if (dead)
                {
                    deadTiles.Add(t);
                }
            }

            foreach (Tile t in deadTiles)
            {
                placeTile(p, t);
                t.Button.BackColor = Color.Black;
            }

            if (deadTiles.Count > 0)
                return true;
            else return false;
        }

        private StockCard.Type doMerger(ArrayList hotels)
        {
            ArrayList majorityHolders = new ArrayList();
            ArrayList minorityHolders = new ArrayList();

            StockCard.Type overtakingHotel = StockCard.Type.None;
            StockCard.Type overtaken = StockCard.Type.None;

            // check to see if any of the chains are of the same length
            ArrayList sameLengthHotels = new ArrayList();
            foreach (StockCard.Type h in hotels)
            {
                int val = Board.getHotelChainLength(h);
                ArrayList compareTo = (ArrayList)hotels.Clone();
                compareTo.Remove(h);
                foreach (StockCard.Type t in compareTo)
                {
                    if (val == Board.getHotelChainLength(t) && !sameLengthHotels.Contains(t))
                    {
                        sameLengthHotels.Add(t);
                    }
                }
            }

            #region if( no hotels are the same length )
            if (sameLengthHotels.Count == 0) // no hotels are the same length
            {
                //order the chains by length
                Dictionary<int, StockCard.Type> lookupDictionary = new Dictionary<int, StockCard.Type>();
                foreach (StockCard.Type t in hotels)
                {
                    lookupDictionary[Board.getHotelChainLength(t)] = t;
                }
                int[] order = new int[lookupDictionary.Count];
                lookupDictionary.Keys.CopyTo(order, 0);
                Array.Sort(order); // sorts least -> greatest
                Array.Reverse(order); //reverse so order is greatest -> least

                // now the arrays are sorted, so execute the merger
                overtakingHotel = lookupDictionary[order[0]];

                // Merge all the remaining hotels one at a time
                foreach (int i in new Range(1, order.Length - 1))  // give bonuses
                {
                    overtaken = lookupDictionary[order[i]];

                    MessageBox.Show(overtakingHotel.ToString() + " acquired " + overtaken.ToString() + ".", "Acquisition",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // get majority/minority holders
                    majorityHolders = getMajorityHolders(overtaken);
                    minorityHolders = getMinorityHolders(overtaken);

                    if (majorityHolders.Count > 1) //multiple majority holders must share the bounty
                    {
                        int bonus = StockCard.getMajorityBonus(overtaken, Board.getHotelChainLength(overtaken)) +
                            StockCard.getMinorityBonus(overtaken, Board.getHotelChainLength(overtaken));

                        //TODO: make this act like the game and round up appropriately
                        int splitBonus = bonus / majorityHolders.Count;
                        string playerNames = "";
                        foreach (Player p in majorityHolders) // Give each player their bonus
                        {
                            p.giveMoney(splitBonus);
                            playerNames += "\n" + p.Name;
                        }
                        MessageBox.Show("The following players were awarded $" + splitBonus.ToString() + " each as the majority stockholders of "
                            + overtaken.ToString() + ":" + playerNames,
                            "Majority Bonus", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    else // only one majority, give him the bonus
                    {
                        Player p = (Player)majorityHolders[0];
                        int bonus = StockCard.getMajorityBonus(overtaken, Board.getHotelChainLength(overtaken));
                        p.giveMoney(bonus);
                        MessageBox.Show(p.Name + " was awarded $" + bonus.ToString() + " as the majority stockholder of " + overtaken.ToString() + ".",
                            "Majority Bonus", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    if (majorityHolders.Count <= 1) // one majority holder, so minority shareholders get bonus
                    {
                        string playerNames = "";
                        int bonus = StockCard.getMinorityBonus(overtaken, Board.getHotelChainLength(overtaken));
                        bonus = bonus / minorityHolders.Count;

                        foreach (Player p in minorityHolders)// Give each player their bonus
                        {
                            p.giveMoney(bonus);
                            playerNames += "\n" + p.Name;
                        }
                        MessageBox.Show("The following players were awarded $" + bonus.ToString() + " each as the minority stockholders of "
                            + overtaken.ToString() + ":" + playerNames,
                            "Minority Bonus", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else // multiple majority holders, so minority holders mean nothing
                    {
                        MessageBox.Show("No players received a minority bonus because there were multiple majority holders.", "Minority Bonus",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    // let each player who has stock in the merged hotel choose what they want to do with their stock
                    foreach (Player p in Players.Values)
                    {
                        if (p.numStock(overtaken) > 0)
                        {
                            PostMergerChoice dialog = new PostMergerChoice(p, overtakingHotel, overtaken);
                            dialog.ShowDialog();
                        }
                    }
                }
            }
            #endregion
            else // some hotels are the same length
            {
                //Display choice dialog
                MergingStockChoice dialog = new MergingStockChoice(sameLengthHotels);
                dialog.ShowDialog();
                overtakingHotel = dialog.SelectedHotel;

                // Merge all the remaining hotels one at a time
                sameLengthHotels.Remove(overtakingHotel);
                foreach (StockCard.Type hotel in sameLengthHotels)  // give majority bonuses
                {
                    overtaken = hotel;

                    MessageBox.Show(overtakingHotel.ToString() + " acquired " + overtaken.ToString() + ".", "Acquisition",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // get majority holders
                    majorityHolders = getMajorityHolders(overtaken);
                    minorityHolders = getMinorityHolders(overtaken);

                    if (majorityHolders.Count > 1) //multiple majority holders must share the bounty
                    {
                        int bonus = StockCard.getMajorityBonus(overtaken, Board.getHotelChainLength(overtaken)) +
                            StockCard.getMinorityBonus(overtaken, Board.getHotelChainLength(overtaken));

                        //TODO: make this act like the game and round up appropriately
                        int splitBonus = bonus / majorityHolders.Count;
                        string playerNames = "";
                        foreach (Player p in majorityHolders) // Give each player their bonus
                        {
                            p.giveMoney(splitBonus);
                            playerNames += "\n" + p.Name;

                            MessageBox.Show("The following players were awarded $" + splitBonus.ToString() + " each as the majority stockholders of "
                                + overtaken.ToString() + ":" + playerNames,
                                "Majority Bonus", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else // only one majority, give him the bonus
                    {
                        Player p = (Player)majorityHolders[0];
                        int bonus = StockCard.getMajorityBonus(overtaken, Board.getHotelChainLength(overtaken));
                        p.giveMoney(bonus);
                        MessageBox.Show(p.Name + " was awarded $" + bonus.ToString() + " as the majority stockholder of " + overtaken.ToString() + ".",
                            "Majority Bonus", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    // Get the ordered player list to determine who picks stock options first
                    ArrayList orderedList = sortPlayersbyStock(overtaken);

                    // let each player who has stock in the merged hotel choose what they want to do with their stock
                    for (int i = 0; i < orderedList.Count; i++)
                    {
                        Player p = (Player)orderedList[i];

                        if (p.numStock(overtaken) > 0)
                        {
                            PostMergerChoice dialog2 = new PostMergerChoice(p, overtakingHotel, overtaken);
                            dialog2.ShowDialog();
                        }
                    }
                }
            }

            return overtakingHotel;
        }

        private void recursiveEquivalent(ArrayList toCheck, bool firstCall, ArrayList returnList)
        {
            ArrayList toReturn = new ArrayList();
            if (firstCall)
            {
                Board.getHotelChainLength((StockCard.Type)toCheck[0]);
            }
            else
            {
            }
        }

        private void placeTile(Player p, Tile t)
        {
            Board.PlacedTiles.Add(t);
            t.Owner = null;
            t.Placed = true;

            //remove tile from player's stash
            p.takeTile(t);
            p.TilesLeftToPlay--;

            //update UI
            GameWindow.Instance.updateUI(p);
        }

        private StockCard.Type showOpenChainDialog()
        {
            HotelChainSelection dialog = new HotelChainSelection();

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                return dialog.SelectedHotel;
            }
            else
            {
                throw new Exception("Invalid selection in hotel selection dialog.");
            }
        }

        private void recursiveSetHotel(Tile t, StockCard.Type hotel, ArrayList calledTiles)
        {
            if (calledTiles != null)
            { calledTiles.Add(t); }
            else { calledTiles = new ArrayList(); }

            ArrayList BorderingTiles = Board.GetNeighborsforTile(t);
            ArrayList toCall = new ArrayList();
            foreach (Tile borderingTile in BorderingTiles)
            {
                if (!calledTiles.Contains(borderingTile))
                {
                    toCall.Add(borderingTile);
                }
            }

            foreach (Tile tile in toCall)
            {
                recursiveSetHotel(tile, hotel, calledTiles);
            }

            t.Hotel = hotel;
        }

        private int getRemainingStock(StockCard.Type stock)
        {
            int toReturn = 25;
            foreach (Player p in Players.Values)
            {
                toReturn -= p.numStock(stock);
            }
            return toReturn;
        }

        private ArrayList sortPlayersbyStock(StockCard.Type stock)
        {
            // uses a quicksort alogirthm
            ArrayList toReturn = new ArrayList();
            int[] keys = new int[Players.Count];
            Player[] values = new Player[Players.Count];

            int i = 0;
            foreach (Player p in Players.Values)
            {
                keys[i] = p.numStock(stock);
                values[i] = p;
                i++;
            }

            //sort arrays
            Array.Sort(keys, values);
            Array.Reverse(values);

            for (i = 0; i < Players.Count; i++)
            {
                toReturn.Add(values[i]);
            }

            return toReturn;
        }

        private ArrayList getMajorityHolders(StockCard.Type hotel)
        {
            ArrayList toReturn = new ArrayList();

            Player majority = Players[(string)PlayerOrder[0]];

            foreach (Player nextPlayer in Players.Values)
            {
                if (nextPlayer != Players[(string)PlayerOrder[0]])
                {
                    if (nextPlayer.numStock(hotel) > majority.numStock(hotel))
                    {
                        majority = nextPlayer;
                    }
                }
            }

            // now see if there are multiple players with the same majority of stock
            foreach (Player p in Players.Values)
            {
                if (p.numStock(hotel) == majority.numStock(hotel))
                {
                    toReturn.Add(p);
                }
            }

            return toReturn;
        }

        private ArrayList getMinorityHolders(StockCard.Type hotel)
        {
            ArrayList toReturn = new ArrayList();
            ArrayList majorityHolders = getMajorityHolders(hotel);
            ArrayList sortedPlayers = sortPlayersbyStock(hotel);

            // do a quick check to see if there was more than one majority - in that case,
            // no minority players get bonuses
            if (majorityHolders.Count > 1)
                return new ArrayList();

            // there's only one majority holder, so he should be on the top of the sorted list
            sortedPlayers.RemoveAt(0);

            // now we have remaining sorted players, so get the player
            // from the top and call him minority holder

            Player minority = (Player)sortedPlayers[0];
            toReturn.Add(minority);

            foreach (Player nextPlayer in sortedPlayers)
            {
                if (nextPlayer != sortedPlayers[0])
                {
                    if (nextPlayer.numStock(hotel) == minority.numStock(hotel))
                    {
                        toReturn.Add(nextPlayer);
                    }
                    else
                    {
                        // we've found all the minority players, return immediately
                        return toReturn;
                    }
                }
            }

            return toReturn;
        }
    }
}