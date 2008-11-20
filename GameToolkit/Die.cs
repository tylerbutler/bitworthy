using System;
using System.Collections.Generic;

namespace TylerButler.GameToolkit
{
    public class Die : ICloneable
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
            set // TODO: Set private for ship, public for debug purposes only
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

        public override string ToString()
        {
            return "Value: " + this.Value;
        }

        #region ICloneable Members

        public object Clone()
        {
            //safe to do a shallow copy because this type only has int members
            return this.MemberwiseClone();
        }

        #endregion
    }

    // TODO reimplement this as a subclass - this was a stupid implementation idea
    public class DiceBag : List<Die>, ICloneable
    {

        public DiceBag()
            : base()
        {
        }

        public DiceBag( int numDice, int maxRoll )
            : base()
        {
            foreach( int i in new Range( 1, numDice ) )
            {
                this.Add( new Die( maxRoll ) );
            }
        }
        //private List<T> bag = new List<T>();

        //public DiceBag( int numDice, int maxValue )
        //{
        //    for( int i = 0; i < numDice; i++ )
        //    {
        //        bag.Add( new T() );
        //    }
        //}

        //public DiceValues Values
        //{
        //    get
        //    {
        //        DiceValues dv = new DiceValues();
        //        foreach( Die die in this )
        //        {
        //            dv.Add( die.Value );
        //        }
        //        return dv;
        //    }
        //}

        ////public T GetDieWithValue( int value )
        ////{
        ////    foreach( T die in bag )
        ////    {
        ////        if
        ////    }
        ////}

        public void RollAllDice()
        {
            foreach( Die die in this )
            {
                die.Roll();
            }
        }

        public List<Die> GetListOfDice()
        {
            return (List<Die>)this;
        }

        //public void AddDie( T die )
        //{
        //    bag.Add( die );
        //}

        //public int Count
        //{
        //    get
        //    {
        //        return bag.Count;
        //    }
        //}

        //public T this[int index]
        //{
        //    get
        //    {
        //        return bag[index];
        //    }
        //}

        //public List<T> Dice
        //{
        //    get
        //    {
        //        return bag;
        //    }
        //}

        //#region IEnumerable<T> Members

        //public IEnumerator<T> GetEnumerator()
        //{
        //    return bag.GetEnumerator();
        //}

        //#endregion

        //#region IEnumerable Members

        //System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        //{
        //    return bag.GetEnumerator();
        //}

        //#endregion

        #region ICloneable Members

        public object Clone()
        {
            List<Die> toReturn = new List<Die>( this );
            return toReturn;
        }

        #endregion
    }

    //public class DiceValues : List<int>
    //{
    //    //private Dictionary<int, List<List<Die>>> sumCombos = new Dictionary<int, List<List<Die>>>();

    //    public DiceValues()
    //        : base()
    //    {
    //    }

    //    public DiceValues( IEnumerable<int> collection )
    //        : base( collection )
    //    {
    //    }

    //    public int TotalValue
    //    {
    //        get
    //        {
    //            int sum = 0;
    //            foreach( int i in this )
    //            {
    //                sum += i;
    //            }
    //            return sum;
    //        }
    //    }

    //    //private HashSet<DiceValues> SumFinderRecursive( /*int singleItem, */ DiceValues remainingSet )
    //    //{
    //    //    HashSet<DiceValues> toReturn = new HashSet<DiceValues>();
    //    //    //toReturn.Add(singleItem);
    //    //    //if( remainingSet.Count > 0 )
    //    //    //{
    //    //    //    DiceValues remainingSetCopy = new DiceValues( remainingSet );
    //    //    //    DiceValues singleItemSet = new DiceValues();
    //    //    //    singleItemSet.Add( remainingSetCopy[0] );
    //    //    //    remainingSetCopy.RemoveAt( 0 );
    //    //    //    toReturn.Add( singleItemSet );
    //    //    //    toReturn.Add( remainingSetCopy );
    //    //    //    HashSet<DiceValues> temp = SumFinderRecursive( remainingSetCopy );
    //    //    //    toReturn.UnionWith( temp );
    //    //    //}

    //    //    for( int i = remainingSet.Count; i > 0; i-- )
    //    //    {

    //    //    }
    //    //}

    //    ////Set<Set<int>> P( Set<Set<int>> S )
    //    ////{
    //    ////    Set<Set<int>> toReturn = new Set<Set<int>>();
    //    ////    foreach( Set<int> e in S )
    //    ////    {
    //    ////        Set<Set<int>> temp = new Set<Set<int>>();
    //    ////        temp.Add(e);
    //    ////        Set<Set<int>> T = new Set<Set<int>>( S );
    //    ////        toReturn.UnionWith(F( e, T ) );
    //    ////    }
    //    ////}

    //    ////Set<Set<int>> F( int e, Set<int> T )
    //    ////{
    //    ////    T.Add( e );
    //    ////    return T;
    //    ////}

    //    public override string ToString()
    //    {
    //        string toReturn = string.Empty;
    //        foreach( int i in this )
    //        {
    //            toReturn += i + " ";
    //        }
    //        return toReturn;
    //    }

    //    //public Dictionary<int, List<DiceValues>> SumCombos
    //    //{
    //    //    get
    //    //    {
    //    //        Dictionary<int, List<DiceValues>> toReturn= new Dictionary<int, List<DiceValues>>();
    //    //        HashSet<int> sums = this.Sums; // local copy so we don't call Sums over and over

    //    //        for( int i = 0; i < this.Count; i++ )
    //    //        {
    //    //            DiceValues dv = new DiceValues();
    //    //            List<DiceValues> l = new List<DiceValues>();
    //    //            l.Add(
    //    //            toReturn.Add(i, new List<List<Die>>
    //    //            for( int j = i + 1; j < this.Count; j++ )
    //    //            {
    //    //                toReturn.Add( this[i] + this[j] );
    //    //            }
    //    //        }
    //    //        toReturn.Add( this.TotalValue );
    //    //        return toReturn;
    //    //    }
    //    //}
    //}
}
