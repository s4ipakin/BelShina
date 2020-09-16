using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public enum GrafType
{
    Temp,
    Pressure,
    Conductivity,
    Flaw,
    Level,
    CO2, 
    Force
}

namespace BelShina_HMI.Chart
{
    public abstract class GrafSet
    {
        public abstract GrafType GrafType { get; }
        public abstract string yAxesName { get; }
        public abstract string unit { get; }
        public abstract double maxValue { get; }
        public abstract GrafSeries[] GetSettings();
    }

    public class ForceGrafSet : GrafSet
    {
        public override GrafType GrafType => GrafType.Force;
        public override string yAxesName => "Сила";
        public override string unit => "N";
        public override double maxValue => 150000;
        public override GrafSeries[] GetSettings()
        {
            return new GrafSeries[]
            {
                new GrafSeries("Сила", 2)
            };
        }
    }
}
