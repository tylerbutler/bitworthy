using Marblets;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace TylerButler.Kingsburg.Windows
{
    public class MainGameScreen : GameScreen
    {
        #region Properties


        #endregion

        #region Constructors
        public MainGameScreen()
            : base()
        {
            this.Exiting += MainGameScreen_Exiting;
        }
        #endregion

        #region Event Handlers
        protected void MainGameScreen_Exiting( object sender, EventArgs e )
        {
            
        }
        #endregion
    }
}
