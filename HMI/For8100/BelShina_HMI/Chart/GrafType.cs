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
    Force,
    Contur,
    LineForce
}

namespace BelShina_HMI.Chart
{
    public abstract class GrafSet
    {
        public abstract GrafType GrafType { get; }
        public abstract string yAxesName { get; }
        public abstract string unit { get; }
        public abstract string xAxesName { get; }
        public abstract string xUnit { get; }
        public abstract double maxValue { get; }
        public abstract GrafSeries[] GetSettings();
        public abstract string seriesName { get; }
    }

    public class ForceGrafSet : GrafSet
    {
        public override GrafType GrafType => GrafType.Force;
        public override string yAxesName => "Сила";
        public override string unit => "N";
        public override string xAxesName => "Перемещение";
        public override string xUnit => "мм";
        public override double maxValue => 10000;
        public override string seriesName => "Зависимость углового\r\nусиления от угла поворота";
        public override GrafSeries[] GetSettings()
        {
            return new GrafSeries[]
            {
                new GrafSeries("Сила", 2)
            };
        }
    }

    public class ForceGrafSetLine_1 : GrafSet
    {
        public override GrafType GrafType => GrafType.LineForce;
        public override string yAxesName => "Сила";
        public override string unit => "N";
        public override string xAxesName => "Перемещение";
        public override string xUnit => "мм";
        public override double maxValue => 10000;
        public override string seriesName => "Зависимость бокового\r\nусиления от перемещения";
        public override GrafSeries[] GetSettings()
        {
            return new GrafSeries[]
            {
                new GrafSeries("Сила", 2)
            };
        }
    }

    public class ForceGrafSetLine_2 : GrafSet
    {
        public override GrafType GrafType => GrafType.LineForce;
        public override string yAxesName => "Сила";
        public override string unit => "N";
        public override string xAxesName => "Перемещение";
        public override string xUnit => "мм";
        public override double maxValue => 10000;
        public override string seriesName => "Зависимость тангенциального\r\nусиления от перемещения";
        public override GrafSeries[] GetSettings()
        {
            return new GrafSeries[]
            {
                new GrafSeries("Сила", 2)
            };
        }
    }


    public class ConturGrafSet : GrafSet
    {
        public override GrafType GrafType => GrafType.Contur;
        public override string yAxesName => "Положение лазера";
        public override string unit => "мм";
        public override string xAxesName => "Расстояние";
        public override string xUnit => "мм";
        public override double maxValue => 1000;
        public override string seriesName => "";
        public override GrafSeries[] GetSettings()
        {
            return new GrafSeries[]
            {
                new GrafSeries("Расстояние", 2)
            };
        }
    }
}
