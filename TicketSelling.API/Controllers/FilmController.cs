using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TicketSelling.API.Models;
using TicketSelling.Services.Contracts.ReadServices;
using TicketSelling.Services.ReadServices;

namespace TicketSelling.API.Controllers
{
    /// <summary>
    /// CRUD контроллер по работе с фильмами
    /// </summary>
    [ApiController]
    [Route("[Controller]")]
    [ApiExplorerSettings(GroupName = "Film")]
    public class FilmController : ControllerBase
    {
        private readonly IFilmService filmService;
        private readonly IMapper mapper;

        public FilmController(IFilmService filmService, IMapper mapper)
        {
            this.filmService = filmService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await filmService.GetAllAsync(cancellationToken);
            return Ok(result.Select(x => mapper.Map<FilmResponse>(x)));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var item = await filmService.GetByIdAsync(id, cancellationToken);

            if (item == null)
            {
                return NotFound("Фильма с таким Id нет!");
            }
            return Ok(mapper.Map<FilmResponse>(item));
        }
    }
}
