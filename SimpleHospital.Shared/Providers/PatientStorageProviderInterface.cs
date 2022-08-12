using SimpleHospital.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleHospital.Shared.Providers
{
    public interface PatientStorageProviderInterface
    {
        int CountAll();
        string Add(PatientEntity patient);
        List<PatientEntity> GetAll();
        List<PatientEntity> Filter(int page, int pageSize);
    }
}
