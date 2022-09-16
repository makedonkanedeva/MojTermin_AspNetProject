using MojTermin.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MojTermin.Repository.Interface
{
    public interface IPatientRepository
    {
        IEnumerable<Patient> GetAll();
        Patient Get(Guid? id);
        Patient GetBySsn(long ssn);
        Patient GetByUhid(long uhid);
        Patient GetByNameAndSurname(string name, string surname);
        void Insert(Patient patient);
        void Update(Patient patient);
        void Delete(Patient patient);
        IEnumerable<Patient> FilterByNameAndSurname(string name, string surname);
    }
}
