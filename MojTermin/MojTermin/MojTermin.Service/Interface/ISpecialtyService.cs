using MojTermin.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MojTermin.Service.Interface
{
    public interface ISpecialtyService
    {
        IEnumerable<Specialty> GetAll();
        Specialty Get(Guid? id);
        Specialty GetByName(string name);
        void CreateNewSpecialty(Specialty specialty);
        void DeleteSpecialty(Guid? id);
        void UpdateSpecialty(Specialty specialty);
    }
}
