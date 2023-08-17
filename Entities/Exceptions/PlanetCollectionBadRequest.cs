using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public sealed class PlanetCollectionBadRequest : BadRequestException
    {
        public PlanetCollectionBadRequest() : base ("Planet collection sent from a client is null.")
        {

        }
    }
}
