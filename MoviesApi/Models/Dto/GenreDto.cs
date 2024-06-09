using System.ComponentModel.DataAnnotations;

namespace MoviesApi.Models.Dto
{
    public class GenreDto
    {
        [Required]
        public string? Name { get; set; }
    }
}
