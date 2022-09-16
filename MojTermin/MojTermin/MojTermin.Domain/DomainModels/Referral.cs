using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MojTermin.Domain.DomainModels
{
    public class Referral : BaseEntity
    {
        public DateTime Term { get; set; }
        public Patient Patient { get; set; }
        public Doctor ForwardTo { get; set; }
    }
}
