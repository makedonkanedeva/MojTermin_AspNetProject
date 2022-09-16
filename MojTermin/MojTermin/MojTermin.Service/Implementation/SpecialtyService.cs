using MojTermin.Domain.DomainModels;
using MojTermin.Repository.Interface;
using MojTermin.Service.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace MojTermin.Service.Implementation
{
    public class SpecialtyService : ISpecialtyService
    {
        private readonly ISpecialtyRepository _specialtyRepository;

        public SpecialtyService(ISpecialtyRepository specialtyRepository)
        {
            _specialtyRepository = specialtyRepository;
        }

        public void CreateNewSpecialty(Specialty specialty)
        {
            this._specialtyRepository.Insert(specialty);
        }

        public void DeleteSpecialty(Guid? id)
        {
            var patient = this.Get(id);
            this._specialtyRepository.Delete(patient);
        }

        public Specialty Get(Guid? id)
        {
            return this._specialtyRepository.Get(id);
        }

        public IEnumerable<Specialty> GetAll()
        {
            return _specialtyRepository.GetAll();
        }

        public Specialty GetByName(string name)
        {
            return _specialtyRepository.GetByName(name);
        }

        public void UpdateSpecialty(Specialty specialty)
        {
            this._specialtyRepository.Update(specialty);
        }
    }
}
