using Microsoft.AspNetCore.Identity;

namespace SpyVK.Entities
{
    public class ApplicationUserIdentity : IdentityUser<Guid>
    {
        public string tokenVK { get; set; }
    }
}
