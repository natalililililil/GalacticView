using Contracts;
using Entities.LinkModels;
using Entities.Models;
using Microsoft.Net.Http.Headers;
using Shared.DataTransferObjects;


namespace GalacticViewWebAPI.Utility
{
    public class SatelliteLinks : ISatelliteLinks
    {
        private readonly LinkGenerator _linkGenerator;
        private readonly IDataShaper<SatelliteDto> _dataShaper;
        public SatelliteLinks(LinkGenerator linkGenerator, IDataShaper<SatelliteDto> dataShaper)
        {
            _linkGenerator = linkGenerator;
            _dataShaper = dataShaper;
        }

        public LinkResponse TryGenerateLinks(IEnumerable<SatelliteDto> satelliteDto, string fields, 
            Guid planetId, HttpContext httpContext)
        {
            var shapedSatellites = ShapeData(satelliteDto, fields);

            if (ShouldGenerateLinks(httpContext))
                return ReturnLinkdedSatellites(satelliteDto, fields, planetId, httpContext, shapedSatellites);

            return ReturnShapedSatellites(shapedSatellites);
        }

        private List<Entity> ShapeData(IEnumerable<SatelliteDto> employeesDto, string fields) => 
            _dataShaper.ShapeData(employeesDto, fields).Select(e => e.Entity).ToList();

        private bool ShouldGenerateLinks(HttpContext httpContext)
        {
            var mediaType = (MediaTypeHeaderValue)httpContext.Items["AcceptHeaderMediaType"];
            return mediaType.SubTypeWithoutSuffix.EndsWith("hateoas", StringComparison.InvariantCultureIgnoreCase);
        }

        private LinkResponse ReturnShapedSatellites(List<Entity> shapedSatellites) =>
         new LinkResponse { ShapedEntities = shapedSatellites };

        private LinkResponse ReturnLinkdedSatellites(IEnumerable<SatelliteDto> satelliteDto, string fields, Guid planetId, HttpContext httpContext, List<Entity> shapedSatellites)
        {
            var satelliteDtoList = satelliteDto.ToList();
            for (var index = 0; index < satelliteDtoList.Count(); index++)
            {
                var satelliteLinks = CreateLinksForSatellite(httpContext, planetId, satelliteDtoList[index].Id, fields);
                shapedSatellites[index].Add("Links", satelliteLinks);
            }

            var employeeCollection = new LinkCollectionWrapper<Entity>(shapedSatellites);
            var linkedEmployees = CreateLinksForSatellites(httpContext, employeeCollection);
            return new LinkResponse { HasLinks = true, LinkedEntities = linkedEmployees };
        }

        private List<Link> CreateLinksForSatellite(HttpContext httpContext, Guid planetId, Guid id, string fields ="")
        {
            var links = new List<Link>
            {
                 new Link(_linkGenerator.GetUriByAction(httpContext, "GetSatelliteForPlanet", values: new { planetId, id, fields }),
                 "self", "GET"),
                 new Link(_linkGenerator.GetUriByAction(httpContext, "DeleteSatelliteForPlanet", values: new { planetId, id }),
                 "delete_employee", "DELETE"),
                 new Link(_linkGenerator.GetUriByAction(httpContext, "UpdateSatelliteForPlanet", values: new { planetId, id }),
                 "update_employee", "PUT"),
                 new Link(_linkGenerator.GetUriByAction(httpContext, "PartiallyUpdateSatelliteForPlanet", values: new { planetId, id }),
                 "partially_update_employee", "PATCH")
            };
            
            return links;
        }

        private LinkCollectionWrapper<Entity> CreateLinksForSatellites(HttpContext httpContext, LinkCollectionWrapper<Entity> satellitesWrapper)
        {
            satellitesWrapper.Links.Add(new Link(_linkGenerator.GetUriByAction(httpContext, "GetSatellitesForPlanet", values: new { }),
            "self", "GET"));
            return satellitesWrapper;
        }
    }
}
