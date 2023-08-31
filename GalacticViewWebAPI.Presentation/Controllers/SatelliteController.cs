﻿using Entities.LinkModels;
using GalacticViewWebAPI.ActionFilters;
using GalacticViewWebAPI.Presentation.ActionFilters;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;
using System.Text.Json;

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
        [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
        public async Task<IActionResult> GetSatellitesForPlanet(Guid planetId, [FromQuery] SatelliteParameters satelliteParameters)
        {
            var linkParams = new LinkParameters(satelliteParameters, HttpContext);
            var result = await _service.SatelliteService.GetSatellitesAsync(planetId, linkParams, trackChanges: false);

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(result.metaData));

            return result.linkResponse.HasLinks ? Ok(result.linkResponse.LinkedEntities) : Ok(result.linkResponse.ShapedEntities);
        }

        [HttpGet("{id:guid}", Name = "GetSatelliteForPlanet")]
        public async Task<IActionResult> GetSatelliteForPlanet(Guid planetId, Guid id)
        {
            var satellite = await _service.SatelliteService.GetSatelliteAsync(planetId, id, trackChanges: false);
            return Ok(satellite);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateSatelliteForPlanet(Guid planetId, [FromBody] SatelliteForCreationDto satellite)
        {
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
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateSatelliteForPlanet(Guid planetId, Guid id,
            [FromBody] SatelliteForUpdateDto satellite)
        {
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
