using GalacticViewWebAPI.Presentation.ModelBinders;
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

        [HttpGet("collection/({ids})", Name = "PlanetCollection")]
        public IActionResult GetPlanetCollection([ModelBinder(BinderType = typeof(ArrayModelBinder))]IEnumerable<Guid> ids)
        {
            var planets = _service.PlanetService.GetByIds(ids, trackChanges: false);

            return Ok(planets);
        }

        [HttpPost]
        public IActionResult CreatePlanet([FromBody] PlanetForCreationDto planet)
        {
            if (planet is null)
                return BadRequest("PlanetForCreationDto object is null");

            var createdPlanet = _service.PlanetService.CreatePlanet(planet);

            return CreatedAtRoute("PlanetById", new { id = createdPlanet.Id }, createdPlanet);
        }

        [HttpPost("collection")]
        public IActionResult CreatePlanetCollection([FromBody] IEnumerable<PlanetForCreationDto> planetColletion)
        {
            var result = _service.PlanetService.CreatePlanetCollecton(planetColletion);

            return CreatedAtRoute("PlanetCollection", new {result.ids}, result.planets);
        }

        [HttpDelete("{id:guid}")]
        public IActionResult DeletePlanet(Guid id)
        {
            _service.PlanetService.DeletePlanet(id, trackChanges: false);
            return NoContent();
        }
    }
}
