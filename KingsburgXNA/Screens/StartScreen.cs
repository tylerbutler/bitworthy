using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KingsburgXNA.Screens
{
    public class StartScreen : TitleBackgroundScreen
    {
        private Game1 game;
        private bool exiting = false;
        private PlayerIndex player1Controller;

        public StartScreen( Game1 game )
            : base()
        {
            this.game = game;
        }

        public override void Update( GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen )
        {
            if( !exiting && InputManager.CheckPlayerOneStart( out player1Controller ) )
            {
                exiting = true;

                //this will load the sign in screen, and then exit this screen after players have signed in
                game.TrySignIn( FinishStartScreen );
            }
            base.Update( gameTime, otherScreenHasFocus, coveredByOtherScreen );
        }

        private void FinishStartScreen()
        {
            game.InitializePlayer1( player1Controller );

            // players have signed in, time to bring up the title screen with menus
            this.ScreenManager.AddScreen( new TitleScreen( game ) );
            this.ExitScreen();
        }

        public override void Draw( GameTime gameTime )
        {
            //draw background first
            base.Draw( gameTime );

            SpriteBatch batch = ScreenManager.SpriteBatch;
            Viewport viewport = ScreenManager.Game.GraphicsDevice.Viewport;
            Vector2 stringSize = Fonts.HeaderFont.MeasureString( "Press Start" );
            Vector2 position = new Vector2( ( viewport.Width - stringSize.X ) / 2, viewport.Height - stringSize.Y - 20 );

            batch.Begin();
            batch.DrawString( Fonts.HeaderFont, "Press Start", position, Color.White );
            batch.End();
        }
    }
}
