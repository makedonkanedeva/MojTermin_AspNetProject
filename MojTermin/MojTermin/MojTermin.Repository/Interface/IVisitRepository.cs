using MojTermin.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MojTermin.Repository.Interface
{
    public interface IVisitRepository
    {
        IEnumerable<Visit> GetAll();
        Visit Get(Guid? id);
        IEnumerable<Visit> GetByPatientSsn(long ssn);
        IEnumerable<Visit> GetByPatientUhid(int uhid);
        IEnumerable<Visit> GetByDoctorSsn(long ssn);
        IEnumerable<Visit> GetByPatientSsnAndDoctorSsn(long patientSsn, long doctorSsn);
        Visit GetByTerm(DateTime term);
        void Insert(Visit visit);
        void Delete(Visit visit);
        void Update(Visit visit);
        IEnumerable<Visit> Filter(long patientSsn, long doctorSsn);
        IEnumerable<Visit> FilterTwo(long patientSsn);

    }
}
