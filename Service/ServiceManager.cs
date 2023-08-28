using Contracts;
using Service.Contracts;
using AutoMapper;
using Shared.DataTransferObjects;

namespace Service
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<IPlanetService> _planetService;
        private readonly Lazy<ISatelliteService> _satelliteService;

        public ServiceManager(IRepositoryManager repositoryManager, ILoggerManager logger, IMapper mapper,
            IDataShaper<SatelliteDto> dataShaper)
        {
            _planetService = new Lazy<IPlanetService>(() => new PlanetService(repositoryManager, logger, mapper));
            _satelliteService = new Lazy<ISatelliteService>(() => new SatelliteService(repositoryManager, logger, mapper, dataShaper));
        }

        public IPlanetService PlanetService => _planetService.Value;
        public ISatelliteService SatelliteService => _satelliteService.Value;
    }
}
