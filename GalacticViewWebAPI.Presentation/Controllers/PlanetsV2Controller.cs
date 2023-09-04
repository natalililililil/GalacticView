using Microsoft.AspNetCore.Mvc;
using Service.Contracts;

namespace GalacticViewWebAPI.Presentation.Controllers
{
    [Route("api/planets")]
    [ApiController]
    public class PlanetsV2Controller : ControllerBase
    {
        private readonly IServiceManager _service;

        public PlanetsV2Controller(IServiceManager service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> GetPlanets()
        {
            var planets = await _service.PlanetService.GetAllPlanetsAsync(trachChanges: false);

            var planets2 = planets.Select(x => $"{x.Name} V2");
            return Ok(planets2);
        }
    }
}
