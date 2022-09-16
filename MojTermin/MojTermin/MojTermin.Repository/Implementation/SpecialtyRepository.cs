using Microsoft.EntityFrameworkCore;
using MojTermin.Domain.DomainModels;
using MojTermin.Repository.Interface;
using MojTermin.Web.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MojTermin.Repository.Implementation
{
    public class SpecialtyRepository : ISpecialtyRepository
    {
        private readonly ApplicationDbContext context;
        private DbSet<Specialty> specialties;

        public SpecialtyRepository(ApplicationDbContext context)
        {
            this.context = context;
            specialties = context.Set<Specialty>();
        }

        public void Delete(Specialty specialty)
        {
            if (specialty == null)
            {
                throw new ArgumentNullException("specialty");
            }
            specialties.Remove(specialty);
            context.SaveChanges();
        }

        public Specialty Get(Guid? id)
        {
            return specialties.SingleOrDefault(s => s.Id.Equals(id));
        }

        public IEnumerable<Specialty> GetAll()
        {
            return specialties.AsEnumerable();
        }

        public Specialty GetByName(string name)
        {
            return (Specialty)specialties.Where(p => p.Name.Contains(name));
        }

        public void Insert(Specialty specialty)
        {
            if (specialty == null)
            {
                throw new ArgumentNullException("specialty");
            }
            specialties.Add(specialty);
            context.SaveChanges();
        }

        public void Update(Specialty specialty)
        {
            if (specialty == null)
            {
                throw new ArgumentNullException("specialty");
            }
            specialties.Update(specialty);
            context.SaveChanges();
        }
    }
}
