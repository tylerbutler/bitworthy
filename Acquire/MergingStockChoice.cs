using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using Bitworthy.Games.Acquire.Components;

namespace Bitworthy.Games.Acquire
{
    public partial class MergingStockChoice : Form
    {
        public StockCard.Type SelectedHotel = StockCard.Type.None;

        public MergingStockChoice(ArrayList StockMerged)
        {
            InitializeComponent();

            //disable all invalid hotel buttons
            foreach (StockCard.Type t in Enum.GetValues(typeof(StockCard.Type)))
            {
                if ( t != StockCard.Type.None && !StockMerged.Contains(t))
                {
                    hotelToButton(t).Enabled = false;
                }
            }

            foreach (Button b in hotelChainGroupbox.Controls)
            {
                b.Click += stockButton_Click;
            }
        }

        private Button hotelToButton(StockCard.Type hotel)
        {
            switch (hotel)
            {
                case StockCard.Type.American:
                    return (Button)hotelChainGroupbox.Controls["americaButton"];

                case StockCard.Type.Fusion:
                    return (Button)hotelChainGroupbox.Controls["fusionButton"];

                case StockCard.Type.Hydra:
                    return (Button)hotelChainGroupbox.Controls["hydraButton"];

                case StockCard.Type.Phoenix:
                    return (Button)hotelChainGroupbox.Controls["phoenixButton"];

                case StockCard.Type.Quantum:
                    return (Button)hotelChainGroupbox.Controls["quantumButton"];

                case StockCard.Type.Sackson:
                    return (Button)hotelChainGroupbox.Controls["sacksonButton"];

                case StockCard.Type.Zeta:
                    return (Button)hotelChainGroupbox.Controls["zetaButton"];

                default:
                    return null;
            }
        }

        public void stockButton_Click(object sender, EventArgs args)
        {
            #region switch
            switch (((Button)sender).Name)
            {
                case ("phoenixButton"):
                    SelectedHotel = StockCard.Type.Phoenix;
                    break;

                case ("quantumButton"):
                    SelectedHotel = StockCard.Type.Quantum;
                    break;

                case ("fusionButton"):
                    SelectedHotel = StockCard.Type.Fusion;
                    break;

                case ("hydraButton"):
                    SelectedHotel = StockCard.Type.Hydra;
                    break;

                case ("americaButton"):
                    SelectedHotel = StockCard.Type.American;
                    break;

                case ("sacksonButton"):
                    SelectedHotel = StockCard.Type.Sackson;
                    break;

                case ("zetaButton"):
                    SelectedHotel = StockCard.Type.Zeta;
                    break;

                default:
                    break;
            }
            #endregion
        }
    }
}