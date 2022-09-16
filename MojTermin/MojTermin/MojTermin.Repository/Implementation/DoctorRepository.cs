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
    public class DoctorRepository : IDoctorRepository
    { 
        private readonly ApplicationDbContext context;
        private DbSet<Doctor> doctors;

        public DoctorRepository(ApplicationDbContext context)
        {
            this.context = context;
            doctors = context.Set<Doctor>();
        }

        public void Delete(Doctor doctor)
        {
            if (doctor == null)
            {
                throw new ArgumentNullException("doctor");
            }
            doctors.Remove(doctor);
            context.SaveChanges();
        }

        public IEnumerable<Doctor> Filter(string specialtyName)
        {
            return (IEnumerable<Doctor>)this.doctors.Where(d => d.Specialty.Name.Contains(specialtyName));
        }

        public IEnumerable<Doctor> FilterByNameAndSurname(string name, string surname)
        {
            return (IEnumerable<Doctor>)this.doctors.Where(d => d.Name.Contains(name) && d.Surname.Contains(surname));
        }

        public Doctor Get(Guid? id)
        {
            return doctors
                .Include(z => z.Specialty)
                .SingleOrDefault(d => d.Id == id);
        }

        public IEnumerable<Doctor> GetAll()
        {
            return doctors
                .Include(z => z.Specialty)
                .AsEnumerable();
        }

        public Doctor GetByLicenceNumber(int licenceNumber)
        {
            return (Doctor)doctors.Where(d => d.LicenceNumber == licenceNumber);
        }

        public Doctor GetByNameAndSurname(string name, string surname)
        {
            return (Doctor) doctors.Where(d => d.Name.Contains(name) && d.Surname.Contains(surname));
        }

        public IEnumerable<Doctor> GetBySpecialty(string specialtyName)
        {
            return doctors.Where(d => d.Specialty.Name.Contains(specialtyName));
        }

        public void Insert(Doctor doctor)
        {
            if (doctor == null)
            {
                throw new ArgumentNullException("doctor");
            }
            doctors.Add(doctor);
            context.SaveChanges();
        }

        public void Update(Doctor doctor)
        {
            if (doctor == null)
            {
                throw new ArgumentNullException("doctor");
            }
            doctors.Update(doctor);
            context.SaveChanges();
        }
    }
}
