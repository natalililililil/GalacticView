using System;
using System.Collections.Generic;
using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class SatelliteRepository : RepositoryBase<Satellite>, ISatelliteRepository
    {
        public SatelliteRepository(RepositoryContext repositoryContext) 
            : base(repositoryContext)
        {

        }

        public async Task<IEnumerable<Satellite>> GetSetellitesAsync(Guid planetId, bool trackChanges) =>
            await FindByCondition(s => s.PlanetId.Equals(planetId), trackChanges).OrderBy(s => s.Name).ToListAsync();

        public async Task<Satellite> GetSetelliteAsync(Guid planetId, Guid id, bool trackChanges) =>
            await FindByCondition(s => s.PlanetId.Equals(planetId) && s.Id.Equals(id), trackChanges).SingleOrDefaultAsync();

        public void CreateSatelliteForPlanet(Guid planetId, Satellite satellite)
        {
            satellite.PlanetId = planetId;
            Create(satellite);
        }

        public void DeleteSatellite(Satellite satellite) => Delete(satellite);
    }
}
