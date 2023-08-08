using Contracts;
using Entities.Models;
using Service.Contracts;

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

        public IEnumerable<Planet> GetAllPlanets(bool trachChanges)
        {
            try
            {
                var planets = _repository.Planet.GetAllPlanets(trachChanges);

                return planets;
            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong in the {nameof(GetAllPlanets)} service method {ex}");
                throw;
            }
        }
    }
}
