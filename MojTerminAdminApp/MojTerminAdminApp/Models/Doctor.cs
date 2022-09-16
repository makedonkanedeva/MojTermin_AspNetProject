using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MojTerminAdminApp.Models
{
    public class Doctor
    {
        public Guid Id { get; set; }
        public long Ssn { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int LicenceNumber { get; set; }
        public Specialty Specialty { get; set; }

        public string PrintFullName()
        {
            return Name + " " + Surname;
        }
    }
}
