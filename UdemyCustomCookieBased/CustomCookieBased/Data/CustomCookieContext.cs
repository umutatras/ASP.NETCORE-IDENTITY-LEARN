using CustomCookieBased.Configuration;
using Microsoft.EntityFrameworkCore;

namespace CustomCookieBased.Data
{
    public class CustomCookieContext:DbContext
    {
        public CustomCookieContext(DbContextOptions<CustomCookieContext> options):base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AppUserConfiguration());
            modelBuilder.ApplyConfiguration(new AppRoleConfiguration());
            modelBuilder.ApplyConfiguration(new AppUserRoleConfiguration());
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<AppUser> Users { get; set; }
        public DbSet<AppRole> Roles { get; set; }
        public DbSet<AppUserRole> UserRoles { get; set; }
    }
}
