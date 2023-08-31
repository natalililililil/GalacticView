using Entities.LinkModels;
using Microsoft.AspNetCore.Http;
using Shared.DataTransferObjects;

namespace Contracts
{
    public interface ISatelliteLinks
    {
        LinkResponse TryGenerateLinks(IEnumerable<SatelliteDto> satelliteDto, string fields,
            Guid planetId, HttpContext httpContext);
    }
}
