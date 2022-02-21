using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using my_book.Data.Services;
using my_book.Data.ViewModels;
using my_book.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_book.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishersController : ControllerBase
    {
        public PublishersService _publishersService;
        public PublishersController(PublishersService publishersService)
        {
            _publishersService = publishersService;
        }

        [HttpGet("get-all-publishers")]
        public IActionResult GetAllPublishers( string sortBy, string searchString, int? pageNumber)
        {
            try
            {
                var allPublishers = _publishersService.GetAllPublishers(sortBy, searchString, pageNumber);
                return Ok(allPublishers);
            }
            catch (Exception)
            {

                return BadRequest("Sorry! Could not loading publisher");
            }
        }

        [HttpGet("get-publisher-by-id/{id}")]
        public IActionResult GetPublisherById(int id)
        {
            //throw new Exception("This is an exception that will be handler by middleware");

            var publisher = _publishersService.GetPublisherById(id);
            if(publisher != null)
            {
                return Ok(publisher);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("get-publisher-with-book-and-author/{id}")]
        public IActionResult GetPublisherWithBookAndAuthor(int id)
        {
            var publisher = _publishersService.GetPublisherWithBookAndAuthors(id);
            if (publisher != null)
            {
                return Ok(publisher);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost("add-publisher")]
        public IActionResult AddPublisher([FromBody] PublisherVM publisher)
        {
            try
            {
                var newPublisher = _publishersService.AddPublisher(publisher);
                return Created(nameof(AddPublisher), newPublisher);
            }
            catch (PublisherNameException ex)
            {
                return BadRequest($"{ex.Message}, Publisher name: {ex.PublisherName}");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPut("update-publisher-by-id/{id}")]
        public IActionResult UpdateBookById(int id, [FromBody] PublisherVM publisher)
        {
            var updatePublisher = _publishersService.UpdatePublisherById(id, publisher);
            return Ok(updatePublisher);
        }

        [HttpDelete("delete-publisher-by-id/{id}")]
        public IActionResult DeletePublisherById(int id)
        {
            try
            {
                _publishersService.DeletePublisherById(id);
                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
