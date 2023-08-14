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
    public class SatelliteRepository : RepositoryBase<Satellite>, ISatelliteRepository
    {
        public SatelliteRepository(RepositoryContext repositoryContext) 
            : base(repositoryContext)
        {

        }

        public IEnumerable<Satellite> GetSetellites(Guid planetId, bool trackChanges) =>
            FindByCondition(s => s.PlanetId.Equals(planetId), trackChanges).OrderBy(s => s.Name).ToList();

        public Satellite GetSetellite(Guid planetId, Guid id, bool trackChanges) =>
            FindByCondition(s => s.PlanetId.Equals(planetId) && s.Id.Equals(id), trackChanges).SingleOrDefault();
    }
}
