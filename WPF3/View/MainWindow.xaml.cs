using Microsoft.EntityFrameworkCore;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPF3.Model;

namespace WPF3.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ApplicationContext db = new();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Гарантировать, что БД уже создана
            db.Database.EnsureCreated();
            //Загрузить данные из БД
            db.Goods.Load();
            //Привязать данные БД как коллекцию
            DataContext = db.Goods.Local.ToObservableCollection();
        }

        private void OpenAddWnd_btn_Click(object sender, RoutedEventArgs e)
        {
            // Открыть окно добавления товара
            AddWindow wnd = new(new Good());
            if (wnd.ShowDialog() == true)
            {
                // Если окно открыто успешно, то привязать его данные к классу и добавить в БД
                Good good = wnd.Good;
                // Проверка
                if (!ValidCheck.IsNameValid(wnd.Good.Name))
                {
                    MessageBox.Show("Значение поля \"Наименование\" превышает допустимый объём");
                }
                else if (!ValidCheck.IsPriceValid(wnd.Good.Price))
                {
                    MessageBox.Show("Значение поля \"Стоимость\" превышает допустимый объём");
                }
                else if (!double.TryParse(Convert.ToString(wnd.Good.Price), out _))
                {
                    MessageBox.Show("Значение поля \"Стоимость\" введено некорректно");
                }
                else if (!ValidCheck.IsDescriptValid(wnd.Good.Description))
                {
                    MessageBox.Show("Значение поля \"Описание\" превышает допустимый объём");
                }
                else
                {
                    db.Goods.Add(good);
                    db.SaveChanges();
                    Lbox_goods.Items.Refresh();
                }
                
            }
        }

        private void OpenEditWnd_btn_Click(object sender, RoutedEventArgs e)
        {
            // Получить выделенный элемент
            Good? selgood = (Good)Lbox_goods.SelectedItem;
            // Если ни один элемент не выделен, то отмена
            if (selgood == null)
                return;
            // Открыть окно редактирования товара
            AddWindow wnd = new(new Good
            {
                // Привязать данные класса к данным выбранного товара
                Name = selgood.Name,
                Price = selgood.Price,
                Description = selgood.Description,
                QrCode = selgood.QrCode,
            });
            if (wnd.ShowDialog() == true)
            {
                // Если окно открыто успешно, то получить изменённый товар
                selgood = db.Goods.Where(x => x.Id == selgood.Id).FirstOrDefault();
                if (selgood != null)
                {
                    // Привязка
                    selgood.Name = wnd.Good.Name;
                    selgood.Price = wnd.Good.Price;
                    selgood.Description = wnd.Good.Description;
                    selgood.QrCode = wnd.Good.QrCode;
                    // Проверка
                    if (!ValidCheck.IsNameValid(wnd.Good.Name))
                    {
                        MessageBox.Show("Значение поля \"Наименование\" превышает допустимый объём");
                    }
                    else if (!ValidCheck.IsPriceValid(wnd.Good.Price))
                    {
                        MessageBox.Show("Значение поля \"Стоимость\" превышает допустимый объём");
                    }
                    else if (!double.TryParse(Convert.ToString(wnd.Good.Price), out _))
                    {
                        MessageBox.Show("Значение поля \"Стоимость\" введено некорректно");
                    }
                    else if (!ValidCheck.IsDescriptValid(wnd.Good.Description))
                    {
                        MessageBox.Show("Значение поля \"Описание\" превышает допустимый объём");
                    }
                    else
                    {
                        db.SaveChanges();
                        // Обновить лист
                        Lbox_goods.Items.Refresh();
                    }
                }
            }
        }

        private void RemoveGood_btn_Click(object sender, RoutedEventArgs e)
        {
            // Получить выделенный товар
            Good? selgood = (Good)Lbox_goods.SelectedItem;
            // Если ни один товар не выделен, то отмена
            if (selgood == null)
                return;

            // Удалить выбранный товар
            db.Remove(selgood);
            db.SaveChanges();
            // Обновить лист
            Lbox_goods.Items.Refresh();
        }
    }
}
