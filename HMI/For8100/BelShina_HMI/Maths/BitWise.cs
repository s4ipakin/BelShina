using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BelShina_HMI.Maths
{
    public static class BitWise
    {
        public static bool IsBitSet(byte b, int pos)
        {
            return (b & (1 << pos)) != 0;
        }

        public static bool IsBitSet(ushort b, int pos)
        {
            return (b & (1 << pos)) != 0;
        }
    }
}
