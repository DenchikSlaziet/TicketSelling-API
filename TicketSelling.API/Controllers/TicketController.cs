using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TicketSelling.API.Models.CreateRequest;
using TicketSelling.API.Models.Response;
using TicketSelling.Services.Contracts.Models;
using TicketSelling.Services.Contracts.ReadServices;

namespace TicketSelling.API.Controllers
{
    /// <summary>
    /// CRUD контроллер по работе с билетами
    /// </summary>
    [ApiController]
    [Route("[Controller]")]
    [ApiExplorerSettings(GroupName = "Ticket")]
    public class TicketController : ControllerBase
    {
        private readonly ICinemaService cinemaService;
        private readonly IClientService clientService;
        private readonly IFilmService filmService;
        private readonly IStaffService staffService;
        private readonly IHallService hallService;
        private readonly ITicketService ticketService;
        private readonly IMapper mapper;

        public TicketController(ICinemaService cinemaService, IClientService clientService, IFilmService filmService, 
            ITicketService ticketService, IStaffService staffService, IHallService hallService, IMapper mapper)
        {
            this.cinemaService = cinemaService;
            this.clientService = clientService;
            this.filmService = filmService;
            this.staffService = staffService;
            this.hallService = hallService;
            this.ticketService = ticketService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Получить список билетов
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(ICollection<TicketResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await ticketService.GetAllAsync(cancellationToken);
            var result2 = result.Select(x => mapper.Map<TicketResponse>(x));
            return Ok(result2);
        }

        /// <summary>
        /// Получить билет по Id
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(TicketResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var item = await ticketService.GetByIdAsync(id, cancellationToken);

            if (item == null)
            {
                return NotFound("Персонала с таким Id нет!");
            }

            return Ok(mapper.Map<TicketResponse>(item));
        }

        /// <summary>
        /// Добавить билет
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(TicketResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Add(CreateTicketRequest model, CancellationToken cancellationToken)
        {
            var result = await ticketService.AddAsync(model.HallId, model.FilmId, model.CinemaId, model.ClientId, 
                model.StaffId, model.Row, model.Place, model.Price, model.Date, cancellationToken);       
            return Ok(mapper.Map<TicketResponse>(result));
        }

        /// <summary>
        /// Изменить билет по Id
        /// </summary>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(TicketResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Edit(Guid id, CreateTicketRequest request, CancellationToken cancellationToken)
        {
            var model = mapper.Map<TicketModel>(request);

            model.Id = id;
            model.Hall = await hallService.GetByIdAsync(request.HallId, cancellationToken);
            model.Cinema = await cinemaService.GetByIdAsync(request.CinemaId, cancellationToken);
            model.Film = await filmService.GetByIdAsync(request.FilmId, cancellationToken);
            model.Client = await clientService.GetByIdAsync(request.ClientId, cancellationToken);
            model.Staff = request.StaffId.HasValue ? await staffService.GetByIdAsync(request.StaffId.Value, cancellationToken)
                : null;

            var result = await ticketService.EditAsync(model, cancellationToken);
            return Ok(mapper.Map<TicketResponse>(result));
        }

        /// <summary>
        /// Удалить билет по Id
        /// </summary>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await ticketService.DeleteAsync(id, cancellationToken);
            return Ok();
        }
    }
}
