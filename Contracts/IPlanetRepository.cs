using Entities.Models;

namespace Contracts
{
    public interface IPlanetRepository
    {
        IEnumerable<Planet> GetAllPlanets(bool trackChanges);
        Planet GetPlanet(Guid planetId, bool trackChanges);
        void CreatePlanet(Planet planet);
        IEnumerable<Planet> GetByIds(IEnumerable<Guid> ids, bool trackChanges);
        void DeletePlanet(Planet planet);
    }
}
