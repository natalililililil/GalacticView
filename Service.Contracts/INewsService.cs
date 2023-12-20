
using Entities.Models;
using Shared.DataTransferObjects;

namespace Service.Contracts
{
    public interface INewsService
    {
        Task<IEnumerable<NewsDto>> GetAllNewsAsync(bool trachChanges);
        Task<NewsDto> GetNewsAsync(Guid newsId, bool trackChanges);
        Task<NewsDto> CreateNewsAsync(NewsForCreationDto news);
        Task<IEnumerable<NewsDto>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);
        //Task<(IEnumerable<PlanetDto> planets, string ids)> CreatePlanetCollectonAsync(IEnumerable<PlanetForCreationDto> planetColletion);
        Task DeleteNewsAsync(Guid newsId, bool trackChanges);
        //Task UpdateNewsAsync(Guid newsId, PlanetForUpdateDto planetForUpdate,
        //    bool trackChanges);
        //Task<(PlanetForUpdateDto planetToPatch, Planet planetEntity)> GetPlanetForPatchAsync(Guid planetId, bool planetTrackChanges);
        //Task SaveChangesForPatchAsync(PlanetForUpdateDto planetToPatch, Planet planetEntity);
    }
}
