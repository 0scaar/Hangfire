using HangfireProject.Application.Repository;
using HangfireProject.Requests;
using Microsoft.AspNetCore.Mvc;

namespace HangfireProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonaController : ControllerBase
    {
        private readonly IPersonRepository personRepository;

        public PersonaController(IPersonRepository personRepository)
        {
            this.personRepository = personRepository;
        }

        [HttpPost]
        public IActionResult GetPeople([FromBody] NameRequest request) 
        {
            var people = personRepository.GetPeople(request.Name);

            return Ok(people);
        }
    }
}
