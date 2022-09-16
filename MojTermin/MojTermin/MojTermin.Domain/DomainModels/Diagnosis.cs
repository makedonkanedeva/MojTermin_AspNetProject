using MojTermin.Domain.Relations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace MojTermin.Domain.DomainModels
{
    public class Diagnosis : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public virtual List<DiagnosisInVisit> Visits { get; set; }
    }
}
