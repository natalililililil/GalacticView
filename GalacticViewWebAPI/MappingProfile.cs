using AutoMapper;
using Entities.Models;
using Shared.DataTransferObjects;

namespace GalacticViewWebAPI
{
    public class MappingProfile : Profile 
    {
        public MappingProfile()
        {
            CreateMap<Planet, PlanetDto>()
                .ForCtorParam("FullPlanetInfo",
                opt => opt.MapFrom(x => string.Join(' ', x.PlanetInfo, "Расстояние от солнца: " + x.DistanceFromTheSun + " млн км")));

            CreateMap<Satellite, SatelliteDto>();
        }
    }
}
