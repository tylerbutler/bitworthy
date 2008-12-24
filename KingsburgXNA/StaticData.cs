using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using TylerButler.GameToolkit;
using System.Text;
using System;

namespace KingsburgXNA
{
    public static class StaticData
    {
        #region Fields
        private static Texture2D defaultGamerPicture, yearMarkerTexture;
        private static Vector2[] playerOrderMarkerPositions = new Vector2[5]
        {
            new Vector2( 666, 156 ), // Player 1
            new Vector2( 666, 186 ), // Player 2
            new Vector2( 666, 217 ), // Player 3
            new Vector2( 666, 248 ), // Player 4
            new Vector2( 666, 282 )  // Player 5
        };

        private static Vector2[] yearMarkerPositions = new Vector2[5]
        {
            // Spaced 29 pixels apart on the y axis 
            new Vector2( 1194, 530 ), // Year 1
            new Vector2( 1194, 559 ), // Year 2
            new Vector2( 1194, 588 ), // Year 3
            new Vector2( 1194, 617 ), // Year 4
            new Vector2( 1194, 646 )  // Year 5
        };

        private static Texture2D[,] diceTextures = new Texture2D[Enum.GetValues( typeof( XNAPlayerColor ) ).Length, 6];
        private static Texture2D[] playerMarkerTextures = new Texture2D[Enum.GetValues( typeof( XNAPlayerColor ) ).Length];
        private static Texture2D[] buildingMarkerTextures = new Texture2D[Enum.GetValues( typeof( XNAPlayerColor ) ).Length];

        #endregion

        public static Texture2D DefaultGamerPicture
        {
            get
            {
                return defaultGamerPicture;
            }
            internal set
            {
                defaultGamerPicture = value;
            }
        }

        public static Texture2D YearMarkerTexture
        {
            get
            {
                return yearMarkerTexture;
            }
            internal set
            {
                yearMarkerTexture = value;
            }
        }

        public static Vector2[] YearMarkerPositions
        {
            get
            {
                return yearMarkerPositions;
            }
        }

        public static Texture2D[,] DiceTextures
        {
            get
            {
                return StaticData.diceTextures;
            }
            internal set
            {
                StaticData.diceTextures = value;
            }
        }

        public static Texture2D[] PlayerMarkerTextures
        {
            get
            {
                return playerMarkerTextures;
            }
            internal set
            {
                playerMarkerTextures = value;
            }
        }

        public static Texture2D[] BuildingMarkerTextures
        {
            get
            {
                return buildingMarkerTextures;
            }
            internal set
            {
                buildingMarkerTextures = value;
            }
        }

        public static Vector2[] PlayerOrderMarkerPositions
        {
            get
            {
                return playerOrderMarkerPositions;
            }
        }

        public static void LoadContent( ContentManager content )
        {
            DefaultGamerPicture = content.Load<Texture2D>( @"Images\SampleAvatar" );
            YearMarkerTexture = content.Load<Texture2D>( @"Images\YearMarker" );

            // Load Dice Textures
            StringBuilder b;
            foreach( XNAPlayerColor color in Enum.GetValues( typeof( XNAPlayerColor ) ) )
            {
                foreach( int j in new Range( 1, 6 ) )
                {
                    b = new StringBuilder( @"Images\Dice\" );
                    b.AppendFormat( "{0}{1}", j, color );
                    DiceTextures[(int)color, j - 1] = content.Load<Texture2D>( b.ToString() );
                }
            }

            // Load Player Marker Textures
            foreach( XNAPlayerColor color in Enum.GetValues( typeof( XNAPlayerColor ) ) )
            {
                if( color != XNAPlayerColor.White ) // no white players
                {
                    b = new StringBuilder( @"Images\PlayerMarkers\" );
                    b.AppendFormat( "Player{0}", color );
                    PlayerMarkerTextures[(int)color] = content.Load<Texture2D>( b.ToString() );
                }
            }

            // Load Building Marker Textures
            foreach( XNAPlayerColor color in Enum.GetValues( typeof( XNAPlayerColor ) ) )
            {
                if( color != XNAPlayerColor.White ) // no white players
                {
                    b = new StringBuilder( @"Images\BuildingMarkers\" );
                    b.AppendFormat( "BuildingToken{0}", color );
                    BuildingMarkerTextures[(int)color] = content.Load<Texture2D>( b.ToString() );
                }
            }
        }
    }
}
