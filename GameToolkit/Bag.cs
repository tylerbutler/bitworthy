using System.Collections.Generic;

namespace TylerButler.GameToolkit
{
    public abstract class Bag<TKey, TVal> : Dictionary<TKey, TVal>, Carryable
    {
        private float maxSize, maxWeight, weight, size;

        public Bag(float maxSizeIn, float maxWeightIn)
        {
            MaxSize = maxSizeIn;
            MaxWeight = maxWeightIn;
        }

        public Bag(float maxSizeIn) : this(maxSizeIn, 0 /* No max Size */ ) { }

        public Bag() : this(0, 0) { }

        public float MaxWeight
        {
            get
            {
                return this.maxWeight;
            }
            set
            {
                this.maxWeight = value;
            }
        }

        public float MaxSize
        {
            get
            {
                return this.maxSize;
            }
            set
            {
                this.maxSize = value;
            }
        }

        #region Carryable Members

        public float Size
        {
            get
            {
                return this.size;
            }
            set
            {
                this.size = value;
            }
        }

        public float Weight
        {
            get
            {
                return this.weight;
            }
            set
            {
                this.weight = value;
            }
        }

        #endregion

    }

    public abstract class Bag<T> : Bag<string, T> { }
}
