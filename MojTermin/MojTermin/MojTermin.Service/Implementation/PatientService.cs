using MojTermin.Domain.DomainModels;
using MojTermin.Repository.Interface;
using MojTermin.Service.Interface;
using System;
using System.Collections.Generic;

namespace MojTermin.Service.Implementation
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;

        public PatientService(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }
        public void CreateNewPatient(Patient p)
        {
            this._patientRepository.Insert(p);
        }

        public void DeletePatient(Guid id)
        {
            var patient = this.Get(id);
            this._patientRepository.Delete(patient);
        }

        public List<Patient> FilterByNameAndSurname(string name, string surname)
        {
            return (List<Patient>)this._patientRepository.FilterByNameAndSurname(name, surname);
        }

        public Patient Get(Guid id)
        {
            return this._patientRepository.Get(id);
        }

        public IEnumerable<Patient> GetAllPatients()
        {
            return _patientRepository.GetAll();
        }

        public Patient GetBySsn(long ssn)
        {
            return _patientRepository.GetBySsn(ssn);
        }

        public Patient GetByNameAndSurname(string name, string surname)
        {
            return _patientRepository.GetByNameAndSurname(name, surname);
        }

        public Patient GetByUhid(long uhid)
        {
            return _patientRepository.GetByUhid(uhid);
        }

        public void UpdatePatient(Patient p)
        {
            this._patientRepository.Update(p);
        }
    }
}
