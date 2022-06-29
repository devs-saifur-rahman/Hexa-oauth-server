using System;
using System.Collections.Generic;
using Hexa.Data.Models.oauth;
using Hexa.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Hexa.Data.DB
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GrantType>();
            modelBuilder.Entity<Scope>();

            modelBuilder.Entity<User>();
            modelBuilder.Entity<Application>();
            modelBuilder.Entity<ClientSecret>();
            modelBuilder.Entity<RedirectURI>();

            modelBuilder.Entity<AuthCode>();
            modelBuilder.Entity<AccessToken>();

        }


        //public DbSet<GrantType> GrantTypes { get; set; }
        //public DbSet<RedirectURI> RedirectUris { get; set; }
        //public DbSet<AccessToken> AccessTokens { get; set; }
        //public DbSet<Application> Applications { get; set; }
        //public DbSet<AuthCode> AuthCodes { get; set; }
        //public DbSet<ClientSecret> ClientSecrets { get; set; }
        //public DbSet<Scope> Scopes { get; set; }
        //public DbSet<User> Users { get; set; }


        public DbSet<Application> Applications => Set<Application>();
        public DbSet<ClientSecret> ClientSecrets => Set<ClientSecret>();
        public DbSet<RedirectURI> RedirectURIs => Set<RedirectURI>();

        public DbSet<User> Users => Set<User>();
        public DbSet<AuthCode> AuthCodes => Set<AuthCode>();
        public DbSet<AccessToken> AccessTokens => Set<AccessToken>();


        public DbSet<GrantType> GrantTypes => Set<GrantType>();
        public DbSet<Scope> Scopes => Set<Scope>();

    }
}
