using SimpleHospital.Shared.Entities;
using SimpleHospital.Shared.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleHospital.Core.Repositories
{
    public class PatientRepository
    {
        PatientStorageProviderInterface storage;
        public int Count => storage.CountAll();
        public PatientRepository(PatientStorageProviderInterface storageProvider)
        {
            storage = storageProvider;
        }
        public string Add(PatientEntity patient)
        {
            return storage.Add(patient);
        }

        public List<PatientEntity> Filter(int page, int pageSize)
        {
            return storage.Filter(page, pageSize);
        }
    }
}
