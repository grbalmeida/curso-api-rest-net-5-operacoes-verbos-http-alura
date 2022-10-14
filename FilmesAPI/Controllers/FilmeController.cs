using FilmesAPI.Data;
using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilmeController : ControllerBase
    {
        private readonly FilmeContext _context;

        public FilmeController(FilmeContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> AdicionaFilme([FromBody] Filme filme)
        {
            _context.Filmes.Add(filme);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(RecuperaFilmePorId), new { id = filme.Id }, filme);
        }

        [HttpGet]
        public async Task<IActionResult> RecuperarFilmes()
        {
            return Ok(await _context.Filmes.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> RecuperaFilmePorId(int id)
        {
            var filme = await _context.Filmes.FirstOrDefaultAsync(filme => filme.Id == id);

            if (filme != null)
            {
                return Ok(filme);
            }

            return NotFound();
        }
    }
}
