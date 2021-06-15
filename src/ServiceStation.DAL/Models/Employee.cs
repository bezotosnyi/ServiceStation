using Microsoft.AspNetCore.Identity;

namespace ServiceStation.DAL.Models
{
    public class Employee : IdentityUser
    {
        public string Surname { get; set; }
        public string FirstName { get; set; }
    }
}
