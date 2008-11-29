using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TylerButler.Kingsburg.Core;

namespace KingsburgWinForms
{
    public partial class GetPlayersForm : Form
    {
        public GetPlayersForm()
        {
            InitializeComponent();

            this.choosePlayersLabel.Text = global::KingsburgWinForms.GameResources.ChoosePlayersString;
        }

        new public PlayerCollection ShowDialog()
        {
            base.ShowDialog();
            //TODO Finish this.
            return null;
        }
    }
}
