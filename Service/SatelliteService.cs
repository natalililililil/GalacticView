using Contracts;
using Service.Contracts;
using AutoMapper;

namespace Service
{
    internal sealed class SatelliteService : ISatelliteService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public SatelliteService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }
    }
}
