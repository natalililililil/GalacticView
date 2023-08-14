using Microsoft.AspNetCore.Mvc;
using Service.Contracts;

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

        [HttpGet("{id:guid}")]
        public IActionResult GetSatelliteForPlanet(Guid planetId, Guid id)
        {
            var satellite = _service.SatelliteService.GetSatellite(planetId, id, trackChanges: false);
            return Ok(satellite);
        }
    }
}
