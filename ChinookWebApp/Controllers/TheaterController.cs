using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ChinookWebApp.DAL;
using ChinookWebApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChinookWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TheaterController : ControllerBase
    {
        private readonly MovieBookingDbContext _context;

        public TheaterController(MovieBookingDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Theater>>> GetTheaters()
        {
            return await _context.Theaters.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Theater>> GetTheater(int id)
        {
            var theater = await _context.Theaters.FindAsync(id);
            if (theater == null) return NotFound();
            return theater;
        }

        [HttpPost]
        public async Task<ActionResult<Theater>> PostTheater(Theater theater)
        {
            _context.Theaters.Add(theater);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTheater), new { id = theater.TheaterID }, theater);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTheater(int id)
        {
            var theater = await _context.Theaters.FindAsync(id);
            if (theater == null) return NotFound();
            _context.Theaters.Remove(theater);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}

