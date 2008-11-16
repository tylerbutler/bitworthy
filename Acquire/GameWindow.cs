using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Bitworthy.Games.Acquire.Components;
using Bitworthy.Games.Acquire;
using gmit;
using Bitworthy.Games.Acquire.Util;
using System.Collections;

namespace Bitworthy.Games.Acquire
{
    public partial class GameWindow : Form
    {
        GameManager gm = GameManager.Instance;
        private static GameWindow instance;

        private GameWindow()
        {
            InitializeComponent();

            // initialize tile buttons
            foreach (int i in new Range(0, 8))
            {
                foreach (int j in new Range(0, 11))
                {
                    Button b = gm.Board.Tiles[j, i].Button;
                    this.flowLayoutPanel1.Controls.Add(b);
                }
            }

            // initialize stock buttons
            americaButton.BackColor = StockCard.getStockColor(StockCard.Type.American);
            fusionButton.BackColor = StockCard.getStockColor(StockCard.Type.Fusion);
            hydraButton.BackColor = StockCard.getStockColor(StockCard.Type.Hydra);
            phoenixButton.BackColor = StockCard.getStockColor(StockCard.Type.Phoenix);
            quantumButton.BackColor = StockCard.getStockColor(StockCard.Type.Quantum);
            sacksonButton.BackColor = StockCard.getStockColor(StockCard.Type.Sackson);
            zetaButton.BackColor = StockCard.getStockColor(StockCard.Type.Zeta);

            // initialize special card buttons
            foreach (SpecialCard.Type t in Enum.GetValues(typeof(SpecialCard.Type)))
            {
                this.Controls.Add(SpecialCard.getButton(t));
            }
        }

        public static GameWindow Instance
        {
            get
            {
                if (GameWindow.instance == null)
                {
                    GameWindow.instance = new GameWindow();
                }
                return instance;
            }
            set { GameWindow.instance = value; }
        }


        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gm.StartGame();
        }

        public void updateUI(Player p)
        {
            americaButton.Text = p.numStock(StockCard.Type.American).ToString() + " : " + gm.stockLeft(StockCard.Type.American)
                + " ($" + StockCard.getStockValue(StockCard.Type.American, gm.Board.getHotelChainLength(StockCard.Type.American)) + ")";

            fusionButton.Text = p.numStock(StockCard.Type.Fusion).ToString() + " : " + gm.stockLeft(StockCard.Type.Fusion)
                 + " ($" + StockCard.getStockValue(StockCard.Type.Fusion, gm.Board.getHotelChainLength(StockCard.Type.Fusion)) + ")";

            hydraButton.Text = p.numStock(StockCard.Type.Hydra).ToString() + " : " + gm.stockLeft(StockCard.Type.Hydra)
                 + " ($" + StockCard.getStockValue(StockCard.Type.Hydra, gm.Board.getHotelChainLength(StockCard.Type.Hydra)) + ")";

            phoenixButton.Text = p.numStock(StockCard.Type.Phoenix).ToString() + " : " + gm.stockLeft(StockCard.Type.Phoenix)
                 + " ($" + StockCard.getStockValue(StockCard.Type.Phoenix, gm.Board.getHotelChainLength(StockCard.Type.Phoenix)) + ")";

            quantumButton.Text = p.numStock(StockCard.Type.Quantum).ToString() + " : " + gm.stockLeft(StockCard.Type.Quantum)
                 + " ($" + StockCard.getStockValue(StockCard.Type.Quantum, gm.Board.getHotelChainLength(StockCard.Type.Quantum)) + ")";

            sacksonButton.Text = p.numStock(StockCard.Type.Sackson).ToString() + " : " + gm.stockLeft(StockCard.Type.Sackson)
                 + " ($" + StockCard.getStockValue(StockCard.Type.Sackson, gm.Board.getHotelChainLength(StockCard.Type.Sackson)) + ")";

            zetaButton.Text = p.numStock(StockCard.Type.Zeta).ToString() + " : " + gm.stockLeft(StockCard.Type.Zeta)
                 + " ($" + StockCard.getStockValue(StockCard.Type.Zeta, gm.Board.getHotelChainLength(StockCard.Type.Zeta)) + ")";

            ArrayList temp = gm.Board.GetHotelsOnBoard();
            foreach (StockCard.Type hotel in Enum.GetValues(typeof(StockCard.Type)))
            {
                if (hotel != StockCard.Type.None && !temp.Contains(hotel))
                {
                    hotelToButton(hotel).Enabled = false;
                }
                else if (hotel != StockCard.Type.None)
                {
                    hotelToButton(hotel).Enabled = true;
                }
            }

            enableSpecialButtons(p);

            cashlabel.Text = Constants.cashUILabel + p.Money.ToString();
            stockLeftLabel.Text = Constants.stockLeftUILabel + p.StockLeftToBuy;
            tilesLeftLabel.Text = Constants.tilesLeftUILabel + p.TilesLeftToPlay;

            gm.Board.disableAllTiles();
            TilesList.Items.Clear();
            foreach (Tile t in p.Tiles)
            {
                TilesList.Items.Add(t.Name);
                t.Button.Enabled = true;
            }
        }

        private Button hotelToButton(StockCard.Type hotel)
        {
            switch (hotel)
            {
                case StockCard.Type.American:
                    return (Button)stockgroupbox.Controls["americaButton"];

                case StockCard.Type.Fusion:
                    return (Button)stockgroupbox.Controls["fusionButton"];

                case StockCard.Type.Hydra:
                    return (Button)stockgroupbox.Controls["hydraButton"];

                case StockCard.Type.Phoenix:
                    return (Button)stockgroupbox.Controls["phoenixButton"];

                case StockCard.Type.Quantum:
                    return (Button)stockgroupbox.Controls["quantumButton"];

                case StockCard.Type.Sackson:
                    return (Button)stockgroupbox.Controls["sacksonButton"];

                case StockCard.Type.Zeta:
                    return (Button)stockgroupbox.Controls["zetaButton"];

                default:
                    return null;
            }
        }

        private void enableSpecialButtons(Player p)
        {
            //Set up specials
            if (p.HasPlayedSpecial)
            {
                foreach (SpecialCard.Type t in Enum.GetValues(typeof(SpecialCard.Type)))
                {
                    SpecialCard.getButton(t).Enabled = false;
                }
            }
            else
            {

                if (p.hasSpecial(SpecialCard.Type.Buy5))
                { SpecialCard.getButton(SpecialCard.Type.Buy5).Enabled = true; }
                else
                { SpecialCard.getButton(SpecialCard.Type.Buy5).Enabled = false; }

                if (p.hasSpecial(SpecialCard.Type.Free3))
                { SpecialCard.getButton(SpecialCard.Type.Free3).Enabled = true; }
                else
                { SpecialCard.getButton(SpecialCard.Type.Free3).Enabled = false; }

                if (p.hasSpecial(SpecialCard.Type.Pick5))
                { SpecialCard.getButton(SpecialCard.Type.Pick5).Enabled = true; }
                else
                { SpecialCard.getButton(SpecialCard.Type.Pick5).Enabled = false; }

                if (p.hasSpecial(SpecialCard.Type.Play3))
                { SpecialCard.getButton(SpecialCard.Type.Play3).Enabled = true; }
                else
                { SpecialCard.getButton(SpecialCard.Type.Play3).Enabled = false; }

                if (p.hasSpecial(SpecialCard.Type.Trade2))
                { SpecialCard.getButton(SpecialCard.Type.Trade2).Enabled = true; }
                else
                { SpecialCard.getButton(SpecialCard.Type.Trade2).Enabled = false; }
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}