using System;
using System.Collections.Generic;
using System.Text;

namespace TylerButler.GameToolkit
{
    public interface Carryable
    {
        float Size
        {
            get;
            set;
        }

        float Weight
        {
            get;
            set;
        }
    }
}
