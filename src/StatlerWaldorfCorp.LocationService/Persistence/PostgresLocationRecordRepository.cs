// using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using StatlerWaldorfCorp.LocationService.Models;

namespace StatlerWaldorfCorp.LocationService.Persistence
{
    public class PostgresLocationRecordRepository : ILocationRecordRepository
    {
        // ApplicationDbContext context;

        public LocationRecord Add(LocationRecord locationRecord)
        {
            return locationRecord;
        }   

        public LocationRecord Update(LocationRecord locationRecord)
        {
            return locationRecord;            
        }

        public LocationRecord Get(Guid memberId, Guid recordId)
        {
            return new LocationRecord();
        }

        public LocationRecord Delete(Guid memberId, Guid recordId)
        {            
            return new LocationRecord();
        }
       
        public LocationRecord GetLatestForMember(Guid memberId)
        {
            return new LocationRecord();
        }
        
        public ICollection<LocationRecord> AllForMember(Guid memberId)
        {
            return new List<LocationRecord>();            
        }
    }
}