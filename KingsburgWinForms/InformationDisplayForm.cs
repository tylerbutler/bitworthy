using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows;

namespace KingsburgWinForms
{
    public partial class InformationDisplayForm : Form
    {
        public InformationDisplayForm(string title, string message)
        {
            InitializeComponent();
            this.messageTitleLabel.Text = title;
            this.messageLabel.Text = message;
        }
    }
}
