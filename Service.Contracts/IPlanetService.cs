using Shared.DataTransferObjects;

namespace Service.Contracts
{
    public interface IPlanetService
    {
        IEnumerable<PlanetDto> GetAllPlanets(bool trachChanges);
        PlanetDto GetPlanet(Guid planetId, bool trackChanges);
        PlanetDto CreatePlanet(PlanetForCreationDto planet);
        IEnumerable<PlanetDto> GetByIds(IEnumerable<Guid> ids, bool trackChanges);
        (IEnumerable<PlanetDto> planets, string ids) CreatePlanetCollecton(IEnumerable<PlanetForCreationDto> planetColletion);
    }
}
