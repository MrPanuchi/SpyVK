using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SpyVK.Entities;

namespace SpyVK.Data
{
    public class ApplicationUserDbContext : IdentityDbContext<ApplicationUser,ApplicationRole,Guid>
    {
        public ApplicationUserDbContext(DbContextOptions<ApplicationUserDbContext> options)
            : base(options)
        {

        }

        public DbSet<ApplicationUser> Users { get; set; }
    }
}
