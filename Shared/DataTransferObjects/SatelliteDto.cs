using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record SatelliteDto(Guid Id, string Name, double DistanceFromThePlanet, string SatelliteInfo);
}
