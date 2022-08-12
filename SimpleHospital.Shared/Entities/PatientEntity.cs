using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleHospital.Shared.Entities
{
    public class PatientEntity
    {
        public string patientId { get; set; }
        public string patientName { get; set; }
        public int patientAge { get; set; }
        public DateTime registrationTime { get; set; }
        public string reason { get; set; }

    }
}
