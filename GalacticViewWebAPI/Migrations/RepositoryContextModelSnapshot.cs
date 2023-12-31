﻿// <auto-generated />
using System;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GalacticViewWebAPI.Migrations
{
    [DbContext(typeof(RepositoryContext))]
    partial class RepositoryContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Entities.Models.Planet", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("PlanetId");

                    b.Property<double>("DistanceFromTheSun")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("PlanetInfo")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Planets");

                    b.HasData(
                        new
                        {
                            Id = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                            DistanceFromTheSun = 1430.0,
                            Name = "Сатурн",
                            PlanetInfo = "Сатурн – вторая по размеру планета в Солнечной системе. Средний радиус Сатурна составляет 58 232 ± 6 километров, то есть около 9 радиусов Земли. Площадь поверхности Сатурна составляет 42,72 миллиарда квадратных километров. Средняя плотность Сатурна составляет 0,687 грамм на кубический сантиметр."
                        },
                        new
                        {
                            Id = new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"),
                            DistanceFromTheSun = 778.57000000000005,
                            Name = "Юпитер",
                            PlanetInfo = "Юпитер — самая большая планета Солнечной системы, газовый гигант. Его экваториальный радиус равен 71,4 тыс. км, что в 11,2 раза превышает радиус Земли . Юпитер — единственная планета, у которой центр масс с Солнцем находится вне Солнца и отстоит от него примерно на 7 % солнечного радиуса."
                        });
                });

            modelBuilder.Entity("Entities.Models.Satellite", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("SatelliteId");

                    b.Property<double>("DistanceFromThePlanet")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<Guid>("PlanetId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("SatelliteInfo")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("PlanetId");

                    b.ToTable("Satellites");

                    b.HasData(
                        new
                        {
                            Id = new Guid("80abbca8-664d-4b20-b5de-024705497d4a"),
                            DistanceFromThePlanet = 1.2,
                            Name = "Титан",
                            PlanetId = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                            SatelliteInfo = "Титан - крупнейший спутник Сатурна, второй по величине спутник в Солнечной системе (после спутника Юпитера Ганимеда), является единственным, кроме Земли, телом в Солнечной системе, для которого доказано стабильное существование жидкости на поверхности, и единственным спутником планеты, обладающим плотной атмосферой."
                        },
                        new
                        {
                            Id = new Guid("86dba8c0-d178-41e7-938c-ed49778fb52a"),
                            DistanceFromThePlanet = 1.0700000000000001,
                            Name = "Ганимед",
                            PlanetId = new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"),
                            SatelliteInfo = "Ганимед - самый большой спутник Юпитера и всей солнечной системы, имеющий размер планеты. Его диаметр составляет 5268 км. Он получил свое название по имени сына троянского царя и нимфы Каллирои."
                        },
                        new
                        {
                            Id = new Guid("021ca3c1-0deb-4afd-ae94-2159a8479811"),
                            DistanceFromThePlanet = 0.34999999999999998,
                            Name = "Ио",
                            PlanetId = new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3")
                        });
                });

            modelBuilder.Entity("Entities.Models.Satellite", b =>
                {
                    b.HasOne("Entities.Models.Planet", "Planet")
                        .WithMany("Satellites")
                        .HasForeignKey("PlanetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Planet");
                });

            modelBuilder.Entity("Entities.Models.Planet", b =>
                {
                    b.Navigation("Satellites");
                });
#pragma warning restore 612, 618
        }
    }
}
