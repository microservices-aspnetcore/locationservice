using System;

namespace StatlerWaldorfCorp.LocationService {

    public class LocationRecord {

        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public float Altitude { get; set; }
        public long Timestamp { get; set; }

        public Guid MemberID { get; set; }
    }

}