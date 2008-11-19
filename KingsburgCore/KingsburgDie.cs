using TylerButler.GameToolkit;

namespace TylerButler.Kingsburg.Core
{
    internal class KingsburgDie : Die
    {
        private bool isUsed;
        private DieTypes type;

        internal enum DieTypes
        {
            Regular,
            White,
        }

        public KingsburgDie()
            : base( 6 )
        {
        }

        public KingsburgDie( DieTypes type )
            : this()
        {
            this.type = type;
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

        internal DieTypes Type
        {
            get
            {
                return type;
            }
            set
            {
                type = value;
            }
        }
    }
}
