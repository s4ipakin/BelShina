using System;
using System.Collections.Generic;

using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

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
                    return " Остановлен";
                case 1:
                    return " Установка параметров";
                case 2:
                    return " Установка силы (грубо)";
                case 3:
                case 4:
                    return " Установка силы (точно)";
                case 5:
                    return " Сила установлена";
                case 6:
                case 7:
                    return " Отпускание";
                case 8:
                    return " Движение в позицию";
                case 9:
                    return " Набор скорости";
                case 10:
                    return " Движение";
                case 11:
                    return " Остановка";

                default:
                    return " Не определен";
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
                    return " Не определен";
                case 1:                   
                case 2:
                    return " Инициализация";
                case 3:
                    return " Инициализирован";
                case 4:
                    return " Инициализация";
                case 5:
                    return " На позиции";
                case 6:
                    return " Движение в позицию";
                case 7:
                    return " На позиции";
                case 8:
                    return " Останавливается";
                case 9:
                    return " Остановлен";


                default:
                    return " Не определен";
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
                    return " Остановлен";
                case 1:
                    return " Установка усиления";
                case 2:                    
                case 3:
                    return " Расчеты";
                case 4:
                    return " Измерение контуров вторичное";
                case 5:
                    return " Завершен";
                case 6:
                    return " Ослабление усиления";
                case 7:
                    return " Остановка";
                case 8:
                    return " Измерение контуров первичное";


                default:
                    return " Не определен";
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



    [ValueConversion(typeof(ushort), typeof(System.Windows.Media.Brushes))]
    public class ProcessStateConvertToColor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ushort state = (ushort)value;
            var bc = new BrushConverter();
            switch (state)
            {
                case 0:
                    return (Brush)bc.ConvertFrom("#FFE2E0E0"); 
                case 1:
                    return (Brush)bc.ConvertFrom("#FFA8F9A0");
                case 2:
                case 3:
                case 4:
                    return (Brush)bc.ConvertFrom("#FFA8F9A0");
                case 5:
                    return (Brush)bc.ConvertFrom("#FFA8F9A0"); // #FF1CF307
                case 6:
                     // #FFD9F5F7
                case 7:
                    return (Brush)bc.ConvertFrom("#FFF7DEDB");
                case 8:
                    return (Brush)bc.ConvertFrom("#FFA8F9A0");


                default:
                    return (Brush)bc.ConvertFrom("#FFE2E0E0");
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


    [ValueConversion(typeof(ushort), typeof(System.Windows.Media.Brushes))]
    public class LaserStateConvertToColor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ushort state = (ushort)value;
            var bc = new BrushConverter();
            switch (state)
            {
                case 0:
                    return (Brush)bc.ConvertFrom("#FFE2E0E0");
                case 1:
                case 2:
                    return (Brush)bc.ConvertFrom("#FFE8FFE7");
                case 3:
                    return (Brush)bc.ConvertFrom("#FFA8F9A0");
                case 4:
                    
                case 5:
                    return (Brush)bc.ConvertFrom("#FFE2E0E0");
                case 6:
                    return (Brush)bc.ConvertFrom("#FFE8FFE7");
                case 7:
                    return (Brush)bc.ConvertFrom("#FFA8F9A0");
                case 8:
                    return (Brush)bc.ConvertFrom("#FFF7DEDB");
                case 9:
                    return (Brush)bc.ConvertFrom("#FFE2E0E0");


                default:
                    return (Brush)bc.ConvertFrom("#FFE2E0E0");
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


    [ValueConversion(typeof(bool), typeof(string))]
    public class StartBtnText : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool state = (bool)value;
            if (state)
            {
                return "Остановить";
            }
            return "Начать испытание";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string strValue = value as string;
            bool resultUshort;
            if (bool.TryParse(strValue, out resultUshort))
            {
                return resultUshort;
            }
            return DependencyProperty.UnsetValue;
        }
    }


    [ValueConversion(typeof(bool), typeof(System.Windows.Media.Brushes))]
    public class StartBtnColor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool state = (bool)value;
            var bc = new BrushConverter();
            if (state)
            {
                return (Brush)bc.ConvertFrom("#FF92F183");
            }
            return (Brush)bc.ConvertFrom("#FFDDDDDD");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string strValue = value as string;
            bool resultUshort;
            
            if (bool.TryParse(strValue, out resultUshort))
            {
                return resultUshort;
            }
            return DependencyProperty.UnsetValue;
        }
    }


    [ValueConversion(typeof(ushort), typeof(System.Windows.Media.Brushes))]
    public class ForceStateConvertToColor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ushort state = (ushort)value;
            var bc = new BrushConverter();
            switch (state)
            {
                case 0:
                    return (Brush)bc.ConvertFrom("#FFE2E0E0");
                case 1:
                    return (Brush)bc.ConvertFrom("#FFA8F9A0");
                case 2:
                case 3:
                case 4:
                    return (Brush)bc.ConvertFrom("#FFA8F9A0");
                case 5:
                    return (Brush)bc.ConvertFrom("#FFA8F9A0"); // #FF1CF307
                case 6:
                // #FFD9F5F7
                case 7:
                    return (Brush)bc.ConvertFrom("#FFF7DEDB");
                case 8:
                    return (Brush)bc.ConvertFrom("#FFA8F9A0");


                default:
                    return (Brush)bc.ConvertFrom("#FFE2E0E0");
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


    [ValueConversion(typeof(object), typeof(int))]
    public class EntryTypetoValueConverter : IValueConverter
    {
        public object Convert(
            object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            int number = (int)System.Convert.ChangeType(value, typeof(int));
            if (number > 1)
                return 1;
            else
                return 0;
        }

        public object ConvertBack(
            object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("ConvertBack not supported");
        }
    }


    [ValueConversion(typeof(ushort), typeof(System.Windows.Media.Brushes))]
    public class CurveStateConvertToColor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ushort state = (ushort)value;
            var bc = new BrushConverter();
            switch (state)
            {
                case 0:
                    return (Brush)bc.ConvertFrom("#FFE2E0E0");
                case 1:
                    return (Brush)bc.ConvertFrom("#FFA8F9A0");
                case 2:
                    return (Brush)bc.ConvertFrom("#FFF7DEDB");        //# FFF7DEDB
                case 3:
                case 4:
                    return (Brush)bc.ConvertFrom("#FFF7DEDB");
                case 5:                    
                case 6:
                    return (Brush)bc.ConvertFrom("#FFE8FFE7");
                // #FFD9F5F7
                case 7:
                    return (Brush)bc.ConvertFrom("#FFA8F9A0");
                case 8:
                    return (Brush)bc.ConvertFrom("#FFA8F9A0");


                default:
                    return (Brush)bc.ConvertFrom("#FFE2E0E0");
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
    public class CurveStateConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ushort state = (ushort)value;

            switch (state)
            {
                case 0:
                    return " Остановлен";
                case 1:
                    return " Завершен";
                case 2:
                    return " Остановка";
                case 3:
                    return " Установка шага";
                case 4:
                    return " Установка в начальное положение";
                case 5:
                    return " Измерение";
                case 6:
                    return " Измерение";
                case 7:
                    return " Сохранение данных";
                


                default:
                    return " Не определен";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ushort alm = (ushort)value;
            if (alm == 0)
            {
                return Visibility.Hidden;
            }
            return DependencyProperty.UnsetValue;
        }
    }


    [ValueConversion(typeof(ushort), typeof(Visibility))]
    public class AlmVisible : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ushort alm = (ushort)value;
            if (alm == 0)
            {
                return Visibility.Hidden;
            }
            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string strValue = value as string;
            bool resultUshort;
            if (bool.TryParse(strValue, out resultUshort))
            {
                return resultUshort;
            }
            return DependencyProperty.UnsetValue;
        }
    }



}
