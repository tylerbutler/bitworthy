using System;
using System.Collections.Generic;
using System.Text;
using TylerButler.GameToolkit;

namespace TylerButler.GameToolkit
{
    public class Die
    {
        private int max;
        private int min;
        private int value;

        public int Max
        {
            get
            {
                return this.max;
            }
            set
            {
                this.max = value;
            }
        }

        public int Min
        {
            get
            {
                return this.min;
            }
            set
            {
                if( value < 1 )
                {
                    throw new ArgumentOutOfRangeException( "Die.Min", "Die.Min cannot be less than 1." );
                }
                else
                {
                    this.min = value;
                }
            }
        }

        public int Value
        {
            get
            {
                return this.value;
            }
            private set
            {
                this.value = value;
            }
        }

        public Die( int minIn, int maxIn )
        {
            Min = minIn;
            Max = maxIn;
        }

        public Die( int maxIn )
            : this( 1, maxIn )
        {
        }

        public int Roll()
        {
            this.Value = RandomNumber.GetRandom( this.Min, this.Max );
            return this.Value;
        }
    }

    public class DiceBag<T> : IEnumerable<T> where T : Die, new()
    {
        private List<T> bag = new List<T>();

        public DiceBag( int numDice, int maxValue )
        {
            for( int i = 0; i < numDice; i++ )
            {
                bag.Add( new T() );
            }
        }

        public DiceValues Values
        {
            get
            {
                DiceValues dv = new DiceValues();
                foreach( T die in bag )
                {
                    dv.Add( die.Value );
                }
                return dv;
            }
        }        
        
        public void RollAllDice()
        {
            foreach( T die in bag )
            {
                die.Roll();
            }
        }

        public void AddDie()
        {
            bag.Add( new T() );
        }

        #region IEnumerable<T> Members

        public IEnumerator<T> GetEnumerator()
        {
            return bag.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return bag.GetEnumerator();
        }

        #endregion
    }

    public class DiceValues : List<int>
    {
        public DiceValues() : base()
        {
        }

        public DiceValues( IEnumerable<int> collection ) : base( collection )
        {
        }

        public int TotalValue
        {
            get
            {
                int sum = 0;
                foreach( int i in this )
                {
                    sum += i;
                }
                return sum;
            }
        }
    }
}
