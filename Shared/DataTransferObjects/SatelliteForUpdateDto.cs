﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record SatelliteForUpdateDto(string Name, double DistanceFromTheSun, string SatelliteInfo);
}