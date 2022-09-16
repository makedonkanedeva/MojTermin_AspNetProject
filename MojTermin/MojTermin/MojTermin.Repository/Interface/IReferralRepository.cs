using MojTermin.Domain;
using MojTermin.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MojTermin.Repository.Interface
{
    public interface IReferralRepository
    {
        IEnumerable<Referral> GetAll();
        Referral Get(Guid? id);
        Referral GetByPatientSsn(long patientSsn);
        void Insert(Referral referral);
        void Update(Referral referral);
        void Delete(Referral referral);
        IEnumerable<Referral> Filter(long patientSsn);
        Referral GetReferralDetails(BaseEntity model);
    }
}
