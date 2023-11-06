using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await cinemaService.GetAllAsync(cancellationToken);
            return Ok(result.Select(x => mapper.Map<CinemaResponse>(x)));
        }

        [HttpGet("{id:guid}")]
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
        public async Task<IActionResult> Add(CinemaResponse model, CancellationToken cancellationToken)
        {
            var result = await cinemaService.AddAsync(model.Address, model.Title, cancellationToken);
            return Ok(mapper.Map<CinemaResponse>(result));
        }
    }
}
