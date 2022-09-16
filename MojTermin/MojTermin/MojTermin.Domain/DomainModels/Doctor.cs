using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MojTermin.Domain.DomainModels
{
    public class Doctor : BaseEntity
    {
        [Required]
        public long Ssn { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public int LicenceNumber { get; set; }
        public Specialty Specialty { get; set; }

        public string getNameAndSurname()
        {
            return Name + " " + Surname;
        }
    }
}
