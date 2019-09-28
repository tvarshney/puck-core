﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace puck.core.Entities
{
    public partial class PuckContext : IdentityDbContext<PuckUser,PuckRole,string
        ,IdentityUserClaim<string>,PuckUserRole,IdentityUserLogin<string>
        ,IdentityRoleClaim<string>,IdentityUserToken<string>>
    {
        private string connectionString;
        public PuckContext(DbContextOptions options):base(options)
        {
            
        }
        public PuckContext(string connectionString)
        {
            this.connectionString = connectionString;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            if (!optionsBuilder.IsConfigured && !string.IsNullOrEmpty(connectionString)) {
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<PuckUser>(b =>
            {
                // Each User can have many UserClaims
                b.HasMany(e => e.Claims)
                    .WithOne()
                    .HasForeignKey(uc => uc.UserId)
                    .IsRequired();

                // Each User can have many UserLogins
                b.HasMany(e => e.Logins)
                    .WithOne()
                    .HasForeignKey(ul => ul.UserId)
                    .IsRequired();

                //// Each User can have many UserTokens
                //b.HasMany(e => e.Tokens)
                //    .WithOne()
                //    .HasForeignKey(ut => ut.UserId)
                //    .IsRequired();

                // Each User can have many entries in the UserRole join table
                b.HasMany(e => e.Roles)
                    .WithOne(e => e.User)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });

            builder.Entity<PuckRole>(b =>
            {
                // Each Role can have many entries in the UserRole join table
                b.HasMany(e => e.UserRoles)
                    .WithOne(e => e.Role)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();
            });
        }
        public DbSet<PuckMeta> PuckMeta { get; set; }
        public DbSet<PuckRevision> PuckRevision { get; set; }
        public DbSet<PuckInstruction> PuckInstruction { get; set; }
        public DbSet<PuckAudit> PuckAudit { get; set; }
        public DbSet<PuckTag> PuckTag { get; set; }
        //public DbSet<GeneratedModel> GeneratedModel { get; set; }
        //public DbSet<GeneratedProperty> GeneratedProperty { get; set; }
        //public DbSet<GeneratedAttribute> GeneratedAttribute { get; set; }

    }
}
