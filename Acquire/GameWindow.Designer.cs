namespace Bitworthy.Games.Acquire
{
    partial class GameWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.TilesList = new System.Windows.Forms.ListBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.newGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stockgroupbox = new System.Windows.Forms.GroupBox();
            this.cashgroupbox = new System.Windows.Forms.GroupBox();
            this.cashlabel = new System.Windows.Forms.Label();
            this.doneButton = new System.Windows.Forms.Button();
            this.playersGroupBox = new System.Windows.Forms.GroupBox();
            this.tilesGroupBox = new System.Windows.Forms.GroupBox();
            this.infoGroupbox = new System.Windows.Forms.GroupBox();
            this.tilesLeftLabel = new System.Windows.Forms.Label();
            this.stockLeftLabel = new System.Windows.Forms.Label();
            this.zetaButton = new System.Windows.Forms.Button();
            this.americaButton = new System.Windows.Forms.Button();
            this.sacksonButton = new System.Windows.Forms.Button();
            this.fusionButton = new System.Windows.Forms.Button();
            this.hydraButton = new System.Windows.Forms.Button();
            this.quantumButton = new System.Windows.Forms.Button();
            this.phoenixButton = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.stockgroupbox.SuspendLayout();
            this.cashgroupbox.SuspendLayout();
            this.tilesGroupBox.SuspendLayout();
            this.infoGroupbox.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Location = new System.Drawing.Point(12, 27);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(990, 610);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // TilesList
            // 
            this.TilesList.FormattingEnabled = true;
            this.TilesList.Location = new System.Drawing.Point(6, 19);
            this.TilesList.Name = "TilesList";
            this.TilesList.Size = new System.Drawing.Size(267, 173);
            this.TilesList.TabIndex = 4;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1300, 24);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newGameToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(35, 20);
            this.toolStripMenuItem1.Text = "&File";
            // 
            // newGameToolStripMenuItem
            // 
            this.newGameToolStripMenuItem.Name = "newGameToolStripMenuItem";
            this.newGameToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.newGameToolStripMenuItem.Text = "&New Game";
            this.newGameToolStripMenuItem.Click += new System.EventHandler(this.newGameToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // stockgroupbox
            // 
            this.stockgroupbox.Controls.Add(this.zetaButton);
            this.stockgroupbox.Controls.Add(this.americaButton);
            this.stockgroupbox.Controls.Add(this.sacksonButton);
            this.stockgroupbox.Controls.Add(this.fusionButton);
            this.stockgroupbox.Controls.Add(this.hydraButton);
            this.stockgroupbox.Controls.Add(this.quantumButton);
            this.stockgroupbox.Controls.Add(this.phoenixButton);
            this.stockgroupbox.Location = new System.Drawing.Point(1011, 425);
            this.stockgroupbox.Name = "stockgroupbox";
            this.stockgroupbox.Size = new System.Drawing.Size(277, 287);
            this.stockgroupbox.TabIndex = 6;
            this.stockgroupbox.TabStop = false;
            this.stockgroupbox.Text = "Stock";
            // 
            // cashgroupbox
            // 
            this.cashgroupbox.Controls.Add(this.cashlabel);
            this.cashgroupbox.Location = new System.Drawing.Point(1009, 370);
            this.cashgroupbox.Name = "cashgroupbox";
            this.cashgroupbox.Size = new System.Drawing.Size(99, 49);
            this.cashgroupbox.TabIndex = 7;
            this.cashgroupbox.TabStop = false;
            this.cashgroupbox.Text = "Cash";
            // 
            // cashlabel
            // 
            this.cashlabel.AutoSize = true;
            this.cashlabel.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cashlabel.Location = new System.Drawing.Point(7, 20);
            this.cashlabel.Name = "cashlabel";
            this.cashlabel.Size = new System.Drawing.Size(30, 16);
            this.cashlabel.TabIndex = 0;
            this.cashlabel.Text = "$ 0";
            // 
            // doneButton
            // 
            this.doneButton.Location = new System.Drawing.Point(1019, 715);
            this.doneButton.Name = "doneButton";
            this.doneButton.Size = new System.Drawing.Size(263, 23);
            this.doneButton.TabIndex = 8;
            this.doneButton.Text = "Next Player";
            this.doneButton.UseVisualStyleBackColor = true;
            // 
            // playersGroupBox
            // 
            this.playersGroupBox.Location = new System.Drawing.Point(1009, 27);
            this.playersGroupBox.Name = "playersGroupBox";
            this.playersGroupBox.Size = new System.Drawing.Size(279, 132);
            this.playersGroupBox.TabIndex = 9;
            this.playersGroupBox.TabStop = false;
            this.playersGroupBox.Text = "Players";
            // 
            // tilesGroupBox
            // 
            this.tilesGroupBox.Controls.Add(this.TilesList);
            this.tilesGroupBox.Location = new System.Drawing.Point(1009, 165);
            this.tilesGroupBox.Name = "tilesGroupBox";
            this.tilesGroupBox.Size = new System.Drawing.Size(279, 199);
            this.tilesGroupBox.TabIndex = 10;
            this.tilesGroupBox.TabStop = false;
            this.tilesGroupBox.Text = "Tiles";
            // 
            // infoGroupbox
            // 
            this.infoGroupbox.Controls.Add(this.tilesLeftLabel);
            this.infoGroupbox.Controls.Add(this.stockLeftLabel);
            this.infoGroupbox.Location = new System.Drawing.Point(1115, 371);
            this.infoGroupbox.Name = "infoGroupbox";
            this.infoGroupbox.Size = new System.Drawing.Size(173, 48);
            this.infoGroupbox.TabIndex = 11;
            this.infoGroupbox.TabStop = false;
            this.infoGroupbox.Text = "Statistics";
            // 
            // tilesLeftLabel
            // 
            this.tilesLeftLabel.AutoSize = true;
            this.tilesLeftLabel.Location = new System.Drawing.Point(6, 29);
            this.tilesLeftLabel.Name = "tilesLeftLabel";
            this.tilesLeftLabel.Size = new System.Drawing.Size(86, 13);
            this.tilesLeftLabel.TabIndex = 0;
            this.tilesLeftLabel.Text = "Tiles left to play: ";
            // 
            // stockLeftLabel
            // 
            this.stockLeftLabel.AutoSize = true;
            this.stockLeftLabel.Location = new System.Drawing.Point(6, 16);
            this.stockLeftLabel.Name = "stockLeftLabel";
            this.stockLeftLabel.Size = new System.Drawing.Size(90, 13);
            this.stockLeftLabel.TabIndex = 0;
            this.stockLeftLabel.Text = "Stock left to buy: ";
            // 
            // zetaButton
            // 
            this.zetaButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.zetaButton.ForeColor = System.Drawing.Color.White;
            this.zetaButton.Image = global::Bitworthy.Games.Acquire.Properties.Resources.zeta;
            this.zetaButton.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.zetaButton.Location = new System.Drawing.Point(76, 217);
            this.zetaButton.Name = "zetaButton";
            this.zetaButton.Size = new System.Drawing.Size(124, 60);
            this.zetaButton.TabIndex = 0;
            this.zetaButton.Text = "0";
            this.zetaButton.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.zetaButton.UseVisualStyleBackColor = true;
            // 
            // americaButton
            // 
            this.americaButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.americaButton.ForeColor = System.Drawing.Color.White;
            this.americaButton.Image = global::Bitworthy.Games.Acquire.Properties.Resources.american;
            this.americaButton.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.americaButton.Location = new System.Drawing.Point(8, 151);
            this.americaButton.Name = "americaButton";
            this.americaButton.Size = new System.Drawing.Size(124, 60);
            this.americaButton.TabIndex = 0;
            this.americaButton.Text = "0";
            this.americaButton.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.americaButton.UseVisualStyleBackColor = true;
            // 
            // sacksonButton
            // 
            this.sacksonButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sacksonButton.ForeColor = System.Drawing.Color.White;
            this.sacksonButton.Image = global::Bitworthy.Games.Acquire.Properties.Resources.sackson;
            this.sacksonButton.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.sacksonButton.Location = new System.Drawing.Point(147, 151);
            this.sacksonButton.Name = "sacksonButton";
            this.sacksonButton.Size = new System.Drawing.Size(124, 60);
            this.sacksonButton.TabIndex = 0;
            this.sacksonButton.Text = "0";
            this.sacksonButton.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.sacksonButton.UseVisualStyleBackColor = true;
            // 
            // fusionButton
            // 
            this.fusionButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.fusionButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fusionButton.ForeColor = System.Drawing.Color.White;
            this.fusionButton.Image = global::Bitworthy.Games.Acquire.Properties.Resources.fusion;
            this.fusionButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.fusionButton.Location = new System.Drawing.Point(8, 85);
            this.fusionButton.Name = "fusionButton";
            this.fusionButton.Size = new System.Drawing.Size(124, 60);
            this.fusionButton.TabIndex = 0;
            this.fusionButton.Text = "0";
            this.fusionButton.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.fusionButton.UseVisualStyleBackColor = true;
            // 
            // hydraButton
            // 
            this.hydraButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hydraButton.ForeColor = System.Drawing.Color.White;
            this.hydraButton.Image = global::Bitworthy.Games.Acquire.Properties.Resources.hydra;
            this.hydraButton.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.hydraButton.Location = new System.Drawing.Point(147, 85);
            this.hydraButton.Name = "hydraButton";
            this.hydraButton.Size = new System.Drawing.Size(124, 60);
            this.hydraButton.TabIndex = 0;
            this.hydraButton.Text = "0";
            this.hydraButton.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.hydraButton.UseVisualStyleBackColor = true;
            // 
            // quantumButton
            // 
            this.quantumButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.quantumButton.ForeColor = System.Drawing.Color.White;
            this.quantumButton.Image = global::Bitworthy.Games.Acquire.Properties.Resources.quantum;
            this.quantumButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.quantumButton.Location = new System.Drawing.Point(147, 19);
            this.quantumButton.Name = "quantumButton";
            this.quantumButton.Size = new System.Drawing.Size(124, 60);
            this.quantumButton.TabIndex = 0;
            this.quantumButton.Text = "0";
            this.quantumButton.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.quantumButton.UseVisualStyleBackColor = true;
            // 
            // phoenixButton
            // 
            this.phoenixButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.phoenixButton.ForeColor = System.Drawing.Color.White;
            this.phoenixButton.Image = global::Bitworthy.Games.Acquire.Properties.Resources.phoenix;
            this.phoenixButton.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.phoenixButton.Location = new System.Drawing.Point(8, 19);
            this.phoenixButton.Name = "phoenixButton";
            this.phoenixButton.Size = new System.Drawing.Size(124, 60);
            this.phoenixButton.TabIndex = 0;
            this.phoenixButton.Text = "0";
            this.phoenixButton.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.phoenixButton.UseVisualStyleBackColor = true;
            // 
            // GameWindow
            // 
            this.ClientSize = new System.Drawing.Size(1300, 750);
            this.Controls.Add(this.infoGroupbox);
            this.Controls.Add(this.tilesGroupBox);
            this.Controls.Add(this.playersGroupBox);
            this.Controls.Add(this.doneButton);
            this.Controls.Add(this.cashgroupbox);
            this.Controls.Add(this.stockgroupbox);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "GameWindow";
            this.Text = "Acquire v 0.8";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.stockgroupbox.ResumeLayout(false);
            this.cashgroupbox.ResumeLayout(false);
            this.cashgroupbox.PerformLayout();
            this.tilesGroupBox.ResumeLayout(false);
            this.infoGroupbox.ResumeLayout(false);
            this.infoGroupbox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.ListBox TilesList;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem newGameToolStripMenuItem;
        private System.Windows.Forms.GroupBox stockgroupbox;
        private System.Windows.Forms.GroupBox cashgroupbox;
        private System.Windows.Forms.Label cashlabel;
        private System.Windows.Forms.Button americaButton;
        private System.Windows.Forms.Button sacksonButton;
        private System.Windows.Forms.Button fusionButton;
        private System.Windows.Forms.Button hydraButton;
        private System.Windows.Forms.Button quantumButton;
        private System.Windows.Forms.Button phoenixButton;
        private System.Windows.Forms.Button zetaButton;
        private System.Windows.Forms.Button doneButton;
        private System.Windows.Forms.GroupBox playersGroupBox;
        private System.Windows.Forms.GroupBox tilesGroupBox;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.GroupBox infoGroupbox;
        private System.Windows.Forms.Label stockLeftLabel;
        private System.Windows.Forms.Label tilesLeftLabel;

        public System.Windows.Forms.GroupBox PlayersGroupBox
        {
            get { return playersGroupBox; }
            set { playersGroupBox = value; }
        }
    }
}

