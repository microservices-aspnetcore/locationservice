using System;
using System.Collections.Generic;

namespace StatlerWaldorfCorp.LocationService.Models {

    public interface ILocationRecordRepository {
        LocationRecord Add(LocationRecord locationRecord);    
        LocationRecord Update(LocationRecord locationRecord);
        LocationRecord Get(Guid memberId, Guid recordId);
        LocationRecord Delete(Guid memberId, Guid recordId);
       
        ICollection<LocationRecord> AllForMember(Guid memberId);
    }
}