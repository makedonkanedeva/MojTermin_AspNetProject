using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MojTermin.Domain.DomainModels
{
    public class Visit : BaseEntity
    {
        [Required]
        public DateTime Term { get; set; }
        public Doctor Doctor { get; set; }
        public Patient Patient { get; set; }
    }
}
