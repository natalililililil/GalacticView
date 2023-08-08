using Entities.Models;

namespace Contracts
{
    public interface IPlanetRepository
    {
        IEnumerable<Planet> GetAllPlanets(bool trackChanges);
    }
}
