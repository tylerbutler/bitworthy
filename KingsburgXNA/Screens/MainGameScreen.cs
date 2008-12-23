using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace KingsburgXNA.Screens
{
    public class MainGameScreen : BoardBackgroundScreen
    {
        #region Properties

        //Vector2 player1DataPosition = Vector2.Zero;
        //Rectangle playerDataBounds = new Rectangle( 0, 0, 560, 720 );
        Texture2D playerImage;
        Rectangle playerImagePosition = new Rectangle( 43, 20, 64, 64 );

        #endregion

        #region Constructors

        public MainGameScreen()
            : base()
        {
        }

        #endregion

        public override void LoadContent()
        {
            playerImage = ScreenManager.Game.Content.Load<Texture2D>( @"Images\SampleAvatar" );
            base.LoadContent();
        }

        public override void Draw( GameTime gameTime )
        {
            // draw the background first
            base.Draw( gameTime );

            SpriteBatch batch = ScreenManager.SpriteBatch;
            batch.Begin();
            batch.Draw( playerImage, playerImagePosition, Color.White );
            batch.DrawString( Fonts.HudDetailFont, "Player Name", new Vector2( 120, 20 ), Color.Black );
            batch.End();
        }
    }
}
