using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TylerButler.Kingsburg.Core.UI;

namespace TylerButler.Kingsburg.Windows
{
    class UIManagerWindows : UIManager
    {
        private UIManagerWindows instance = new UIManagerWindows();

        private UIManagerWindows() { }

        public UIManagerWindows Instance
        {
            get { return instance; }
        }
    }
}
