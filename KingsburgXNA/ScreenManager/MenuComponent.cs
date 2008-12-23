using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.GamerServices;

namespace KingsburgXNA.Screens
{
    public class MenuComponent : DrawableGameComponent
    {
        private SpriteBatch batch;
        private List<string> MenuItems;
        private List<string> HelpText;
        private List<int> ItemHeights;
        private List<int> ItemWidths;

        public Rectangle uiBounds;
        public SpriteFont Font;
        public Color SelectedColor = Color.White;
        public Color UnselectedColor = Color.LightGray;
        public int Selection = 0;
        public PlayerIndex Controller;

        public event MenuEventHandler MenuOptionSelected;
        public event MenuEventHandler MenuCanceled;

        #region Constructors

        public MenuComponent( Game game )
            : base( game )
        {
            this.MenuItems = new List<string>();
            this.HelpText = new List<string>();
            this.ItemHeights = new List<int>();
            this.ItemWidths = new List<int>();
        }

        public MenuComponent( Game game, SpriteFont font )
            : this( game )
        {
            this.Font = font;
        }

        public MenuComponent( Game game, SpriteFont font, SpriteBatch batch )
            : this( game, font )
        {
            this.batch = batch;
        }

        public MenuComponent( Game game, SpriteFont font, SpriteBatch batch, Rectangle bounds )
            : this( game, font, batch )
        {
            this.uiBounds = bounds;
        }

        #endregion

        public int Count
        {
            get
            {
                return MenuItems.Count;
            }
        }

        public void Clear()
        {
            MenuItems.Clear();
            HelpText.Clear();
            ItemHeights.Clear();
            ItemWidths.Clear();

            Selection = 0;
        }

        public void AddText( string menu, string help )
        {
            MenuItems.Add( menu );
            HelpText.Add( help );
            Vector2 result = Font.MeasureString( menu + help );
            ItemHeights.Add( RoundUp( result.Y ) );
            ItemWidths.Add( RoundUp( result.X ) );
        }

        public void AddText( string menu )
        {
            AddText( menu, "" );
        }

        public void RemoveAt( int index )
        {
            MenuItems.RemoveAt( index );
            HelpText.RemoveAt( index );
            ItemHeights.RemoveAt( index );
            ItemWidths.RemoveAt( index );
        }

        protected override void LoadContent()
        {
            if( batch == null )
                batch = new SpriteBatch( GraphicsDevice );

            base.LoadContent();
        }

        public void HandleInput( InputState input )
        {
            // If back or B are pressed, cancel menu
            if( input.IsNewButtonPress( Buttons.B ) ||
                input.IsNewButtonPress( Buttons.Back ) )
            {
                MenuCanceled.Invoke( -1 );
                return;
            }

            if( input.CheckAction( Action.OK ) )
            {
                if( MenuOptionSelected != null )
                    MenuOptionSelected( Selection );

                return;
            }

            if( input.IsNewButtonPress( Buttons.DPadDown ) ||
                input.IsNewButtonPress( Buttons.LeftThumbstickDown ) )
            {
                Selection++;
            }
            if( input.IsNewButtonPress( Buttons.DPadUp ) ||
                input.IsNewButtonPress( Buttons.LeftThumbstickUp ) )
            {
                Selection--;
            }

            if( Selection >= MenuItems.Count )
                Selection -= MenuItems.Count;
            if( Selection < 0 )
                Selection += MenuItems.Count;
        }

        private static int RoundUp( float value )
        {
            int retval = (int)value;
            if( value > retval )
                retval++;
            return retval;
        }

        // Get the menu size, in pixels
        public Rectangle GetExtents()
        {
            int width = 0;
            int height = 0;
            for( int i = 0; i < MenuItems.Count; i++ )
            {
                // Take the width of the widest string
                width = Math.Max( width, ItemWidths[i] );
                // Combined with the height of all the strings
                height += ItemHeights[i];
            }
            return new Rectangle( uiBounds.X, uiBounds.Y, width, height );
        }
        public Rectangle GetExtent( int index )
        {
            int totalheight = 0;
            for( int i = 0; i < index; i++ )
            {
                totalheight += ItemHeights[i];
            }
            return new Rectangle( uiBounds.X, uiBounds.Y + totalheight, ItemWidths[index], ItemHeights[index] );
        }
        private Viewport CreateViewport()
        {
            Viewport view = new Viewport(); // create a new viewport
            view.X = uiBounds.X;       // using our UIBounds
            view.Y = uiBounds.Y;
            view.Width = uiBounds.Width;
            view.Height = uiBounds.Height;
            return view;
        }
        /// <summary>
        /// Center the menu in a given viewport
        /// </summary>
        /// <param name="view">The viewport to center inside</param>
        public void CenterMenu( Viewport view )
        {
            Vector2 centerView = new Vector2( view.X + ( view.Width / 2 ),
                view.Y + ( view.Height / 2 ) );
            Rectangle bounds = GetExtents();
            bounds.X = 0;
            bounds.Y = 0;
            bounds.Offset( (int)( centerView.X - ( bounds.Width / 2 ) ),
                (int)( centerView.Y - ( bounds.Height / 2 ) ) );
            this.uiBounds = bounds;
        }

        private Vector2 GetTopLeft()
        {
            int selectheight = ItemHeights[Selection];
            int aboveheight = 0;
            int belowheight = 0;
            for( int i = 0; i < Selection; i++ )
            {
                aboveheight += ItemHeights[i];
            }
            for( int i = Selection + 1; i < ItemHeights.Count; i++ )
            {
                belowheight += ItemHeights[i];
            }
            int totalheight = selectheight + aboveheight + belowheight;

            // If there aren't enough items above to be worth scrolling down
            if( aboveheight + ( selectheight / 2 ) <= uiBounds.Height / 2 )
            {
                // Display the menu with no scrolling
                return new Vector2( 0, 0 );
            }
            // or if there aren't enough items below to be worth scrolling up
            else if( belowheight + ( selectheight / 2 ) < uiBounds.Height / 2 )
            {
                // Display the menu scrolled to the bottom
                return new Vector2( 0, uiBounds.Height - totalheight );
            }
            else
            {
                // scroll the display to put the selection near the center   
                int temp = aboveheight + ( selectheight / 2 );
                return new Vector2( 0, uiBounds.Height / 2 - temp );
            }


        }

        public override void Draw( GameTime gameTime )
        {
            if( Count == 0 )
                return;

            Vector2 current = GetTopLeft();

            /* Setup a viewport so that we don't draw past
             * the UIBounds set for us
             */

            Viewport oldv = Game.GraphicsDevice.Viewport;  // cache the current viewport
            Game.GraphicsDevice.Viewport = CreateViewport();  // set viewport to our UIBounds

            batch.Begin( SpriteBlendMode.AlphaBlend );

            for( int i = 0; i < MenuItems.Count; i++ )
            {
                if( Selection == i )
                {
                    batch.DrawString( Font, MenuItems[i] + HelpText[i], current, SelectedColor );
                }
                else
                {
                    batch.DrawString( Font, MenuItems[i], current, UnselectedColor );
                }

                current.Y += ItemHeights[i];
            }

            batch.End();


            GraphicsDevice.Viewport = oldv;  // return to the old viewport
            base.Draw( gameTime );
        }
    }
}
