using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public sealed class MaxDistanceFromThePlanetBadRequestException : BadRequestException
    {
        public MaxDistanceFromThePlanetBadRequestException() : 
            base("Max distance from the planet can't be less than min distance.")
        {
        }
    }
}
