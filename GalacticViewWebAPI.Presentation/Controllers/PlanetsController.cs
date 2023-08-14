using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        [HttpGet("{id:guid}")]
        public IActionResult GetPlanet(Guid id)
        {
            var planet = _service.PlanetService.GetPlanet(id, trackChanges: false);
            return Ok(planet);
        }
    }
}
