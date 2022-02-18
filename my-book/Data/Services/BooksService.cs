using my_book.Data.Models;
using my_book.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_book.Data.Services
{
    public class BooksService
    {
        private AppDbContext _context;
        public BooksService(AppDbContext context)
        {
            _context = context;
        }

        public void AddBook(BookVM book)
        {
            var _book = new Models.Book()
            {
                Title = book.Title,
                Description = book.Description,
                IsRead = book.IsRead,
                DateRead = book.IsRead ? book.DateRead : null,
                Rate = book.IsRead ? book.Rate : null,
                Genre = book.Genre,
                Author = book.Author,
                CoverUrl = book.CoverUrl,
                DateAdded = DateTime.Now
            };
            _context.Books.Add(_book);
            _context.SaveChanges();
        }

        public List<Book> GetAllBooks() => _context.Books.ToList();
        public Book GetBookById(int bookId) => _context.Books.FirstOrDefault(n=> n.Id == bookId);
        public Book UpdateBookById(int bookId, BookVM book)
        {
            var _bookId = _context.Books.FirstOrDefault(n => n.Id == bookId);
            if(_bookId != null)
            {
                _bookId.Title = book.Title;
                _bookId.Description = book.Description;
                _bookId.IsRead = book.IsRead;
                _bookId.DateRead = book.IsRead ? book.DateRead : null;
                _bookId.Rate = book.IsRead ? book.Rate : null;
                _bookId.Genre = book.Genre;
                _bookId.Author = book.Author;
                _bookId.CoverUrl = book.CoverUrl;

                _context.SaveChanges();
            }
            //trả ra id để lên API còn có cái mà đối chiếu
            return _bookId;
        }
        public void DeleteBookById(int bookId)
        {
            var _bookId = _context.Books.FirstOrDefault(n => n.Id == bookId);
            if (_bookId != null)
            {
                _context.Remove(_bookId);
                _context.SaveChanges();
            }
        }
    }
}
