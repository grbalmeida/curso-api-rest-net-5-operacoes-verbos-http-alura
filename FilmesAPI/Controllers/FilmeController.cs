using FilmesAPI.Data;
using FilmesAPI.Data.Dtos;
using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
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
        public async Task<IActionResult> AdicionaFilme([FromBody] CreateFilmeDto filmeDto)
        {
            var filme = new Filme
            {
                Diretor = filmeDto.Diretor,
                Duracao = filmeDto.Duracao,
                Genero = filmeDto.Genero,
                Titulo = filmeDto.Titulo
            };

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
                var filmeDto = new ReadFilmeDto
                {
                    Id = filme.Id,
                    Titulo = filme.Titulo,
                    Diretor = filme.Diretor,
                    Duracao = filme.Duracao,
                    Genero = filme.Genero,
                    HoraDaConsulta = DateTime.Now
                };

                return Ok(filmeDto);
            }

            return NotFound();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizaFilme(int id, [FromBody] UpdateFilmeDto filmeDto)
        {
            var filme = await _context.Filmes.FirstOrDefaultAsync(filme => filme.Id == id);

            if (filme == null)
            {
                return NotFound();
            }

            filme.Titulo = filmeDto.Titulo;
            filme.Diretor = filmeDto.Diretor;
            filme.Genero = filmeDto.Genero;
            filme.Duracao = filmeDto.Duracao;

            _context.Filmes.Update(filme);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletaFilme(int id)
        {
            var filme = await _context.Filmes.FirstOrDefaultAsync(filme => filme.Id == id);

            if (filme == null)
            {
                return NotFound();
            }

            _context.Filmes.Remove(filme);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
