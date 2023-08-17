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
        public IActionResult GetPlanets()
        {
            var planets = _service.PlanetService.GetAllPlanets(trachChanges: false);
            return Ok(planets);
        }

        [HttpGet("{id:guid}", Name = "PlanetById")]
        public IActionResult GetPlanet(Guid id)
        {
            var planet = _service.PlanetService.GetPlanet(id, trackChanges: false);
            return Ok(planet);
        }

        [HttpPost]
        public IActionResult CreatePlanet([FromBody] PlanetForCreationDto planet)
        {
            if (planet is null)
                return BadRequest("PlanetForCreationDto object is null");

            var createdPlanet = _service.PlanetService.CreatePlanet(planet);

            return CreatedAtRoute("PlanetById", new { id = createdPlanet.Id }, createdPlanet);
        }
    }
}
