using Xunit;

using StatlerWaldorfCorp.LocationService.Models;
using StatlerWaldorfCorp.LocationService.Persistence;

using System;
using System.Linq;
using Steeltoe.Extensions.Configuration;
using Steeltoe.CloudFoundry.Connector.PostgreSql.EFCore;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;


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

            LocationRecord firstRecord = new LocationRecord(){ ID = Guid.NewGuid(), Timestamp = 1,
                MemberID = Guid.NewGuid(), Latitude = 12.3f }; 
            repository.Add(firstRecord);

            LocationRecord targetRecord = repository.Get(firstRecord.MemberID, firstRecord.ID);

            // assert values equal first and targetRecord
            Assert.Equal(firstRecord.Timestamp, targetRecord.Timestamp);
            Assert.Equal(firstRecord.MemberID, targetRecord.MemberID);
            Assert.Equal(firstRecord.ID, targetRecord.ID);
            Assert.Equal(firstRecord.Latitude, targetRecord.Latitude);

            // modify firstRecord.
            firstRecord.Longitude = 12.5f;
            firstRecord.Latitude = 47.09f;
            repository.Update(firstRecord);

            LocationRecord target2 = repository.Get(firstRecord.MemberID, firstRecord.ID);

            Assert.Equal(firstRecord.Timestamp, target2.Timestamp);
            Assert.Equal(firstRecord.Longitude, target2.Longitude);
            Assert.Equal(firstRecord.Latitude, target2.Latitude);
            Assert.Equal(firstRecord.ID, target2.ID);
            Assert.Equal(firstRecord.MemberID, target2.MemberID);

        }
    }
}
