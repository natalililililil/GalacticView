using Contracts;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<IPlanetService> _planetService;
        private readonly Lazy<ISatelliteService> _satelliteService;

        public ServiceManager(IRepositoryManager repositoryManager, ILoggerManager logger)
        {
            _planetService = new Lazy<IPlanetService>(() => new PlanetService(repositoryManager, logger));
            _satelliteService = new Lazy<ISatelliteService>(() => new SatelliteService(repositoryManager, logger));
        }

        public IPlanetService PlanetService => _planetService.Value;
        public ISatelliteService SatelliteService => _satelliteService.Value;
    }
}
