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
using TylerButler.Kingsburg.Core;
using Marblets;

namespace TylerButler.Kingsburg.Windows
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class KingsburgWindowsGame : Game
    {
        private GraphicsDeviceManager graphics;
        private InputHelper inputHelper;
        private int previousWindowWidth = 1280;
        private int previousWindowHeight = 720;

        private UIManagerWindows uiManager;
        private ScreenManager screenManager;
        private GameManager gameManager;


        /// <summary>
        /// A cache of content used by the game
        /// </summary>
        public new static ContentManager Content;

        /// <summary>
        /// The game settings from settings.xml
        /// </summary>
        //public static Settings Settings = new Settings();

        /// <summary>
        /// The storage device that the game is saving high-scores to.
        /// </summary>
        public static StorageDevice StorageDevice = null;


        public KingsburgWindowsGame()
        {
            graphics = new GraphicsDeviceManager( this );
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;

            base.Content.RootDirectory = "Content";
            KingsburgWindowsGame.Content = base.Content;

            gameManager = GameManager.Instance;
            gameManager.UI = new UIManagerWindows( this );
            uiManager = (UIManagerWindows)gameManager.UI;
            screenManager = uiManager.ScreenManager;

            this.Components.Add( new GamerServicesComponent( this ) );
            this.Components.Add( screenManager );

            //gameManager.MainExecutionMethod();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            InputManager.Initialize();

            base.Initialize();

            screenManager.AddScreen( new TitleScreen() );
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // TODO: use this.Content to load your game content here
            Fonts.LoadContent( Content );
            base.LoadContent();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            base.UnloadContent();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update( GameTime gameTime )
        {
            InputManager.Update();

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

        #region Entry Point
        
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main( string[] args )
        {
            using( KingsburgWindowsGame game = new KingsburgWindowsGame() )
            {
                game.Run();
            }
        }

        #endregion
    }
}
