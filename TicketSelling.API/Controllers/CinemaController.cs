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
    /// CRUD контроллер по работе с кинотеатрами
    /// </summary>
    [ApiController]
    [Route("[Controller]")]
    [ApiExplorerSettings(GroupName = "Cinema")]
    public class CinemaController : ControllerBase
    {
        private readonly ICinemaService cinemaService;
        private readonly IMapper mapper;

        public CinemaController(ICinemaService cinemaService, IMapper mapper)
        {
            this.cinemaService = cinemaService;
            this.mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CinemaResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await cinemaService.GetAllAsync(cancellationToken);
            return Ok(result.Select(x => mapper.Map<CinemaResponse>(x)));
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(CinemaResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(Guid id,CancellationToken cancellationToken)
        {
            var item = await cinemaService.GetByIdAsync(id,cancellationToken);

            if(item == null)
            {
                return NotFound("Кинотеатра с таким Id нет!");
            }
            return Ok(mapper.Map<CinemaResponse>(item));
        }

        [HttpPost]
        [ProducesResponseType(typeof(CinemaResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Add(CreateCinemaRequest model, CancellationToken cancellationToken)
        {
            var result = await cinemaService.AddAsync(model.Address, model.Title, cancellationToken);
            return Ok(mapper.Map<CinemaResponse>(result));
        }

        [HttpPut]
        [ProducesResponseType(typeof(CinemaResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Edit(CinemaRequest request, CancellationToken cancellationToken)
        {
            var model = mapper.Map<CinemaModel>(request);
            var result = await cinemaService.EditAsync(model, cancellationToken);
            return Ok(mapper.Map<CinemaResponse>(result));
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await cinemaService.DeleteAsync(id, cancellationToken);
            return Ok();
        }
    }
}
