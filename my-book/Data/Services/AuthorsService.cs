using my_book.Data.Models;
using my_book.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_book.Data.Services
{
    public class AuthorsService
    {
        private AppDbContext _context;
        public AuthorsService(AppDbContext context)
        {
            _context = context;
        }

        public void AddAuthor(AuthorVM author)
        {
            var _author = new Models.Author()
            {
                FullName = author.FullName
            };
            _context.Authors.Add(_author);
            _context.SaveChanges();
        }

        public List<Author> GetAllAuthors() => _context.Authors.ToList();
        public Author GetAuthorById(int authorId) => _context.Authors.FirstOrDefault(n=> n.Id == authorId);
        public AuthorWithBooksVM GetAuthorWithBooks(int authorId)
        {
            var _author = _context.Authors.Where(n => n.Id == authorId).Select(author => new AuthorWithBooksVM()
            {
                FullName = author.FullName,
                BookTitles = author.Book_Authors.Select(n => n.Book.Title).ToList()
            }).FirstOrDefault();

            return _author;
        }
        public Author UpdateAuthorById(int authorId, AuthorVM author)
        {
            var _authorId = _context.Authors.FirstOrDefault(n => n.Id == authorId);
            if(_authorId != null)
            {
                _authorId.FullName = author.FullName;
               
                _context.SaveChanges();
            }
            //trả ra id để lên API còn có cái mà đối chiếu
            return _authorId;
        }
        public void DeleteAuthorById(int authorId)
        {
            var _authorId = _context.Authors.FirstOrDefault(n => n.Id == authorId);
            if (_authorId != null)
            {
                _context.Remove(_authorId);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception($"Not found id {authorId} of author");
            }
        }
       
    }
}
