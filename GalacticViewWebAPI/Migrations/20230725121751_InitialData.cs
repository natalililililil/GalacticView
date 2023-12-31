﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GalacticViewWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Planets",
                columns: new[] { "PlanetId", "DistanceFromTheSun", "Name", "PlanetInfo" },
                values: new object[,]
                {
                    { new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"), 778.57000000000005, "Юпитер", "Юпитер — самая большая планета Солнечной системы, газовый гигант. Его экваториальный радиус равен 71,4 тыс. км, что в 11,2 раза превышает радиус Земли . Юпитер — единственная планета, у которой центр масс с Солнцем находится вне Солнца и отстоит от него примерно на 7 % солнечного радиуса." },
                    { new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), 1430.0, "Сатурн", "Сатурн – вторая по размеру планета в Солнечной системе. Средний радиус Сатурна составляет 58 232 ± 6 километров, то есть около 9 радиусов Земли. Площадь поверхности Сатурна составляет 42,72 миллиарда квадратных километров. Средняя плотность Сатурна составляет 0,687 грамм на кубический сантиметр." }
                });

            migrationBuilder.InsertData(
                table: "Satellites",
                columns: new[] { "SatelliteId", "DistanceFromThePlanet", "Name", "PlanetId", "SatelliteInfo" },
                values: new object[,]
                {
                    { new Guid("021ca3c1-0deb-4afd-ae94-2159a8479811"), 0.34999999999999998, "Ио", new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"), null },
                    { new Guid("80abbca8-664d-4b20-b5de-024705497d4a"), 1.2, "Титан", new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), "Титан - крупнейший спутник Сатурна, второй по величине спутник в Солнечной системе (после спутника Юпитера Ганимеда), является единственным, кроме Земли, телом в Солнечной системе, для которого доказано стабильное существование жидкости на поверхности, и единственным спутником планеты, обладающим плотной атмосферой." },
                    { new Guid("86dba8c0-d178-41e7-938c-ed49778fb52a"), 1.0700000000000001, "Ганимед", new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"), "Ганимед - самый большой спутник Юпитера и всей солнечной системы, имеющий размер планеты. Его диаметр составляет 5268 км. Он получил свое название по имени сына троянского царя и нимфы Каллирои." }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Satellites",
                keyColumn: "SatelliteId",
                keyValue: new Guid("021ca3c1-0deb-4afd-ae94-2159a8479811"));

            migrationBuilder.DeleteData(
                table: "Satellites",
                keyColumn: "SatelliteId",
                keyValue: new Guid("80abbca8-664d-4b20-b5de-024705497d4a"));

            migrationBuilder.DeleteData(
                table: "Satellites",
                keyColumn: "SatelliteId",
                keyValue: new Guid("86dba8c0-d178-41e7-938c-ed49778fb52a"));

            migrationBuilder.DeleteData(
                table: "Planets",
                keyColumn: "PlanetId",
                keyValue: new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"));

            migrationBuilder.DeleteData(
                table: "Planets",
                keyColumn: "PlanetId",
                keyValue: new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"));
        }
    }
}
