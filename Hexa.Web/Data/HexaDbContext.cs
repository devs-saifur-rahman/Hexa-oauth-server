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
            dbConnectionString = $"Server=DESKTOP-QTUAET0\\MSSQLSERVER2019;Database=Hexa;User Id=sa;Password=rahman;Trusted_Connection=True;";
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlServer(dbConnectionString);


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
        public DbSet<GrantType> GrantTypes { get; set; }
        public DbSet<RedirectURI> RedirectUris { get; set; }
        public DbSet<AccessToken> AccessTokens { get; set; }
        public DbSet<Application> Applications { get; set; }
        public DbSet<AuthCode> AuthCodes { get; set; }
        public DbSet<ClientSecret> ClientSecrets { get; set; }
        public DbSet<Scope> Scopes { get; set; }
        public DbSet<User> Users { get; set; }

    }
}
