using Contracts;
using Service.Contracts;
using Shared.DataTransferObjects;
using AutoMapper;
using Entities.Exceptions;

namespace Service
{
    internal sealed class PlanetService : IPlanetService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public PlanetService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public IEnumerable<PlanetDto> GetAllPlanets(bool trachChanges)
        {
            var planets = _repository.Planet.GetAllPlanets(trachChanges);

            var planetsDto = _mapper.Map<IEnumerable<PlanetDto>>(planets);
            return planetsDto;
        }

        public PlanetDto GetPlanet(Guid id, bool trackChanges)
        {
            var planet = _repository.Planet.GetPlanet(id, trackChanges);
            if (planet is null)
                throw new PlanetNotFoundException(id);

            var planetDto = _mapper.Map<PlanetDto>(planet);
            return planetDto;
        }
    }
}
