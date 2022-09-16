using Microsoft.EntityFrameworkCore;
using MojTermin.Domain;
using MojTermin.Domain.DomainModels;
using MojTermin.Repository.Interface;
using MojTermin.Web.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MojTermin.Repository.Implementation
{
    public class ReferralRepository : IReferralRepository
    {
        private readonly ApplicationDbContext context;
        private DbSet<Referral> referrals;
        string errorMessage = string.Empty;

        public ReferralRepository(ApplicationDbContext context)
        {
            this.context = context;
            referrals = context.Set<Referral>();
        }

        public void Delete(Referral referral)
        {
            if (referral == null)
            {
                throw new ArgumentNullException("referral");
            }
            referrals.Remove(referral);
            context.SaveChanges();
        }

        public IEnumerable<Referral> Filter(long patientSsn)
        {
            return (IEnumerable<Referral>)this.referrals.Where(r => r.Patient.Ssn == patientSsn);
        }

        public Referral Get(Guid? id)
        {
            return referrals
                .Include(z => z.Patient)
                .Include(z => z.ForwardTo)
                .Include("ForwardTo.Specialty")
                .Include("Patient.Doctor")
                .SingleOrDefault(r => r.Id == id);
        }

        public IEnumerable<Referral> GetAll()
        {
            return referrals
                .Include(z => z.Patient)
                .Include(z => z.ForwardTo)
                .Include("ForwardTo.Specialty")
                .Include("Patient.Doctor")
                .AsEnumerable();
        }

        public Referral GetByPatientSsn(long patientSsn)
        {
            return (Referral)referrals.Where(r => r.Patient.Ssn == patientSsn);
        }

        public Referral GetReferralDetails(BaseEntity model)
        {
            return referrals
                .Include(z => z.Patient)
                .Include(z => z.Patient.Doctor)
                .Include(z => z.ForwardTo)
                .Include("ForwardTo.Specialty")
                .SingleOrDefaultAsync(z => z.Id == model.Id).Result;
        }

        public void Insert(Referral referral)
        {
            if (referral == null)
            {
                throw new ArgumentNullException("referral");
            }
            referrals.Add(referral);
            context.SaveChanges();
        }

        public void Update(Referral referral)
        {
            if (referral == null)
            {
                throw new ArgumentNullException("referral");
            }
            referrals.Update(referral);
            context.SaveChanges();
        }
    }
}
