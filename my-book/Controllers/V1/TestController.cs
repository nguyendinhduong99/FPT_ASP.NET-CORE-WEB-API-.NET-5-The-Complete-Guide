using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_book.Controllers.V1
{
    //[ApiVersion("1.0")]
    [ApiVersion("1.0")]
    [ApiVersion("1.1")]
    [ApiVersion("1.2")]
    [ApiVersion("1.3")]


    [Route("api/[controller]")]
    //[Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        //[HttpGet("get-data-test")]
        //public IActionResult GetV11()
        //{
        //    return Ok("This is controller V1.1");
        //}

        [HttpGet("get-data-test"),MapToApiVersion("1.1")]
        public IActionResult GetV11()
        {
            return Ok("This is controller V1.1");
        }

        [HttpGet("get-data-test"), MapToApiVersion("1.2")]
        public IActionResult GetV12()
        {
            return Ok("This is controller V1.2");
        }

        [HttpGet("get-data-test"), MapToApiVersion("1.3")]
        public IActionResult GetV13()
        {
            return Ok("This is controller V1.3");
        }
    }
}
