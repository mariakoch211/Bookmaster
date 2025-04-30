using Bookmaster.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Bookmaster.AppData
{
    public class PaginationService
    {
        private const int PAGE_SIZE = 50;
        private readonly List<Book> _books;
        private int _currentPageIndex = 0;
        private int _currentPageNumber = 1;

        public int CurrentPageNumber
        { 
        get
            {
                return _currentPageNumber =  _currentPageIndex + 1;
            }
            set
            {
                _currentPageIndex = value-1 ;
                _currentPageNumber = value;
            }
        }

        public int BooksCount => _books.Count;

        public int TotalPages => (BooksCount + PAGE_SIZE - 1) / PAGE_SIZE;
        public List<Book> CurrentPageOfBooks => _books.Skip(_currentPageIndex * PAGE_SIZE).Take(PAGE_SIZE).ToList();



        public PaginationService(List<Book> books)
        {
            _books = books;
        }
        public List<Book> PreviousPage()
        {
            if (_currentPageIndex > 0)
            {
                _currentPageIndex--;
            }

            return CurrentPageOfBooks;
        }

        public List<Book> NextPage()
        {
            if (_currentPageIndex < TotalPages - 1)
            {
                _currentPageIndex++;
            }
            return CurrentPageOfBooks;
        }
        public void UpdatePaginationButtons(Button previousBtn, Button nextBtn)
        {
            previousBtn.IsEnabled = _currentPageIndex > 0;
            nextBtn.IsEnabled = _currentPageIndex < TotalPages - 1;
        }

        public List<Book> SetCurrentPage(int pageNumber)
        {
            _currentPageNumber = pageNumber;

            return CurrentPageOfBooks;
        }
    }
}
