using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MICROSERVICES.Common.Proxy.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ApiCustomer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly IHttpClient apiClient;

        public CustomerController(IConfiguration _configuration ,IHttpClient _apiClient)
        {
            configuration = _configuration;
            apiClient = _apiClient;
        }

        [HttpPost]
        public async Task<ActionResult> Post()
        {

            var getUri = ApiPaths.Order.Get(configuration["urlOrder"]);
            var dataString = await apiClient.GetStringAsync(getUri);

            return Ok(dataString);
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {           

            return BadRequest();
        }
    }
}