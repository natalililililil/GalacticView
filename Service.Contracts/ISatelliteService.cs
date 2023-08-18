using Shared.DataTransferObjects;

namespace Service.Contracts
{
    public interface ISatelliteService
    {
        IEnumerable<SatelliteDto> GetSatellite(Guid planetId, bool trackChanges);
        SatelliteDto GetSatellite(Guid planetId, Guid id, bool trackChanges);
        SatelliteDto CreateSatelliteForPlanet(Guid planetId, SatelliteForCreationDto satelliteForCreation, bool trackChanges);
        void DeleteSatelliteForPlanet(Guid planetId, Guid id, bool trackChanges);
        void UpdateSatelliteForPlanet(Guid planetId, Guid id, SatelliteForUpdateDto satelliteForUpdate, bool planetTrackChanges,
            bool satTrackChanges);
    }
}
