using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleHospital.Core.Repositories;
using SimpleHospital.Web.Models;
using System.Text.Json;

namespace SimpleHospital.Web.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class PatientRegistrationController : ControllerBase
    {
        private readonly ILogger<PatientRegistrationController> _logger;
        private readonly PatientRepository patientRepo;
        public PatientRegistrationController(ILogger<PatientRegistrationController> logger, PatientRepository patientRepository)
        {
            _logger = logger;
            patientRepo = patientRepository;
        }

        [HttpPost]
        public IActionResult Post(
            [FromBody] PatientRegistrationModel model
            )
        {
            string id = patientRepo.Add(model.createEntity());
            Console.WriteLine(JsonSerializer.Serialize(model));
            return Ok(new { id = id });
        }
    }
}