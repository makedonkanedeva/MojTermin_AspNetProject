using MojTermin.Domain.DomainModels;
using MojTermin.Repository.Interface;
using MojTermin.Service.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace MojTermin.Service.Implementation
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _doctorRepository;

        public DoctorService(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }
        public Doctor Get(Guid id)
        {
            return this._doctorRepository.Get(id);
        }
        public void CreateNewDoctor(Doctor d)
        {
            this._doctorRepository.Insert(d);
        }

        public void DeleteDoctor(Guid id)
        {
            var doctor = this.Get(id);
            this._doctorRepository.Delete(doctor);
        }

        public IEnumerable<Doctor> Filter(string specialtyName)
        {
            return this._doctorRepository.Filter(specialtyName);
        }

        public IEnumerable<Doctor> FilterByNameAndSurname(string name, string surname)
        {
            return this._doctorRepository.FilterByNameAndSurname(name, surname);
        }

        public Doctor GetByLicenceNumber(int licenceNumber)
        {
            return this._doctorRepository.GetByLicenceNumber(licenceNumber);
        }

        public Doctor GetByNameAndSurname(string name, string surname)
        {
            return this._doctorRepository.GetByNameAndSurname(name, surname);
        }

        public IEnumerable<Doctor> GetBySpecialty(string specialtyName)
        {
            return this._doctorRepository.GetBySpecialty(specialtyName);
        }

        public IEnumerable<Doctor> GetAllDoctors()
        {
            return _doctorRepository.GetAll();
        }

        public void UpdateDoctor(Doctor d)
        {
            this._doctorRepository.Update(d);
        }

    }
}
