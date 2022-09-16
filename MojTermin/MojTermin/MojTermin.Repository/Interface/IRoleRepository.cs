using MojTermin.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MojTermin.Repository.Interface
{
    public interface IRoleRepository
    {
        Role Get(Guid? id);
        IEnumerable<Role> GetAll();
        void Insert(Role role);
        void Delete(Role role);
    }
}
