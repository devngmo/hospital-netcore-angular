using Microsoft.AspNetCore.Identity;
using SimpleHospital.Shared.Entities;

namespace SimpleHospital.Web.Models
{
    public class PatientRegistrationModel
    {
        public string patientName { get; set; }
        public int patientAge { get; set; }
        public string reason { get; set; }

        internal PatientEntity createEntity()
        {
            return new PatientEntity { 
                patientAge = patientAge,
                patientName = patientName,
                reason = reason,
                registrationTime = DateTime.Now
            };
        }
    }
}