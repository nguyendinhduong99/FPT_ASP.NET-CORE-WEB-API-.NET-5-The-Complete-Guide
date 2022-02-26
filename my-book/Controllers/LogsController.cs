using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using my_book.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_book.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogsController : ControllerBase
    {
        private LogsService _logsService;
        public LogsController(LogsService logsService)
        {
            _logsService = logsService;
        }

        [HttpGet("get-all-logdb")]
        public IActionResult GetAllLog()
        {
            try
            {
                var allLog = _logsService.GetAllLog();
                return Ok(allLog);
            }
            catch (Exception )
            {

                return BadRequest("Sorry! We couldn't load the logs from database.");
            }
        }
    }
}
