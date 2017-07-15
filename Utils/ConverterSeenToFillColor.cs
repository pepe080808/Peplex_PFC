using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace Utils
{
    public class SeenToFillDarkColorConvert : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var seen = (bool) values[0];
            return seen ? (Color)Application.Current.Resources["DarkGreenColor"] : (Color)Application.Current.Resources["DarkGrayColor"];
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class SeenToFillLightColorConvert : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var seen = (bool)values[0];
            return seen ? (Color)Application.Current.Resources["LightGreenColor"] : (Color)Application.Current.Resources["LightGrayColor"];
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
