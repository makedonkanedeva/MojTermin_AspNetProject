using MojTermin.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MojTermin.Service.Interface
{
    public interface IVisitService
    {
        IEnumerable<Visit> GetAllVisits();
        Visit Get(Guid? id);
        IEnumerable<Visit> GetByPatientSsn(long ssn);
        IEnumerable<Visit> GetByPatientUhid(int uhid);
        IEnumerable<Visit> GetByDoctorSsn(long ssn);
        IEnumerable<Visit> GetByPatientSsnAndDoctorSsn(long patientSsn, long doctorSsn);
        Visit GetByTerm(DateTime term);
        void CreateNewVisit(Visit v, string userId);
        void DeleteVisit(Guid? id);
        void UpdateVisit(Visit visit);
        IEnumerable<Visit> Filter(long patientSsn, long doctorSsn);
        IEnumerable<Visit> FilterTwo(long patientSsn);
    }
}
