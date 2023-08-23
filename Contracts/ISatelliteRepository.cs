using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface ISatelliteRepository
    {
        Task<IEnumerable<Satellite>> GetSetellitesAsync(Guid planetId, bool trackChanges);
        Task<Satellite> GetSetelliteAsync(Guid planetId, Guid id, bool trackChanges);
        void CreateSatelliteForPlanet(Guid planetId, Satellite satellite);
        void DeleteSatellite(Satellite satellite);
    }
}
