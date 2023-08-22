using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects
{
    public abstract record PlanetForManipulationDto
    {
        [Required(ErrorMessage = "Planet name is a required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length for the Name is 30 characters.")]
        public string? Name { get; init; }

        [Required(ErrorMessage = "DistanceFromTheSun is a required field.")]
        [Range(58, double.MaxValue, ErrorMessage = "DistanceFromTheSun is required ant it can't be lower " +
            "than 58 million kilometers (distance from the sun to the first planet).")]
        public double DistanceFromTheSun { get; init; }

        [Required(ErrorMessage = "PlanetInfo is a required field.")]
        [MaxLength(500, ErrorMessage = "Maximum lenght for the PlanetInfo is 500 characters.")]
        public string? PlanetInfo { get; init; }

        public IEnumerable<SatelliteForCreationDto>? Satellites { get; init; }
    }
}
