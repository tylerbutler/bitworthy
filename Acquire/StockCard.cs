using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Bitworthy.Games.Acquire.Components
{
    public abstract class StockCard : Bitworthy.Games.Acquire.Components.Card
    {
        public enum Type { Quantum, Phoenix, American, Fusion, Hydra, Sackson, Zeta, None };

        public static int getStockValue(Type type, int chainLength)
        {
            switch (type)
            {
                case Type.Phoenix:
                    goto case Type.Quantum;

                case Type.Quantum:
                    if (chainLength < 2)
                    {
                        return 400;
                    }                    
                    else if (chainLength >= 2 && chainLength <= 5)
                    {
                        return 100 * (2 + chainLength);
                    }
                    else if (chainLength >= 6 && chainLength <= 10)
                    {
                        return 800;
                    }
                    else if (chainLength >= 11 && chainLength <= 20)
                    {
                        return 900;
                    }
                    else if (chainLength >= 21 && chainLength <= 30)
                    {
                        return 1000;
                    }
                    else if (chainLength >= 31 && chainLength <= 40)
                    {
                        return 1100;
                    }
                    else
                    {
                        return 1200;
                    }

                case Type.Fusion:
                    goto case Type.Hydra;

                case Type.American:
                    goto case Type.Hydra;

                case Type.Hydra:
                    if (chainLength < 2)
                    {
                        return 300;
                    }
                    else if (chainLength >= 2 && chainLength <= 5)
                    {
                        return 100 * (1 + chainLength);
                    }
                    else if (chainLength >= 6 && chainLength <= 10)
                    {
                        return 700;
                    }
                    else if (chainLength >= 11 && chainLength <= 20)
                    {
                        return 800;
                    }
                    else if (chainLength >= 21 && chainLength <= 30)
                    {
                        return 900;
                    }
                    else if (chainLength >= 31 && chainLength <= 40)
                    {
                        return 1000;
                    }
                    else
                    {
                        return 1100;
                    }

                case Type.Sackson:
                    goto case Type.Zeta;

                case Type.Zeta:
                    if (chainLength < 2)
                    {
                        return 200;
                    }
                    else if (chainLength >= 2 && chainLength <= 5)
                    {
                        return 100 * chainLength;
                    }
                    else if (chainLength >= 6 && chainLength <= 10)
                    {
                        return 600;
                    }
                    else if (chainLength >= 11 && chainLength <= 20)
                    {
                        return 700;
                    }
                    else if (chainLength >= 21 && chainLength <= 30)
                    {
                        return 800;
                    }
                    else if (chainLength >= 31 && chainLength <= 40)
                    {
                        return 900;
                    }
                    else
                    {
                        return 1000;
                    }
                default:
                    return 0;
            }
        }

        public static Color getStockColor(Type type)
        {
            switch (type)
            {
                case Type.Phoenix:
                    return Color.FromArgb(127, 85, 157);

                case Type.Quantum:
                    return Color.FromArgb(58, 149, 165);

                case Type.American:
                    return Color.FromArgb(35, 100, 181);

                case Type.Fusion:
                    return Color.FromArgb(90, 143, 26);

                case Type.Hydra:
                    return Color.FromArgb(255, 120, 9);

                case Type.Sackson:
                    return Color.FromArgb(235, 25, 73);

                case Type.Zeta:
                    return Color.FromArgb(255, 193, 0);

                case Type.None:
                    return Color.Gray;

                default:
                    return Color.WhiteSmoke;
            }
        }

        public static int getMinorityBonus(Type type, int chainLength)
        {
            return getStockValue(type, chainLength) * 5;
        }

        public static int getMajorityBonus(Type type, int chainLength)
        {
            return getStockValue(type, chainLength) * 10;
        }

    }
}
