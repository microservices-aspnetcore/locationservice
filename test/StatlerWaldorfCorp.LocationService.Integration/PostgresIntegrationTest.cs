using Xunit;
using System.Collections.Generic;
using StatlerWaldorfCorp.LocationService.Models;
using StatlerWaldorfCorp.LocationService.Controllers;
using StatlerWaldorfCorp.LocationService.Persistence;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using Steeltoe.Extensions.Configuration;
using Steeltoe.CloudFoundry.Connector.PostgreSql.EFCore;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace StatlerWaldorfCorp.LocationService.Integration 
{

    public class PostgresIntegrationTest
    {
        private IConfigurationRoot config;

        public PostgresIntegrationTest() 
        {
			config = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .Build();
        }

        [Fact]
        public void Postgres() 
        {
            string connStr = config.GetValue<string>("ConnectionStrings:LocationDB");
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseNpgsql(connStr);
            ApplicationDbContext context = new ApplicationDbContext(optionsBuilder.Options);            
            
            PostgresLocationRecordRepository repository = new PostgresLocationRecordRepository(context);
            repository.Add(new LocationRecord(){ ID = Guid.NewGuid(), Timestamp = 1,
                MemberID = Guid.NewGuid(), Latitude = 12.3f });
        }
    }
}
