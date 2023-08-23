using Entities.Models;

namespace Contracts
{
    public interface IPlanetRepository
    {
        Task<IEnumerable<Planet>> GetAllPlanetsAsync(bool trackChanges);
        Task<Planet> GetPlanetAsync(Guid planetId, bool trackChanges);
        void CreatePlanet(Planet planet);
        Task<IEnumerable<Planet>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);
        void DeletePlanet(Planet planet);
    }
}
