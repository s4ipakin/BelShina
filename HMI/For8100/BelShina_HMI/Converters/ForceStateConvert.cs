using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace BelShina_HMI.Converters
{
    [ValueConversion(typeof(ushort), typeof(String))]
    public class ForceStateConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ushort state = (ushort)value;

            switch (state)
            {
                case 0:
                    return "Остановлен";
                case 1:
                    return "Установка параметров";
                case 2:
                    return "Установка силы (грубо)";
                case 3:
                case 4:
                    return "Установка силы (точно)";
                case 5:
                    return "Сила установлена";
                case 6:
                case 7:
                    return "Отпускание";
                case 8:
                    return "Движение в позицию";
                case 9:
                    return "Набор скорости";
                case 10:
                    return "Движение";
                case 11:
                    return "Остановка";

                default:
                    return "Не определен";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string strValue = value as string;
            ushort resultUshort;
            if (ushort.TryParse(strValue, out resultUshort))
            {
                return resultUshort;
            }
            return DependencyProperty.UnsetValue;
        }
    }


    [ValueConversion(typeof(ushort), typeof(String))]
    public class LaserStateConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ushort state = (ushort)value;

            switch (state)
            {
                case 0:
                    return "Не определен";
                case 1:
                    return "Инициализация";
                case 2:
                    return "Инициализирован";
                case 3:
                case 4:
                    return "Даижение в позицию";
                case 5:
                    return "На позиции";
                case 6:
                    return "остонавливается";
                case 7:
                    return "Остановлен";
                

                default:
                    return "Не определен";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string strValue = value as string;
            ushort resultUshort;
            if (ushort.TryParse(strValue, out resultUshort))
            {
                return resultUshort;
            }
            return DependencyProperty.UnsetValue;
        }
    }

    [ValueConversion(typeof(ushort), typeof(String))]
    public class ProcessStateConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ushort state = (ushort)value;

            switch (state)
            {
                case 0:
                    return "Остановлен";
                case 1:
                    return "Установка усиление";
                case 2:                    
                case 3:
                    return "Расчеты";
                case 4:
                    return "Измерение контуров";
                case 5:
                    return "Завершен";
                case 6:
                    return "Ослабление усиления";
                case 7:
                    return "Остановка";


                default:
                    return "Не определен";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string strValue = value as string;
            ushort resultUshort;
            if (ushort.TryParse(strValue, out resultUshort))
            {
                return resultUshort;
            }
            return DependencyProperty.UnsetValue;
        }
    }
}
