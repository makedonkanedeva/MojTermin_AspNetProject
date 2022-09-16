using MojTermin.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MojTermin.Repository.Interface
{
    public interface IDiagnosisRepository
    {
        IEnumerable<Diagnosis> GetAll();
        Diagnosis Get(Guid? id);
        Diagnosis GetByName(string name);
        void Update(Diagnosis diagnosis);
        Diagnosis GetByVisit(long patientSsn);
        void Insert(Diagnosis diagnosis);
        void Delete(Diagnosis diagnosis);
        IEnumerable<Diagnosis> Filter(string diagnoseName);
    }
}
