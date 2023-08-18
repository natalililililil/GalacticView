using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace GalacticViewWebAPI.Presentation.Controllers
{
    [Route("api/planets/{planetId}/satellites")]
    [ApiController]
    public class SatelliteController : ControllerBase
    {
        private readonly IServiceManager _service;

        public SatelliteController(IServiceManager service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetSatellitesForPlanet(Guid planetId)
        {
            var satellites = _service.SatelliteService.GetSatellite(planetId, trackChanges: false);
            return Ok(satellites);
        }

        [HttpGet("{id:guid}", Name = "GetSatelliteForPlanet")]
        public IActionResult GetSatelliteForPlanet(Guid planetId, Guid id)
        {
            var satellite = _service.SatelliteService.GetSatellite(planetId, id, trackChanges: false);
            return Ok(satellite);
        }

        [HttpPost]
        public IActionResult CreateSatelliteForPlanet(Guid planetId, [FromBody] SatelliteForCreationDto satellite)
        {
            if (satellite is null)
                return BadRequest("SatelliteForCreationDto is null");

            var satelliteToReturn = _service.SatelliteService.CreateSatelliteForPlanet(planetId, satellite, trackChanges: false);

            return CreatedAtRoute("GetSatelliteForPlanet", new { planetId, id = satelliteToReturn.Id}, satelliteToReturn);
        }

        [HttpDelete("{id:guid}")]
        public IActionResult DeleteSatelliteForPlanet(Guid planetId, Guid id)
        {
            _service.SatelliteService.DeleteSatelliteForPlanet(planetId, id, trackChanges: false);
            return NoContent();
        }
    }
}
