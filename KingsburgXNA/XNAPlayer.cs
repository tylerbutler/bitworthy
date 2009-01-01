using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TylerButler.Kingsburg.Core;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Storage;
using KingsburgXNA.Screens;
using Microsoft.Xna.Framework.Net;

namespace KingsburgXNA
{
    public class XNAPlayer : Player
    {
        public XNAPlayerColor Color;
        public SignedInGamer SignedInGamer;
        public Texture2D Picture;
        public bool IsPlaying = false;
        public PlayerIndex Controller;
        public StorageDevice Device;

        public XNAPlayer( GameManager gm )
            : base( gm )
        {
        }

        public new string Name
        {
            get
            {
                if( SignedInGamer != null )
                {
                    return SignedInGamer.Gamertag;
                }
                else
                {
                    return base.Name;
                }
            }

            set
            {
                base.Name = value;
            }
        }

        public void InitFromGamer( SignedInGamer gamer, XNAPlayerColor color )
        {
            this.SignedInGamer = gamer;
            Texture2D picture;

            try
            {
                GamerProfile profile = SignedInGamer.GetProfile();
                picture = profile.GamerPicture;
            }
            catch( NetworkException )
            {
                // Due to a bug in XNA Windows Live, this call might fail if the player has a picture
                // based on an Avatar from the NXE. Therefor, catching the exception and setting
                // the value to a default. See http://forums.xna.com/forums/p/21649/114949.aspx for
                // more info.
                picture = StaticData.DefaultGamerPicture;
            }
            InitCommon( SignedInGamer.PlayerIndex, picture, color );
        }

        public void InitLocal( PlayerIndex controller, string name, Texture2D gamerPicture, XNAPlayerColor color )
        {
            this.Name = name;
            InitCommon( controller, gamerPicture, color );
        }

        private void InitCommon( PlayerIndex controller, Texture2D picture, XNAPlayerColor color )
        {
            this.Controller = controller;
            this.Color = color;
            this.Picture = picture;
            this.IsPlaying = true;
        }

        public void GamerSignedOut( SignedInGamer gamer )
        {
            if( this.SignedInGamer == gamer )
            {
                this.SignedInGamer = null;
                this.Device = null;
                this.IsPlaying = false;
            }
        }
    }
}