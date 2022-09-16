using MojTermin.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MojTermin.Service.Interface
{
    public interface IDoctorService
    {
        IEnumerable<Doctor> GetAllDoctors();
        Doctor Get(Guid id);
        Doctor GetByLicenceNumber(int licenceNumber);
        Doctor GetByNameAndSurname(string name, string surname);
        IEnumerable<Doctor> GetBySpecialty(string specialtyName);
        void CreateNewDoctor(Doctor d);

        void DeleteDoctor(Guid id);

        void UpdateDoctor(Doctor d);
        public IEnumerable<Doctor> Filter(string specialtyName);
        IEnumerable<Doctor> FilterByNameAndSurname(string name, string surname);
    }
}
