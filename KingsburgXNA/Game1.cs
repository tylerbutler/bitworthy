using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using KingsburgXNA.Screens;
using System.Diagnostics;

namespace KingsburgXNA
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        ScreenManager screenManager;
        InputManager inputManager;
        public GameData Data;

        public Game1()
        {
            graphics = new GraphicsDeviceManager( this );
            graphics.PreferredBackBufferHeight = 720;
            graphics.PreferredBackBufferWidth = 1280;

            Content.RootDirectory = "Content";

            // Create the screen manager component.
            screenManager = new ScreenManager( this );
            Components.Add( screenManager );

            // Create the Input manager
            inputManager = new InputManager( this );
            Components.Add( inputManager );

            // This gives us access to the guide
            Components.Add( new GamerServicesComponent( this ) );

            Data = new GameData( this );

            DebugSetup();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();

            screenManager.AddScreen( new StartScreen( this ) );
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch( GraphicsDevice );

            // TODO: use this.Content to load your game content here            
            Fonts.LoadContent( Content );
            StaticData.LoadContent( Content );
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update( GameTime gameTime )
        {
            // TODO: Add your update logic here

            base.Update( gameTime );
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw( GameTime gameTime )
        {
            GraphicsDevice.Clear( Color.CornflowerBlue );

            // TODO: Add your drawing code here

            base.Draw( gameTime );
        }

        public void TrySignIn( ScreenExited handler )
        {
            // Prompt for sign in if nobody is signed in
            if( SignedInGamer.SignedInGamers.Count == 0 )
            {
                SignInScreen screen = new SignInScreen( this );
                screen.ScreenExiting += handler;
                screenManager.AddScreen( screen );
            }
            else
                handler();
        }

        public void InitializePlayer1( PlayerIndex index )
        {
            if( !Data.Player1.IsPlaying )
            {
                SignedInGamer gamer = NetworkManager.FindGamer( index );
                if( gamer == null )  // No signed in gamer on this controller
                {
                    Data.Player1.InitLocal( index, "Player1", StaticData.DefaultGamerPicture, XNAPlayerColor.Blue );
                }
                else
                {
                    Data.Player1.InitFromGamer( gamer, XNAPlayerColor.Blue );
                }
            }
        }

        //public void InitializePlayer2( PlayerIndex index )
        //{
        //    if( !Data.Player2.IsPlaying )
        //    {
        //        SignedInGamer gamer = NetworkManager.FindGamer( index );
        //        if( gamer == null )  // No signed in gamer on this controller
        //        {
        //            Data.Player2.InitLocal( index, "Player 2", StaticData.DefaultGamerPicture, XNAPlayerColor.Red );
        //        }
        //        else
        //        {
        //            Data.Player2.InitFromGamer( gamer, XNAPlayerColor.Red );
        //        }
        //    }
        //}

        public void StartGame()
        {
            ResetGame();
            Data.StartGame();
            screenManager.AddScreen( new MainGameScreen( this ) );
        }

        public void ResetGame()
        {
            //this.Data = new GameData( this );
            Data.CurrentPlayer = Data.Player1;
        }

        [Conditional( "DEBUG" )]
        private void DebugSetup()
        {
            this.screenManager.TraceEnabled = true;
        }
    }
}