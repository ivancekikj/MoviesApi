using Microsoft.AspNetCore.Mvc;
using MoviesApi.Data;
using MoviesApi.Models.Dto;
using MoviesApi.Models;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MoviesApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public GenresController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /<GenresController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<Genre> result = await _context.Genres.Include(g => g.Movies)
                .ToListAsync();
            return Ok(result);
        }

        // GET /<GenresController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            Genre? genre = await _context.Genres.Include(g => g.Movies)
                .FirstOrDefaultAsync(g => g.Id.Equals(id));
            if (genre == null)
                return NotFound($"Couldn't find resouce with id {id}");
            return Ok(genre);
        }

        // POST /<GenresController>
        [HttpPost]
        public async Task<IActionResult> Post([FromQuery] GenreDto model)
        {
            Genre genre = new()
            {
                Id = Guid.NewGuid(),
                Name = model.Name
            };
            genre = _context.Genres.AddAsync(genre).Result.Entity;
            await _context.SaveChangesAsync();
            return Ok(genre);
        }

        // PUT /<GenresController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromQuery] GenreDto model)
        {
            Genre? genre = await _context.Genres.FindAsync(id);
            if (genre == null)
                return NotFound($"Couldn't find resouce with id {id}");
            genre.Name = model.Name;
            _context.Update(genre);
            await _context.SaveChangesAsync();
            return Ok(genre);
        }

        // DELETE /<GenresController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            Genre? genre = await _context.Genres.FindAsync(id);
            if (genre == null)
                return NotFound($"Couldn't find resouce with id {id}");
            _context.Genres.Remove(genre);
            await _context.SaveChangesAsync();
            return Ok(genre);
        }
    }
}
