using Microsoft.EntityFrameworkCore;
using MojTermin.Domain.Identity;
using MojTermin.Repository.Interface;
using MojTermin.Web.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MojTermin.Repository.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext context;
        private DbSet<MojTerminUser> entities;
        string errorMessage = string.Empty;

        public UserRepository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<MojTerminUser>();
        }
        public IEnumerable<MojTerminUser> GetAll()
        {
            return entities.AsEnumerable();
        }

        public MojTerminUser Get(string id)
        {
            return entities
               .SingleOrDefault(s => s.Id == id);
        }
        public void Insert(MojTerminUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            context.SaveChanges();
        }

        public void Update(MojTerminUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Update(entity);
            context.SaveChanges();
        }

        public void Delete(MojTerminUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            context.SaveChanges();
        }
    }
}
