using Xunit;
using System.Collections.Generic;
using StatlerWaldorfCorp.LocationService.Models;
using StatlerWaldorfCorp.LocationService.Controllers;
using StatlerWaldorfCorp.LocationService.Persistence;
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
    }
}
