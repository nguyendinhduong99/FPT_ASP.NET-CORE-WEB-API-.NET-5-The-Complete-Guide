using my_book.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_book.Data.Services
{
    public class LogsService
    {
        private AppDbContext _context;
        public LogsService(AppDbContext context)
        {
            _context = context;
        }
        public List<Log> GetAllLog() => _context.Logs.ToList();
    }
}
