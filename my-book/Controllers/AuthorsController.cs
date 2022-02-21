using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using my_book.Data.Services;
using my_book.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_book.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        public AuthorsService _authorsService;
        public AuthorsController(AuthorsService authorsService)
        {
            _authorsService = authorsService;
        }

        [HttpGet("get-all-authors")]
        public IActionResult GetAllAuthors()
        {
            var allAuthors = _authorsService.GetAllAuthors();
            return Ok(allAuthors);
        }

        [HttpGet("get-author-by-id/{id}")]
        public IActionResult GetAuthorById(int id)
        {
            var author = _authorsService.GetAuthorById(id);
            if (author != null)
            {
                return Ok(author);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("get-author-with-book-title/{id}")]
        public IActionResult GetAuthorWithBooks(int id)
        {
            var author = _authorsService.GetAuthorWithBooks(id);
            if (author != null)
            {
                return Ok(author);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost("add-author")]
        public IActionResult AddAuthor([FromBody] AuthorVM author)
        {
            try
            {
                _authorsService.AddAuthor(author);
                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPut("update-author-by-id/{id}")]
        public IActionResult UpdateAuthorById(int id, [FromBody] AuthorVM author)
        {
            var updateAuthor = _authorsService.UpdateAuthorById(id, author);
            return Ok(updateAuthor);
        }

        [HttpDelete("delete-author-by-id/{id}")]
        public IActionResult DeleteAuthorById(int id)
        {
            try
            {
                _authorsService.DeleteAuthorById(id);
                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
