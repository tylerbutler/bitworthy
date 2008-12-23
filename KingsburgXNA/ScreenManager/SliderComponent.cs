using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KingsburgXNA
{
    public class SliderComponent : DrawableGameComponent
    {
        public int SliderUnits = 10;
        public Rectangle SliderArea = new Rectangle( 0, 0, 80, 28 );

        private int setting = 5;
        public int SliderSetting
        {
            get
            {
                return setting;
            }
            set
            {
                if( value > SliderUnits )
                    setting = SliderUnits;
                else if( value < 0 )
                    setting = 0;
                else
                    setting = value;
            }
        }
        public Color UnsetColor = Color.DodgerBlue;
        public Color SetColor = Color.Cyan;


        SpriteBatch batch;

        public SliderComponent( Game game, SpriteBatch batch )
            : base( game )
        {
            this.batch = batch;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        Texture2D blank;
        Vector2 origin;
        public new void LoadContent()
        {
            blank = Game.Content.Load<Texture2D>( "whitepixel" );
            origin = new Vector2( 0, blank.Height );
            if( batch == null )
                batch = new SpriteBatch( Game.GraphicsDevice );
            base.LoadContent();
        }

        public override void Draw( GameTime gameTime )
        {
            batch.Begin( SpriteBlendMode.AlphaBlend );
            for( int i = 1; i <= SliderUnits; i++ )
            {
                float percent = (float)i / SliderUnits;
                int height = (int)( percent * SliderArea.Height );

                int gaps = SliderUnits - 1;
                int width = (int)( SliderArea.Width / ( gaps + SliderUnits ) );

                int x= ( ( i - 1 ) * 2 * width );

                Rectangle displayArea = new Rectangle( SliderArea.Left + x, SliderArea.Bottom, width, height );

                if( i <= setting )
                    batch.Draw( blank, displayArea, null, SetColor, 0, origin, SpriteEffects.None, 1.0f );
                else
                    batch.Draw( blank, displayArea, null, UnsetColor, 0, origin, SpriteEffects.None, 1.0f );

            }

            batch.End();
            base.Draw( gameTime );
        }
    }
}
