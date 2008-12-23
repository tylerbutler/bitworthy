using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.GamerServices;

namespace KingsburgXNA.Screens
{
    public delegate void ScreenExited();

    class SignInScreen : TitleBackgroundScreen
    {
        #region Fields

        public event ScreenExited ScreenExiting;
        bool GuideShown = false;
        Game1 Game;

        #endregion

        #region Constructors

        public SignInScreen( Game1 game )
            : base()
        {
            this.Game = game;
        }

        #endregion

        #region Initialization

        public override void Initialize()
        {
            this.IsPopup = false;
            base.Initialize();
        }

        #endregion

        #region Draw
        public override void Draw( GameTime gameTime )
        {
            base.Draw( gameTime );
        }
        #endregion

        public override void Update( GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen )
        {
            // If we haven't activated the guide yet, and it's not up for another purpose,
            // activate it now
            if( ( !GuideShown ) && ( !Guide.IsVisible ) )
            {
                Guide.ShowSignIn( 1, false );
                GuideShown = true;
            }
            else if( ( GuideShown ) && ( Guide.IsVisible ) )
            {
                // If the guide is up, do nothing
            }
            // Screen must have been shown and closed
            else if( !Guide.IsVisible )
            {
                // Activate our callback function and exit
                if( ScreenExiting != null )
                {
                    ScreenExiting();
                }
                ExitScreen();
            }
            base.Update( gameTime, otherScreenHasFocus, coveredByOtherScreen );
        }
    }
}
