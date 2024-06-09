using System.ComponentModel.DataAnnotations;

namespace MoviesApi.Models
{
    public class Director : BaseEntity
    {
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }

        public virtual ICollection<Movie>? Movies { get; set; }
    }
}
