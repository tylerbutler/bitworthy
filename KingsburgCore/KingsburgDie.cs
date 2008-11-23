using System;
using System.Collections.Generic;
using TylerButler.GameToolkit;

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

        //public List<KingsburgDie> GetListOfDice()
        //{
        //    return this;
        //}

        #region ICloneable Members

        public object Clone()
        {
            DiceCollection toReturn = new DiceCollection( this );
            return toReturn;
        }

        #endregion
    }

}
