using System;
using System.Collections.Generic;
using Hexa.Web.Models;
using Hexa.Web.Models.oatuh;
using Microsoft.EntityFrameworkCore;

namespace Hexa.Web.DB
{
    public class HexaDbContext : DbContext
    {

        public DbSet<GrantType> GrantTypes { get; set; }


        public string dbConnectionString { get; }
        public HexaDbContext(DbContextOptions<HexaDbContext> options):base (options)
        {
            dbConnectionString = $"Server=SAIFURLAP\\SRSQLSEVER2019;Database=Hexa;User Id=sa;Password=Enosis123;Trusted_Connection=True;";
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlServer(dbConnectionString);


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GrantType>().ToTable("GrantType");

        }


    }
}
