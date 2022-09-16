using MojTermin.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MojTermin.Service.Interface
{
    public interface IPatientService
    {
        IEnumerable<Patient> GetAllPatients();
        Patient Get(Guid id);
        Patient GetBySsn(long ssn);
        Patient GetByUhid(long uhid);
        Patient GetByNameAndSurname(string name, string surname);
        void CreateNewPatient(Patient p);
        void UpdatePatient(Patient p);
        void DeletePatient(Guid id);
        List<Patient> FilterByNameAndSurname(string name, string surname);
    }
}
