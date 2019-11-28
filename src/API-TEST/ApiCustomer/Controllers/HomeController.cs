using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ApiCustomer.Controllers
{
    [Route("")]
    public class HomeController : ControllerBase
    {
        [HttpGet("ping")]
        public IActionResult Ping() => Ok();
    }
}