using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class News
    {
        [Column("NewsId")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "URL is a required field.")]
        [MaxLength(255, ErrorMessage = "Maximum length for the URL is 255 characters.")]
        public string URL { get; set; }

        [Required(ErrorMessage = "Title is a required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length for the Title is 30 characters.")]
        public string Title { get; set; }

        [MaxLength(30, ErrorMessage = "Maximum length for the Subtitle is 30 characters.")]
        public string? Subtitle { get; set; }
        public string? Text { get; set; }
        public string TitleImagePath {  get; set; } 
    }
}
