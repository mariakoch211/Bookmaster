using Bookmaster.AppData;
using Bookmaster.Model;
using Bookmaster.View.Windows;
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

namespace Bookmaster.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для BrowsCatalogPage.xaml
    /// </summary>
    public partial class BrowsCatalogPage : Page
    {
        List<Book> _books = App.context.Book.ToList();
        PaginationService _booksPagination;

        public BrowsCatalogPage()
        {
            InitializeComponent();

           
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void SearchBtn_Click(object sender, RoutedEventArgs e)
        {
            SearchResultsGrid.Visibility = Visibility.Visible;

            if (string.IsNullOrEmpty(SearchByBookTitleTb.Text) && string.IsNullOrEmpty(SearchByAuthorNameTb.Text)
                && string.IsNullOrEmpty(SearchByBookSubjectTb.Text)) 
            {
                _booksPagination = new PaginationService(_books);                           
            }
            else
            {
                List<Book> searchResults = _books.Where(book => book.Title.ToLower().Contains(SearchByBookTitleTb.Text.ToLower()) &&
                book.Authors.ToLower().Contains(SearchByAuthorNameTb.Text.ToLower())).ToList() ;
                //Реализовываем алгоритм поиска
                _booksPagination = new PaginationService(searchResults);
            }
      
            //Загружаем данные из таблицы Bookauthor в список ListView.
            BookAuthorLv.ItemsSource = _booksPagination.CurrentPageOfBooks;
            TotalPagesTb.DataContext = TotalBooksTb.DataContext = _booksPagination;
            CurrentPageTb.Text = _booksPagination.CurrentPageNumber.ToString();
            _booksPagination.UpdatePaginationButtons(PreviousBookBtn, NextBookBtn);
        }

        private void PreviousBookBtn_Click(object sender, RoutedEventArgs e)
        {
            BookAuthorLv.ItemsSource = _booksPagination.PreviousPage();
            CurrentPageTb.Text = _booksPagination.CurrentPageNumber.ToString();
            _booksPagination.UpdatePaginationButtons(PreviousBookBtn, NextBookBtn);
        }

        private void NextBookBtn_Click(object sender, RoutedEventArgs e)
        {
            BookAuthorLv.ItemsSource = _booksPagination.NextPage();
            CurrentPageTb.Text = _booksPagination.CurrentPageNumber.ToString();
            _booksPagination.UpdatePaginationButtons(PreviousBookBtn, NextBookBtn);
        }

        private void CurrentPageTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (int.TryParse(CurrentPageTb.Text ,out int pageNumber) && pageNumber >=1 && pageNumber <= _booksPagination.TotalPages)
            {
                BookAuthorLv.ItemsSource = _booksPagination.SetCurrentPage(pageNumber);
            }
            _booksPagination.UpdatePaginationButtons(PreviousBookBtn, NextBookBtn);
        }

        private void PreviousCoverBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void NextCoverBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BookAuthorLv_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Book selectedBook = BookAuthorLv.SelectedItem as Book;

            BookDetailGrid.DataContext = selectedBook;
        }

        private void AuthorsDetailsHl_Click(object sender, RoutedEventArgs e)
        {
            BookAuthorsDetailsWindow bookAuthorsDetailsWindow = new BookAuthorsDetailsWindow();
            bookAuthorsDetailsWindow.ShowDialog();
        }
    }
}
