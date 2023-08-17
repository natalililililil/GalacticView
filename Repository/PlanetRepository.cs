using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using Entities;
using Entities.Models;

namespace Repository
{
    public class PlanetRepository : RepositoryBase<Planet>, IPlanetRepository
    {
        public PlanetRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {

        }

        public IEnumerable<Planet> GetAllPlanets(bool trackChanges) =>
            FindAll(trackChanges).OrderBy(c => c.Name).ToList();

        public Planet GetPlanet(Guid planetId, bool trackChanges) =>
            FindByCondition(с => с.Id.Equals(planetId), trackChanges).SingleOrDefault();

        public void CreatePlanet(Planet planet) => Create(planet);
    }
}
