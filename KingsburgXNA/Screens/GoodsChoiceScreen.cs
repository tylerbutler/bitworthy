using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TylerButler.Kingsburg.Core;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.GamerServices;

namespace KingsburgXNA.Screens
{
    class GoodsChoiceScreen : MessageBox
    {
        Game1 game;
        MenuComponent menu;
        GoodsChoiceOptions options;
        PlayerCollection players;
        XNAPlayer currentPlayer;

        public GoodsChoiceScreen( Game1 game, PlayerCollection players )
            : base( "Choose a good.", "Choose a good.", false )
        {
            this.game = game;
            this.players = new PlayerCollection( players ); // clone the original collection
            currentPlayer = (XNAPlayer)players[0];

            menu = new MenuComponent( game, Fonts.DescriptionFont );
            menu.Initialize();

            menu.AddText( "Gold" );
            menu.AddText( "Wood" );
            menu.AddText( "Stone" );

            menu.uiBounds = menu.GetExtents();
            menu.CenterMenu( this.BackgroundRectangle );

            menu.SelectedColor = Color.White;
            menu.UnselectedColor = Color.Black;

            menu.MenuOptionSelected += new MenuEventHandler( menu_MenuOptionSelected );
        }

        public GoodsChoiceScreen( Game1 game, PlayerCollection players, GoodsChoiceOptions options )
            : this( game, players )
        {
            this.options = options;
        }

        public override void HandleInput( InputState input )
        {
            menu.HandleInput( input, currentPlayer.Controller );
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
        }

        void menu_MenuOptionSelected( int selection )
        {
            switch( selection )
            {
                case 0: // Gold
                    currentPlayer.AddGood( GoodsChoiceOptions.Gold );
                    break;
                case 1: // Wood
                    currentPlayer.AddGood( GoodsChoiceOptions.Wood );
                    break;
                case 2: // Stone
                    currentPlayer.AddGood( GoodsChoiceOptions.Stone );
                    break;
            }
            players.Remove( (Player)currentPlayer );
            if( players.Count == 0 )
            {
                this.ExitScreen();
            }
            else
            {
                currentPlayer = (XNAPlayer)players[0];
            }
        }
    }
}
