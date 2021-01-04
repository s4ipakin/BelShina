using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BelShina_HMI.Maths
{
    internal static class MovingAverageExtensions
    {
        public static IEnumerable<double> MovingAverage<T>(this IEnumerable<T> inputStream, Func<T, double> selector, int period)
        {
            var ma = new MovingAverage(period);
            foreach (var item in inputStream)
            {
                ma.Push(selector(item));
                yield return ma.Current;
            }
        }

        public static IEnumerable<double> MovingAverage(this IEnumerable<double> inputStream, int period)
        {
            var ma = new MovingAverage(period);
            foreach (var item in inputStream)
            {
                ma.Push(item);
                yield return ma.Current;
            }
        }
    }
}
