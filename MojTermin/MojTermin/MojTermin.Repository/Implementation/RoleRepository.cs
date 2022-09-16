using Microsoft.EntityFrameworkCore;
using MojTermin.Domain.DomainModels;
using MojTermin.Repository.Interface;
using MojTermin.Web.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MojTermin.Repository.Implementation
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ApplicationDbContext context;
        private DbSet<Role> roles;

        public RoleRepository(ApplicationDbContext context)
        {
            this.context = context;
            roles = context.Set<Role>();
        }
        public void Delete(Role role)
        {
            if (role == null)
            {
                throw new ArgumentNullException("role");
            }
            roles.Remove(role);
            context.SaveChanges();
        }

        public Role Get(Guid? id)
        {
            return roles.SingleOrDefault(r => r.Id == id);
        }

        public IEnumerable<Role> GetAll()
        {
            return roles.AsEnumerable();
        }

        public void Insert(Role role)
        {
            if (role == null)
            {
                throw new ArgumentNullException("role");
            }
            roles.Add(role);
            context.SaveChanges();
        }
    }
}
