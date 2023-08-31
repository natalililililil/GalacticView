using Contracts;
using Service.Contracts;
using AutoMapper;
using Shared.DataTransferObjects;
using Entities.Exceptions;
using Entities.Models;
using Shared.RequestFeatures;
using Entities.LinkModels;

namespace Service
{
    internal sealed class SatelliteService : ISatelliteService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly ISatelliteLinks _satelliteLinks;

        public SatelliteService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper,
            ISatelliteLinks satelliteLinks)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _satelliteLinks = satelliteLinks;
        }

        public async Task<(LinkResponse linkResponse, MetaData metaData)> GetSatellitesAsync(Guid planetId, LinkParameters linkParameters, bool trackChanges)
        {
            if (!linkParameters.SatellitesParameters.ValidDistanceFromThePlanetRange)
                throw new MaxDistanceFromThePlanetBadRequestException();

            await CheckIfPlanetExists(planetId, trackChanges);

            var satellitesWithMetaData = await _repository.Satellite.GetSetellitesAsync(planetId, linkParameters.SatellitesParameters, trackChanges);
            
            var satellitesDto = _mapper.Map<IEnumerable<SatelliteDto>>(satellitesWithMetaData);

            var links = _satelliteLinks.TryGenerateLinks(satellitesDto, linkParameters.SatellitesParameters.Fields,
                planetId, linkParameters.Content);

            return (linkResponse: links, metaData: satellitesWithMetaData.MetaData);
        }

        public async Task<SatelliteDto> GetSatelliteAsync(Guid planetId, Guid id, bool trackChanges)
        {
            await CheckIfPlanetExists(planetId, trackChanges);

            var satelliteDb = await GetSatelliteFromPlanetAndCheckIfExists(planetId, id, trackChanges);

            var satellite = _mapper.Map<SatelliteDto>(satelliteDb);
            return satellite;
        }

        public async Task<SatelliteDto> CreateSatelliteForPlanetAsync(Guid planetId, SatelliteForCreationDto satelliteForCreation, bool trackChanges)
        {
            await CheckIfPlanetExists(planetId, trackChanges);

            var satelliteEntity = _mapper.Map<Satellite>(satelliteForCreation);

            _repository.Satellite.CreateSatelliteForPlanet(planetId, satelliteEntity);
            await _repository.SaveAsync();

            var satelliteToReturn = _mapper.Map<SatelliteDto>(satelliteEntity);

            return satelliteToReturn;
        }

        public async Task DeleteSatelliteForPlanetAsync(Guid planetId, Guid id, bool trackChanges)
        {
            await CheckIfPlanetExists(planetId, trackChanges);

            var satellite = await GetSatelliteFromPlanetAndCheckIfExists(planetId, id, trackChanges);

            _repository.Satellite.DeleteSatellite(satellite);
            await _repository.SaveAsync();
        }

        public async Task UpdateSatelliteForPlanetAsync(Guid planetId, Guid id, SatelliteForUpdateDto satelliteForUpdate, 
            bool planetTrackChanges, bool satTrackChanges)
        {
            await CheckIfPlanetExists(planetId, planetTrackChanges);

            var satelliteEntity = await GetSatelliteFromPlanetAndCheckIfExists(planetId, id, satTrackChanges);

            _mapper.Map(satelliteForUpdate, satelliteEntity);
            await _repository.SaveAsync();
        }

        public async Task<(SatelliteForUpdateDto satelliteToPatch, Satellite satelliteEntity)> GetSatelliteForPatchAsync(Guid planetId,
            Guid id, bool planetTrackChanges, bool satTrackChanges)
        {
            await CheckIfPlanetExists(planetId, planetTrackChanges);

            var satelliteDb = await GetSatelliteFromPlanetAndCheckIfExists(planetId, id, satTrackChanges);

            var satelliteToPatch = _mapper.Map<SatelliteForUpdateDto>(satelliteDb);

            return (satelliteToPatch, satelliteDb);
        }

        private async Task CheckIfPlanetExists(Guid planetId, bool trackChanges)
        {
            var planet = await _repository.Planet.GetPlanetAsync(planetId, trackChanges);
            if (planet is null)
                throw new PlanetNotFoundException(planetId);
        }

        private async Task<Satellite> GetSatelliteFromPlanetAndCheckIfExists(Guid planetId, Guid id, bool trackChanges)
        {
            var satelliteDb = await _repository.Satellite.GetSetelliteAsync(planetId, id, trackChanges);
            if (satelliteDb is null)
                throw new SatelliteNotFoundException(id);

            return satelliteDb;
        }

        public async Task SaveChangesForPatchAsync(SatelliteForUpdateDto satelliteForPatch, Satellite satelliteEntity)
        {
            _mapper.Map(satelliteForPatch, satelliteEntity);
            await _repository.SaveAsync();
        }
    }
}
