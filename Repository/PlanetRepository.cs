using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using Entities;
using Entities.Models;

namespace Repository
{
    public class PlanetRepository : RepositoryBase<Planet>, IPlanetRepository
    {
        public PlanetRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {

        }
    }
}
