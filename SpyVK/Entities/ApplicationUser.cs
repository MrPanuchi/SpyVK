using Microsoft.AspNetCore.Identity;

namespace SpyVK.Entities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string tokenVK { get; set; }
    }
}
