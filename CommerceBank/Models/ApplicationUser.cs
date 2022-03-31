using Microsoft.AspNetCore.Identity;

namespace CommerceBank.Models
{
    public class ApplicationUser : IdentityUser
    {
        public float TotalBalance { get; set; }
    }
}
