using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MojTerminAdminApp.Models
{
    public class Patient
    {
        public Guid Id { get; set; }
        public long Ssn { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public long Uhid { get; set; }
        public Doctor Doctor { get; set; }

        public string PrintFullName()
        {
            return Name + " " + Surname;
        }
    }
}
