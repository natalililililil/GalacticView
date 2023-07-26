using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using Entities;

namespace Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _repositoryContext;
        private IPlanetRepository? _planetRepository;
        private ISatelliteRepository? _satelliteRepository;
        
        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }
        public IPlanetRepository Planet
        {
            get 
            {
                if (_planetRepository == null)
                    _planetRepository = new PlanetRepository(_repositoryContext);
                return _planetRepository; 
            }
        }

        public ISatelliteRepository Satellite
        {
            get
            {
                if (_satelliteRepository == null)
                    _satelliteRepository = new SatelliteRepository(_repositoryContext);

                return _satelliteRepository;
            }
        }

        public void Save() => _repositoryContext.SaveChanges();
    }
}
