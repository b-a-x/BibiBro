using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BibiBro.Client.Telegram.Collections;
using Microsoft.AspNetCore.Mvc;

namespace BibiBro.Server.Controllers
{
    [Route("api")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private readonly IChatCollection _chatCollection;
        public DataController(IChatCollection chatCollection)
        {
            _chatCollection = chatCollection;
        }

        [HttpGet]
        [Route("chat")]
        public IActionResult Get()
        {
            return Ok(_chatCollection.Collection);
        }
    }
}
