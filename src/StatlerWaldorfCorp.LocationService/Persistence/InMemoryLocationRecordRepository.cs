using System;
using System.Collections.Generic;
using StatlerWaldorfCorp.LocationService.Models;
using System.Linq;

namespace StatlerWaldorfCorp.LocationService.Persistence {

    public class InMemoryLocationRecordRepository : ILocationRecordRepository
    {
        private static Dictionary<Guid, SortedList<long, LocationRecord>> locationRecords;

        public InMemoryLocationRecordRepository() {
            if (locationRecords == null) {
                locationRecords = new Dictionary<Guid, SortedList<long, LocationRecord>>();
            }
        }
        
        public LocationRecord Add(LocationRecord locationRecord)
        {
            var memberRecords = getMemberRecords(locationRecord.MemberID);

            memberRecords.Add(locationRecord.Timestamp, locationRecord);
            return locationRecord;
        }

        public ICollection<LocationRecord> AllForMember(Guid memberId)
        {
            var memberRecords = getMemberRecords(memberId);
            return memberRecords.Values.Where( l => l.MemberID == memberId).ToList();
        }

        public LocationRecord Delete(Guid memberId, Guid recordId)
        {
            var memberRecords = getMemberRecords(memberId);
            LocationRecord lr = memberRecords.Values.Where( l => l.ID == recordId).FirstOrDefault();
            
            if (lr != null) {
                memberRecords.Remove(lr.Timestamp);
            }

            return lr;
        }

        public LocationRecord Get(Guid memberId, Guid recordId)
        {
            var memberRecords = getMemberRecords(memberId);

            LocationRecord lr = memberRecords.Values.Where( l => l.ID == recordId).FirstOrDefault();
            return lr;
        }

        public LocationRecord Update(LocationRecord locationRecord)
        {
            return Delete(locationRecord.MemberID, locationRecord.ID);
        }

        public LocationRecord GetLatestForMember(Guid memberId) {
            var memberRecords = getMemberRecords(memberId);

            LocationRecord lr = memberRecords.Values.LastOrDefault();
            return lr;
        }

        private SortedList<long, LocationRecord> getMemberRecords(Guid memberId) {
            if (!locationRecords.ContainsKey(memberId)) {
                locationRecords.Add(memberId, new SortedList<long, LocationRecord>());
            }

            var list = locationRecords[memberId];
            return list;
        }
    }
}