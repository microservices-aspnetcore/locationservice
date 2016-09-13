using Xunit;
using System.Collections.Generic;
using StatlerWaldorfCorp.LocationService.Models;
using StatlerWaldorfCorp.LocationService.Controllers;
using StatlerWaldorfCorp.LocationService.Persistence;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace StatlerWaldorfCorp.LocationService 
{

    public class LocationRecordControllerTest
    {
        [Fact]
        public void ShouldAdd() 
        {
            ILocationRecordRepository repository = new InMemoryLocationRecordRepository();
            LocationRecordController controller = new LocationRecordController(repository);
            Guid memberGuid = Guid.NewGuid();

            controller.AddLocation(memberGuid, new LocationRecord(){ MemberID = memberGuid });

            Assert.Equal(1, repository.AllForMember(memberGuid).Count());
        }

        [Fact]
        public void ShouldTrackAllLocationsForMember()
        {
            ILocationRecordRepository repository = new InMemoryLocationRecordRepository();
            LocationRecordController controller = new LocationRecordController(repository);
            Guid memberGuid = Guid.NewGuid();

            controller.AddLocation(memberGuid, new LocationRecord(){ ID = Guid.NewGuid(), Timestamp = 1,
                MemberID = memberGuid, Latitude = 12.3f });
            controller.AddLocation(memberGuid, new LocationRecord(){ ID = Guid.NewGuid(), Timestamp = 2,
                 MemberID = memberGuid, Latitude = 23.4f });
            controller.AddLocation(Guid.NewGuid(), new LocationRecord(){ ID = Guid.NewGuid(), Timestamp = 3,
                 MemberID = Guid.NewGuid(), Latitude = 23.4f });                 

            ICollection<LocationRecord> locationRecords = 
                ((controller.GetLocationsForMember(memberGuid) as ObjectResult).Value as ICollection<LocationRecord>);

            Assert.Equal(2, locationRecords.Count());
        }

        [Fact]
        public void ShouldTrackLatestLocationsForMember()
        {
            ILocationRecordRepository repository = new InMemoryLocationRecordRepository();
            LocationRecordController controller = new LocationRecordController(repository);
            Guid memberGuid = Guid.NewGuid();

            Guid latestId = Guid.NewGuid();
            controller.AddLocation(memberGuid, new LocationRecord(){ ID = Guid.NewGuid(), Timestamp = 1,
                MemberID = memberGuid, Latitude = 12.3f });
            controller.AddLocation(memberGuid, new LocationRecord(){ ID = latestId, Timestamp = 3,
                 MemberID = memberGuid, Latitude = 23.4f });                 
            controller.AddLocation(memberGuid, new LocationRecord(){ ID = Guid.NewGuid(), Timestamp = 2,
                 MemberID = memberGuid, Latitude = 23.4f });
            controller.AddLocation(Guid.NewGuid(), new LocationRecord(){ ID = Guid.NewGuid(), Timestamp = 4,
                 MemberID = Guid.NewGuid(), Latitude = 23.4f });                 

            LocationRecord latest = ((controller.GetLatestForMember(memberGuid) as ObjectResult).Value as LocationRecord);

            Assert.Equal(latestId, latest.ID);
        }        
    }
}
