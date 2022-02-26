using Microsoft.EntityFrameworkCore;
using my_book.Data;
using my_book.Data.Models;
using my_book.Data.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace my_books_tests
{
    public class PublisherServiceTest
    {
        private static DbContextOptions<AppDbContext> dbContextOptions = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: "BookDbTest").Options;
        AppDbContext context;

        PublishersService publishersService;

        [OneTimeSetUp]
        public void Setup()
        {
            context = new AppDbContext(dbContextOptions);
            context.Database.EnsureCreated();

            SeedDatabase();
            publishersService = new PublishersService(context);
        }

        [Test, Order(1)]
        public void GetAllPublishers_WithNoSortBy_WithNoSearch_WithNoPaging_Test()
        {
            var result = publishersService.GetAllPublishers("", "", null);
            Assert.That(result.Count, Is.EqualTo(5));
            Assert.AreEqual(result.Count, 5);
            
        }
        [Test, Order(2)]
        public void GetAllPublishers_WithSortBy_WithNoSearch_WithNoPaging_Test()
        {
            var result = publishersService.GetAllPublishers("name-desc", "", null);
            Assert.That(result.Count, Is.EqualTo(5));
            Assert.That(result.FirstOrDefault().Name, Is.EqualTo("Publisher 6"));
        }
        [Test, Order(3)]
        public void GetAllPublishers_WithNoSortBy_WithSearch_WithNoPaging_Test()
        {
            var result = publishersService.GetAllPublishers("", "3", null);
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.FirstOrDefault().Name, Is.EqualTo("Publisher 3"));
        }
        [Test, Order(4)]
        public void GetAllPublishers_WithNoSortBy_WithNoSearch_WithPaging_Test()
        {
            var result = publishersService.GetAllPublishers("", "", 1);
            Assert.That(result.Count, Is.EqualTo(5));
        }
        //GetPublisherById
        [Test, Order(5)]
        public void GetPublisherById_WithResponse_Test()
        {
            var result = publishersService.GetPublisherById(1);
            Assert.That(result.Id, Is.EqualTo(1));
            Assert.That(result.Name, Is.EqualTo("Publisher 1"));
        }
        [Test, Order(6)]
        public void GetPublisherById_WithoutResponse_Test()
        {
            var result = publishersService.GetPublisherById(99);
            Assert.That(result, Is.Null);
        }
        [OneTimeTearDown]
        public void CleanUp()
        {
            context.Database.EnsureDeleted();
        }
        private void SeedDatabase()
        {
            var publisher = new List<Publisher>
            {
                new Publisher()
                {
                    Id = 1,
                    Name = "Publisher 1"
                },
                new Publisher()
                {
                    Id = 2,
                    Name = "Publisher 2"
                },
                new Publisher()
                {
                    Id = 3,
                    Name = "Publisher 3"
                }, new Publisher()
                {
                    Id = 4,
                    Name = "Publisher 4"
                },
                new Publisher()
                {
                    Id = 5,
                    Name = "Publisher 5"
                },
                new Publisher()
                {
                    Id = 6,
                    Name = "Publisher 6"
                }
            };
            context.Publishers.AddRange(publisher);

            
            var author = new List<Author>
            {
                new Author()
                {
                    Id = 1,
                    FullName = "Author 1"
                },
                new Author()
                {
                    Id = 2,
                    FullName = "Author 2"
                }
            };
            context.Authors.AddRange(author);

            var book = new List<Book>
            {
                new Book()
                {
                    Id = 1,
                    Title = "Book 1",
                    Description = "Description 1",
                    IsRead = false,
                    Genre = "Genre 1",
                    CoverUrl = "https:...1",
                    DateAdded = DateTime.Now.AddDays(-10),
                    PublisherId = 1
                },
                new Book()
                {
                    Id = 2,
                    Title = "Book 2",
                    Description = "Description 2",
                    IsRead = false,
                    Genre = "Genre 2",
                    CoverUrl = "https:...2",
                    DateAdded = DateTime.Now.AddDays(-10),
                    PublisherId = 2
                }
            };
            context.Books.AddRange(book);

            var book_author = new List<Book_Author>
            {
                new Book_Author()
                {
                    Id = 1,
                    BookId = 1,
                    AuthorId = 1
                },
                new Book_Author()
                {
                    Id = 2,
                    BookId = 1,
                    AuthorId = 2
                },
                new Book_Author()
                {
                    Id = 3,
                    BookId = 2,
                    AuthorId = 2
                },
                new Book_Author()
                {
                    Id = 4,
                    BookId = 2,
                    AuthorId = 1
                }
            };
            context.Book_Authors.AddRange(book_author);


            context.SaveChanges();
        }
    }
} 