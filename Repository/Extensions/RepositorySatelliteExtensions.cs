using Entities.Models;

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
    }
}
