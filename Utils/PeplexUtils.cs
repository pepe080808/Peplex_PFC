﻿using System.IO;
using System.Web.Security;
using System.Windows.Media.Imaging;

namespace Utils
{
    public static class PeplexUtils
    {
        public static byte[] ConvertBitmapImageToByteArray(BitmapImage bitmapImage)
        {
            byte[] data;
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bitmapImage));
            using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                data = ms.ToArray();
            }

            return data;
        }

        public static BitmapImage ConvertByteArrayToBitmapImage(byte[] byteArray)
        {
            if (byteArray != null)
            {
                var memStream = new MemoryStream();
                memStream.Write(byteArray, 0, byteArray.Length);
                memStream.Seek(0, SeekOrigin.Begin);

                var img = new BitmapImage();
                img.BeginInit();
                img.StreamSource = memStream;
                img.EndInit();

                return img;
            }

            return null;
        }

        public static string GeneratePassword()
        {
            return Membership.GeneratePassword(8, 1);
        }
    }
}
