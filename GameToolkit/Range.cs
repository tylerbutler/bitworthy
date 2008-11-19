using System;
using System.Collections;


namespace TylerButler.GameToolkit
{
    public struct Range : ICollection, IEnumerable
    {
        public Range(int start, int end)
        {
            rstart = start;
            rend = 0;
            this.end = end;
        }

        public Range(ICollection col)
        {
            rstart = 0;
            rend = ((col != null) ? col.Count : 0) - 1;
        }

        public int start
        {
            get { return rstart; }
            set
            {
                rstart = value;
                if (rend < (rstart - 1)) rend = rstart - 1;
            }
        }

        public int end
        {
            get { return rend; }
            set
            {
                rend = (value >= rstart) ? value : (rstart - 1);
            }
        }

        public int mid
        {
            get
            {
                return (rstart + rend) / 2;
            }
        }

        public void Set(int start, int end)
        {
            rstart = start;
            this.end = end;
        }

        public bool Between(int value)
        {
            return (rstart <= value) && (value <= rend);
        }

        public static bool Between(int left, int value, int right)
        {
            return (left <= value) && (value <= right);
        }

        public int Saturate(int value)
        {
            return (value > rstart) ? ((value < rend) ? value : rend) : rstart;
        }

        public static int Saturate(int left, int value, int right)
        {
            return (value > left) ? ((value < right) ? value : right) : left;
        }

        public static Range operator &(Range r1, Range r2)
        {
            return new Range(Math.Max(r1.start, r2.start), Math.Min(r1.end, r2.end));
        }

        public static Range operator |(Range r1, Range r2)
        {
            return new Range(Math.Min(r1.start, r2.start), Math.Max(r1.end, r2.end));
        }

        public void Offset(int o)
        {
            rstart += o;
            rend += o;
        }

        public void Resize(int s)
        {
            end += s;
        }

        public int Count
        {
            get { return rend - rstart + 1; }
        }

        public bool Empty
        {
            get { return rend == (rstart - 1); }
        }

        public int this[int index]
        {
            get
            {
                if (Count == 0) throw new ArgumentException("index");
                return rstart + index % Count;
            }
        }

        public void CopyTo(Array array, int index)
        {
            if (array == null) throw new ArgumentNullException("array");
            if (index < 0) throw new ArgumentOutOfRangeException("index", index, "");
            if (array.Rank != 1) throw new ArgumentException("Array is multidimensional.", "array");
            if (!new Range(array).Between(index)) throw new ArgumentException("Index out of array bounds.", "index");
            if (Count == 0) return;
            if (!new Range(array).Between(index + Count - 1)) throw new ArgumentException("Index out of array bounds.", "index");
            foreach (int i in this)
            {
                array.SetValue(i, index + i - rstart);
            }
        } // <end of function> void CopyTo(Array array, int index)

        public bool IsSynchronized
        {
            get { return false; }
        }

        public object SyncRoot
        {
            get { return this; }
        }

        public IEnumerator GetEnumerator()
        {
            return new Iterator(ref this);
        }


        private class Iterator : IEnumerator
        {
            public Iterator(ref Range r)
            {
                this.r = r;
                Reset();
            }

            public object Current
            {
                get
                {
                    if (!r.Between(i + r.start)) throw new InvalidOperationException();
                    return r[i];
                }
            }

            public bool MoveNext()
            {
                return r.Between((++i) + r.start);
            }

            public void Reset()
            {
                i = -1;
            }

            private Range r;
            private int i;
        }; // <end of> class Iterator : IEnumerator

        private int rstart;
        private int rend;
    } // <end of> struct Range : ICollection, IEnumerable

} // <end of> namespace gmit
