using System.ComponentModel.DataAnnotations;

namespace MoviesApi.Models.Dto
{
    public class MovieDto
    {
        [Required]
        public string? Title { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime? ReleaseDate { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int DurationInMinutes { get; set; }
        [Required]
        public string? PosterUrl { get; set; }
        [Required]
        public string? Description { get; set; }
    }
}
