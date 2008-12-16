using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace KingsburgXNA.Screens
{
    public class MainGameScreen : BoardBackground
    {
        #region Properties

        Vector2 player1DataPosition = Vector2.Zero;
        Rectangle playerDataBounds = new Rectangle( 0, 0, 560, 720 );

        #endregion

        #region Constructors
        public MainGameScreen()
            : base()
        {
            this.Exiting += MainGameScreen_Exiting;
        }
        #endregion

        public override void Draw( GameTime gameTime )
        {
            SpriteBatch batch = ScreenManager.SpriteBatch;
            batch.Begin();
            batch.DrawString( Fonts.HudDetailFont, "Player Name", new Vector2( 30, 30 ), Color.Black );
            batch.End();
            base.Draw( gameTime );
        }


        #region Event Handlers
        protected void MainGameScreen_Exiting( object sender, EventArgs e )
        {
            
        }
        #endregion
    }
}
