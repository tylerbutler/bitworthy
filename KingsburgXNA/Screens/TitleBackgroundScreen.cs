using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace KingsburgXNA.Screens
{
    public abstract class TitleBackgroundScreen : GameScreen
    {
        private Texture2D backgroundTexture;
        private Vector2 backgroundPosition;

        public override void LoadContent()
        {
            backgroundTexture = ScreenManager.Game.Content.Load<Texture2D>( @"Images\MainMenu\title_background" );
            backgroundPosition = Vector2.Zero;

            base.LoadContent();
        }

        public override void Draw( GameTime gameTime )
        {
            SpriteBatch batch = ScreenManager.SpriteBatch;
            batch.Begin();
            batch.Draw( backgroundTexture, backgroundPosition, Color.White );
            batch.End();

            base.Draw( gameTime );
        }
    }
}