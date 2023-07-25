using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Configuration
{
    public class PlanetConfiguration : IEntityTypeConfiguration<Planet>
    {
        public void Configure(EntityTypeBuilder<Planet> builder)
        {
            builder.HasData
            (
                new Planet
                {
                    Id = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                    Name = "Сатурн",
                    DistanceFromTheSun = 1430,
                    PlanetInfo = "Сатурн – вторая по размеру планета в Солнечной системе. Средний радиус Сатурна составляет 58 232 ± 6 километров, " +
                    "то есть около 9 радиусов Земли. Площадь поверхности Сатурна составляет 42,72 миллиарда квадратных километров. Средняя плотность " +
                    "Сатурна составляет 0,687 грамм на кубический сантиметр."
                },
                new Planet
                {
                    Id = new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"),
                    Name = "Юпитер", 
                    DistanceFromTheSun = 778.57,
                    PlanetInfo = "Юпитер — самая большая планета Солнечной системы, газовый гигант. Его экваториальный радиус равен 71,4 тыс. км, что в 11,2 " +
                    "раза превышает радиус Земли . Юпитер — единственная планета, у которой центр масс с Солнцем находится вне Солнца и отстоит от него примерно " +
                    "на 7 % солнечного радиуса."
                }
            );
        }
    }
}
