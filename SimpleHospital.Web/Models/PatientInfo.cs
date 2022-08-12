using SimpleHospital.Shared.Entities;

namespace SimpleHospital.Web.Models
{
    public class PatientInfo
    {
        public string patientId { get; set; }
        public string patientName { get; set; }
        public int patientAge { get; set; }
        public string registrationTime { get; set; }
        public string reason { get; set; }

        public int orderNo { get; set; }

        internal static PatientInfo from(PatientEntity e, int orderNo)
        {
            return new PatientInfo { 
                patientId = e.patientId,
                patientName = e.patientName,
                patientAge = e.patientAge,
                registrationTime = e.registrationTime.ToString("dd/MM/yyyy HH:mm:ss"),
                orderNo = orderNo,
                reason = e.reason
            };
        }
    }
}