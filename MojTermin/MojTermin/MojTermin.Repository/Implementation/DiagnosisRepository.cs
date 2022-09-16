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
    public class DiagnosisRepository : IDiagnosisRepository
    {
        private readonly ApplicationDbContext context;
        private DbSet<Diagnosis> diagnoses;

        public DiagnosisRepository(ApplicationDbContext context)
        {
            this.context = context;
            diagnoses = context.Set<Diagnosis>();
        }

        public void Delete(Diagnosis diagnosis)
        {
            if (diagnosis == null)
            {
                throw new ArgumentNullException("diagnosis");
            }
            
            diagnoses.Remove(diagnosis);
            context.SaveChanges();
        }

        public IEnumerable<Diagnosis> Filter(string diagnoseName)
        {
            return (IEnumerable<Diagnosis>)this.diagnoses.Where(d => d.Name.Contains(diagnoseName));
        }

        public Diagnosis Get(Guid? id)
        {
            return diagnoses
                .Include(z => z.Visits)
                .SingleOrDefault(d => d.Id == id);
        }

        public IEnumerable<Diagnosis> GetAll()
        {
            return diagnoses
                .Include(z => z.Visits)
                .AsEnumerable();
        }

        public Diagnosis GetByName(string name)
        {
            return (Diagnosis)diagnoses.Where(d => d.Name.Contains(name));
        }

        public Diagnosis GetByVisit(long patientSsn)
        {
            throw new NotImplementedException();
        }

        public void Insert(Diagnosis diagnosis)
        {
            if (diagnosis == null)
            {
                throw new ArgumentNullException("diagnosis");
            }
            diagnoses.Add(diagnosis);
            context.SaveChanges();
        }

        public void Update(Diagnosis diagnosis)
        {
            if (diagnosis == null)
            {
                throw new ArgumentNullException("diagnosis");
            }
            diagnoses.Update(diagnosis);
            context.SaveChanges();
        }
    }
}
