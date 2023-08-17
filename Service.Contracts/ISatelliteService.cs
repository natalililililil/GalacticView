using Shared.DataTransferObjects;

namespace Service.Contracts
{
    public interface ISatelliteService
    {
        IEnumerable<SatelliteDto> GetSatellite(Guid planetId, bool trackChanges);
        SatelliteDto GetSatellite(Guid planetId, Guid id, bool trackChanges);
        SatelliteDto CreateSatelliteForPlanet(Guid planetId, SatelliteForCreationDto satelliteForCreation, bool trackChanges);
    }
}
