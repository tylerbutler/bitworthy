using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TylerButler.Kingsburg.Core;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Storage;

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

        public XNAPlayer()
            : base()
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
            InitCommon( SignedInGamer.PlayerIndex, color );
        }

        public void InitLocal( PlayerIndex controller, string name, XNAPlayerColor color )
        {
            this.Name = name;
            InitCommon( controller, color );
        }

        private void InitCommon( PlayerIndex controller, XNAPlayerColor color )
        {
            this.Controller = controller;
            this.Color = color;
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