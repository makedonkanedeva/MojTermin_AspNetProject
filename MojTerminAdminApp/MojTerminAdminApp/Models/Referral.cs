using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MojTerminAdminApp.Models
{
    public class Referral
    {
        public Guid Id { get; set; }
        public DateTime Term { get; set; }
        public Patient Patient { get; set; }
        public Doctor ForwardTo { get; set; }
    }
}
