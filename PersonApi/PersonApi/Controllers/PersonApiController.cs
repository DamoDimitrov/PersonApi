using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PersonApi.BL.Services;
using PersonApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonApiController : ControllerBase
    {
        private readonly PersonService _personService;
        private readonly ILogger<PersonApiController> _logger;

        public PersonApiController(PersonService personService, ILogger<PersonApiController> logger)
        {
            _personService = personService;
            _logger = logger;
        }

        [HttpPost]
        public IActionResult RegisterPerson(Person person)
        {
            _personService.RegisterPersonASync(person);
            return Ok();
        }
    }
}
