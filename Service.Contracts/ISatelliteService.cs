using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface ISatelliteService
    {
        IEnumerable<SatelliteDto> GetSatellite(Guid planetId, bool trackChanges);
        SatelliteDto GetSatellite(Guid planetId, Guid id, bool trackChanges);
    }
}
