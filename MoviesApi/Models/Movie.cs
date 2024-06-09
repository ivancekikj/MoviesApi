using System.ComponentModel.DataAnnotations;

namespace MoviesApi.Models
{
    public class Movie : BaseEntity
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

        public virtual ICollection<Director>? Directors { get; set; }
        public virtual ICollection<Genre>? Genres { get; set; }
    }
}
