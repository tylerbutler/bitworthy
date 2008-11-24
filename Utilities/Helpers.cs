using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Bitworthy.Utilities
{
    public static class Helpers
    {
        //Code for this method came from http://www.csharper.net/blog/getting_the_text_name_of_an_enum_value_with_spaces.aspx
        public static string ProperSpace( string text )
        {
            StringBuilder sb = new StringBuilder();
            string lowered = text.ToLower();

            for( int i = 0; i < text.Length; i++ )
            {
                string a = text.Substring( i, 1 );
                string b = lowered.Substring( i, 1 );
                if( a != b )
                    sb.Append( " " );

                sb.Append( a );
            }

            return sb.ToString().Trim();
        }
    }
}
