using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public class SatelliteNotFoundException : NotFoundException
    {
        public SatelliteNotFoundException(Guid satellitesId)
            : base($"The satellite with id: {satellitesId} doesn't exist in the database.")
        {

        }
    }
}
