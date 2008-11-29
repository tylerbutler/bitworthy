using Marblets;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TylerButler.Kingsburg.Windows
{
    public class MainGameScreen : Screen
    {
        private GameBoard board;

        public MainGameScreen( Game game, string backgroundImage, SoundEntry backgroundMusic )
            : base( game, backgroundImage, backgroundMusic )
        {
            board = new GameBoard( game );
        }

        public override void Initialize()
        {
            base.Initialize();
            board.Initialize();
        }

        public override void Update( GameTime gameTime )
        {
            base.Update( gameTime );
            board.Update( gameTime );
        }

        public override void Draw( GameTime gameTime )
        {
            base.Draw( gameTime );

            //SpriteBatch.Begin( SpriteBlendMode.AlphaBlend, SpriteSortMode.Deferred,
            //                  SaveStateMode.None );

            board.Draw( SpriteBatch );

            //Draw Score
            //Font.Draw( SpriteBatch, FontStyle.Small, 946, 140,
            //          String.Format( "{0:###,##0}", MarbletsGame.Score ) );

            if( board.GameOver )
            {
                //SpriteBatch.Draw( gameOver, new Vector2( 329, 264 ), Color.White );
            }

            SpriteBatch.End();

        }
    }
}
