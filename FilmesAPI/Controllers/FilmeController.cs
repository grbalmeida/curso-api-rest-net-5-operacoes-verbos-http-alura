using AutoMapper;
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
        private readonly IMapper _mapper;

        public FilmeController(FilmeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> AdicionaFilme([FromBody] CreateFilmeDto filmeDto)
        {
            var filme = _mapper.Map<Filme>(filmeDto);

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
                var filmeDto = _mapper.Map<ReadFilmeDto>(filme);
                filmeDto.HoraDaConsulta = DateTime.Now;

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

            _mapper.Map(filmeDto, filme);
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
