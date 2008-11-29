using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TylerButler.Kingsburg.Windows
{
    class PlayerManager
    {
        private PlayerManager instance = new PlayerManager();
        private PlayerManager() { }

        public PlayerManager Instance
        {
            get
            {
                return Instance;
            }
        }
    }
}
