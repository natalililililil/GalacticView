using Contracts;

namespace Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _repositoryContext;
        private readonly Lazy<IPlanetRepository> _planetRepository;
        private readonly Lazy<ISatelliteRepository> _satelliteRepository;
        
        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
            _planetRepository = new Lazy<IPlanetRepository>(() => new 
            PlanetRepository(repositoryContext));
            _satelliteRepository = new Lazy<ISatelliteRepository>(() => new 
            SatelliteRepository(repositoryContext));
        }
        public IPlanetRepository Planet => _planetRepository.Value;

        public ISatelliteRepository Satellite => _satelliteRepository.Value;

        public async Task SaveAsync() => await _repositoryContext.SaveChangesAsync();
    }
}
