using System;


namespace TylerButler.GameToolkit
{
    // This code based on sample at http://dotnetperls.com/Content/Random-Number-Class.aspx
    static class RandomNumber
    {
        /// <summary>
        /// The random object. It is automatically initialized with a time
        /// seed. If you only create one object at a time, this is sufficient.
        /// </summary>
        private static Random random = new Random();

        /// <summary>
        /// Wrapper method to return the next random number in our
        /// sequence. It is more efficient and more 'random' to
        /// store only one Random object and reuse it.
        /// </summary>
        public static int GetRandom( int minValue, int maxValue )
        {
            return random.Next( minValue, maxValue );
        }
    }
}
