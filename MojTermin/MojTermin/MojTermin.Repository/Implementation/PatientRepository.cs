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
    public class PatientRepository : IPatientRepository
    {
        private readonly ApplicationDbContext context;
        private DbSet<Patient> patients;
        string errorMessage = string.Empty;

        public PatientRepository(ApplicationDbContext context)
        {
            this.context = context;
            patients = context.Set<Patient>();
        }

        public void Delete(Patient patient)
        {
            if (patient == null)
            {
                throw new ArgumentNullException("patient");
            }
            patients.Remove(patient);
            context.SaveChanges();
        }

        public IEnumerable<Patient> FilterByNameAndSurname(string name, string surname)
        {
            return (IEnumerable<Patient>)this.patients.Where(p => p.Name.Contains(name) && p.Surname.Contains(surname));
        }

        public Patient Get(Guid? id)
        {
            return patients
                .Include(z => z.Doctor)
                .SingleOrDefault(p => p.Id == id);
        }

        public IEnumerable<Patient> GetAll()
        {
            return patients
                .Include(z => z.Doctor)
                .AsEnumerable();
        }

        public Patient GetBySsn(long ssn)
        {
            return (Patient)patients.Where(p => p.Ssn == ssn);
        }

        public Patient GetByNameAndSurname(string name, string surname)
        {
            return (Patient)patients.Where(p => p.Name.Contains(name) && p.Surname.Contains(surname));
        }

        public Patient GetByUhid(long uhid)
        {
            return (Patient)patients.Where(p => p.Uhid == uhid);
        }

        public void Insert(Patient patient)
        {
            if (patient == null)
            {
                throw new ArgumentNullException("patient");
            }
            patients.Add(patient);
            context.SaveChanges();
        }

        public void Update(Patient patient)
        {
            if (patient == null)
            {
                throw new ArgumentNullException("patient");
            }
            patients.Update(patient);
            context.SaveChanges();
        }
    }
}
