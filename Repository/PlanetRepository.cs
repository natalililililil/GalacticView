using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class PlanetRepository : RepositoryBase<Planet>, IPlanetRepository
    {
        public PlanetRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {

        }

        public async Task<IEnumerable<Planet>> GetAllPlanetsAsync(bool trackChanges) =>
            await FindAll(trackChanges).OrderBy(c => c.Name).ToListAsync();

        public async Task<Planet> GetPlanetAsync(Guid planetId, bool trackChanges) =>
            await FindByCondition(с => с.Id.Equals(planetId), trackChanges).SingleOrDefaultAsync();

        public void CreatePlanet(Planet planet) => Create(planet);

        public async Task<IEnumerable<Planet>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges) =>
            await FindByCondition(x => ids.Contains(x.Id), trackChanges).ToListAsync();

        public void DeletePlanet(Planet planet) => Delete(planet);
    }
}
