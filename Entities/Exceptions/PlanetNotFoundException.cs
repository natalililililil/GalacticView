using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public class PlanetNotFoundException : NotFoundException
    {
        public PlanetNotFoundException(Guid planetId) 
            : base($"The planet with id: {planetId} doesn't exist in the database.")
        {
        }
    }
}
