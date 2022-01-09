using System;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ObserverPattern.Base.Organization;

namespace ObserverPatternWeb.Data
{
    public class ApplicationDbContext : IdentityDbContext<CusUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
       {
           base.OnModelCreating(modelBuilder);


            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
            {
                b.ToTable("ObsRoles");
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
            {
                b.ToTable("ObsRoleClaims");
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
            {
                b.ToTable("ObsUserClaims");
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
            {
                b.ToTable("ObsUserLogins");
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
            {
                b.ToTable("ObsUserRoles");
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
            {
                b.ToTable("ObsUserTokens");
            });

            modelBuilder.Entity<CusUser>(b =>
            {
                b.ToTable("ObsUsers");
            });
            modelBuilder.Entity<OUs>(b =>
            {
                b.ToTable("ObsOUs");
            });
            modelBuilder.Entity<OUMembers>(b =>
            {
                b.ToTable("ObsOUMembers");
            });

        }
    }
}
