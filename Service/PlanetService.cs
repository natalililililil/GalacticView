using Contracts;
using Service.Contracts;
using Shared.DataTransferObjects;
using AutoMapper;
using Entities.Exceptions;
using Entities.Models;

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

        public PlanetDto CreatePlanet(PlanetForCreationDto planet)
        {
            var planetEntity = _mapper.Map<Planet>(planet);

            _repository.Planet.CreatePlanet(planetEntity);
            _repository.Save();

            var planetToReturn = _mapper.Map<PlanetDto>(planetEntity);
            return planetToReturn;
        }

        public IEnumerable<PlanetDto> GetByIds(IEnumerable<Guid> ids, bool trackChanges)
        {
            if (ids is null)
                throw new IdParametersBadRequestException();

            var planetEntities = _repository.Planet.GetByIds(ids, trackChanges);

            if (ids.Count() != planetEntities.Count())
                throw new CollectionByIdsBadRequestException();

            var planetsToReturn = _mapper.Map<IEnumerable<PlanetDto>>(planetEntities);

            return planetsToReturn;
        }

        public (IEnumerable<PlanetDto> planets, string ids) CreatePlanetCollecton
            (IEnumerable<PlanetForCreationDto> planetColletion)
        {
            if (planetColletion is null)
                throw new PlanetCollectionBadRequest();

            var planetEntities = _mapper.Map<IEnumerable<Planet>>(planetColletion);
            foreach (var planet in planetEntities)
            {
                _repository.Planet.CreatePlanet(planet);
            }

            _repository.Save();

            var planetCollectionToReturn = _mapper.Map<IEnumerable<PlanetDto>>(planetEntities);
            var ids = string.Join(",", planetCollectionToReturn.Select(p => p.Id));

            return (planets: planetCollectionToReturn, ids: ids);
        }

        public void DeletePlanet(Guid planetId, bool trackChanges)
        {
            var planet = _repository.Planet.GetPlanet(planetId, trackChanges);
            if (planet is null)
                throw new PlanetNotFoundException(planetId);

            _repository.Planet.DeletePlanet(planet);
            _repository.Save();
        }

        public void UpdatePlanet(Guid planetId, PlanetForUpdateDto planetForUpdate, bool trackChanges)
        {
            var planetEntity = _repository.Planet.GetPlanet(planetId, trackChanges);
            if (planetEntity is null)
                throw new PlanetNotFoundException(planetId);

            _mapper.Map(planetForUpdate, planetEntity);
            _repository.Save();
        }

        public (PlanetForUpdateDto, Planet) GetPlanetForPatch(Guid planetId, bool planetTrackChanges)
        {
            var planetEntity = _repository.Planet.GetPlanet(planetId, planetTrackChanges);
            if (planetEntity is null)
                throw new PlanetNotFoundException(planetId);

            var planetToPatch = _mapper.Map<PlanetForUpdateDto>(planetEntity);

            return (planetToPatch, planetEntity);
        }

        public void SaveChangesForPatch(PlanetForUpdateDto planetToPatch, Planet planetEntity)
        {
            _mapper.Map(planetToPatch, planetEntity);
            _repository.Save();
        }
    }
}
