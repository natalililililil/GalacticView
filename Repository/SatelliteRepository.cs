using System;
using System.Collections.Generic;
using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions;
using Shared.RequestFeatures;

namespace Repository
{
    public class SatelliteRepository : RepositoryBase<Satellite>, ISatelliteRepository
    {
        public SatelliteRepository(RepositoryContext repositoryContext) 
            : base(repositoryContext)
        {

        }

        public async Task<PagedList<Satellite>> GetSetellitesAsync(Guid planetId, SatelliteParameters satelliteParameters, bool trackChanges)
        {
            var satellites = await FindByCondition(s => s.PlanetId.Equals(planetId), trackChanges)
            .FilterSatellites(satelliteParameters.MinDistanceFromThePlanet, satelliteParameters.MaxDistanceFromThePlanet)
            .Search(satelliteParameters.SearchTerm)
            .OrderBy(s => s.Name).ToListAsync();

            return PagedList<Satellite>.ToPagedList(satellites, satelliteParameters.PageNumber, satelliteParameters.PageSize);
        }
            

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
