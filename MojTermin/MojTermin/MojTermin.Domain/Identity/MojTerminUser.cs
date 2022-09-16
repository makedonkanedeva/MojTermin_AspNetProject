using Microsoft.AspNetCore.Identity;
using MojTermin.Domain.DomainModels;

namespace MojTermin.Domain.Identity
{
    public class MojTerminUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public Role Role { get; set; }
    }
}
