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
        static Phase1 phase1 = new Phase1();
        bool firstUpdate = true;

        public Phase1InfoScreen()
            : base( phase1.Title, phase1.Description, false )
        {
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
