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
                .ForMember(p => p.FullPlanetInfo,
                opt => opt.MapFrom(x => string.Join(' ', x.PlanetInfo, "Расстояние от солнца: " + x.DistanceFromTheSun + " млн км")));

            CreateMap<Satellite, SatelliteDto>();

            CreateMap<PlanetForCreationDto, Planet>();

            CreateMap<SatelliteForCreationDto, Satellite>();
        }
    }
}
