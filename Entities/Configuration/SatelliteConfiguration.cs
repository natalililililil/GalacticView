using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.Configuration
{
    public class SatelliteConfiguration : IEntityTypeConfiguration<Satellite>
    {
        public void Configure(EntityTypeBuilder<Satellite> builder)
        {
            builder.HasData
            (
                new Satellite
                {
                    Id = new Guid("80abbca8-664d-4b20-b5de-024705497d4a"),
                    Name = "Титан",
                    DistanceFromThePlanet = 1.2,
                    SatelliteInfo = "Титан - крупнейший спутник Сатурна, второй по величине спутник в Солнечной системе " +
                    "(после спутника Юпитера Ганимеда), является единственным, кроме Земли, телом в Солнечной системе, для которого " +
                    "доказано стабильное существование жидкости на поверхности, и единственным спутником планеты, обладающим плотной атмосферой.",
                    PlanetId = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870")
                },
                new Satellite
                {
                    Id = new Guid("86dba8c0-d178-41e7-938c-ed49778fb52a"),
                    Name = "Ганимед",
                    DistanceFromThePlanet = 1.07,
                    SatelliteInfo = "Ганимед - самый большой спутник Юпитера и всей солнечной системы, имеющий размер планеты. Его диаметр составляет " +
                    "5268 км. Он получил свое название по имени сына троянского царя и нимфы Каллирои.",
                    PlanetId = new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3")
                },
                new Satellite
                {
                    Id = new Guid("021ca3c1-0deb-4afd-ae94-2159a8479811"),
                    Name = "Ио",
                    DistanceFromThePlanet = 0.35,
                    PlanetId = new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3")
                }
            );
        }
    }
}
