using MojTermin.Domain.DomainModels;
using MojTermin.Repository.Interface;
using MojTermin.Service.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace MojTermin.Service.Implementation
{
    public class VisitService : IVisitService
    {
        private readonly IVisitRepository _visitRepository;
        private readonly IEmailRepository _emailRepository;
        private readonly IUserRepository _userRepository;

        public VisitService(IVisitRepository visitRepository, IEmailRepository emailRepository, IUserRepository userRepository)
        {
            _visitRepository = visitRepository;
            _emailRepository = emailRepository;
            _userRepository = userRepository;
        }

        public void CreateNewVisit(Visit v, string userId)
        {
            var loggedInUser = this._userRepository.Get(userId);

            EmailMessage message = new EmailMessage();
            message.MailTo = loggedInUser.Email;
            message.Subject = "Sucessfuly created visit!";
            message.Status = false;

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Your visit is created. Visit details: ");

            sb.AppendLine("Patient Name: " + v.Patient.Name);
            sb.AppendLine("Patient Surname: " + v.Patient.Surname);
            sb.AppendLine("Patient Ssn: " + v.Patient.Ssn);
            sb.AppendLine("Patient Uhid: " + v.Patient.Uhid);
            sb.AppendLine("Doctor: " + v.Doctor.getNameAndSurname());
            sb.AppendLine("Term: " + v.Term.ToString());

            message.Content = sb.ToString();

            this._emailRepository.Insert(message);
            this._visitRepository.Insert(v);
        }

        public void DeleteVisit(Guid? id)
        {
            var visit = this.Get(id);
            this._visitRepository.Delete(visit);
        }

        public IEnumerable<Visit> Filter(long patientSsn, long doctorSsn)
        {
            return this._visitRepository.Filter(patientSsn, doctorSsn);
        }

        public IEnumerable<Visit> FilterTwo(long patientSsn)
        {
            return this._visitRepository.FilterTwo(patientSsn);
        }

        public Visit Get(Guid? id)
        {
            return this._visitRepository.Get(id);
        }

        public IEnumerable<Visit> GetAllVisits()
        {
            return _visitRepository.GetAll();
        }

        public IEnumerable<Visit> GetByDoctorSsn(long ssn)
        {
            return _visitRepository.GetByDoctorSsn(ssn);
        }

        public IEnumerable<Visit> GetByPatientSsn(long ssn)
        {
            return _visitRepository.GetByPatientSsn(ssn);
        }

        public IEnumerable<Visit> GetByPatientSsnAndDoctorSsn(long patientSsn, long doctorSsn)
        {
            return _visitRepository.GetByPatientSsnAndDoctorSsn(patientSsn, doctorSsn);
        }

        public IEnumerable<Visit> GetByPatientUhid(int uhid)
        {
            return _visitRepository.GetByPatientUhid(uhid);
        }

        public Visit GetByTerm(DateTime term)
        {
            return _visitRepository.GetByTerm(term);
        }

        public void UpdateVisit(Visit visit)
        {
            this._visitRepository.Update(visit);
        }
    }
}
