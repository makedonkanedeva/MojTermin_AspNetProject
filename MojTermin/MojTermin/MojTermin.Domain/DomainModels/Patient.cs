using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MojTermin.Domain.DomainModels
{
    public class Patient : BaseEntity {

        [Required]
        public long Ssn { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public long Uhid { get; set; }
        public Doctor Doctor { get; set; }
    }
}
