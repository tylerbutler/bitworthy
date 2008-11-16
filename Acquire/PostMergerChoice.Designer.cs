namespace Bitworthy.Games.Acquire
{
    partial class PostMergerChoice
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
            this.questionLabel = new System.Windows.Forms.Label();
            this.inventoryGroupbox = new System.Windows.Forms.GroupBox();
            this.cashLabel = new System.Windows.Forms.Label();
            this.overtakenStockButton = new System.Windows.Forms.Label();
            this.overtakingStockLabel = new System.Windows.Forms.Label();
            this.sell1Button = new System.Windows.Forms.Button();
            this.trade2Button = new System.Windows.Forms.Button();
            this.keepButton = new System.Windows.Forms.Button();
            this.sellAllButton = new System.Windows.Forms.Button();
            this.mergeLabel = new System.Windows.Forms.Label();
            this.inventoryGroupbox.SuspendLayout();
            this.SuspendLayout();
            // 
            // questionLabel
            // 
            this.questionLabel.AutoSize = true;
            this.questionLabel.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.questionLabel.Location = new System.Drawing.Point(14, 38);
            this.questionLabel.Name = "questionLabel";
            this.questionLabel.Size = new System.Drawing.Size(259, 18);
            this.questionLabel.TabIndex = 0;
            this.questionLabel.Text = "What do you want to do with your stock?";
            // 
            // inventoryGroupbox
            // 
            this.inventoryGroupbox.Controls.Add(this.cashLabel);
            this.inventoryGroupbox.Controls.Add(this.overtakenStockButton);
            this.inventoryGroupbox.Controls.Add(this.overtakingStockLabel);
            this.inventoryGroupbox.Location = new System.Drawing.Point(17, 69);
            this.inventoryGroupbox.Name = "inventoryGroupbox";
            this.inventoryGroupbox.Size = new System.Drawing.Size(341, 72);
            this.inventoryGroupbox.TabIndex = 1;
            this.inventoryGroupbox.TabStop = false;
            this.inventoryGroupbox.Text = "Inventory";
            // 
            // cashLabel
            // 
            this.cashLabel.AutoSize = true;
            this.cashLabel.Location = new System.Drawing.Point(7, 51);
            this.cashLabel.Name = "cashLabel";
            this.cashLabel.Size = new System.Drawing.Size(34, 13);
            this.cashLabel.TabIndex = 0;
            this.cashLabel.Text = "Cash:";
            // 
            // overtakenStockButton
            // 
            this.overtakenStockButton.AutoSize = true;
            this.overtakenStockButton.Location = new System.Drawing.Point(6, 16);
            this.overtakenStockButton.Name = "overtakenStockButton";
            this.overtakenStockButton.Size = new System.Drawing.Size(89, 13);
            this.overtakenStockButton.TabIndex = 0;
            this.overtakenStockButton.Text = "Overtaken stock:";
            // 
            // overtakingStockLabel
            // 
            this.overtakingStockLabel.AutoSize = true;
            this.overtakingStockLabel.Location = new System.Drawing.Point(6, 34);
            this.overtakingStockLabel.Name = "overtakingStockLabel";
            this.overtakingStockLabel.Size = new System.Drawing.Size(91, 13);
            this.overtakingStockLabel.TabIndex = 0;
            this.overtakingStockLabel.Text = "Overtaking stock:";
            // 
            // sell1Button
            // 
            this.sell1Button.Location = new System.Drawing.Point(17, 148);
            this.sell1Button.Name = "sell1Button";
            this.sell1Button.Size = new System.Drawing.Size(175, 23);
            this.sell1Button.TabIndex = 2;
            this.sell1Button.Text = "Sell one (1) stock.";
            this.sell1Button.UseVisualStyleBackColor = true;
            // 
            // trade2Button
            // 
            this.trade2Button.Location = new System.Drawing.Point(17, 177);
            this.trade2Button.Name = "trade2Button";
            this.trade2Button.Size = new System.Drawing.Size(341, 23);
            this.trade2Button.TabIndex = 2;
            this.trade2Button.Text = "Trade two (2) stock for one (1).";
            this.trade2Button.UseVisualStyleBackColor = true;
            // 
            // keepButton
            // 
            this.keepButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.keepButton.Location = new System.Drawing.Point(17, 206);
            this.keepButton.Name = "keepButton";
            this.keepButton.Size = new System.Drawing.Size(341, 23);
            this.keepButton.TabIndex = 2;
            this.keepButton.Text = "Keep remaining stock (Done).";
            this.keepButton.UseVisualStyleBackColor = true;
            // 
            // sellAllButton
            // 
            this.sellAllButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.sellAllButton.Location = new System.Drawing.Point(198, 148);
            this.sellAllButton.Name = "sellAllButton";
            this.sellAllButton.Size = new System.Drawing.Size(160, 23);
            this.sellAllButton.TabIndex = 3;
            this.sellAllButton.Text = "Sell All";
            this.sellAllButton.UseVisualStyleBackColor = true;
            // 
            // mergeLabel
            // 
            this.mergeLabel.AutoSize = true;
            this.mergeLabel.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mergeLabel.Location = new System.Drawing.Point(14, 9);
            this.mergeLabel.Name = "mergeLabel";
            this.mergeLabel.Size = new System.Drawing.Size(117, 18);
            this.mergeLabel.TabIndex = 0;
            this.mergeLabel.Text = "X has acquired y.";
            // 
            // PostMergerChoice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(374, 238);
            this.ControlBox = false;
            this.Controls.Add(this.sellAllButton);
            this.Controls.Add(this.keepButton);
            this.Controls.Add(this.trade2Button);
            this.Controls.Add(this.sell1Button);
            this.Controls.Add(this.inventoryGroupbox);
            this.Controls.Add(this.mergeLabel);
            this.Controls.Add(this.questionLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "PostMergerChoice";
            this.Text = "PostMergerChoice";
            this.inventoryGroupbox.ResumeLayout(false);
            this.inventoryGroupbox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label questionLabel;
        private System.Windows.Forms.GroupBox inventoryGroupbox;
        private System.Windows.Forms.Button sell1Button;
        private System.Windows.Forms.Button trade2Button;
        private System.Windows.Forms.Button keepButton;
        private System.Windows.Forms.Button sellAllButton;
        private System.Windows.Forms.Label cashLabel;
        private System.Windows.Forms.Label overtakenStockButton;
        private System.Windows.Forms.Label overtakingStockLabel;
        private System.Windows.Forms.Label mergeLabel;
    }
}