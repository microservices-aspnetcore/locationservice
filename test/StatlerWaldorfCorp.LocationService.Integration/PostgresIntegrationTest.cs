using Xunit;

using StatlerWaldorfCorp.LocationService.Models;
using StatlerWaldorfCorp.LocationService.Persistence;
using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;


namespace StatlerWaldorfCorp.LocationService.Integration
{

    public class PostgresIntegrationTest
    {
        private IConfigurationRoot config;
        private LocationDbContext context;

        public PostgresIntegrationTest()
        {
			config = new ConfigurationBuilder()
                .SetBasePath(System.IO.Directory.GetCurrentDirectory())                
                .AddEnvironmentVariables()                
                .Build();

            var connectionString = config.GetSection("postgres:cstr").Value;
            var optionsBuilder = new DbContextOptionsBuilder<LocationDbContext>();
            optionsBuilder.UseNpgsql(connectionString);
            this.context = new LocationDbContext(optionsBuilder.Options);                
        }

        [Fact]
        public void ShouldPersistRecord()
        {
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
        }

        [Fact]
        public void ShouldUpdateRecord()
        {
            LocationRecordRepository repository = new LocationRecordRepository(context);

            LocationRecord firstRecord = new LocationRecord(){ ID = Guid.NewGuid(), Timestamp = 1,
                MemberID = Guid.NewGuid(), Latitude = 12.3f }; 
            repository.Add(firstRecord);

            LocationRecord targetRecord = repository.Get(firstRecord.MemberID, firstRecord.ID);

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

        [Fact]
        public void ShouldDeleteRecord()
        {
            LocationRecordRepository repository = new LocationRecordRepository(context);
            Guid memberId = Guid.NewGuid(); 

            LocationRecord firstRecord = new LocationRecord(){ ID = Guid.NewGuid(), Timestamp = 1,
                MemberID = memberId, Latitude = 12.3f }; 
            repository.Add(firstRecord);
            LocationRecord secondRecord = new LocationRecord(){ ID = Guid.NewGuid(), Timestamp = 2,
                MemberID = memberId, Latitude = 24.4f };
            repository.Add(secondRecord);
            
            int initialCount = repository.AllForMember(memberId).Count();
            repository.Delete(memberId, secondRecord.ID);
            int afterCount = repository.AllForMember(memberId).Count();
        
            LocationRecord target1 = repository.Get(firstRecord.MemberID, firstRecord.ID);
            LocationRecord target2 = repository.Get(firstRecord.MemberID, secondRecord.ID);
            
            Assert.Equal(initialCount -1, afterCount);
            Assert.Equal(target1.ID, firstRecord.ID);            
            Assert.NotNull(target1);
            Assert.Null(target2);
        }

        [Fact]
        public void ShouldGetAllForMember()
        {
            LocationRecordRepository repository = new LocationRecordRepository(context);
            Guid memberId = Guid.NewGuid(); 

            int initialCount = repository.AllForMember(memberId).Count();

            LocationRecord firstRecord = new LocationRecord(){ ID = Guid.NewGuid(), Timestamp = 1,
                MemberID = memberId, Latitude = 12.3f }; 
            repository.Add(firstRecord);
            LocationRecord secondRecord = new LocationRecord(){ ID = Guid.NewGuid(), Timestamp = 2,
                MemberID = memberId, Latitude = 24.4f };
            repository.Add(secondRecord);

            ICollection<LocationRecord> records = repository.AllForMember(memberId);
            int afterCount = records.Count();        

            Assert.Equal(initialCount + 2, afterCount);
            Assert.NotNull(records.FirstOrDefault(r => r.ID == firstRecord.ID));
            Assert.NotNull(records.FirstOrDefault(r => r.ID == secondRecord.ID));            
        }

        [Fact]
        public void ShouldGetLatestForMember()
        {
            LocationRecordRepository repository = new LocationRecordRepository(context);
            Guid memberId = Guid.NewGuid();

            LocationRecord firstRecord = new LocationRecord(){ ID = Guid.NewGuid(), Timestamp = 1,
                MemberID = memberId, Latitude = 12.3f }; 
            repository.Add(firstRecord);
            LocationRecord secondRecord = new LocationRecord(){ ID = Guid.NewGuid(), Timestamp = 2,
                MemberID = memberId, Latitude = 24.4f };
            repository.Add(secondRecord);

            LocationRecord latest = repository.GetLatestForMember(memberId);

            Assert.NotNull(latest);
            Assert.Equal(latest.ID, secondRecord.ID);
            Assert.NotEqual(latest.ID, firstRecord.ID);
        }
    }
}