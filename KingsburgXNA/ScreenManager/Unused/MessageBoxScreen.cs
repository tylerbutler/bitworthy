#region File Description
//-----------------------------------------------------------------------------
// MessageBoxScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace KingsburgXNA.Screens
{
    abstract class MessageBoxScreen : GameScreen
    {
        #region Fields

        string message, title;
        bool isCancellable = true;

        private string backgroundTexturePath;
        private Texture2D backgroundTexture;
        private Vector2 backgroundPosition;

        private string loadingBlackTexturePath = @"Images\FadeScreen";
        private Texture2D loadingBlackTexture;
        private Rectangle loadingBlackTextureDestination;

        private string backTexturePath;
        private Texture2D backTexture;
        private Vector2 backPosition;

        private string selectTexturePath;
        private Texture2D selectTexture;
        private Vector2 selectPosition;

        private Vector2 confirmPosition, messagePosition;

        #endregion

        #region Fields
        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
            }
        }

        public string Message
        {
            get
            {
                return message;
            }
            set
            {
                message = value;
            }
        }
        #endregion

        #region Events

        public event EventHandler<EventArgs> Accepted;
        public event EventHandler<EventArgs> Cancelled;

        #endregion


        #region Initialization

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message">The message that should be displayed.</param>
        /// <param name="isCancellable">Whether the message box can be cancelled.</param>
        public MessageBoxScreen( string title, string message, bool isCancellable )
        {
            this.title = title;
            this.message = message;
            this.isCancellable = isCancellable;

            IsPopup = true;

            TransitionOnTime = TimeSpan.FromSeconds( 0.2 );
            TransitionOffTime = TimeSpan.FromSeconds( 0.2 );
        }

        public MessageBoxScreen( string title, string message, bool isCancellable, string pathToBackgroundTexture,
            string pathToBackButtonTexture, string pathToConfirmButtonTexture )
            : this( title, message, isCancellable )
        {
            this.backTexturePath = pathToBackButtonTexture;
            this.backgroundTexturePath = pathToBackgroundTexture;
            this.selectTexturePath = pathToConfirmButtonTexture;
        }


        /// <summary>
        /// Loads graphics content for this screen. This uses the shared ContentManager
        /// provided by the Game class, so the content will remain loaded forever.
        /// Whenever a subsequent MessageBoxScreen tries to load this same content,
        /// it will just get back another reference to the already loaded data.
        /// </summary>
        public override void LoadContent()
        {
            ContentManager content = ScreenManager.Game.Content;

            backgroundTexture = content.Load<Texture2D>( backgroundTexturePath );
            backTexture = content.Load<Texture2D>( backTexturePath );
            selectTexture = content.Load<Texture2D>( selectTexturePath );
            loadingBlackTexture =
                content.Load<Texture2D>( loadingBlackTexturePath );

            Viewport viewport = ScreenManager.GraphicsDevice.Viewport;
            backgroundPosition = new Vector2(
                ( viewport.Width - backgroundTexture.Width ) / 2,
                ( viewport.Height - backgroundTexture.Height ) / 2 );
            loadingBlackTextureDestination = new Rectangle( viewport.X, viewport.Y,
                viewport.Width, viewport.Height );

            backPosition = backgroundPosition + new Vector2( 50f,
                backgroundTexture.Height - 100 );
            selectPosition = backgroundPosition + new Vector2(
                backgroundTexture.Width - 100, backgroundTexture.Height - 100 );

            confirmPosition.X = backgroundPosition.X + ( backgroundTexture.Width -
                Fonts.HeaderFont.MeasureString( this.title ).X ) / 2f;
            confirmPosition.Y = backgroundPosition.Y + 47;

            message = Fonts.BreakTextIntoLines( message, 36, 10 );
            messagePosition.X = backgroundPosition.X + (int)( ( backgroundTexture.Width -
                Fonts.GearInfoFont.MeasureString( message ).X ) / 2 );
            messagePosition.Y = ( backgroundPosition.Y * 2 ) - 20;
        }

        public override void Initialize()
        {
            //backPosition = new Vector2( ( backgroundTexture.Width - backTexture.Width ) / 2,
            //    ( backgroundTexture.Height - backgroundTexture.Height ) / 2 );

            base.Initialize();
        }

        #endregion

        #region Handle Input

        /// <summary>
        /// Responds to user input, accepting or cancelling the message box.
        /// </summary>
        public override void HandleInput( InputState input )
        {
            if( input.CheckAction( Action.OK ) )
            {
                // Raise the accepted event, then exit the message box.
                if( Accepted != null )
                    Accepted( this, EventArgs.Empty );

                ExitScreen();
            }
            else if( input.CheckAction( Action.Back ) && isCancellable )
            {
                // Raise the cancelled event, then exit the message box.
                if( Cancelled != null )
                    Cancelled( this, EventArgs.Empty );

                ExitScreen();
            }
        }


        #endregion

        #region Draw

        /// <summary>
        /// Draws the message box.
        /// </summary>
        public override void Draw( GameTime gameTime )
        {
            SpriteBatch batch = ScreenManager.SpriteBatch;

            batch.Begin();

            batch.Draw( loadingBlackTexture, loadingBlackTextureDestination,
                Color.White );
            batch.Draw( backgroundTexture, backgroundPosition, Color.White );
            batch.Draw( selectTexture, selectPosition, Color.White );
            if( isCancellable )
            {
                batch.Draw( backTexture, backPosition, Color.White );
                batch.DrawString( Fonts.ButtonNamesFont, "Cancel",
                                    new Vector2( backPosition.X + backTexture.Width + 5, backPosition.Y + 5 ),
                                    Color.White );
            }
            batch.DrawString( Fonts.ButtonNamesFont, "OK",
                new Vector2( selectPosition.X - Fonts.ButtonNamesFont.MeasureString( "Yes" ).X,
                selectPosition.Y + 5 ), Color.White );
            batch.DrawString( Fonts.HeaderFont, "Confirmation", confirmPosition,
                Fonts.CaptionColor );
            batch.DrawString( Fonts.CaptionFont, Fonts.BreakTextIntoLines( message, 300 ), messagePosition,
                Fonts.CaptionColor );

            batch.End();
        }

        #endregion
    }
}
