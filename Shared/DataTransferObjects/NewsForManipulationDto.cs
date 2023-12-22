using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public abstract record NewsForManipulationDto
    {
        [Required(ErrorMessage = "URL is a required field.")]
        [MaxLength(255, ErrorMessage = "Maximum length for the URL is 255 characters.")]
        public string URL { get; init; }

        [Required(ErrorMessage = "Title is a required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length for the Title is 30 characters.")]
        public string Title { get; init; }

        [MaxLength(30, ErrorMessage = "Maximum length for the Subtitle is 30 characters.")]
        public string? Subtitle { get; init; }

        public string? Text { get; init; }
        public string? TitleImagePath { get; init; }
    }
}
