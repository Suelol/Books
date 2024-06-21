using Books39_40.Classes;
using Books39_40.DBModel;
using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace Books39_40.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddBooksPage.xaml
    /// </summary>
    public partial class AddBooksPage : Page
    {
        public AddBooksPage()
        {
            InitializeComponent();
            GenerateArticle();
            Makercmb.ItemsSource = ConnectionClass.conn.Maker.ToList();
        }

        private void GenerateArticle()
        {
            var listArt = (from article in ConnectionClass.conn.Book
                           select article.Code).ToList();

            string generatedArticle;
            do
            {
                generatedArticle = GenerateRandomArticle();
            } while (listArt.Contains(generatedArticle));

            articleTxt.Text = generatedArticle;
        }

        private string GenerateRandomArticle()
        {
            const string uppercaseLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string numbers = "0123456789";
            const string allChars = uppercaseLetters + numbers;

            var random = new Random();
            var article = new char[6];

            // гарантировать, что в артикуле есть хотя бы одна заглавная буква
            article[0] = uppercaseLetters[random.Next(uppercaseLetters.Length)];

            // гарантировать, что в артикуле есть хотя бы одна цифра
            article[1] = numbers[random.Next(numbers.Length)];

            // заполнить оставшиеся символы случайными значениями
            for (int i = 2; i < 6; i++)
            {
                article[i] = allChars[random.Next(allChars.Length)];
            }

            // перемешать символы, чтобы они не были в порядке заглавной буквы, цифры, ...
            for (int i = 0; i < 6; i++)
            {
                int j = random.Next(6);
                char temp = article[i];
                article[i] = article[j];
                article[j] = temp;
            }

            return new string(article);
        }



        //private void Button_Click_1(object sender, RoutedEventArgs e)
        //{
        //    ConnectionClass.conn.Book.Add(new Book
        //    {
        //        Name = type.Text,
        //        Code = articleTxt.Text,
        //        Price = Convert.ToInt32(serial.Text),
        //        Unit = "шт.",
        //        Id_MaxDiscount = maxDisountcmb.SelectedIndex,
        //        Id_Maker = Makercmb.SelectedIndex,
        //        Id_Category = Categorycmb.SelectedIndex,
        //        Id_Supplier = Suppliercmb.SelectedIndex,
        //        Id_Ative_Discount = ActiveDiscmb.SelectedIndex,
        //        Count = Convert.ToInt32(CountTB.Text),
        //        Description = DescTB.Text,
        //    }) ;
        //    ConnectionClass.conn.SaveChanges();
        //    NavigationService.Navigate(new BooksPage());
        //}

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(type.Text))
            {
                MessageBox.Show("Введите название криптовалюты");
                return;
            }

            if (string.IsNullOrWhiteSpace(articleTxt.Text))
            {
                MessageBox.Show("Введите артикул криптовалюты");
                return;
            }

            if (string.IsNullOrWhiteSpace(serial.Text))
            {
                MessageBox.Show("Введите цену криптовалюты");
                return;
            }

            if (!int.TryParse(serial.Text, out int price))
            {
                MessageBox.Show("Серийный номер должен быть числом");
                return;
            }

            if (price <= 0)
            {
                MessageBox.Show("Серийный номер должен быть положительным числом");
                return;
            }

            if (string.IsNullOrWhiteSpace(CountTB.Text))
            {
                MessageBox.Show("Введите количество книг");
                return;
            }

            if (!int.TryParse(CountTB.Text, out int count))
            {
                MessageBox.Show("Количество книг должно быть числом");
                return;
            }

            if (count <= 0)
            {
                MessageBox.Show("Количество книг должно быть положительным числом");
                return;
            }



            var selectedMaker = (Maker)Makercmb.SelectedValue;
            if (selectedMaker == null)
            {
                MessageBox.Show("Выберите производителя");
                return;
            }

            var makerId = selectedMaker.Id_Maker;


            ConnectionClass.conn.Book.Add(new Book
            {
                Name = type.Text,
                Code = articleTxt.Text,
                Price = price,
                Unit = "шт.",
                Id_Maker = makerId,
                Count = count,
            });
            ConnectionClass.conn.SaveChanges();
            NavigationService.Navigate(new BooksPage());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new BooksPage());
        }

        private void type_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            foreach (char c in e.Text)
            {
                if (!char.IsLetter(c))
                {
                    e.Handled = true;
                    return;
                }
            }
        }

        private void serial_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            foreach (char c in e.Text)
            {
                if (!char.IsDigit(c))
                {
                    e.Handled = true;
                    return;
                }
            }
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            foreach (char c in e.Text)
            {
                if (!char.IsDigit(c))
                {
                    e.Handled = true;
                    return;
                }
            }
        }

        private void Makercmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var p = Makercmb.SelectedItem as Maker;
            var Makerr = (from mak in ConnectionClass.conn.Maker
                          where mak.Id_Maker == p.Id_Maker
                          select mak).FirstOrDefault();

            Makercmb.ItemsSource = ConnectionClass.conn.Maker.ToList();
        }


        private void articleTxt_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
