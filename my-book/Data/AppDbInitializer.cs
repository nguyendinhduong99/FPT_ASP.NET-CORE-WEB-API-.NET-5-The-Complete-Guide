using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_book.Data
{
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder appliacationBuilder)
        {
            using (var servicesScope = appliacationBuilder.ApplicationServices.CreateScope())
            {
                var context = servicesScope.ServiceProvider.GetService<AppDbContext>();

                if (!context.Books.Any())
                {
                    context.Books.AddRange(new Models.Book()
                    {
                        Title = "1st Book Title",
                        Description = "1st Book Description",
                        IsRead = true,
                        DateRead = DateTime.Now.AddDays(-10),
                        Rate = 4,
                        Genre = "Biography",
                        Author = "First Author",
                        CoverUrl = "https:...",
                        DateAdded = DateTime.Now
                    },
                    new Models.Book()
                    {
                        Title = "2nd Book Title",
                        Description = "2nd Book Description",
                        IsRead = false,
                        
                        Genre = "Biography",
                        Author = "Second Author",
                        CoverUrl = "https:...",
                        DateAdded = DateTime.Now
                    }
                    );
                    context.SaveChanges();
                }
            }
        }
    }
}
