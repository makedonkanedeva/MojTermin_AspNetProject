using MojTermin.Domain;
using MojTermin.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MojTermin.Service.Interface
{
    public interface IReferralService
    {
        IEnumerable<Referral> GetAllReferrals();
        Referral Get(Guid id);
        Referral GetByPatientSsn(long patientSsn);
        void CreateNewReferral(Referral r);

        void DeleteReferral(Guid id);

        void UpdateReferral(Referral d);
        public IEnumerable<Referral> Filter(long patientSsn);
        Referral GetReferalDetails(BaseEntity model);
    }
}
