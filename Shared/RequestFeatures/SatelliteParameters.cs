using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.RequestFeatures
{
    public class SatelliteParameters : RequestParameters
    {
        public double MinDistanceFromThePlanet { get; set; }
        public double MaxDistanceFromThePlanet { get; set; } = double.MaxValue;

        public bool ValidDistanceFromThePlanetRange => MaxDistanceFromThePlanet > MinDistanceFromThePlanet;
    }
}
