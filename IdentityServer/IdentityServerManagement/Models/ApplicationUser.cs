using Microsoft.AspNetCore.Identity;

namespace IdentityServerManagement.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string Country { get; set; }
    }
}
