using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using TylerButler.Kingsburg.Core;

namespace KingsburgXNA.Screens
{
    class MessageBox : MessageBoxScreen
    {
        public MessageBox( string title, string message, bool isCancellable )
            : base( title, message, isCancellable, @"Images\Scroll", @"Images\Buttons\BButton", @"Images\Buttons\AButton" )
        {
        }

        public override void Draw( GameTime gameTime )
        {
            base.Draw( gameTime );
        }
    }
}
