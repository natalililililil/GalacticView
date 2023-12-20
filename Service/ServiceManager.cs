using Contracts;
using Service.Contracts;
using AutoMapper;
using Shared.DataTransferObjects;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using Entities.Models;
using Microsoft.Extensions.Options;
using Entities.ConfigurationModels;

namespace Service
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<IPlanetService> _planetService;
        private readonly Lazy<ISatelliteService> _satelliteService;
        private readonly Lazy<IAuthenticationService> _authenticationService;
        private readonly Lazy<INewsService> _newsService;

        public ServiceManager(IRepositoryManager repositoryManager, ILoggerManager logger, IMapper mapper,
            ISatelliteLinks satelliteLinks, UserManager<User> userManager, IOptions<JwtConfiguration> configuration)
        {
            _planetService = new Lazy<IPlanetService>(() => new PlanetService(repositoryManager, logger, mapper));
            _satelliteService = new Lazy<ISatelliteService>(() => new SatelliteService(repositoryManager, logger, mapper, satelliteLinks));
            _authenticationService = new Lazy<IAuthenticationService>(() => new AuthenticationService(logger, mapper, userManager, configuration));
            _newsService = new Lazy<INewsService>(() => new NewsService(repositoryManager, logger, mapper));
        }

        public IPlanetService PlanetService => _planetService.Value;
        public ISatelliteService SatelliteService => _satelliteService.Value;
        public IAuthenticationService AuthenticationService => _authenticationService.Value;   
        public INewsService NewsService => _newsService.Value;
    }
}
