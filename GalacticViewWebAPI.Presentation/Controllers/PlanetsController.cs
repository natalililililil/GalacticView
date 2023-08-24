﻿using GalacticViewWebAPI.ActionFilters;
using GalacticViewWebAPI.Presentation.ModelBinders;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace GalacticViewWebAPI.Presentation.Controllers
{
    [Route("api/planets")]
    [ApiController]
    public class PlanetsController : ControllerBase
    {
        private readonly IServiceManager _service;
       
        public PlanetsController(IServiceManager service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetPlanets()
        {
            var planets = await _service.PlanetService.GetAllPlanetsAsync(trachChanges: false);
            return Ok(planets);
        }

        [HttpGet("{id:guid}", Name = "PlanetById")]
        public async Task<IActionResult> GetPlanet(Guid id)
        {
            var planet = await _service.PlanetService.GetPlanetAsync(id, trackChanges: false);
            return Ok(planet);
        }

        [HttpGet("collection/({ids})", Name = "PlanetCollection")]
        public async Task<IActionResult> GetPlanetCollection([ModelBinder(BinderType = typeof(ArrayModelBinder))]IEnumerable<Guid> ids)
        {
            var planets = await _service.PlanetService.GetByIdsAsync(ids, trackChanges: false);

            return Ok(planets);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreatePlanet([FromBody] PlanetForCreationDto planet)
        {
            var createdPlanet = await _service.PlanetService.CreatePlanetAsync(planet);

            return CreatedAtRoute("PlanetById", new { id = createdPlanet.Id }, createdPlanet);
        }

        [HttpPost("collection")]
        public async Task<IActionResult> CreatePlanetCollection([FromBody] IEnumerable<PlanetForCreationDto> planetColletion)
        {
            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            var result = await _service.PlanetService.CreatePlanetCollectonAsync(planetColletion);

            return CreatedAtRoute("PlanetCollection", new {result.ids}, result.planets);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeletePlanet(Guid id)
        {
            await _service.PlanetService.DeletePlanetAsync(id, trackChanges: false);
            return NoContent();
        }

        [HttpPut("{id:guid}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdatePlanet(Guid id, [FromBody] PlanetForUpdateDto planet)
        {
            await _service.PlanetService.UpdatePlanetAsync(id, planet, trackChanges: true);

            return NoContent();
        }

        [HttpPatch("{id:guid}")]
        public async Task<IActionResult> PartiallyUpdatePlanet(Guid id, [FromBody] JsonPatchDocument<PlanetForUpdateDto> patchDoc)
        {
            if (patchDoc is null)
                return BadRequest("patchDoc object sent from client is null.");

            var result = await _service.PlanetService.GetPlanetForPatchAsync(id, planetTrackChanges: true);

            patchDoc.ApplyTo(result.planetToPatch, ModelState);

            TryValidateModel(result.planetToPatch);
            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState); 

            await _service.PlanetService.SaveChangesForPatchAsync(result.planetToPatch, result.planetEntity);

            return NoContent();
        }
    }
}
