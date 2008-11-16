using System;
using System.Collections.Generic;
using System.Text;

namespace Bitworthy.Games.Acquire.Components
{
    public abstract class Card
    {
        private Player owner;

        public Player Owner
        {
            get { return owner; }
            set { owner = value; }
        }
    }
}
