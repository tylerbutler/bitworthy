using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace KingsburgXNA.Screens
{
    public class TitleScreen : MenuScreen
    {
        #region Graphics Data

        private Texture2D backgroundTexture;
        private Vector2 backgroundPosition;

        private Texture2D descriptionAreaTexture;
        private Vector2 descriptionAreaPosition;
        private Vector2 descriptionAreaTextPosition;

        //private Texture2D iconTexture;
        //private Vector2 iconPosition;

        //private Texture2D backTexture;
        //private Vector2 backPosition;

        private Texture2D selectTexture;
        private Vector2 selectPosition;

        //private Texture2D plankTexture1, plankTexture2, plankTexture3;

        #endregion

        MenuEntry newGameMenuEntry, exitGameMenuEntry;

        public TitleScreen()
            : base()
        {
            float menuStartVerticalPosition = 800;

            newGameMenuEntry = new MenuEntry( "New Game" );
            newGameMenuEntry.Description = "Start a new game.";
            newGameMenuEntry.Font = Fonts.HeaderFont;
            newGameMenuEntry.Position = new Vector2( menuStartVerticalPosition, 0 );
            newGameMenuEntry.Selected += newGameMenuEntry_Selected;
            MenuEntries.Add( newGameMenuEntry );

            exitGameMenuEntry = new MenuEntry( "Exit" );
            exitGameMenuEntry.Description = "Exit the game.";
            exitGameMenuEntry.Font = Fonts.HeaderFont;
            exitGameMenuEntry.Position = new Vector2( menuStartVerticalPosition, 0 );
            exitGameMenuEntry.Selected += exitGameSelected;
            MenuEntries.Add( exitGameMenuEntry );
        }

        public override void LoadContent()
        {
            ContentManager content = ScreenManager.Game.Content;
            backgroundTexture = content.Load<Texture2D>( @"Images\MainMenu\title_background" );
            backgroundPosition = Vector2.Zero;

            descriptionAreaTexture = content.Load<Texture2D>( @"Images\Scroll" );
            descriptionAreaPosition = new Vector2( 720, 340 );

            selectTexture = content.Load<Texture2D>( @"Images\Buttons\AButton" );
            selectPosition = new Vector2( 1120, 610 );

            // now that they have textures, set the proper positions on the menu entries
            for( int i = 0; i < MenuEntries.Count; i++ )
            {
                MenuEntries[i].Position = new Vector2( MenuEntries[i].Position.X, 490 - ( 40f * ( MenuEntries.Count - 1 - i ) ) );
            }


            base.LoadContent();
        }

        public override void Draw( GameTime gameTime )
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;

            spriteBatch.Begin();
            spriteBatch.Draw( backgroundTexture, backgroundPosition, Color.White );
            spriteBatch.Draw( descriptionAreaTexture, descriptionAreaPosition, Color.White );

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
            LoadingScreen.Load( ScreenManager, true, new MainGameScreen() );
        }

    }
}
