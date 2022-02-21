using my_book.Data.Models;
using my_book.Data.Paging;
using my_book.Data.ViewModels;
using my_book.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace my_book.Data.Services
{
    public class PublishersService
    {
        private AppDbContext _context;
        public PublishersService(AppDbContext context)
        {
            _context = context;
        }

        public Publisher AddPublisher(PublisherVM publisher)
        {
            if (StringStartWithNumber(publisher.Name)) throw new PublisherNameException("Name start with number", publisher.Name);

            var _publisher = new Models.Publisher()
            {
                Name = publisher.Name
            };
            _context.Publishers.Add(_publisher);
            _context.SaveChanges();

            return _publisher;
        }

        public List<Publisher> GetAllPublishers(string sortBy, string searchString, int? pageNumber)
        {
            var allPublishers =  _context.Publishers.OrderBy(n => n.Name).ToList();
            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy)
                {
                    case "name-desc":
                        allPublishers = allPublishers.OrderByDescending(n => n.Name).ToList();
                        break;
                    default:
                        break;
                }
            }
            if (!string.IsNullOrEmpty(searchString))
            {
                allPublishers = allPublishers.Where(n => n.Name.Contains(searchString, StringComparison.CurrentCultureIgnoreCase)).ToList();
            }

            //Paging
            int pageSize = 55;
            allPublishers = PaginatedList<Publisher>.Create(allPublishers.AsQueryable(), pageNumber ?? 1, pageSize);
            return allPublishers;
        }
        public Publisher GetPublisherById(int publisherId) => _context.Publishers.FirstOrDefault(n=> n.Id == publisherId);
        public PublisherWithBookAndAuthorsVM GetPublisherWithBookAndAuthors(int publisherId)
        {
            var _publisherWithBookAndAuthorsVM = _context.Publishers.Where(n => n.Id == publisherId).Select(publisher => new PublisherWithBookAndAuthorsVM()
            {
                Name = publisher.Name,
                BookAuthors = publisher.Books.Select(n => new BookAuthorsVM()
                {
                    BookName = n.Title,
                    BookAuthors = n.Book_Authors.Select(n => n.Author.FullName).ToList()
                }).ToList()
            }).FirstOrDefault();

            return _publisherWithBookAndAuthorsVM;
        }
        public Publisher UpdatePublisherById(int publisherId, PublisherVM publisher)
        {
            var _publisherId = _context.Publishers.FirstOrDefault(n => n.Id == publisherId);
            if(_publisherId != null)
            {
                _publisherId.Name = publisher.Name;
                
                _context.SaveChanges();
            }
            //trả ra id để lên API còn có cái mà đối chiếu
            return _publisherId;
        }
        public void DeletePublisherById(int publisherId)
        {
            var _publisherId = _context.Publishers.FirstOrDefault(n => n.Id == publisherId);
            if (_publisherId != null)
            {
                _context.Remove(_publisherId);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception($"Not found id {publisherId} of publisher");
            }
        }

        private bool StringStartWithNumber(string name) => Regex.IsMatch(name, @"^\d");

    }
}
