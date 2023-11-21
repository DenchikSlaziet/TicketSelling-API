using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TicketSelling.API.Models.CreateRequest;
using TicketSelling.API.Models.Response;
using TicketSelling.Services.Contracts.ModelsRequest;
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
        private readonly ITicketService ticketService;
        private readonly IMapper mapper;

        public TicketController(ITicketService ticketService, IMapper mapper)
        {           
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
        public async Task<IActionResult> Add(CreateTicketRequest request, CancellationToken cancellationToken)
        {
            var model = mapper.Map<TicketRequestModel>(request);        
            var result = await ticketService.AddAsync(model, cancellationToken);  
            
            return Ok(mapper.Map<TicketResponse>(result));
        }

        /// <summary>
        /// Изменить билет по Id
        /// </summary>
        [HttpPut]
        [ProducesResponseType(typeof(TicketResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Edit(TicketRequest request, CancellationToken cancellationToken)
        {
            var model = mapper.Map<TicketRequestModel>(request);
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
