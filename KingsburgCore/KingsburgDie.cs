using System.Collections;
using TylerButler.GameToolkit;

namespace TylerButler.Kingsburg.Core
{
    internal class KingsburgDie : Die
    {
        private bool isUsed;

        public KingsburgDie()
            : base( 6 )
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
    }
}
