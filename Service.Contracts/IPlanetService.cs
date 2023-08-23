using Entities.Models;
using Shared.DataTransferObjects;

namespace Service.Contracts
{
    public interface IPlanetService
    {
        Task<IEnumerable<PlanetDto>> GetAllPlanetsAsync(bool trachChanges);
        Task<PlanetDto> GetPlanetAsync(Guid planetId, bool trackChanges);
        Task<PlanetDto> CreatePlanetAsync(PlanetForCreationDto planet);
        Task<IEnumerable<PlanetDto>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);
        Task<(IEnumerable<PlanetDto> planets, string ids)> CreatePlanetCollectonAsync(IEnumerable<PlanetForCreationDto> planetColletion);
        Task DeletePlanetAsync(Guid planetId, bool trackChanges);   
        Task UpdatePlanetAsync(Guid planetId, PlanetForUpdateDto planetForUpdate,
            bool trackChanges);
        Task<(PlanetForUpdateDto planetToPatch, Planet planetEntity)> GetPlanetForPatchAsync(Guid planetId, bool planetTrackChanges);
        Task SaveChangesForPatchAsync(PlanetForUpdateDto planetToPatch, Planet planetEntity);
    }
}
