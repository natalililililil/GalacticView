using Contracts;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Service
{
    internal sealed class PlanetService : IPlanetService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;

        public PlanetService(IRepositoryManager repository, ILoggerManager logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public IEnumerable<PlanetDto> GetAllPlanets(bool trachChanges)
        {
            try
            {
                var planets = _repository.Planet.GetAllPlanets(trachChanges);

                var planetsDto = planets.Select(c => new PlanetDto(c.Id, c.Name ?? "", 
                    string.Join(' ', c.PlanetInfo, "Расстояние от солнца: " + c.DistanceFromTheSun + " млн км"))).ToList();
                return planetsDto;
            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong in the {nameof(GetAllPlanets)} service method {ex}");
                throw;
            }
        }
    }
}
