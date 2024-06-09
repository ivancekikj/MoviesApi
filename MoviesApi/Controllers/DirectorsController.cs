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
    public class DirectorsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DirectorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /<DirectorsController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<Director> result = await _context.Directors.Include(d => d.Movies)
                .ToListAsync();
            return Ok(result);
        }

        // GET /<DirectorsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            Director? director = await _context.Directors
                .Include(d => d.Movies)
                .FirstOrDefaultAsync(p => p.Id.Equals(id));
            if (director == null)
                return NotFound($"Couldn't find resouce with id {id}");
            return Ok(director);
        }

        // POST /<DirectorsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromQuery] DirectorDto model)
        {
            Director director = new()
            {
                Id = Guid.NewGuid(),
                FirstName = model.FirstName,
                LastName = model.LastName,
            };
            director = _context.Directors.AddAsync(director).Result.Entity;
            await _context.SaveChangesAsync();
            return Ok(director);
        }

        // PUT /<DirectorsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromQuery] DirectorDto model)
        {
            Director? director = await _context.Directors.FindAsync(id);
            if (director == null)
                return NotFound($"Couldn't find resouce with id {id}");
            director.FirstName = model.FirstName;
            director.LastName = model.LastName;
            _context.Update(director);
            await _context.SaveChangesAsync();
            return Ok(director);
        }

        // DELETE /<DirectorsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            Director? director = await _context.Directors.FindAsync(id);
            if (director == null)
                return NotFound($"Couldn't find resouce with id {id}");
            _context.Directors.Remove(director);
            await _context.SaveChangesAsync();
            return Ok(director);
        }
    }
}
