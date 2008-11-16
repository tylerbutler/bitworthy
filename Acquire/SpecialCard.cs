using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Bitworthy.Games.Acquire.Components
{
    public class SpecialCard : Bitworthy.Games.Acquire.Components.Card
    {
        private static Button free3Button, buy5Button, pick5Button, trade2Button, play3Button;
        public enum Type { Free3, Buy5, Pick5, Trade2, Play3 };

        internal static Button getButton(Type t)
        {
            switch (t)
            {
                case Type.Free3:
                    if (free3Button == null)
                    {
                        free3Button = new Button();
                        free3Button.Font = new System.Drawing.Font("Trebuchet MS", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        free3Button.ForeColor = System.Drawing.Color.White;
                        free3Button.Image = global::Bitworthy.Games.Acquire.Properties.Resources.special;
                        free3Button.Location = new System.Drawing.Point(292, 644);
                        free3Button.Name = "free3Button";
                        free3Button.Size = new System.Drawing.Size(137, 94);
                        free3Button.TabIndex = 11;
                        free3Button.Text = "3 Free Stock";
                        free3Button.TextAlign = System.Drawing.ContentAlignment.TopCenter;
                        free3Button.UseVisualStyleBackColor = true;
                    }
                    return free3Button;

                case Type.Buy5:
                    if (buy5Button == null)
                    {
                        buy5Button = new Button();
                        buy5Button.Font = new System.Drawing.Font("Trebuchet MS", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        buy5Button.ForeColor = System.Drawing.Color.White;
                        buy5Button.Image = global::Bitworthy.Games.Acquire.Properties.Resources.special;
                        buy5Button.Location = new System.Drawing.Point(435, 644);
                        buy5Button.Name = "buy5Button";
                        buy5Button.Size = new System.Drawing.Size(137, 94);
                        buy5Button.TabIndex = 11;
                        buy5Button.Text = "Buy 5 Stock";
                        buy5Button.TextAlign = System.Drawing.ContentAlignment.TopCenter;
                        buy5Button.UseVisualStyleBackColor = true;
                    }
                    return buy5Button;

                case Type.Pick5:
                    if (pick5Button == null)
                    {
                        pick5Button = new Button();
                        pick5Button.Font = new System.Drawing.Font("Trebuchet MS", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        pick5Button.ForeColor = System.Drawing.Color.White;
                        pick5Button.Image = global::Bitworthy.Games.Acquire.Properties.Resources.special;
                        pick5Button.Location = new System.Drawing.Point(578, 644);
                        pick5Button.Name = "pick5Button";
                        pick5Button.Size = new System.Drawing.Size(137, 94);
                        pick5Button.TabIndex = 11;
                        pick5Button.Text = "Pick 5 Tiles";
                        pick5Button.TextAlign = System.Drawing.ContentAlignment.TopCenter;
                        pick5Button.UseVisualStyleBackColor = true;
                    }
                    return pick5Button;

                case Type.Trade2:
                    if (trade2Button == null)
                    {
                        trade2Button = new Button();
                        trade2Button.Font = new System.Drawing.Font("Trebuchet MS", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        trade2Button.ForeColor = System.Drawing.Color.White;
                        trade2Button.Image = global::Bitworthy.Games.Acquire.Properties.Resources.special;
                        trade2Button.Location = new System.Drawing.Point(721, 644);
                        trade2Button.Name = "trade2Button";
                        trade2Button.Size = new System.Drawing.Size(137, 94);
                        trade2Button.TabIndex = 11;
                        trade2Button.Text = "Trade Stock 2:1";
                        trade2Button.TextAlign = System.Drawing.ContentAlignment.TopCenter;
                        trade2Button.UseVisualStyleBackColor = true;
                    }
                    return trade2Button;

                case Type.Play3:
                    if (play3Button == null)
                    {
                        play3Button = new Button();
                        play3Button.Font = new System.Drawing.Font("Trebuchet MS", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        play3Button.ForeColor = System.Drawing.Color.White;
                        play3Button.Image = global::Bitworthy.Games.Acquire.Properties.Resources.special;
                        play3Button.Location = new System.Drawing.Point(864, 644);
                        play3Button.Name = "play3Button";
                        play3Button.Size = new System.Drawing.Size(137, 94);
                        play3Button.TabIndex = 11;
                        play3Button.Text = "Play 3 Tiles";
                        play3Button.TextAlign = System.Drawing.ContentAlignment.TopCenter;
                        play3Button.UseVisualStyleBackColor = true;
                    }
                    return play3Button;

                default:
                    return null;
            }
        }
    }
}
