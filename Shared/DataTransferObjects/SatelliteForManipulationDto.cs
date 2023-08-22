using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects
{
    public abstract record SatelliteForManipulationDto
    {
        [Required(ErrorMessage = "Satellite name is a required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length for the Name is 30 characters.")]
        public string? Name { get; init; }

        [Required(ErrorMessage = "DistanceFromThePlanet is a required field.")]
        [Range(0, double.MaxValue, ErrorMessage = "DistanceFromThePlanet is required and it can't be lower than zero.")]
        public double DistanceFromThePlanet { get; init; }

        [Required(ErrorMessage = "SatelliteInfo is a required field.")]
        [MaxLength(500, ErrorMessage = "Maximum lenght for the SatelliteInfo is 500 characters.")]
        public string? SatelliteInfo { get; init; }
    }
}
