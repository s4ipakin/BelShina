using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BelShina_HMI.Maths
{
    public class Round
    {
        public static float RoundOneDigit(float convertedValue)
        {
            int buffer = Convert.ToInt32(convertedValue * 10);
            return (buffer / 10);
        }
    }
}
