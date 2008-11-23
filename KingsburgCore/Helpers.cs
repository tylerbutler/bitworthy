using System.Collections.Generic;
using System.Text;
using TylerButler.GameToolkit;
using TylerButler.Kingsburg.Core;

namespace TylerButler.Kingsburg.Utilities
{
    internal class SumComboFinder
    {
        private List<List<KingsburgDie>> mResults;

        internal List<List<KingsburgDie>> Find( int targetSum, DiceCollection bag )
        {
            //DiceCollection clone = (DiceCollection)bag.Clone();
            DiceCollection clone = bag;
            List<KingsburgDie> elements = (List<KingsburgDie>)clone;
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
    
        // This is a horrible way to do this but I am sick of trying to do the right algorithm to find all subset sums
        internal static HashSet<int> Sums( DiceCollection diceVals )
        {

            HashSet<int> toReturn = new HashSet<int>();
            DiceCollection bag = new DiceCollection( 0, 6 );
            SumComboFinder comboFinder = new SumComboFinder();

            int j= 0;
            foreach( KingsburgDie i in diceVals )
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
                d.Value = i.Value;
                bag.Add( d );
                j++;
            }

            foreach( int i in new Range( 1, 18 ) )
            {
                if( comboFinder.Find( i, bag ).Count > 0 )
                {
                    toReturn.Add( i );
                }
            }
            return toReturn;
        }
    }
}
