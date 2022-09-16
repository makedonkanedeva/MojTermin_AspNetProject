using MojTermin.Domain;
using MojTermin.Domain.DomainModels;
using MojTermin.Repository.Interface;
using MojTermin.Service.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace MojTermin.Service.Implementation
{
    public class ReferralService : IReferralService
    {
        private readonly IReferralRepository _referralRepository;

        public ReferralService(IReferralRepository referralRepository)
        {
            _referralRepository = referralRepository;
        }

        public void CreateNewReferral(Referral r)
        {
            this._referralRepository.Insert(r);
        }

        public void DeleteReferral(Guid id)
        {
            var referral = this.Get(id);
            this._referralRepository.Delete(referral);
        }

        public IEnumerable<Referral> Filter(long patientSsn)
        {
            return this._referralRepository.Filter(patientSsn);
        }

        public Referral Get(Guid id)
        {
            return this._referralRepository.Get(id);
        }

        public IEnumerable<Referral> GetAllReferrals()
        {
            return this._referralRepository.GetAll();
        }

        public Referral GetByPatientSsn(long patientSsn)
        {
            return this._referralRepository.GetByPatientSsn(patientSsn);
        }

        public Referral GetReferalDetails(BaseEntity model)
        {
            return this._referralRepository.GetReferralDetails(model);
        }

        public void UpdateReferral(Referral d)
        {
            this._referralRepository.Update(d);
        }
    }
}
