using System;
using System.IO;
using System.Windows.Media.Imaging;

namespace WPF3.Model
{
    public static class ConverterImageToBase64
    {
        public static string ConvertImageToString(BitmapImage image)
        {
            using MemoryStream ms = new();
            PngBitmapEncoder encoder = new();
            encoder.Frames.Add(BitmapFrame.Create(image));
            encoder.Save(ms);
            byte[] buffer = ms.ToArray();

            return Convert.ToBase64String(buffer);
        }
    }
}
