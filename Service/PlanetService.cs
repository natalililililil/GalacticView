using Contracts;
using Service.Contracts;
using Shared.DataTransferObjects;
using AutoMapper;

namespace Service
{
    internal sealed class PlanetService : IPlanetService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public PlanetService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public IEnumerable<PlanetDto> GetAllPlanets(bool trachChanges)
        {
            var planets = _repository.Planet.GetAllPlanets(trachChanges);

            var planetsDto = _mapper.Map<IEnumerable<PlanetDto>>(planets);

            return planetsDto;
        }
    }
}
