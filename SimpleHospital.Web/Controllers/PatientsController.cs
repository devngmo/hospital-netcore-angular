using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleHospital.Core.Repositories;
using SimpleHospital.Shared.Entities;
using SimpleHospital.Web.Models;
using System.Text.Json;

namespace SimpleHospital.Web.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class PatientsController : ControllerBase
    {
        private readonly ILogger<PatientsController> _logger;
        private readonly PatientRepository patientRepo;
        public class CustomCommand
        {
            public string cmd { get; set; }
        }

        public PatientsController(ILogger<PatientsController> logger, PatientRepository patientRepository)
        {
            _logger = logger;
            this.patientRepo = patientRepository;
        }

        [HttpPost]
        public IActionResult Post([FromQuery] CustomCommand command)
        {
            if (command.cmd == "generateMassiveData")
            {
                String letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                for (int i = 0; i < 100; i++)
                {
                    patientRepo.Add(
                        new PatientEntity {
                            patientAge = Random.Shared.Next(15, 80),
                            patientName = $"Nguyen Van {letters[Random.Shared.Next(0, letters.Length)]}",
                            reason = "hurt somewhere",
                            registrationTime = DateTime.Now
                        }
                        );
                }
            }
            return Ok();
        }

        [HttpGet("")]
        public IEnumerable<PatientInfo> Get(
            [FromQuery] PaginationParams query
            )
        {
            List<PatientEntity> foundEntities = patientRepo.Filter(query.Page, query.ItemsPerPage);
            var paginationMetadata = new PaginationMetadata(query.Page, patientRepo.Count, query.ItemsPerPage);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetadata));

            int startIndex = (query.Page - 1) * query.ItemsPerPage;
            return foundEntities.Select(e => PatientInfo.from(e, ++startIndex));
        }

        public PatientInfo[] generateSample(PaginationParams query)
        {
            String letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            int totalRecords = 200;
            var paginationMetadata = new PaginationMetadata(query.Page, totalRecords, query.ItemsPerPage);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetadata));

            int startIndex = (query.Page - 1) * query.ItemsPerPage;
            int n = query.ItemsPerPage;
            if (startIndex + n > totalRecords)
                n = totalRecords - startIndex;
            var result = Enumerable.Range(1, n).Select(index => new PatientInfo
            {
                registrationTime = DateTime.Now.AddHours(-index).ToString("dd/MM/yyyy HH:mm:ss"),
                patientName = $"Nguyen Van {letters[Random.Shared.Next(0, letters.Length)]}",
                reason = "Hurt somewhere",
                patientAge = Random.Shared.Next(15, 80),
                patientId = Guid.NewGuid().ToString(),
                orderNo = ++startIndex
            })
            .ToArray();

            return result;
        }
    }
}