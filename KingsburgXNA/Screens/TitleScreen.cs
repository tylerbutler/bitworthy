using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

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

            float menuStartVerticalPosition = 800;

            menu.AddText( "New Game", "Start a new game." );
            menu.AddText( "Exit", "Exit the game." );

            menu.uiBounds = menu.GetExtents();
            menu.uiBounds.Offset( menu.uiBounds.X, 300 );
            menu.SelectedColor = Color.MediumBlue;
            menu.MenuOptionSelected += new MenuEventHandler( menu_MenuOptionSelected );
            menu.MenuCanceled += new MenuEventHandler( menu_MenuCancelled );
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

            // now that they have textures, set the proper positions on the menu entries
            //for( int i = 0; i < MenuEntries.Count; i++ )
            //{
            //    MenuEntries[i].Position = new Vector2( MenuEntries[i].Position.X, 490 - ( 40f * ( MenuEntries.Count - 1 - i ) ) );
            //}

            base.LoadContent();
        }

        public override void Draw( GameTime gameTime )
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;

            spriteBatch.Begin();
            //spriteBatch.Draw( descriptionAreaTexture, descriptionAreaPosition, Color.White );

            // Draw each menu entry in turn.
            for( int i = 0; i < MenuEntries.Count; i++ )
            {
                MenuEntry menuEntry = MenuEntries[i];
                bool isSelected = IsActive && ( i == selectedEntry );
                menuEntry.Draw( this, isSelected, gameTime );
            }

            MenuEntry selectedMenuEntry = SelectedMenuEntry;
            if( selectedMenuEntry != null && !String.IsNullOrEmpty( selectedMenuEntry.Description ) )
            {
                spriteBatch.DrawString( Fonts.DescriptionFont, selectedMenuEntry.Description, new Vector2( 50, 50 ), Color.White );
            }

            // draw the select instruction
            spriteBatch.Draw( selectTexture, selectPosition, Color.White );
            spriteBatch.DrawString( Fonts.ButtonNamesFont, "Select",
                new Vector2(
                selectPosition.X - Fonts.ButtonNamesFont.MeasureString( "Select" ).X - 5,
                selectPosition.Y + 5 ), Color.White );

            spriteBatch.End();

            base.Draw( gameTime );
        }

        protected void exitGameSelected( object sender, EventArgs e )
        {
            base.OnCancel();
        }

        protected void newGameMenuEntry_Selected( object sender, EventArgs e )
        {
            ContentManager content = ScreenManager.Game.Content;
            //LoadingScreen.Load( ScreenManager, true, new MainGameScreen() );
            ScreenManager.AddScreen( new SignInScreen( this.Game ) );
            this.Game.TrySignIn( FinishStart );
        }
    }
}
