using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TylerButler.Kingsburg.Core;
using Microsoft.Xna.Framework;
using TylerButler.GameToolkit;

namespace KingsburgXNA.Screens
{
    class PhaseInfoScreen : MessageBox
    {
        bool firstUpdate = true;
        Phase phase;

        public PhaseInfoScreen( Phase phase )
            : base( "", "", false )
        {
            this.phase = phase;
            this.Title = phase.Title;
            this.Message = phase.Description;
        }

        public override void Update( GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen )
        {
            base.Update( gameTime, otherScreenHasFocus, coveredByOtherScreen );

            Type type = phase.GetType();

            if( type == typeof( Phase1 ) )
            {
                if( firstUpdate )
                {
                    firstUpdate = false;
                    GameData data = ( (Game1)ScreenManager.Game ).Data;
                    data.CurrentYear++;
                }
            }
            else if( type == typeof( Phase2 ) )
            {

            }
        }
    }

    class Phase1KingsAidScreen : MessageBox
    {
        //bool firstUpdate = true;

        public Phase1KingsAidScreen( PlayerCollection players )
            : base( "King's Aid", players.ToString(), false )
        {
            this.Message = String.Format( "{0} received a white die for the spring season because the king gave him aid.", players.ToString() );

            if( players.Count > 1 ) // multiple players, so each gets to choose a good
            {
                GoodsChoiceScreen chooseGoods = new GoodsChoiceScreen( (Game1)ScreenManager.Game, players );
                ScreenManager.AddScreen( chooseGoods );
                this.ExitScreen();
            }
            else //Only one player, so he gets a white die
            {
                foreach( Player p in players )
                {
                    p.AddDie( new KingsburgDie( KingsburgDie.DieTypes.White ) );
                }
            }
        }

        //public override void Update( GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen )
        //{
        //    base.Update( gameTime, otherScreenHasFocus, coveredByOtherScreen );

        //    if( firstUpdate )
        //    {
        //        firstUpdate = false;
        //        Game1 g = (Game1)this.ScreenManager.Game;
        //    }
        //}
    }

}
