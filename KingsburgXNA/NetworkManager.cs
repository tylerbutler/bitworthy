using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.GamerServices;

namespace KingsburgXNA
{
    public class NetworkManager : DrawableGameComponent
    {
        public NetworkManager( Game game )
            : base( game )
        {
            this.Visible = false;
        }

        /// <summary>
        /// Finds a SignedInGamer associated with a controller.
        /// </summary>
        /// <param name="index">The controller to query.</param>
        /// <returns>The SignedInGamer for the controller, or null if none is found.</returns>
        public static SignedInGamer FindGamer( PlayerIndex index )
        {
            foreach( SignedInGamer gamer in SignedInGamer.SignedInGamers )
            {
                if( gamer.PlayerIndex == index )
                    return gamer;
            }
            return null;
        }

    }
}
