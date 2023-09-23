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
    public class FilmController : ControllerBase
    {
        private readonly IFilmService filmService;

        public FilmController(IFilmService filmService)
        {
            this.filmService = filmService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await filmService.GetAllAsync(cancellationToken);
            return Ok(result.Select(x => new FilmResponse
            {
                Id = x.Id,
                Description = x.Description,
                Limitation = x.Limitation,
                Title = x.Title,
            }));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var item = await filmService.GetByIdAsync(id, cancellationToken);

            if (item == null)
            {
                return NotFound("Фильма с таким Id нет!");
            }

            return Ok(new FilmResponse
            {
                Id = item.Id,
                Description = item.Description,
                Limitation = item.Limitation,
                Title= item.Title
            });
        }
    }
}
