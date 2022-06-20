using System;
using System.Collections.Generic;
using Hexa.Web.Models;
using Hexa.Web.Models.oatuh;
using Microsoft.EntityFrameworkCore;

namespace Hexa.Web.DB
{
    public class HexaDbContext : DbContext
    {


        public string dbConnectionString { get; }
        public HexaDbContext(DbContextOptions<HexaDbContext> options) : base(options)
        {
            dbConnectionString = $"Server=SAIFURLAP\\SRSQLSEVER2019;Database=Hexa;User Id=sa;Password=Enosis123;Trusted_Connection=True;";
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlServer(dbConnectionString);


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GrantType>().ToTable("GrantType");
            modelBuilder.Entity<RedirectURI>().ToTable("RedirectUri");
            modelBuilder.Entity<ClientApp>().ToTable("ClientApp");
            modelBuilder.Entity<AccessToken>().ToTable("AccessToken");
            modelBuilder.Entity<Application>().ToTable("Application");
            modelBuilder.Entity<AuthCode>().ToTable("AuthCode");
            modelBuilder.Entity<ClientSecret>().ToTable("ClientSecret");
            modelBuilder.Entity<Scope>().ToTable("Scope");
            modelBuilder.Entity<User>().ToTable("User");

        }
        public DbSet<GrantType> GrantTypes { get; set; }
        public DbSet<RedirectURI> RedirectUris { get; set; }
        public DbSet<ClientApp> ClientApps { get; set; }
        public DbSet<AccessToken> AccessTokens { get; set; }
        public DbSet<Application> Applications { get; set; }
        public DbSet<AuthCode> AuthCodes { get; set; }
        public DbSet<ClientSecret> ClientSecrets { get; set; }
        public DbSet<Scope> Scopes { get; set; }
        public DbSet<User> Users { get; set; }

    }
}
