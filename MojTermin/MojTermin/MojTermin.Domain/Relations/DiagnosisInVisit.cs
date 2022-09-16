using MojTermin.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MojTermin.Domain.Relations
{
    public class DiagnosisInVisit : BaseEntity
    {
        public Guid DiagnosisId { get; set; }

        public Guid VisitId { get; set; }

        public DiagnosisInVisit(Guid diagnosisId, Guid visitId)
        {
            DiagnosisId = diagnosisId;
            VisitId = visitId;
        }

    }
}
