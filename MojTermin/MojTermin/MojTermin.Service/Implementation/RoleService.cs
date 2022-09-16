using MojTermin.Domain.DomainModels;
using MojTermin.Repository.Interface;
using MojTermin.Service.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace MojTermin.Service.Implementation
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }
        public void CreateNewRole(Role r)
        {
            this._roleRepository.Insert(r);
        }

        public void DeleteRole(Guid id)
        {
            var role = this.Get(id);
            this._roleRepository.Delete(role);
        }

        public Role Get(Guid id)
        {
            return this._roleRepository.Get(id);
        }

        public IEnumerable<Role> GetAllRoles()
        {
            return _roleRepository.GetAll();
        }
    }
}
