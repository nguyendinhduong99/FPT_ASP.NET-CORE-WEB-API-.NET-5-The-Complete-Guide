using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_book.Controllers.V2
{
    //[ApiVersion("2.0")]
    [ApiVersion("2.0")]
    [ApiVersion("2.1")]
    [ApiVersion("2.2")]
    [ApiVersion("2.3")]

    [Route("api/[controller]")]
    //[Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        //[HttpGet("get-data-test")]
        //public IActionResult Get()
        //{
        //    return Ok("This is controller V2");

        [HttpGet("get-data-test"), MapToApiVersion("2.0")]
        public IActionResult Get20()
        {
            return Ok("This is controller V2.0");
        }

        [HttpGet("get-data-test"), MapToApiVersion("2.1")]
        public IActionResult Get21()
        {
            return Ok("This is controller V2.1");
        }

        [HttpGet("get-data-test"), MapToApiVersion("2.2")]
        public IActionResult GetV22()
        {
            return Ok("This is controller V2.2");
        }

        [HttpGet("get-data-test"), MapToApiVersion("2.3")]
        public IActionResult GetV23()
        {
            return Ok("This is controller V2.3");
        }
    }
}
