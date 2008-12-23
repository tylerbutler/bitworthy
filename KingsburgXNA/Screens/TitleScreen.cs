using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.GamerServices;

namespace KingsburgXNA.Screens
{
    public class TitleScreen : TitleBackgroundScreen
    {
        #region Fields

        private Texture2D selectTexture;
        private Vector2 selectPosition;

        MenuComponent menu;
        Game1 Game;

        #endregion


        public TitleScreen( Game1 game )
            : base()
        {
            this.Game = game;
            menu = new MenuComponent( Game, Fonts.DescriptionFont );

            menu.AddText( "New Game", "Start a new game." );
            menu.AddText( "Exit", "Exit the game." );

            menu.uiBounds = menu.GetExtents();
            menu.uiBounds.Offset( menu.uiBounds.X, 300 );
            menu.SelectedColor = Color.MediumBlue;
            menu.MenuOptionSelected += new MenuEventHandler( menu_MenuOptionSelected );
            menu.MenuCancelled += new MenuEventHandler( menu_MenuCancelled );
        }

        void menu_MenuCancelled( int selection )
        {
            // If they hit B or Back, go back to Start Screen
            ExitScreen();
            ScreenManager.AddScreen( new StartScreen( (Game1)ScreenManager.Game ) );
        }

        void menu_MenuOptionSelected( int selection )
        {
            switch( selection )
            {
                case 0: // New Game
                    ExitScreen();
                    ( (Game1)ScreenManager.Game ).StartGame();
                    break;
                case 1: // Exit
                    this.ScreenManager.Game.Exit();
                    break;
                default:
                    break;
            }
        }

        public override void LoadContent()
        {
            ContentManager content = ScreenManager.Game.Content;

            selectTexture = content.Load<Texture2D>( @"Images\Buttons\AButton" );
            selectPosition = new Vector2( 1120, 610 );

            base.LoadContent();
        }

        public override void HandleInput( InputState input )
        {
            menu.HandleInput( input );
            base.HandleInput( input );
        }

        public override void Update( GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen )
        {
            if( !coveredByOtherScreen && !Guide.IsVisible )
            {
                menu.Update( gameTime );
            }
            base.Update( gameTime, otherScreenHasFocus, coveredByOtherScreen );
        }

        public override void Draw( GameTime gameTime )
        {
            // Draw the background first
            base.Draw( gameTime );

            // Draw the menu
            menu.Draw( gameTime );

            // Draw the "press 'A' to select instructions
            DrawSelect( gameTime, selectPosition );
        }

        private void DrawSelect( GameTime gameTime, Vector2 position )
        {
            string selectString = "Select";
            SpriteFont font = Fonts.DescriptionFont;
            Vector2 stringMeasure = font.MeasureString( "Select" );
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;

            spriteBatch.Begin(SpriteBlendMode.AlphaBlend);
            spriteBatch.DrawString( Fonts.DescriptionFont, selectString, position, Color.White );
            position.X += stringMeasure.X;
            spriteBatch.Draw( selectTexture, position, null, Color.White, 0, Vector2.Zero, .33f, SpriteEffects.None, 1.0f );
            spriteBatch.End();
        }
    }
}
