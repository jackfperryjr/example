using Microsoft.AspNetCore.Identity;
using System;

namespace Example.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public Nullable<int> ZipCode { get; set; } = 00000; // This is hack due to SQLite.
        public DateTime BirthDate { get; set; }
        public int Age { get; set; }
        public string Picture { get; set; }
    }
}
