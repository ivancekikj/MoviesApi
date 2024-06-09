using System.ComponentModel.DataAnnotations;

namespace MoviesApi.Models
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
    }
}
