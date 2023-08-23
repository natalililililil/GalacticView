using Contracts;
using Service.Contracts;
using AutoMapper;
using Shared.DataTransferObjects;
using Entities.Exceptions;
using Entities.Models;

namespace Service
{
    internal sealed class SatelliteService : ISatelliteService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public SatelliteService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SatelliteDto>> GetSatelliteAsync(Guid planetId, bool trackChanges)
        {
            var planet = await _repository.Planet.GetPlanetAsync(planetId, trackChanges);
            if (planet is null)
                throw new PlanetNotFoundException(planetId);

            var satellitesFromDb = await _repository.Satellite.GetSetellitesAsync(planetId, trackChanges);
            
            var satellitesDto = _mapper.Map<IEnumerable<SatelliteDto>>(satellitesFromDb);
            return satellitesDto;
        }

        public async Task<SatelliteDto> GetSatelliteAsync(Guid planetId, Guid id, bool trackChanges)
        {
            var planet = await _repository.Planet.GetPlanetAsync(planetId, trackChanges);
            if (planet is null)
                throw new PlanetNotFoundException(planetId);

            var satelliteDb = await _repository.Satellite.GetSetelliteAsync(planetId, id, trackChanges);
            if (satelliteDb is null)
                throw new SatelliteNotFoundException(id);

            var satellite = _mapper.Map<SatelliteDto>(satelliteDb);
            return satellite;
        }

        public async Task<SatelliteDto> CreateSatelliteForPlanetAsync(Guid planetId, SatelliteForCreationDto satelliteForCreation, bool trackChanges)
        {
            var planet = await _repository.Planet.GetPlanetAsync(planetId, trackChanges);
            if (planet is null)
                throw new PlanetNotFoundException(planetId);

            var satelliteEntity = _mapper.Map<Satellite>(satelliteForCreation);

            _repository.Satellite.CreateSatelliteForPlanet(planetId, satelliteEntity);
            await _repository.SaveAsync();

            var satelliteToReturn = _mapper.Map<SatelliteDto>(satelliteEntity);

            return satelliteToReturn;
        }

        public async Task DeleteSatelliteForPlanetAsync(Guid planetId, Guid id, bool trackChanges)
        {
            var planet = await _repository.Planet.GetPlanetAsync(planetId, trackChanges);
            if (planet is null)
                throw new PlanetNotFoundException(planetId);

            var satellite = await _repository.Satellite.GetSetelliteAsync(planetId, id, trackChanges);
            if (satellite is null)
                throw new SatelliteNotFoundException(id);

            _repository.Satellite.DeleteSatellite(satellite);
            await _repository.SaveAsync();
        }

        public async Task UpdateSatelliteForPlanetAsync(Guid planetId, Guid id, SatelliteForUpdateDto satelliteForUpdate, 
            bool planetTrackChanges, bool satTrackChanges)
        {
            var planet = await _repository.Planet.GetPlanetAsync(planetId, planetTrackChanges);
            if (planet is null)
                throw new PlanetNotFoundException(planetId);

            var satelliteEntity = await _repository.Satellite.GetSetelliteAsync(planetId, id, satTrackChanges);
            if (satelliteEntity is null)
                throw new SatelliteNotFoundException(id);

            _mapper.Map(satelliteForUpdate, satelliteEntity);
            await _repository.SaveAsync();
        }

        public async Task<(SatelliteForUpdateDto satelliteToPatch, Satellite satelliteEntity)> GetSatelliteForPatchAsync(Guid planetId,
            Guid id, bool planetTrackChanges, bool satTrackChanges)
        {
            var planet = await _repository.Planet.GetPlanetAsync(planetId, planetTrackChanges);
            if (planet is null)
                throw new PlanetNotFoundException(planetId);

            var satelliteEntity = await _repository.Satellite.GetSetelliteAsync(planetId, id, satTrackChanges);
            if (satelliteEntity is null)
                throw new SatelliteNotFoundException(id);

            var satelliteToPatch = _mapper.Map<SatelliteForUpdateDto>(satelliteEntity);

            return (satelliteToPatch, satelliteEntity);
        }

        public async Task SaveChangesForPatchAsync(SatelliteForUpdateDto satelliteForPatch, Satellite satelliteEntity)
        {
            _mapper.Map(satelliteForPatch, satelliteEntity);
            await _repository.SaveAsync();
        }
    }
}
