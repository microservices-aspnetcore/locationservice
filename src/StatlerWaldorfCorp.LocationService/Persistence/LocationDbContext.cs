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
    public class LocationDbContext : DbContext
    {
        public LocationDbContext(DbContextOptions<LocationDbContext> options) :base(options)
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
