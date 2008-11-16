using System;
using System.Collections.Generic;
using System.Text;

namespace TylerButler.GameToolkit
{
    public interface Valuable
    {
        float Value
        {
            get;
            set;
        }

        string Name
        {
            get;
            set;
        }

        string Description
        {
            get;
            set;
        }
    }
}
