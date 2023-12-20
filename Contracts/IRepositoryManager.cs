namespace Contracts
{
    public interface IRepositoryManager
    {
        IPlanetRepository Planet { get; }
        ISatelliteRepository Satellite { get; }
        INewsRepository News { get; }
        Task SaveAsync();
    }
}
