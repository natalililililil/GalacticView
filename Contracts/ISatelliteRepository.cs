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
        IEnumerable<Satellite> GetSetellites(Guid planetId, bool trackChanges);
        Satellite GetSetellite(Guid planetId, Guid id, bool trackChanges);
    }
}
