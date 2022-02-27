using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using my_book.Controllers;
using my_book.Data;
using my_book.Data.Models;
using my_book.Data.Services;
using my_book.Data.ViewModels;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace my_books_tests
{
    public class PublisherControllerTest
    {

        private static DbContextOptions<AppDbContext> dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "BookDbControllerTest").Options;

        AppDbContext context;

        PublishersService publishersService;

        PublishersController publishersController;

        [OneTimeSetUp]
        public void Setup()
        {
            context = new AppDbContext(dbContextOptions);
            context.Database.EnsureCreated();

            SeedDatabase();
            publishersService = new PublishersService(context);
            publishersController = new PublishersController(publishersService, new NullLogger<PublishersController>());
        }

        [Test, Order(1)]
        public void HTTPGET_GetAllPublishers_With_Sort_Search_Paging_ResultOk_Test()
        {
            //page 1
            IActionResult actionResultPage1 = publishersController.GetAllPublishers("name-desc", "Publisher", 1);

            Assert.That(actionResultPage1, Is.TypeOf<OkObjectResult>());

            var result = (actionResultPage1 as OkObjectResult).Value as List<Publisher>;
            Assert.That(result.First().Id, Is.EqualTo(6));
            Assert.That(result.First().Name, Is.EqualTo("Publisher 6"));
            Assert.That(result.Count, Is.EqualTo(5));

            //page 2
            IActionResult actionResultPage2 = publishersController.GetAllPublishers("name-desc", "Publisher", 2);

            Assert.That(actionResultPage1, Is.TypeOf<OkObjectResult>());

            var result2 = (actionResultPage2 as OkObjectResult).Value as List<Publisher>;
            Assert.That(result2.First().Id, Is.EqualTo(1));
            Assert.That(result2.First().Name, Is.EqualTo("Publisher 1"));
            Assert.That(result2.Count, Is.EqualTo(1));
        }
        
        [Test, Order(2)]
        public void HTTPGET_GetPublisherById_Ok_Test()
        {
            int idPublisher = 5;
            IActionResult actionResult = publishersController.GetPublisherById(idPublisher);
            Assert.That(actionResult, Is.TypeOf<OkObjectResult>());

            var result = (actionResult as OkObjectResult).Value as Publisher;
            Assert.That(result.Id, Is.EqualTo(5));
            Assert.That(result.Name, Is.EqualTo("Publisher 5"));

        }
        [Test, Order(3)]
        public void HTTPGET_GetPublisherById_NotFound_Test()
        {
            int idPublisher = 50;
            IActionResult actionResult = publishersController.GetPublisherById(idPublisher);
            Assert.That(actionResult, Is.TypeOf<NotFoundResult>());
        }

        [Test, Order(4)]
        public void HTTPPOST_AddPublisher_Ok_Test()
        {
            var newPublisher = new PublisherVM()
            {
                Name = "Publiser Test"
            };
            IActionResult actionResult = publishersController.AddPublisher(newPublisher);
            Assert.That(actionResult, Is.TypeOf<CreatedResult>());
        }
        [Test, Order(5)]
        public void HTTPPOST_AddPublisher_Fail_Test()
        {
            var newPublisher = new PublisherVM()
            {
                Name = "123 Publiser Test"
            };
            IActionResult actionResult = publishersController.AddPublisher(newPublisher);
            Assert.That(actionResult, Is.TypeOf<BadRequestObjectResult>());
        }

        [Test, Order(6)]
        public void HTTPDELETE_DeletePublisherById_Ok_Test()
        {
            int idPublisher = 6;
            IActionResult actionResult = publishersController.DeletePublisherById(idPublisher);
            Assert.That(actionResult, Is.TypeOf<OkResult>());
        }
        [Test, Order(7)]
        public void HTTPDELETE_DeletePublisherById_Fail_Test()
        {
            int idPublisher = 6;
            IActionResult actionResult = publishersController.DeletePublisherById(idPublisher);
            Assert.That(actionResult, Is.TypeOf<BadRequestObjectResult>());
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
            context.SaveChanges();
        }
    }
}
