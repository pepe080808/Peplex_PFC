using System;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Utils
{
    public class ByteToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || value.GetType() != typeof(byte[]))
                return DependencyProperty.UnsetValue; // null;

            var binaryData = (byte[])value;

            var bmp = new BitmapImage();

            using (var stream = new MemoryStream(binaryData))
            {
                bmp.BeginInit();
                bmp.StreamSource = stream;
                bmp.CacheOption = BitmapCacheOption.OnLoad;
                bmp.EndInit();
            }

            if (bmp.CanFreeze)
                bmp.Freeze();

            return bmp;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            var bmp = (BitmapSource)value;
            var stride = bmp.PixelWidth * ((bmp.Format.BitsPerPixel + 7) / 8);
            var binaryData = new byte[bmp.PixelHeight * stride];
            bmp.CopyPixels(binaryData, stride, 0);

            return binaryData;
        }
    }
}
