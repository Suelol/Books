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
using Books39_40.DBModel;
using Books39_40.Classes;
using Microsoft.CSharp.RuntimeBinder;
using System.Linq.Expressions;

namespace Books39_40.Pages
{
    /// <summary>
    /// Логика взаимодействия для BooksPage.xaml
    /// </summary>
    public partial class BooksPage : Page
    {
        public BooksPage()
        {
            InitializeComponent();
            LvBooks.ItemsSource = ConnectionClass.conn.Book.ToList();
            

        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("Удалить запись?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
            { return; }
            else
            {
                var b = LvBooks.SelectedItem as Book;
                if (b != null)
                {
                    ConnectionClass.conn.Book.Remove(b);
                    ConnectionClass.conn.SaveChanges();
                    LvBooks.ItemsSource = ConnectionClass.conn.Book.ToList();
                    MessageBox.Show($"Книга {b.Name} удалена", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Для начала выберите запись!!!");
                }
            }
        }

        private void BtnSort_az_Click(object sender, RoutedEventArgs e)
        {
            LvBooks.ItemsSource = ConnectionClass.conn.Book.OrderBy(z => z.Name).ToList();
        }

        private void TxtSearch_TextChanged (object sender, RoutedEventArgs e)
        {
            LvBooks.ItemsSource = ConnectionClass.conn.Book.Where(z => z.Name.ToLower().Contains(TxtSearch.Text)).ToList();
        }

        private void TxtSearch_TextChange(object sender, RoutedEventArgs e)
        {

        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnAdd_az_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddBooksPage());
        }

        private void LvBooks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
