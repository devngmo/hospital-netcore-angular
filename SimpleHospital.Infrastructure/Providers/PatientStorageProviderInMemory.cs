using SimpleHospital.Shared.Entities;
using SimpleHospital.Shared.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleHospital.Infrastructure.Providers
{
    public class PatientStorageProviderInMemory : PatientStorageProviderInterface
    {
        List<PatientEntity> _entities = new List<PatientEntity>();
        public string Add(PatientEntity patient)
        {
            patient.patientId = Guid.NewGuid().ToString("N");
            _entities.Add(patient);
            return patient.patientId;
        }

        public int CountAll()
        {
            return _entities.Count();   
        }

        public List<PatientEntity> Filter(int page, int pageSize)
        {
            int totalPage = (int)Math.Ceiling( _entities.Count /(double) pageSize);
            int startIndex = (page - 1) * pageSize;
            if (page > totalPage)
            {
                startIndex = (totalPage - 1) * pageSize;
            }
            
            int n = pageSize;
            if (startIndex + n > _entities.Count)
            {
                n = _entities.Count - startIndex;
            }
            return _entities.Skip(startIndex).Take(n).ToList();
        }

        public List<PatientEntity> GetAll()
        {
            return _entities;
        }

        public async void init()
        {
        }
    }
}
