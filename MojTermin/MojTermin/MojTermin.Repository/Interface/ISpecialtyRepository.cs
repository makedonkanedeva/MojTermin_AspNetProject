using MojTermin.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MojTermin.Repository.Interface
{
    public interface ISpecialtyRepository
    {
        IEnumerable<Specialty> GetAll();
        Specialty Get(Guid? id);
        Specialty GetByName(string name);
        void Insert(Specialty specialty);
        void Delete(Specialty specialty);
        void Update(Specialty specialty);
    }
}
