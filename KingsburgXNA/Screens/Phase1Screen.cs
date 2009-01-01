using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TylerButler.Kingsburg.Core;
using Microsoft.Xna.Framework;

namespace KingsburgXNA.Screens
{
    class Phase1InfoScreen : MessageBox
    {
        bool firstUpdate = true;

        public Phase1InfoScreen()
            : base( "", "", false )
        {
            Phase1 phase1 = new Phase1( ( (Game1)this.ScreenManager.Game ).Data );
            this.Title = phase1.Title;
            this.Message = phase1.Description;
        }

        public override void Update( GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen )
        {
            base.Update( gameTime, otherScreenHasFocus, coveredByOtherScreen );

            if( firstUpdate )
            {
                firstUpdate = false;
                GameData data = ( (Game1)ScreenManager.Game ).Data;
                data.CurrentYear++;
            }
        }
    }

    class Phase1KingsAidScreen : MessageBox
    {
        //bool firstUpdate = true;

        public Phase1KingsAidScreen( PlayerCollection players )
            : base( "King's Aid", players.ToString(), false )
        {
        }

        //public override void Update( GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen )
        //{
        //    base.Update( gameTime, otherScreenHasFocus, coveredByOtherScreen );

        //    if( firstUpdate )
        //    {
        //        firstUpdate = false;

        //        Phase1 p = new Phase1();
        //        PlayerCollection pc = p.FindKingsAid();
        //        this = new 
        //    }
        //}
    }

}
