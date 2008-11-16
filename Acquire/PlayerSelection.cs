using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace Bitworthy.Games.Acquire
{
    public partial class PlayerSelection : Form
    {
        public ArrayList names;

        public PlayerSelection()
        {
            InitializeComponent();
            startButton.Click += new EventHandler(startButton_Click);
        }

        void startButton_Click(object sender, EventArgs e)
        {
            //validate player names
            names = new ArrayList();
            foreach (Control c in this.Controls)
            {
                if (c is TextBox)
                {
                    TextBox t = (TextBox)c;
                    if (names.Contains(t.Text.Trim()))
                    {
                        // pop up error dialog
                        MessageBox.Show("Players must have unique names.", "Duplicate Player Names", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        DialogResult = DialogResult.None;
                        return;
                    }

                    // only add non blank entries
                    if (t.Text.Trim() != "")
                    {
                        names.Add(t.Text.Trim());
                    }
                }
            }
            if (names.Count < 2)
            {
                MessageBox.Show("Please enter at least 2 players.", "Need more players", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.None;
                return;
            }
        }
    }
}