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

        public IEnumerable<SatelliteDto> GetSatellite(Guid planetId, bool trackChanges)
        {
            var planet = _repository.Planet.GetPlanet(planetId, trackChanges);
            if (planet is null)
                throw new PlanetNotFoundException(planetId);

            var satellitesFromDb = _repository.Satellite.GetSetellites(planetId, trackChanges);
            
            var satellitesDto = _mapper.Map<IEnumerable<SatelliteDto>>(satellitesFromDb);
            return satellitesDto;
        }

        public SatelliteDto GetSatellite(Guid planetId, Guid id, bool trackChanges)
        {
            var planet = _repository.Planet.GetPlanet(planetId, trackChanges);
            if (planet is null)
                throw new PlanetNotFoundException(planetId);

            var satelliteDb = _repository.Satellite.GetSetellite(planetId, id, trackChanges);
            if (satelliteDb is null)
                throw new SatelliteNotFoundException(id);

            var satellite = _mapper.Map<SatelliteDto>(satelliteDb);
            return satellite;
        }

        public SatelliteDto CreateSatelliteForPlanet(Guid planetId, SatelliteForCreationDto satelliteForCreation, bool trackChanges)
        {
            var planet = _repository.Planet.GetPlanet(planetId, trackChanges);
            if (planet is null)
                throw new PlanetNotFoundException(planetId);

            var satelliteEntity = _mapper.Map<Satellite>(satelliteForCreation);

            _repository.Satellite.CreateSatelliteForPlanet(planetId, satelliteEntity);
            _repository.Save();

            var satelliteToReturn = _mapper.Map<SatelliteDto>(satelliteEntity);

            return satelliteToReturn;
        }
    }
}
