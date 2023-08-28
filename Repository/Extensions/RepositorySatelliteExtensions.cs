using Entities.Models;
using Repository.Extensions.Utility;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;

namespace Repository.Extensions
{
    public static class RepositorySatelliteExtensions
    {
        public static IQueryable<Satellite> FilterSatellites(this IQueryable<Satellite>
            satellites, double minDistanceFromThePlanet, double maxDistanceFromThePlanet) =>
            satellites.Where(s => (s.DistanceFromThePlanet >= minDistanceFromThePlanet && s.DistanceFromThePlanet <= maxDistanceFromThePlanet));

        public static IQueryable<Satellite> Search(this IQueryable<Satellite> satellites, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return satellites;
            var lowerCaseTerm = searchTerm.Trim().ToLower();
            return satellites.Where(e => e.Name.ToLower().Contains(lowerCaseTerm));
        }

        public static IQueryable<Satellite> Sort(this IQueryable<Satellite> satellites, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return satellites.OrderBy(e => e.Name);

            var orderQuery = OrderQueryBuilder.CreateOrderQuery<Satellite>(orderByQueryString);

            if (string.IsNullOrWhiteSpace(orderQuery))
                return satellites.OrderBy(e => e.Name);

            return satellites.OrderBy(orderQuery);
        }
    }
}
