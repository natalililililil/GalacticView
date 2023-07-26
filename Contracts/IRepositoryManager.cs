namespace Contracts
{
    public interface IRepositoryManager
    {
        IPlanetRepository Planet { get; }
        ISatelliteRepository Satellite { get; }
        void Save();
    }
}
