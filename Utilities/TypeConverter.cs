using System;

namespace Bitworthy.Utilities
{
    // Code for this class from http://www.25hoursaday.com/weblog/2007/08/09/CGenericsImplicitTypeConversionHell.aspx
    public class TypeConverter
    {
        /// <summary>
        /// Returns a delegate that can be used to cast a subtype back to its base type. 
        /// </summary>
        /// <typeparam name="T">The derived type</typeparam>
        /// <typeparam name="U">The base type</typeparam>
        /// <returns>Delegate that can be used to cast a subtype back to its base type. </returns>
        public static Converter<T, U> UpCast<T, U>() where T : U
        {
            return delegate( T item )
            {
                return (U)item;
            };
        }

        public static Converter<B, D> DownCast<B, D>() where D : B
        {
            return delegate( B item )
            {
                return (D)item;
            };
        }
    }
}
