﻿using Microsoft.EntityFrameworkCore;

namespace IdentityServerHost.Data
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; } //DbSet of our user entity
        public DbSet<UserExternalProvider> UserExternalProviders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //adding indexes, for userName and SubjectId
            modelBuilder.Entity<User>().HasIndex(i => i.UserName).IsUnique();
            modelBuilder.Entity<User>().HasIndex(i => i.SubjectId).IsUnique();

            modelBuilder.Entity<UserExternalProvider>().HasIndex(i => i.ProviderName);
            modelBuilder.Entity<UserExternalProvider>().HasIndex(i => i.ProviderSubjectId);
            modelBuilder.Entity<UserExternalProvider>().HasIndex(i => new { i.ProviderName, i.ProviderSubjectId });
        }
    }
}
