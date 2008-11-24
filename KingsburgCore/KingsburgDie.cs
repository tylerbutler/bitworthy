using System;
using System.Collections.Generic;
using TylerButler.GameToolkit;
using System.Text;
using System.Linq;

namespace TylerButler.Kingsburg.Core
{
    internal class KingsburgDie : Die
    {
        private bool isUsed;
        private DieTypes type;

        internal enum DieTypes
        {
            Regular,
            White,
            Market, // Special type
            PlusTwo, // Special type
        }

        public KingsburgDie()
            : base( 6 )
        {
        }

        public KingsburgDie( DieTypes type )
            : this()
        {
            this.type = type;
        }

        public KingsburgDie( int maxRoll )
            : base( maxRoll )
        {
        }

        public bool IsUsed
        {
            get
            {
                return isUsed;
            }
            set
            {
                isUsed = value;
            }
        }

        internal DieTypes Type
        {
            get
            {
                return type;
            }
            set
            {
                type = value;
            }
        }

        public override string ToString()
        {
            string toReturn=this.Value.ToString();
            switch( this.Type )
            {
                case DieTypes.White:
                    toReturn += "*";
                    break;
                case DieTypes.Market:
                    toReturn += "M";
                    break;
            }
            return toReturn;
        }
    }

    internal class DiceCollection : List<KingsburgDie>, ICloneable
    {

        public DiceCollection()
            : base()
        {
        }

        public DiceCollection( IEnumerable<KingsburgDie> collection )
            : base( collection )
        {
        }

        public DiceCollection( int numDice, int maxRoll )
            : base()
        {
            foreach( int i in new Range( 1, numDice ) )
            {
                this.Add( new KingsburgDie( maxRoll ) );
            }
        }

        public void RollAllDice()
        {
            foreach( KingsburgDie die in this )
            {
                die.Roll();
            }
        }

        /// <summary>
        /// Resets all the dice in the collection to be "unused" (IsUsed == false).
        /// </summary>
        internal void ResetDiceUsage()
        {
            foreach( KingsburgDie die in this )
            {
                die.IsUsed = false;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach( KingsburgDie die in this )
            {
                sb.Append( die + " " );
            }

            return sb.ToString();
        }

        internal bool AllSameRoll()
        {
            KingsburgDie d = this[0];
            foreach( KingsburgDie die in this )
            {
                if( die.Value != d.Value )
                {
                    return false;
                }
            }
            return true;
        }

        internal void RemoveAllWhiteDice()
        {
            IEnumerable<KingsburgDie> toRemove = from b in this
                                                 where b.Type == KingsburgDie.DieTypes.White
                                                 select b;
            foreach( KingsburgDie d in toRemove )
            {
                this.Remove( d );
            }
        }

        #region ICloneable Members

        public object Clone()
        {
            DiceCollection toReturn = new DiceCollection( this );
            return toReturn;
        }

        #endregion
    }

    internal sealed class MarketDiePositive : KingsburgDie
    {
        new internal int Value
        {
            get
            {
                return 1;
            }
        }
    }

    internal sealed class MarketDieNegative : KingsburgDie
    {
        new internal int Value
        {
            get
            {
                return -1;
            }
        }
    }

}
