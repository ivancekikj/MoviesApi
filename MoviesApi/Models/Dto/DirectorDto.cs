using System.ComponentModel.DataAnnotations;

namespace MoviesApi.Models.Dto
{
    public class DirectorDto
    {
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
    }
}
