using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace WPF3.Model
{
    public static class GeneratorQrCode
    {
        public static BitmapImage GenerateQR(string name, double price, string description)
        {
            string info = $"Наименование товара: {name} \nСтоимость: {price} \nОписание товара: {description}";
            QRCodeGenerator qRGenerator = new();
            QRCodeData qRCodeData = qRGenerator.CreateQrCode(info, QRCodeGenerator.ECCLevel.Q);
            QRCode qRCode = new(qRCodeData);
            Bitmap image = qRCode.GetGraphic(30);
            return Converter(image);
        }
        public static BitmapImage Converter(Bitmap bmp)
        {
            MemoryStream ms = new();
            ((System.Drawing.Bitmap)bmp).Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            BitmapImage image = new();
            image.BeginInit();
            image.StreamSource = ms;
            ms.Seek(0, SeekOrigin.Begin);
            image.EndInit();
            return image;
        }
    }
}
