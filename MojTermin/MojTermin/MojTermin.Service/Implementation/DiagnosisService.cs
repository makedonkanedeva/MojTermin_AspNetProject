using MojTermin.Domain.DomainModels;
using MojTermin.Repository.Interface;
using MojTermin.Service.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace MojTermin.Service.Implementation
{
    public class DiagnosisService : IDiagnosisService
    {
        private readonly IDiagnosisRepository _diagnosisRepository;

        public DiagnosisService(IDiagnosisRepository diagnosisRepository)
        {
            _diagnosisRepository = diagnosisRepository;
        }

        public void CreateNewDiagnosis(Diagnosis d)
        {
            this._diagnosisRepository.Insert(d);
        }

        public void DeleteDiagnosis(Guid id)
        {
            var diagnosis = this.Get(id);
            this._diagnosisRepository.Delete(diagnosis);
        }

        public IEnumerable<Diagnosis> Filter(string diagnosisName)
        {
            return this._diagnosisRepository.Filter(diagnosisName);
        }

        public Diagnosis Get(Guid id)
        {
            return this._diagnosisRepository.Get(id);
        }

        public IEnumerable<Diagnosis> GetAllDiagnoses()
        {
            return _diagnosisRepository.GetAll();
        }

        public Diagnosis GetByName(string name)
        {
            return this._diagnosisRepository.GetByName(name);
        }

        public void UpdateDiagnosis(Diagnosis d)
        {
            this._diagnosisRepository.Update(d);
        }
    }
}
