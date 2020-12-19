using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BibiBro.Client.Telegram.Parser;
using Microsoft.AspNetCore.Mvc;

namespace BibiBro.Server.Controllers
{
    [Route("api")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IParserAutoRu _pars;
        public HomeController(IParserAutoRu parser)
        {
            _pars = parser;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Ok");
        }

        [HttpGet]
        [Route("run")]
        public IActionResult Run()
        {
            _pars.Pars();
            return Ok("Ok");
        }
    }
}
