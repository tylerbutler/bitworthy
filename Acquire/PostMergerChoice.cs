using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Bitworthy.Games.Acquire.Components;
using Bitworthy.Games.Acquire.Util;

namespace Bitworthy.Games.Acquire
{
    public partial class PostMergerChoice : Form
    {
        private GameManager gm = GameManager.Instance;
        private Player player;
        private StockCard.Type overtaken, overtaking;

        public PostMergerChoice(Player p, StockCard.Type overtakingHotel, StockCard.Type overtakenHotel)
        {
            InitializeComponent();
            player = p;
            overtaken = overtakenHotel;
            overtaking = overtakingHotel;

            //set up additional UI based on the player
            questionLabel.Text = p.Name + Constants.PostMergerChoiceUILabel;
            mergeLabel.Text = overtaking.ToString() + " has acquired " + overtaken.ToString() + ".";
            sell1Button.Text = "Sell one (1) " + overtaken.ToString() + " stock for $" +
                StockCard.getStockValue(overtaken, gm.Board.getHotelChainLength(overtaken)) + ".";
            sellAllButton.Text = "Sell all for $" + p.numStock(overtaken) * StockCard.getStockValue(overtaken, gm.Board.getHotelChainLength(overtaken)) + ".";
            trade2Button.Text = "Trade two (2) " + overtaken.ToString() + " stock for one (1) " + overtaking.ToString() + " stock.";
            updateUI();

            // add event listeners to the buttons
            sell1Button.Click += new EventHandler(sell1Button_Click);
            sellAllButton.Click += new EventHandler(sellAllButton_Click);
            trade2Button.Click += new EventHandler(trade2Button_Click);
        }

        void trade2Button_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.None;

            if (player.numStock(overtaken) < 2 )
            {
                MessageBox.Show("You need at least 2 " + overtaken.ToString() + " stock to trade.", "Not enough stock to trade.",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (gm.stockLeft(overtaking) < 1)
            {
                MessageBox.Show("There is not any " + overtaking.ToString() + " stock to trade for.", "No stock to trade for.",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                player.takeStock(2, overtaken);
                player.giveStock(1, overtaking);
            }
        }

        void sellAllButton_Click(object sender, EventArgs e)
        {
            int numStock = player.numStock(overtaken);
            int value = StockCard.getStockValue(overtaken, gm.Board.getHotelChainLength(overtaken));

            player.giveMoney(numStock * value);
            player.takeStock(numStock, overtaken);
        }

        void sell1Button_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.None;

            //int numStock = player.numStock(overtaken);
            int value = StockCard.getStockValue(overtaken, gm.Board.getHotelChainLength(overtaken));

            player.giveMoney(value);
            player.takeStock(1, overtaken);


            //close out if no stock left to sell
            if (player.numStock(overtaken) == 0)
            {
                DialogResult = DialogResult.OK;
            }
            else
            {
                updateUI();
            }
        }

        void updateUI()
        {
            overtakenStockButton.Text = overtaken.ToString() + " Stock: " + player.numStock(overtaken);
            overtakingStockLabel.Text = overtaking.ToString() + " Stock: " + player.numStock(overtaking);
            cashLabel.Text = "Cash: " + Constants.cashUILabel + player.Money;
        }
    }
}