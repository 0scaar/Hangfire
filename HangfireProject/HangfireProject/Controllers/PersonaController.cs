using Hangfire;
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
        private readonly IBackgroundJobClient backgroundJobClient;
        private readonly INoticationRepository noticationRepository;

        public PersonaController(IPersonRepository personRepository, IBackgroundJobClient backgroundJobClient, INoticationRepository noticationRepository)
        {
            this.personRepository = personRepository;
            this.backgroundJobClient = backgroundJobClient;
            this.noticationRepository = noticationRepository;
        }

        [HttpPost("GetPeople")]
        public IActionResult GetPeople([FromBody] NameRequest request) 
        {
            var people = personRepository.GetPeople(request.Name);

            people.ForEach(person =>
            {
                backgroundJobClient.Enqueue<INoticationRepository>(not => not.SendEmail(person.Email));
            });

            return Ok(people);
        }

        [HttpPost("ScheduleSendEmail")]
        public IActionResult ScheduleSendEmail([FromBody] NameRequest request)
        {
            var people = personRepository.GetPeople(request.Name);
            var period = 3;

            people.ForEach(person =>
            {
                var jobId = backgroundJobClient.Schedule(() => noticationRepository.SendEmail(person.Email), TimeSpan.FromSeconds(period));
                backgroundJobClient.ContinueJobWith(jobId, () => Console.WriteLine($"the job {jobId} has been finished"));
                period += 3;
            });

            return Ok(people);
        }
    }
}
