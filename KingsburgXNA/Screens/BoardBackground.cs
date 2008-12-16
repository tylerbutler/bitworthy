using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace KingsburgXNA.Screens
{
    public class BoardBackground : GameScreen
    {
        Texture2D background;

        public override void LoadContent()
        {
            ContentManager content = ScreenManager.Game.Content;
            background = content.Load<Texture2D>( @"Images\game_background" );
            base.LoadContent();
        }

        public override void Draw( Microsoft.Xna.Framework.GameTime gameTime )
        {
            SpriteBatch batch = ScreenManager.SpriteBatch;
            batch.Begin();
            batch.Draw( background, Vector2.Zero, Color.White );
            batch.End();
            base.Draw( gameTime );
        }
    }
}
