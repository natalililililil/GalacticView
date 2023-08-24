using Entities.Models;
using Shared.RequestFeatures;

namespace Contracts
{
    public interface ISatelliteRepository
    {
        Task<PagedList<Satellite>> GetSetellitesAsync(Guid planetId, SatelliteParameters satelliteParameters, bool trackChanges);
        Task<Satellite> GetSetelliteAsync(Guid planetId, Guid id, bool trackChanges);
        void CreateSatelliteForPlanet(Guid planetId, Satellite satellite);
        void DeleteSatellite(Satellite satellite);
    }
}
