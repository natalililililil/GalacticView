using Entities.Models;
using Shared.DataTransferObjects;

namespace Service.Contracts
{
    public interface ISatelliteService
    {
        Task<IEnumerable<SatelliteDto>> GetSatellitesAsync(Guid planetId, bool trackChanges);
        Task<SatelliteDto> GetSatelliteAsync(Guid planetId, Guid id, bool trackChanges);
        Task<SatelliteDto> CreateSatelliteForPlanetAsync(Guid planetId, SatelliteForCreationDto satelliteForCreation, bool trackChanges);
        Task DeleteSatelliteForPlanetAsync(Guid planetId, Guid id, bool trackChanges);
        Task UpdateSatelliteForPlanetAsync(Guid planetId, Guid id, SatelliteForUpdateDto satelliteForUpdate, bool planetTrackChanges,
            bool satTrackChanges);
        Task<(SatelliteForUpdateDto satelliteToPatch, Satellite satelliteEntity)> GetSatelliteForPatchAsync(Guid planetId,
            Guid id, bool planetTrackChanges, bool satTrackChanges);
        Task SaveChangesForPatchAsync(SatelliteForUpdateDto satelliteForPatch, Satellite satelliteEntity);
    }
}
