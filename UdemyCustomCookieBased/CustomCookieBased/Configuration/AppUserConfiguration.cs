using CustomCookieBased.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCookieBased.Configuration
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>

    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.HasData(new AppUser
            {
                Id = 1,
                UserName = "umut",
                Password = "1"
            });
            builder.Property(x=>x.Password).HasMaxLength(128); 
            builder.Property(x=>x.UserName).HasMaxLength(128);  
        }
    }
    public class AppRoleConfiguration : IEntityTypeConfiguration<AppRole>

    {
        public void Configure(EntityTypeBuilder<AppRole> builder)
        {
            builder.HasData(new AppRole
            {
                Id = 1,
                Definition="Admin"
            });
            builder.Property(x => x.Definition).HasMaxLength(250);
            
        }
    }
    public class AppUserRoleConfiguration : IEntityTypeConfiguration<AppUserRole>

    {
        public void Configure(EntityTypeBuilder<AppUserRole> builder)
        {
            builder.HasData(new AppUserRole
            {
               RoleId = 1,
               UserId = 1
            });
            builder.HasKey(x => new { x.UserId, x.RoleId });
            builder.HasOne(x => x.AppRole).WithMany(x => x.AppUserRoles).HasForeignKey(x => x.RoleId);
            builder.HasOne(x => x.AppUser).WithMany(x => x.AppUserRoles).HasForeignKey(x => x.UserId);

        }
    }

}
