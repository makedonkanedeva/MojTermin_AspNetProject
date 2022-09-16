using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MojTermin.Domain.DomainModels
{
    public class Specialty : BaseEntity
    {
        [Required]
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
