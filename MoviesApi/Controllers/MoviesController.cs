using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesApi.Data;
using MoviesApi.Models;
using MoviesApi.Models.Dto;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

// Activate pipeline
namespace MoviesApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MoviesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /<MoviesController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<Movie> result = await _context.Movies.Include(m => m.Genres)
                .Include(m => m.Directors)
                .ToListAsync();
            return Ok(result);
        }

        // GET /<MoviesController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            Movie? movie = await _context.Movies.Include(m => m.Genres)
                .Include(m => m.Directors)
                .FirstOrDefaultAsync(m => m.Id.Equals(id));
            if (movie == null)
                return NotFound($"Couldn't find resouce with id {id}");
            return Ok(movie);
        }

        // POST /<MoviesController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] MovieDto model)
        {
            Movie movie = new()
            {
                Id = Guid.NewGuid(),
                Title = model.Title,
                Description = model.Description,
                ReleaseDate = model.ReleaseDate,
                DurationInMinutes = model.DurationInMinutes,
                PosterUrl = model.PosterUrl
            };
            movie = _context.Movies.AddAsync(movie).Result.Entity;
            await _context.SaveChangesAsync();
            return Ok(movie);
        }

        // PUT /<MoviesController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] MovieDto model)
        {
            Movie? movie = await _context.Movies.FindAsync(id);
            if (movie == null)
                return NotFound($"Couldn't find resouce with id {id}");
            movie.Title = model.Title;
            movie.Description = model.Description;
            movie.ReleaseDate = model.ReleaseDate;
            movie.DurationInMinutes = model.DurationInMinutes;
            movie.PosterUrl = model.PosterUrl;
            _context.Update(movie);
            await _context.SaveChangesAsync();
            return Ok(movie);
        }

        // DELETE /<MoviesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            Movie? movie = await _context.Movies.FindAsync(id);
            if (movie == null)
                return NotFound($"Couldn't find resouce with id {id}");
            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();
            return Ok(movie);
        }

        [HttpPost("{id}/Genres")]
        public async Task<IActionResult> AddGenre(Guid id, [FromBody] Guid genreId)
        {
            Movie? movie = await _context.Movies.Include(m => m.Genres)
                .Include(m => m.Directors)
                .FirstOrDefaultAsync(m => m.Id.Equals(id));
            if (movie == null)
                return NotFound($"Couldn't find resouce with id {id}");
            Genre genre = await _context.Genres.FindAsync(genreId);
            if (genre == null)
                return NotFound($"Couldn't find resouce with id {genreId}");
            movie.Genres.Add(genre);
            await _context.SaveChangesAsync();
            return Ok(movie);
        }

        [HttpDelete("{id}/Genres/{genreId}")]
        public async Task<IActionResult> RemoveGenre(Guid id, Guid genreId)
        {
            Movie? movie = await _context.Movies.Include(m => m.Genres)
                .Include(m => m.Directors)
                .FirstOrDefaultAsync(m => m.Id.Equals(id));
            if (movie == null)
                return NotFound($"Couldn't find resouce with id {id}");
            Genre genre = movie.Genres.FirstOrDefault(g => g.Id.Equals(genreId));
            if (genre == null)
                return NotFound($"Couldn't find resouce with id {genreId}");
            movie.Genres.Remove(genre);
            await _context.SaveChangesAsync();
            return Ok(movie);
        }


        [HttpPost("{id}/Directors")]
        public async Task<IActionResult> AddDirector(Guid id, [FromBody] Guid directorId)
        {
            Movie? movie = await _context.Movies.Include(m => m.Genres)
                .Include(m => m.Directors)
                .FirstOrDefaultAsync(m => m.Id.Equals(id));
            if (movie == null)
                return NotFound($"Couldn't find resouce with id {id}");
            Director director = await _context.Directors.FindAsync(directorId);
            if (director == null)
                return NotFound($"Couldn't find resouce with id {directorId}");
            movie.Directors.Add(director);
            await _context.SaveChangesAsync();
            return Ok(movie);
        }

        [HttpDelete("{id}/Directors/{directorId}")]
        public async Task<IActionResult> RemoveDirector(Guid id, Guid directorId)
        {
            Movie? movie = await _context.Movies.Include(m => m.Genres)
                .Include(m => m.Directors)
                .FirstOrDefaultAsync(m => m.Id.Equals(id));
            if (movie == null)
                return NotFound($"Couldn't find resouce with id {id}");
            Director director = movie.Directors.FirstOrDefault(d => d.Id.Equals(directorId));
            if (director == null)
                return NotFound($"Couldn't find resouce with id {directorId}");
            movie.Directors.Remove(director);
            await _context.SaveChangesAsync();
            return Ok(movie);
        }
    }
}
