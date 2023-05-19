using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WPF3.Model;

namespace WPF3.View
{
    /// <summary>
    /// Логика взаимодействия для AddWindow.xaml
    /// </summary>
    public partial class AddWindow : Window
    {
        public Good Good { get; set; }
        public AddWindow(Good good)
        {
            InitializeComponent();
            Good = good;
            DataContext = Good;
        }

        private void AddGood_btn_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void CreateQR_btn_Click(object sender, RoutedEventArgs e)
        {
            BitmapImage image = GeneratorQrCode.GenerateQR(nametbox.Text, Convert.ToDouble(pricetbox.Text), descripttbox.Text);
            Qr_img.Source = image;
            Good.QrCode = ConverterImageToBase64.ConvertImageToString(image);
        }
    }
}
