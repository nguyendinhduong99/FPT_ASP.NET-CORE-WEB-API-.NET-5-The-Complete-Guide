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

        public void AddBookWithAuthor(BookVM book)
        {
            var _book = new Models.Book()
            {
                Title = book.Title,
                Description = book.Description,
                IsRead = book.IsRead,
                DateRead = book.IsRead ? book.DateRead : null,
                Rate = book.IsRead ? book.Rate : null,
                Genre = book.Genre,
                CoverUrl = book.CoverUrl,
                DateAdded = DateTime.Now,
                PublisherId= book.PublisherId
            };
            _context.Books.Add(_book);
            _context.SaveChanges();

            foreach (var id in book.AuthorIds)
            {
                var _book_author = new Book_Author()
                {
                    BookId = _book.Id,
                    AuthorId = id
                };
                _context.Book_Authors.Add(_book_author);
                _context.SaveChanges();
            }
        }

        public List<Book> GetAllBooks() => _context.Books.ToList();
        public BookWithAuthorsVM GetBookById(int bookId)
        {
            var _bookWithAuthorVm = _context.Books.Where(n => n.Id == bookId).Select(book => new BookWithAuthorsVM()
            {

                Title = book.Title,
                Description = book.Description,
                IsRead = book.IsRead,
                DateRead = book.IsRead ? book.DateRead : null,
                Rate = book.IsRead ? book.Rate : null,
                Genre = book.Genre,
                CoverUrl = book.CoverUrl,
                PublisherName = book.Publisher.Name,
                AuthorName = book.Book_Authors.Select(n => n.Author.FullName).ToList()
            }).FirstOrDefault();
            return _bookWithAuthorVm;
        }
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
            else
            {
                throw new Exception($"Not found id: {bookId} of the book");
            }
        }
    }
}
