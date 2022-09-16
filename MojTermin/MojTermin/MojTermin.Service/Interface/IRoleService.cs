using MojTermin.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MojTermin.Service.Interface
{
    public interface IRoleService
    {
        Role Get(Guid id);
        IEnumerable<Role> GetAllRoles();
        void CreateNewRole(Role r);

        void DeleteRole(Guid id);
    }
}
