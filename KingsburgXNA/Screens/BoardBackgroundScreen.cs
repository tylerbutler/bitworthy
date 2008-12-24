using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace KingsburgXNA.Screens
{
    public abstract class BoardBackgroundScreen : GameScreen
    {
        bool isBackgroundOn = false;
        
        Texture2D board;
        Texture2D buildings;
        Texture2D playerTitle;
        Texture2D gold, wood, stone;
        Texture2D info;

        Vector2 boardPosition = Vector2.Zero;
        Vector2 buildingsPosition = new Vector2( 1, 100 );
        Vector2 playerTitlePosition = Vector2.Zero;
        Vector2 goldPosition = new Vector2( 262, 0 );
        Vector2 woodPosition = new Vector2( 362, 0 );
        Vector2 stonePosition = new Vector2( 462, 0 );
        Rectangle infoPosition = new Rectangle( 277, 515, 283, 205 );

        public Game1 game;

        public BoardBackgroundScreen( Game1 game ) : base()
        {
            this.game = game;
        }
        
        public override void Initialize()
        {
            isBackgroundOn = true;
            base.Initialize();
        }

        public override void LoadContent()
        {
            ContentManager content = ScreenManager.Game.Content;
            board = content.Load<Texture2D>( @"Images\game_background" );
            buildings = content.Load<Texture2D>( @"Images\buildings" );
            playerTitle = content.Load<Texture2D>( @"Images\TitleBar" );
            gold = content.Load<Texture2D>( @"Images\Gold" );
            wood = content.Load<Texture2D>( @"Images\Wood" );
            stone = content.Load<Texture2D>( @"Images\Stone" );
            info = content.Load<Texture2D>( @"Images\PopupScreen" );
            base.LoadContent();
        }

        public override void Draw( Microsoft.Xna.Framework.GameTime gameTime )
        {
            SpriteBatch batch = ScreenManager.SpriteBatch;
            batch.Begin();
            //batch.GraphicsDevice.Clear( new Color( 148f, 140f, 105f ) );
            batch.Draw( board, boardPosition, Color.White );
            batch.Draw( buildings, buildingsPosition, Color.White );
            batch.Draw( playerTitle, playerTitlePosition, Color.White );
            batch.Draw( gold, goldPosition, Color.White );
            batch.Draw( wood, woodPosition, Color.White );
            batch.Draw( stone, stonePosition, Color.White );
            batch.Draw( info, infoPosition, Color.White );
            batch.End();
            base.Draw( gameTime );
        }
    }
}
