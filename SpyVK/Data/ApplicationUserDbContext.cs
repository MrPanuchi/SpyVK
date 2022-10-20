using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SpyVK.Entities;

namespace SpyVK.Data
{
    public class ApplicationUserDbContext : IdentityDbContext<ApplicationUserIdentity,ApplicationRole,Guid>
    {
        public ApplicationUserDbContext(DbContextOptions<ApplicationUserDbContext> options)
            : base(options)
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        public DbSet<ApplicationUserIdentity> Users { get; set; }
    }
}
