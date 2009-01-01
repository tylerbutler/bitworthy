using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using TylerButler.Kingsburg.Core;
using TylerButler.GameToolkit;

namespace KingsburgXNA.Screens
{
    public class MainGameScreen : BoardBackgroundScreen
    {
        #region Properties

        //Vector2 player1DataPosition = Vector2.Zero;
        //Rectangle playerDataBounds = new Rectangle( 0, 0, 560, 720 );
        //Texture2D playerImage;
        Rectangle playerImagePosition = new Rectangle( 43, 20, 64, 64 );

        #endregion

        #region Constructors

        public MainGameScreen( Game1 game )
            : base( game )
        {
        }

        #endregion

        public override void LoadContent()
        {
            base.LoadContent();
        }

        #region Draw

        public override void Draw( GameTime gameTime )
        {
            // draw the background first
            base.Draw( gameTime );

            SpriteBatch batch = ScreenManager.SpriteBatch;
            batch.Begin();
            batch.Draw( game.Data.CurrentPlayer.Picture, playerImagePosition, Color.White );
            batch.DrawString( Fonts.HudDetailFont, game.Data.CurrentPlayer.Name, new Vector2( 120, 20 ), Color.Black );
            DrawCurrentYear( batch );
            DrawPlayers( batch );
            batch.End();
        }

        private void DrawPlayers( SpriteBatch batch )
        {
            int i = 0;
            foreach( XNAPlayer p in game.Data.AllPlayers )
            {
                batch.Draw( StaticData.PlayerMarkerTextures[(int)p.Color], StaticData.PlayerOrderMarkerPositions[i], Color.White );
                i++;
            }
        }

        private void DrawCurrentYear( SpriteBatch batch )
        {
            int year = game.Data.CurrentYear;
            if( year > 0 && year <= 5 )
            {
                Vector2 position = StaticData.YearMarkerPositions[year - 1];
                batch.Draw( StaticData.YearMarkerTexture, position, Color.White );
            }
        }

        #endregion

        public override void HandleInput( InputState input )
        {
            base.HandleInput( input );
            InputManager.DebugExit( input, ScreenManager.Game );
        }

        #region Update

        public override void Update( GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen )
        {
            base.Update( gameTime, otherScreenHasFocus, coveredByOtherScreen );

            bool hidden = coveredByOtherScreen || otherScreenHasFocus;
            if( !hidden )
            {
                switch( game.Data.CurrentPhase )
                {
                    case PhasesEnum.Start:
                        // The game has just started, set the start phase to Phase1
                        game.Data.CurrentPhase = PhasesEnum.Phase1;
                        break;
                    case PhasesEnum.Phase1:
                        HandlePhase1();
                        break;
                }
            }
        }

        #endregion

        #region Handle the Phases

        #region Phase 1
        Phase phase;
        public void HandlePhase1()
        {
            phase = new Phase1( ( (Game1)this.ScreenManager.Game ).Data );
            Phase1 phase1 = (Phase1)phase;
            Phase1InfoScreen phaseInfo = new Phase1InfoScreen();
            phaseInfo.Accepted += new EventHandler<EventArgs>( phaseInfo_Accepted );
            ScreenManager.AddScreen( phaseInfo );
        }

        void phaseInfo_Accepted( object sender, EventArgs e )
        {
            // User has closed the phase info message, so do the other phase action
            Phase1 phase1 = (Phase1)phase;
            PlayerCollection pc = phase1.FindKingsAid();
            Phase1KingsAidScreen kingsAidScreen = new Phase1KingsAidScreen( pc );
            kingsAidScreen.Accepted += new EventHandler<EventArgs>( kingsAidScreen_Accepted );
            ScreenManager.AddScreen( kingsAidScreen );
        }

        void kingsAidScreen_Accepted( object sender, EventArgs e )
        {
            throw new NotImplementedException();
        }
        #endregion

        #endregion
    }
}
