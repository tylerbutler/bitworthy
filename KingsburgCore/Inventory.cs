using System;
using System.Collections.Generic;
using System.Text;
using TylerButler.GameToolkit;

namespace TylerButler.Kingsburg.Core
{
    public class Inventory : TylerButler.GameToolkit.InventoryGeneric<int>
    {
        public Inventory() : base()
        {
            this.Add("Gold", 0);
            this.Add("Wood", 0);
            this.Add("Stone", 0);
            this.Add("PlusTwoTokens", 0);
        }
    }
}
