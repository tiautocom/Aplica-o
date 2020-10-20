using APP.DOMAIN;
using APP.DOMAIN.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace APP.REPOSITORY
{
    public class DataContext : IdentityDbContext<User, Role, string, 
                                                IdentityUserClaim<string>, UserRole, 
                                                IdentityUserLogin<string>, IdentityRoleClaim<string>, 
                                                IdentityUserToken<string>>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        { }

        public DbSet<Address> Address { get; set; }

        public DbSet<Company> Company { get; set; }

        // public DbSet<Role> Role { get; set; }

        // public DbSet<User> User { get; set; }

        // public DbSet<UserRole> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder mb)
        {
                base.OnModelCreating(mb);
                mb.Entity<UserRole>(userRole => {

                    userRole.HasKey(ur => new {ur.UserId, ur.RoleId});

                    userRole.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                     userRole.HasOne(ur => ur.User)
                    .WithMany(u => u.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
                });

// Essa parte n
            // mb.Entity<UserRole>()
            // .HasKey(UR => new { UR.UserId, UR.RoleId });
        }
    }
}