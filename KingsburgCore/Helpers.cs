﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using TylerButler.GameToolkit;
using TylerButler.Kingsburg.Core;
using TylerButler.Kingsburg.Core.UI;


namespace TylerButler.Kingsburg.Utilities
{
    public static class Helpers
    {
        public static void InfluenceAdvisorHelper( Advisor a, Player p )
        {
            // For actions that require return data from the UI
            List<object> returnData;
            GoodsChoiceOptions choice;
            
            // TODO: Finish this
            switch( a.AdvisorNameEnum )
            {
                case Advisors.Jester:
                    // Player gains 1 VP
                    p.VictoryPoints++;
                    UIManager.Instance.DisplayInfluenceAdvisor( a, p );
                    break;
                case Advisors.Squire:
                    // Take 1 gold from the supply.
                    p.Goods["Gold"]++;
                    UIManager.Instance.DisplayInfluenceAdvisor( a, p );
                    break;
                case Advisors.Architect:
                    // Take 1 wood from the supply.
                    p.Goods["Wood"]++;
                    UIManager.Instance.DisplayInfluenceAdvisor( a, p );
                    break;
                case Advisors.Merchant:
                    // Take 1 wood OR 1 gold from the supply.
                    UIManager.Instance.DisplayInfluenceAdvisor( a, p, out returnData );
                    choice = (GoodsChoiceOptions)returnData[0];
                    p.AddGood(choice);
                    break;
                case Advisors.Sergeant:
                    // Recruit 1 soldier.
                    p.Soldiers++;
                    UIManager.Instance.DisplayInfluenceAdvisor( a, p );
                    break;
                case Advisors.Alchemist:
                    //trade a single good for one of each of the other two goods
                    UIManager.Instance.DisplayInfluenceAdvisor( a, p, out returnData );
                    choice = (GoodsChoiceOptions)returnData[0];
                    switch( choice )
                    {
                        case GoodsChoiceOptions.Gold:
                            p.Goods["Wood"]++;
                            p.Goods["Stone"]++;
                            break;
                        case GoodsChoiceOptions.Wood:
                            p.Goods["Gold"]++;
                            p.Goods["Stone"]++;
                            break;
                        case GoodsChoiceOptions.Stone:
                            p.Goods["Gold"]++;
                            p.Goods["Wood"]++;
                            break;
                    }
                    break;
                case Advisors.Astronomer:
                    // Receive a good of choice and a +2 token
                    UIManager.Instance.DisplayInfluenceAdvisor( a, p, out returnData );
                    choice = (GoodsChoiceOptions)returnData[0];
                    p.AddGood( choice );
                    p.PlusTwoTokens++;
                    break;
                case Advisors.Treasurer:
                    // Receive 2 gold
                    UIManager.Instance.DisplayInfluenceAdvisor( a, p );
                    p.Goods["Gold"] += 2;
                    break;
                case Advisors.MasterHunter:
                    // Take a wood and a gold, or a wood and a stone
                    UIManager.Instance.DisplayInfluenceAdvisor( a, p, out returnData );
                    choice = (GoodsChoiceOptions)returnData[0];
                    p.AddGood( choice );
                    break;
                case Advisors.General:
                    // recruit two soliders and secretly look at the enemy card
                    UIManager.Instance.DisplayInfluenceAdvisor( a, p );
                    p.Soldiers += 2;
                    break;
                case Advisors.Swordsmith:
                    // receive a stone and a wood or a stone and a gold
                    UIManager.Instance.DisplayInfluenceAdvisor( a, p, out returnData );
                    choice = (GoodsChoiceOptions)returnData[0];
                    p.AddGood( choice );
                    break;
                case Advisors.Duchess:
                    // Take 2 goods of choice and a "+2" token
                    UIManager.Instance.DisplayInfluenceAdvisor( a, p, out returnData );
                    p.AddGood( (GoodsChoiceOptions)returnData[0] );
                    p.AddGood( (GoodsChoiceOptions)returnData[1] );
                    p.PlusTwoTokens++;
                    break;
                case Advisors.Champion:
                    // Take 3 stone
                    UIManager.Instance.DisplayInfluenceAdvisor( a, p );
                    p.Goods["Stone"] += 3;
                    break;
                case Advisors.Smuggler:
                    // Pay a VP to take 3 goods of choice.
                    UIManager.Instance.DisplayInfluenceAdvisor( a, p, out returnData );
                    p.AddGood( (GoodsChoiceOptions)returnData[0] );
                    p.AddGood( (GoodsChoiceOptions)returnData[1] );
                    p.AddGood( (GoodsChoiceOptions)returnData[2] );
                    break;
                case Advisors.Inventor:
                    // receive 1 of each good
                    UIManager.Instance.DisplayInfluenceAdvisor( a, p );
                    p.Goods["Gold"]++;
                    p.Goods["Wood"]++;
                    p.Goods["Stone"]++;
                    break;
                case Advisors.Wizard:
                    // Take 4 gold
                    UIManager.Instance.DisplayInfluenceAdvisor( a, p );
                    p.Goods["Gold"] += 4;
                    break;
                case Advisors.Queen:
                    // Take 2 goods of choice, spy on the enemy, and get 3 VP
                    UIManager.Instance.DisplayInfluenceAdvisor( a, p, out returnData );
                    p.AddGood( (GoodsChoiceOptions)returnData[0] );
                    p.AddGood( (GoodsChoiceOptions)returnData[1] );
                    p.VictoryPoints += 3;
                    break;
                case Advisors.King:
                    // take one of each good and a soldier
                    UIManager.Instance.DisplayInfluenceAdvisor( a, p );
                    p.Goods["Gold"]++;
                    p.Goods["Wood"]++;
                    p.Goods["Stone"]++;
                    p.Soldiers++;
                    break;
                default:
                    throw new Exception( "Something went wrong when running the ExecuteAdvisor method. Advisor={0}, Player={1}" );
            }
        }

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

