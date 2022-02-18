using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_book.Data.Models
{
    public class Book_Author
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int AuthorId { get; set; }


        //Navigate properties
        public Book Book { get; set; }
        public Author Author { get; set; }
    }
}
