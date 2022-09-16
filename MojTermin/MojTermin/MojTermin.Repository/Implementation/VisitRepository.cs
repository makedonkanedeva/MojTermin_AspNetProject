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
    public class VisitRepository : IVisitRepository
    {
        private readonly ApplicationDbContext context;
        private DbSet<Visit> visits;
        string errorMessage = string.Empty;

        public VisitRepository(ApplicationDbContext context)
        {
            this.context = context;
            visits = context.Set<Visit>();
        }

        public void Delete(Visit visit)
        {
            if (visit == null)
            {
                throw new ArgumentNullException("visit");
            }
            visits.Remove(visit);
            context.SaveChanges();
        }

        public IEnumerable<Visit> Filter(long patientSsn, long doctorSsn)
        {
            return (IEnumerable<Visit>)this.visits.Where(v => v.Patient.Ssn == patientSsn && v.Doctor.Ssn == doctorSsn);
        }

        public IEnumerable<Visit> FilterTwo(long patientSsn)
        {
            return (IEnumerable<Visit>)this.visits.Where(v => v.Patient.Ssn == patientSsn);
        }

        public Visit Get(Guid? id)
        {
            return visits.SingleOrDefault(v => v.Id == id);
        }

        public IEnumerable<Visit> GetAll()
        {
            return visits
                .Include(visit => visit.Patient)
                .Include(visit => visit.Doctor)
                .AsEnumerable();
        }

        public IEnumerable<Visit> GetByDoctorSsn(long ssn)
        {
            return visits.Where(v => v.Doctor.Ssn == ssn);
        }

        public IEnumerable<Visit> GetByPatientSsn(long ssn)
        {
            return visits.Where(v => v.Patient.Ssn == ssn);
        }

        public IEnumerable<Visit> GetByPatientSsnAndDoctorSsn(long patientSsn, long doctorSsn)
        {
            return visits.Where(v => v.Patient.Ssn == patientSsn && v.Doctor.Ssn == doctorSsn);
        }

        public IEnumerable<Visit> GetByPatientUhid(int uhid)
        {
            return visits.Where(v => v.Patient.Uhid == uhid);
        }

        public Visit GetByTerm(DateTime term)
        {
            return (Visit)visits.Where(v => v.Term == term);
        }

        public void Insert(Visit visit)
        {
            if (visit == null)
            {
                throw new ArgumentNullException("visit");
            }
            visits.Add(visit);
            context.SaveChanges();
        }

        public void Update(Visit visit)
        {
            if (visit == null)
            {
                throw new ArgumentNullException("visit");
            }
            visits.Update(visit);
            context.SaveChanges();
        }
    }
}
