using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace src.app.webapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            string apllicationEnvironment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            string defaultEnvironment = "CI";
            return apllicationEnvironment ?? defaultEnvironment;
        }
    }
}
