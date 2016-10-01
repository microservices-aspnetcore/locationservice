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
    
    public class ApplicationDbContextFactory : IDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext Create(DbContextFactoryOptions options)
        {
            var configBuilder = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .AddCloudFoundry();

            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseNpgsql(configBuilder.Build());
            return new ApplicationDbContext(builder.Options);
        }
    }    
}