using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Satellite
    {
        [Column("SatelliteId")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Satellite name is a required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length for the Name is 30 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Distance from the planet is a required field.")]
        public int DistanceFromThePlanet { get; set; }

        public string? SatelliteInfo { get; set; }

        [ForeignKey(nameof(Planet))]
        public Guid PlanetId { get; set; }
        public Planet Planet { get; set; }
    }
}