        internal class SumComboFinder
        {
            private List<List<KingsburgDie>> mResults;

            public List<List<KingsburgDie>> Find( int targetSum, List<KingsburgDie> elements )
            {

                mResults = new List<List<KingsburgDie>>();
                RecursiveFind( targetSum, 0, new List<KingsburgDie>(), elements, 0 );

                List<List<KingsburgDie>> copy = new List<List<KingsburgDie>>( mResults );
                foreach( List<KingsburgDie> list in copy )
                {
                    if( list.Count == 1 )
                    {
                        if( list[0].Type == KingsburgDie.DieTypes.White )
                        {
                            mResults.Remove( list );
                        }
                    }
                }
                return mResults;
            }

            private void RecursiveFind( int targetSum, int currentSum,
                List<KingsburgDie> included, List<KingsburgDie> notIncluded, int startIndex )
            {

                for( int index = startIndex; index < notIncluded.Count; index++ )
                {

                    KingsburgDie nextDie = notIncluded[index];
                    if( currentSum + nextDie.Value == targetSum )
                    {
                        List<KingsburgDie> newResult = new List<KingsburgDie>( included );
                        newResult.Add( nextDie );
                        mResults.Add( newResult );
                    }
                    else if( currentSum + nextDie.Value < targetSum )
                    {
                        List<KingsburgDie> nextIncluded = new List<KingsburgDie>( included );
                        nextIncluded.Add( nextDie );
                        List<KingsburgDie> nextNotIncluded = new List<KingsburgDie>( notIncluded );
                        nextNotIncluded.Remove( nextDie );
                        RecursiveFind( targetSum, currentSum + nextDie.Value,
                            nextIncluded, nextNotIncluded, startIndex++ );
                    }
                }
            }
        }

        // This is a horrible way to do this but I am sick of trying to do the right algorithm to find all subset sums
        internal static HashSet<int> Sums( DiceValues diceVals )
        {

            HashSet<int> toReturn = new HashSet<int>();
            DiceBag<KingsburgDie> bag = new DiceBag<KingsburgDie>( 0, 6 );
            SumComboFinder comboFinder = new SumComboFinder();

            int j= 0;
            foreach( int i in diceVals )
            {
                KingsburgDie d;
                if( j < 3 )
                {
                    d = new KingsburgDie( KingsburgDie.DieTypes.Regular );
                }
                else
                {
                    d = new KingsburgDie( KingsburgDie.DieTypes.White );
                }
                d.Value = i;
                bag.AddDie( d );
                j++;
            }

            foreach( int i in new Range( 1, 18 ) )
            {
                if( comboFinder.Find( i, bag.Dice ).Count > 0 )
                {
                    toReturn.Add( i );
                }
            }
            return toReturn;
        }
    }

    // Code for this class from http://weblogs.asp.net/pwelter34/archive/2006/05/03/444961.aspx
    [XmlRoot( "dictionary" )]
    public class SerializableDictionary<TKey, TValue>
        : Dictionary<TKey, TValue>, IXmlSerializable
    {
        #region IXmlSerializable Members

        public System.Xml.Schema.XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml( System.Xml.XmlReader reader )
        {
            XmlSerializer keySerializer = new XmlSerializer( typeof( TKey ) );
            XmlSerializer valueSerializer = new XmlSerializer( typeof( TValue ) );
            bool wasEmpty = reader.IsEmptyElement;
            reader.Read();
            if( wasEmpty )
                return;

            while( reader.NodeType != System.Xml.XmlNodeType.EndElement )
            {
                reader.ReadStartElement( "item" );
                reader.ReadStartElement( "key" );

                TKey key = (TKey)keySerializer.Deserialize( reader );
                reader.ReadEndElement();
                reader.ReadStartElement( "value" );
                TValue value = (TValue)valueSerializer.Deserialize( reader );
                reader.ReadEndElement();
                this.Add( key, value );
                reader.ReadEndElement();
                reader.MoveToContent();
            }
            reader.ReadEndElement();
        }

        public void WriteXml( System.Xml.XmlWriter writer )
        {
            XmlSerializer keySerializer = new XmlSerializer( typeof( TKey ) );

            XmlSerializer valueSerializer = new XmlSerializer( typeof( TValue ) );
            foreach( TKey key in this.Keys )
            {
                writer.WriteStartElement( "item" );
                writer.WriteStartElement( "key" );
                keySerializer.Serialize( writer, key );
                writer.WriteEndElement();
                writer.WriteStartElement( "value" );
                TValue value = this[key];
                valueSerializer.Serialize( writer, value );
                writer.WriteEndElement();
                writer.WriteEndElement();
            }
        }
        #endregion
    }

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
