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
            string connStr = config.GetValue<string>("vcap:services:postgres:0:credentials:uri");
            System.Console.WriteLine("----------");
            System.Console.WriteLine(connStr);

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseNpgsql(connStr);
            ApplicationDbContext context = new ApplicationDbContext(optionsBuilder.Options);            
        }
    }
}
