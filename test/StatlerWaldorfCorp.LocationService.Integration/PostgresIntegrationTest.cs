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
                .AddCloudFoundry()
                .Build();
        }

        [Fact]
        public void Postgres()
        {
            var optionsBuilder = new DbContextOptionsBuilder<LocationDbContext>();
            optionsBuilder.UseNpgsql(config);
            LocationDbContext context = new LocationDbContext(optionsBuilder.Options);

            LocationRecordRepository repository = new LocationRecordRepository(context);
            repository.Add(new LocationRecord(){ ID = Guid.NewGuid(), Timestamp = 1,
                MemberID = Guid.NewGuid(), Latitude = 12.3f });
        }
    }
}
