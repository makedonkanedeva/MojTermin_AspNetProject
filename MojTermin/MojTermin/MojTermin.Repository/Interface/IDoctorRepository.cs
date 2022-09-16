using MojTermin.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MojTermin.Repository.Interface
{
    public interface IDoctorRepository
    {
        IEnumerable<Doctor> GetAll();
        Doctor Get(Guid? id);
        void Insert(Doctor doctor);
        void Update(Doctor doctor);
        void Delete(Doctor doctor);
        Doctor GetByNameAndSurname(string name, string surname);
        Doctor GetByLicenceNumber(int licenceNumber);
        IEnumerable<Doctor> GetBySpecialty(string specialtyName);
        IEnumerable<Doctor> Filter(string specialtyName);
        IEnumerable<Doctor> FilterByNameAndSurname(string name, string surname);
    }
}
