using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using SimpleHospital.Shared.Entities;
using SimpleHospital.Shared.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleHospital.Infrastructure.Providers
{

    public class PatientStorageProviderMongoDB : PatientStorageProviderInterface
    {
        String _connectionStr;
        MongoClient client;
        IMongoDatabase dbHospital;
        IMongoCollection<PatientEntity> patients;
        public PatientStorageProviderMongoDB(String connectionStr)
        {
            this._connectionStr = connectionStr;
            BsonClassMap.RegisterClassMap<PatientEntity>(cm => {
                cm.AutoMap();
                cm.MapIdMember(e => e.patientId)
                .SetIdGenerator(StringObjectIdGenerator.Instance)
                .SetSerializer(new StringSerializer(BsonType.ObjectId));
            });
        }

        public async void init()
        {
            client = new MongoClient("mongodb://localhost:27017");
            dbHospital = client.GetDatabase("hospital");
            patients = dbHospital.GetCollection<PatientEntity>("patients");
        }

        public string Add(PatientEntity patient)
        {
            var task = patients.InsertOneAsync(patient);
            task.Wait();
            return patient.patientId;
        }

        public int CountAll()
        {
            return (int)patients.CountDocuments(Builders<PatientEntity>.Filter.Empty);
        }

        public List<PatientEntity> Filter(int page, int pageSize)
        {
            int countAll = CountAll();
            int totalPage = (int)Math.Ceiling(countAll /(double) pageSize);
            int startIndex = (page - 1) * pageSize;
            if (page > totalPage)
            {
                startIndex = (totalPage - 1) * pageSize;
            }
            
            int n = pageSize;
            if (startIndex + n > countAll)
            {
                n = countAll - startIndex;
            }
            
            return patients.Find(Builders<PatientEntity>.Filter.Empty).Skip(startIndex).Limit(n).ToList();
        }

        public List<PatientEntity> GetAll()
        {
            return patients.Find(Builders<PatientEntity>.Filter.Empty).ToList();
        }


    }
}
