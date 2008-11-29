namespace KingsburgWinForms
{
    partial class GetPlayersForm
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
            this.continueButton = new System.Windows.Forms.Button();
            this.choosePlayersLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // continueButton
            // 
            this.continueButton.Location = new System.Drawing.Point(339, 253);
            this.continueButton.Name = "continueButton";
            this.continueButton.Size = new System.Drawing.Size(75, 23);
            this.continueButton.TabIndex = 0;
            this.continueButton.Text = "Continue";
            this.continueButton.UseVisualStyleBackColor = true;
            // 
            // choosePlayersLabel
            // 
            this.choosePlayersLabel.AutoSize = true;
            this.choosePlayersLabel.Location = new System.Drawing.Point(13, 13);
            this.choosePlayersLabel.Name = "choosePlayersLabel";
            this.choosePlayersLabel.Size = new System.Drawing.Size(102, 13);
            this.choosePlayersLabel.TabIndex = 1;
            this.choosePlayersLabel.Text = "choosePlayersLabel";
            // 
            // GetPlayersForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(426, 288);
            this.Controls.Add(this.choosePlayersLabel);
            this.Controls.Add(this.continueButton);
            this.Name = "GetPlayersForm";
            this.Text = "GetPlayersForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button continueButton;
        private System.Windows.Forms.Label choosePlayersLabel;
    }
}