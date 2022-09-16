using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MojTerminAdminApp.Models
{
    public class User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }

        public string Email { get; set; }
        public Guid Role { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
