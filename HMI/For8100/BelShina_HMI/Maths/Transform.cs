using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BelShina_HMI.Maths
{
    public static class Transform
    {
        public static double LinTrafo(double inValue, double inMax, double inMin, double outMax, double outMin)
        {
            double diff = inMax - inMin;
            bool err = diff == 0 || inValue < inMin || inValue > inMax;
            if (!err)
            {
                return (inValue - inMin) / diff * (outMax - outMin) + outMin;
            }
            else
                return 0;
        }
    }
}
