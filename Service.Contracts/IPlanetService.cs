﻿using Shared.DataTransferObjects;

namespace Service.Contracts
{
    public interface IPlanetService
    {
        public IEnumerable<PlanetDto> GetAllPlanets(bool trachChanges);
        PlanetDto GetPlanet(Guid planetId, bool trackChanges);
    }
}
