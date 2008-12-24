#region File Description
//-----------------------------------------------------------------------------
// MenuScreen.cs
//
// XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.GamerServices;
#endregion

namespace KingsburgXNA.Screens
{
    public delegate void MenuEventHandler( int selection );

    public class MainMenuScreen : BoardBackgroundScreen
    {
        Rectangle uiBounds;
        Rectangle titleBounds;
        Vector2 selectPos;

        Texture2D title;
        Texture2D btnA;

        public MainMenuScreen( Game1 game )
            : base( game )
        {
        }

        private MenuComponent menu;
        public override void Initialize()
        {
            Viewport view = this.ScreenManager.GraphicsDevice.Viewport;
            int borderheight = (int)( view.Height * .05 );

            // Deflate 10% to provide for title safe area on CRT TVs
            //uiBounds = GetTitleSafeArea();

            //titleBounds = new Rectangle( uiBounds.X, uiBounds.Y, uiBounds.Width,
            //    (int)view.Height / 2 - borderheight );
            //titleBounds.Inflate(0, (int)(-view.Height * .4));

            selectPos = new Vector2( uiBounds.X + uiBounds.Width / 2 - 50, uiBounds.Bottom - 30 );

            menu = new MenuComponent( this.ScreenManager.Game, this.ScreenManager.Font );

            //Initialize Main Menu
            menu.Initialize();

            menu.AddText( "  Local Game", " - Up to two players on this console" );
            menu.AddText( "     Options", " - Change volume levels or controller configuration" );
            menu.AddText( "        Help", " - How to play" );
            menu.AddText( "        Quit", " - Return to Arcade" );
            menu.uiBounds = menu.GetExtents();
            menu.uiBounds.Offset( uiBounds.X, titleBounds.Bottom + 60 );
            menu.SelectedColor = Color.LightBlue;
            menu.MenuOptionSelected += new MenuEventHandler( menu_MenuOptionSelected );
            menu.MenuCancelled += new MenuEventHandler( menu_MenuCancelled );

            this.PresenceMode = GamerPresenceMode.AtMenu;

            base.Initialize();
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
                case 0: // Local Game
                    ExitScreen();
                    //( (Game1)this.ScreenManager.Game ).BeginSinglePlayer();
                    break;
                case 1: // Options       
                    //( (Game1)this.ScreenManager.Game ).DisplayOptions();
                    break;
                case 2: // Help
                    //ScreenManager.AddScreen( new HelpScreen() );
                    break;
                case 3: // Quit
                    //this.ScreenManager.Game.Exit();
                    break;
                default:
                    break;
            }
        }

        public override void LoadContent()
        {
            title = this.ScreenManager.Game.Content.Load<Texture2D>( "title" );
            btnA = this.ScreenManager.Game.Content.Load<Texture2D>( "xboxControllerButtonA" );

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
            base.Draw( gameTime );

            this.ScreenManager.SpriteBatch.Begin();
            this.ScreenManager.SpriteBatch.Draw( title, titleBounds, Color.LightBlue );
            this.ScreenManager.SpriteBatch.End();

            menu.Draw( gameTime );

            DrawSelect( selectPos, this.ScreenManager.SpriteBatch );
        }

        private void DrawSelect( Vector2 pos, SpriteBatch batch )
        {
            batch.Begin( SpriteBlendMode.AlphaBlend );
            batch.DrawString( this.ScreenManager.Font, "Select", pos, Color.White );
            pos.X += 76;
            pos.Y -= 3;
            batch.Draw( btnA, pos, null, Color.White, 0, Vector2.Zero, 0.33f, SpriteEffects.None, 1.0f );
            batch.End();
        }
    }
}
