using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DevopsGenie.Service.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        [HttpGet("ping")]   // ping
        public IActionResult GetPing()
        {
            return Ok("200 OK");
        }
        [HttpGet("version")] // set via startup version
        public IActionResult GetVersion()
        {
            string version = typeof(Startup).Assembly.GetName().Version.ToString();
            return Ok(version);
        }
    }
}