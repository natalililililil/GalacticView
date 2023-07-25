using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Planet
    {
        [Column("PlanetId")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Planet name is a required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length for the Name is 30 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Distance from the sun is a required field.")]
        public double DistanceFromTheSun { get; set; }

        public string? PlanetInfo { get; set; }

        public ICollection<Satellite> Satellites { get; set; }
    }
}
