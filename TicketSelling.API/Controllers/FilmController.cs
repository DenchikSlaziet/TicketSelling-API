using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TicketSelling.API.Models.CreateRequest;
using TicketSelling.API.Models.Request;
using TicketSelling.API.Models.Response;
using TicketSelling.Services.Contracts.Models;
using TicketSelling.Services.Contracts.ReadServices;

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
        [ProducesResponseType(typeof(ICollection<FilmResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await filmService.GetAllAsync(cancellationToken);
            return Ok(result.Select(x => mapper.Map<FilmResponse>(x)));
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(FilmResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var item = await filmService.GetByIdAsync(id, cancellationToken);

            if (item == null)
            {
                return NotFound("Фильма с таким Id нет!");
            }
            return Ok(mapper.Map<FilmResponse>(item));
        }

        [HttpPost]
        [ProducesResponseType(typeof(FilmResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Add(CreateFilmRequest model, CancellationToken cancellationToken)
        {
            var result = await filmService.AddAsync(model.Title, model.Limitation, model.Description, cancellationToken);
            return Ok(mapper.Map<FilmResponse>(result));
        }

        [HttpPut]
        [ProducesResponseType(typeof(FilmResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Edit(FilmRequest request, CancellationToken cancellationToken)
        {
            var model = mapper.Map<FilmModel>(request);
            var result = await filmService.EditAsync(model, cancellationToken);
            return Ok(mapper.Map<FilmResponse>(result));
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await filmService.DeleteAsync(id, cancellationToken);
            return Ok();
        }
    }
}
