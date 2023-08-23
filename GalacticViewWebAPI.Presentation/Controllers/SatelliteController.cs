using Microsoft.AspNetCore.JsonPatch;
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
        public async Task<IActionResult> GetSatellitesForPlanet(Guid planetId)
        {
            var satellites = await _service.SatelliteService.GetSatelliteAsync(planetId, trackChanges: false);
            return Ok(satellites);
        }

        [HttpGet("{id:guid}", Name = "GetSatelliteForPlanet")]
        public async Task<IActionResult> GetSatelliteForPlanet(Guid planetId, Guid id)
        {
            var satellite = await _service.SatelliteService.GetSatelliteAsync(planetId, id, trackChanges: false);
            return Ok(satellite);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSatelliteForPlanet(Guid planetId, [FromBody] SatelliteForCreationDto satellite)
        {
            if (satellite is null)
                return BadRequest("SatelliteForCreationDto is null");

            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            var satelliteToReturn = await _service.SatelliteService.CreateSatelliteForPlanetAsync(planetId, satellite, trackChanges: false);

            return CreatedAtRoute("GetSatelliteForPlanet", new { planetId, id = satelliteToReturn.Id}, satelliteToReturn);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteSatelliteForPlanet(Guid planetId, Guid id)
        {
            await _service.SatelliteService.DeleteSatelliteForPlanetAsync(planetId, id, trackChanges: false);
            return NoContent();
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateSatelliteForPlanet(Guid planetId, Guid id,
            [FromBody] SatelliteForUpdateDto satellite)
        {
            if (satellite is null)
                return BadRequest("SatelliteForUpdateDto object is null");

            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            await _service.SatelliteService.UpdateSatelliteForPlanetAsync(planetId, id, satellite,
                planetTrackChanges: false, satTrackChanges: true);

            return NoContent();
        }

        [HttpPatch("{id:guid}")]
        public async Task<IActionResult> PartiallyUpdateSatelliteForPlanet(Guid planetId, Guid id,
            [FromBody] JsonPatchDocument<SatelliteForUpdateDto> patchDoc)
        {
            if (patchDoc is null)
                return BadRequest("patchDoc object sent from client is null.");

            var result = await _service.SatelliteService.GetSatelliteForPatchAsync(planetId, id, planetTrackChanges: false, 
                satTrackChanges: true);

            patchDoc.ApplyTo(result.satelliteToPatch, ModelState);

            TryValidateModel(result.satelliteToPatch);
            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            await _service.SatelliteService.SaveChangesForPatchAsync(result.satelliteToPatch, result.satelliteEntity);

            return NoContent();
        }
    }
}
