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

        public async Task<IEnumerable<PlanetDto>> GetAllPlanetsAsync(bool trachChanges)
        {
            var planets = await _repository.Planet.GetAllPlanetsAsync(trachChanges);

            var planetsDto = _mapper.Map<IEnumerable<PlanetDto>>(planets);
            return planetsDto;
        }

        public async Task<PlanetDto> GetPlanetAsync(Guid id, bool trackChanges)
        {
            var planet = await GetPlanetAndCheckIfItExists(id, trackChanges);

            var planetDto = _mapper.Map<PlanetDto>(planet);
            return planetDto;
        }

        public async Task<PlanetDto> CreatePlanetAsync(PlanetForCreationDto planet)
        {
            var planetEntity = _mapper.Map<Planet>(planet);

            _repository.Planet.CreatePlanet(planetEntity);
            await _repository.SaveAsync();

            var planetToReturn = _mapper.Map<PlanetDto>(planetEntity);
            return planetToReturn;
        }

        public async Task<IEnumerable<PlanetDto>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges)
        {
            if (ids is null)
                throw new IdParametersBadRequestException();

            var planetEntities = await _repository.Planet.GetByIdsAsync(ids, trackChanges);

            if (ids.Count() != planetEntities.Count())
                throw new CollectionByIdsBadRequestException();

            var planetsToReturn = _mapper.Map<IEnumerable<PlanetDto>>(planetEntities);

            return planetsToReturn;
        }

        public async Task<(IEnumerable<PlanetDto> planets, string ids)> CreatePlanetCollectonAsync
            (IEnumerable<PlanetForCreationDto> planetColletion)
        {
            if (planetColletion is null)
                throw new PlanetCollectionBadRequest();

            var planetEntities = _mapper.Map<IEnumerable<Planet>>(planetColletion);
            foreach (var planet in planetEntities)
            {
                _repository.Planet.CreatePlanet(planet);
            }

            await _repository.SaveAsync();

            var planetCollectionToReturn = _mapper.Map<IEnumerable<PlanetDto>>(planetEntities);
            var ids = string.Join(",", planetCollectionToReturn.Select(p => p.Id));

            return (planets: planetCollectionToReturn, ids: ids);
        }

        public async Task DeletePlanetAsync(Guid planetId, bool trackChanges)
        {
            var planet = await GetPlanetAndCheckIfItExists(planetId, trackChanges);

            _repository.Planet.DeletePlanet(planet);
            await _repository.SaveAsync();
        }

        public async Task UpdatePlanetAsync(Guid planetId, PlanetForUpdateDto planetForUpdate, bool trackChanges)
        {
            var planet = await GetPlanetAndCheckIfItExists(planetId, trackChanges);

            _mapper.Map(planetForUpdate, planet);
            await _repository.SaveAsync();
        }

        public async Task<(PlanetForUpdateDto, Planet)> GetPlanetForPatchAsync(Guid planetId, bool planetTrackChanges)
        {
            var planet = await GetPlanetAndCheckIfItExists(planetId, planetTrackChanges);
            var planetToPatch = _mapper.Map<PlanetForUpdateDto>(planet);

            return (planetToPatch, planet);
        }

        private async Task<Planet> GetPlanetAndCheckIfItExists(Guid id, bool trackChanges)
        {
            var planetEntity = await _repository.Planet.GetPlanetAsync(id, trackChanges);
            if (planetEntity is null)
                throw new PlanetNotFoundException(id);

            return planetEntity;
        }

        public async Task SaveChangesForPatchAsync(PlanetForUpdateDto planetToPatch, Planet planetEntity)
        {
            _mapper.Map(planetToPatch, planetEntity);
            await _repository.SaveAsync();
        }
    }
}
