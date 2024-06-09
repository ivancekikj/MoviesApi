using System.ComponentModel.DataAnnotations;

namespace MoviesApi.Models
{
    public class Genre : BaseEntity
    {
        [Required]
        public string? Name { get; set; }

        public virtual ICollection<Movie>? Movies { get; set; }
    }
}
