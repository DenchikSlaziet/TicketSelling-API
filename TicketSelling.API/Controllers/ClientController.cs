using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TicketSelling.API.Models.CreateRequest;
using TicketSelling.API.Models.Response;
using TicketSelling.Services.Contracts.Models;
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

        /// <summary>
        /// Получить список клиентов
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(ICollection<ClientResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await clientService.GetAllAsync(cancellationToken);
            return Ok(result.Select(x => mapper.Map<ClientResponse>(x)));
        }

        /// <summary>
        /// Получить клиента по Id
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(ClientResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var item = await clientService.GetByIdAsync(id, cancellationToken);

            if (item == null)
            {
                return NotFound("Клиента с таким Id нет!");
            }
            return Ok(mapper.Map<ClientResponse>(item));
        }

        /// <summary>
        /// Добавить клиента
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(ClientResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Add(CreateClientRequest model, CancellationToken cancellationToken)
        {
            var result = await clientService.AddAsync(model.FirstName, model.LastName, model.Patronymic, model.Age, model.Email, cancellationToken);
            return Ok(mapper.Map<ClientResponse>(result));
        }

        /// <summary>
        /// Изменить клиента по Id
        /// </summary>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(ClientResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Edit(Guid id, CreateClientRequest request, CancellationToken cancellationToken)
        {
            var model = mapper.Map<ClientModel>(request);
            model.Id = id;
            var result = await clientService.EditAsync(model, cancellationToken);
            return Ok(mapper.Map<ClientResponse>(result));
        }

        /// <summary>
        /// Удалить клиента по Id
        /// </summary>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await clientService.DeleteAsync(id, cancellationToken);
            return Ok();
        }
    }
}
