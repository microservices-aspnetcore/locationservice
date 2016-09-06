using System;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using StatlerWaldorfCorp.LocationService.Models;

namespace StatlerWaldorfCorp.LocationService.Controllers {
    
    [Route("locations/{memberId}")]
    public class LocationRecordController : Controller {

        private ILocationRecordRepository locationRepository;

        public LocationRecordController(ILocationRecordRepository repository) {
            this.locationRepository = repository;
        }

        [HttpPost]
        public IActionResult AddLocation(Guid memberId, [FromBody]LocationRecord locationRecord) {
             return this.Created($"/locations/{memberId}/{locationRecord.ID}", locationRecord);
        }

        [HttpGet]
        public IActionResult GetLocationsForMember(Guid memberId) {
            return this.Ok(locationRepository.AllForMember(memberId));
        }
    }
}