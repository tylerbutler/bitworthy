using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Lights_Out
{
    class Selector : GameObject
    {
        private int[] index = new int[2];

        public Selector(Texture2D texture)
            : base(texture)
        {
            index[0] = 0;
            index[1] = 0;
        }

        public int[] getIndex()
        {
            return index;
        }

        public Vector2 getOffsetPosition(Vector2 position)
        {
            Vector2 toReturn = new Vector2();
            toReturn.X = position.X;
            toReturn.Y = position.Y + Constants.selectorOffsetTop;
            return toReturn;
        }

        public void moveRight()
        {
            if (index[0] < 4)
            {
                index[0]++;
            }
        }
        public void moveLeft()
        {
            if (index[0] > 0)
            {
                index[0]--;
            }
        }
        public void moveUp()
        {
            if (index[1] > 0)
            {
                index[1]--;
            }
        }
        public void moveDown()
        {
            if (index[1] < 4)
            {
                index[1]++;
            }
        }
    }
}