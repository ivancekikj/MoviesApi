using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using MoviesApi.Models;

namespace MoviesApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Director> Directors { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            try
            {
                var creator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
                if (creator != null)
                {
                    if (!creator.CanConnect())
                        creator.Create();
                    if (!creator.HasTables())
                        creator.CreateTables();
                }
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
