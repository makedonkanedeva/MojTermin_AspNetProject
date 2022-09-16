using MojTermin.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MojTermin.Service.Interface
{
    public interface IDiagnosisService
    {
        IEnumerable<Diagnosis> GetAllDiagnoses();
        Diagnosis Get(Guid id);
        Diagnosis GetByName(string name);
        void CreateNewDiagnosis(Diagnosis d);

        void DeleteDiagnosis(Guid id);

        void UpdateDiagnosis(Diagnosis d);
        public IEnumerable<Diagnosis> Filter(string diagnosisName);
        //public Diagnosis GetByVisit(long patientSsn);
    }
}
