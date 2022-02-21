using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_book.Data.ViewModels
{
    public class PublisherVM
    {
        public string Name { get; set; }
    }
    public class PublisherWithBookAndAuthorsVM
    {
        public string Name { get; set; }
        public List<BookAuthorsVM> BookAuthors { get; set; }
    }
    public class BookAuthorsVM
    {
        public string BookName { get; set; }
        public List<string> BookAuthors { get; set; }
    }
}
