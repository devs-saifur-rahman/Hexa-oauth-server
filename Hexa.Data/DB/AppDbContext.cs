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
            modelBuilder.Entity<User>();

            modelBuilder.Entity<Application>();
            modelBuilder.Entity<ClientSecret>();
            modelBuilder.Entity<RedirectURI>();


            modelBuilder.Entity<User>();
            modelBuilder.Entity<AuthCode>();
            modelBuilder.Entity<AccessToken>();


            modelBuilder.Entity<GrantType>();
            modelBuilder.Entity<Scope>();


            modelBuilder.Entity<ApplicationScope>().HasKey(appscp => new { appscp.ApplicationId, appscp.ScopeId });
            modelBuilder.Entity<ApplicationScope>().HasOne(appscp => appscp.Application).WithMany(app => app.ApplicationScopes).HasForeignKey(appscp => appscp.ApplicationId);
            modelBuilder.Entity<ApplicationScope>().HasOne(appscp => appscp.Scope).WithMany(scp => scp.ApplicationScopes).HasForeignKey(appscp => appscp.ScopeId);
         
            modelBuilder.Entity<AuthorizationRequest>();

            //modelBuilder.Entity<AuthorizationRequest>().HasKey(a => a.AuthorizationRequestId);
            //modelBuilder.Entity<AuthorizationRequest>().HasOne(a=>a.Application).WithMany(b=>b.)


        }



        public DbSet<Application> Applications => Set<Application>();
        public DbSet<ClientSecret> ClientSecrets => Set<ClientSecret>();
        public DbSet<RedirectURI> RedirectURIs => Set<RedirectURI>();

        public DbSet<User> Users => Set<User>();
        public DbSet<AuthCode> AuthCodes => Set<AuthCode>();
        public DbSet<AccessToken> AccessTokens => Set<AccessToken>();


        public DbSet<GrantType> GrantTypes => Set<GrantType>();
        public DbSet<Scope> Scopes => Set<Scope>();


        public DbSet<ApplicationScope> ApplicationScopes => Set<ApplicationScope>();
        public DbSet<AuthorizationRequest> AuthorizationRequests => Set<AuthorizationRequest>();

    }
}
