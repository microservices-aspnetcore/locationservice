using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using StatlerWaldorfCorp.LocationService.Models; 
using Npgsql.EntityFrameworkCore.PostgreSQL;
using Steeltoe.CloudFoundry.Connector.PostgreSql.EFCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Steeltoe.Extensions.Configuration;


namespace StatlerWaldorfCorp.LocationService.Persistence 
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasPostgresExtension("uuid-ossp");
        }

        public DbSet<LocationRecord> LocationRecords {get; set;}        
    }      
}