﻿using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace XtraSpurt.MultiDbSupport.Domains
{
    public class XtraSpurtDbContext : IdentityDbContext<XtraSpurtUser, XtraSpurtRole, Guid>
    {
        public XtraSpurtDbContext(DbContextOptions<XtraSpurtDbContext> options)
            : base(options)
        {
        }
        public DbSet<Post> Posts { get; set; }

        public DbSet<Blog> Blogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

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
