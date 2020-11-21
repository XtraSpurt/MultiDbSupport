using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace XtraSpurt.MultiDbSupport.Domains
{
    public class XtraSpurtDbContext : IdentityDbContext<XtraSpurtUser, XtraSpurtRole, Guid>
    {

        public DbSet<Post> Posts { get; set; }

        public DbSet<Blog> Blogs { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("spurt");
            modelBuilder.Entity<XtraSpurtUser>().ToTable("xtrausers");
            modelBuilder.Entity<XtraSpurtRole>().ToTable("xtraroles");
            modelBuilder.Entity<IdentityUserClaim<Guid>>().ToTable("xtrauserclaims");
            modelBuilder.Entity<IdentityUserRole<Guid>>().ToTable("xtrauserroles");
            modelBuilder.Entity<IdentityRoleClaim<Guid>>().ToTable("xtraroleclaims");
            modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable("xtrauserclaims");
            modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable("xtrausertokens");
            modelBuilder.Entity<IdentityUserLogin<Guid>>().ToTable("xtrauserlogins");


            modelBuilder.Entity<Post>().ToTable("xtraposts");
            modelBuilder.Entity<Blog>().ToTable("xtrablogs");


        }
    }
}
