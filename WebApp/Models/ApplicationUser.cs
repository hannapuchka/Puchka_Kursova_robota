using Microsoft.AspNetCore.Identity;

namespace WebApp.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public Guid InternalUserId { get; set; }
        public string? DisplayName { get; set; }
    }
}
