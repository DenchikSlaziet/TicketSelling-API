using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TicketSelling.API.Models.Response;
using TicketSelling.Services.Contracts.ReadServices;

namespace TicketSelling.API.Controllers
{
    /// <summary>
    /// CRUD контроллер по работе с клиентами
    /// </summary>
    [ApiController]
    [Route("[Controller]")]
    [ApiExplorerSettings(GroupName = "Client")]
    public class ClientController : ControllerBase
    {
        private readonly IClientService clientService;
        private readonly IMapper mapper;

        public ClientController(IClientService clientService, IMapper mapper)
        {
            this.clientService = clientService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await clientService.GetAllAsync(cancellationToken);
            return Ok(result.Select(x => mapper.Map<ClientResponse>(x)));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var item = await clientService.GetByIdAsync(id, cancellationToken);

            if (item == null)
            {
                return NotFound("Клиента с таким Id нет!");
            }
            return Ok(mapper.Map<ClientResponse>(item));
        }
    }
}
