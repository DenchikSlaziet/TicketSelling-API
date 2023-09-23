using Microsoft.AspNetCore.Mvc;
using TicketSelling.API.Models;
using TicketSelling.Services.Contracts.ReadServices;
using TicketSelling.Services.ReadServices;

namespace TicketSelling.API.Controllers
{
    /// <summary>
    /// CRUD контроллер по работе с клиентами
    /// </summary>
    [ApiController]
    [Route("[Controller]")]
    public class ClientController : ControllerBase
    {
        private readonly IClientService clientService;

        public ClientController(IClientService clientService)
        {
            this.clientService = clientService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await clientService.GetAllAsync(cancellationToken);
            return Ok(result.Select(x => new ClientResponse
            {
                Id = x.Id,
                FirstName = x.FirstName,
                Age = x.Age,
                LastName = x.LastName,
                Patronymic = x.Patronymic
            }));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var item = await clientService.GetByIdAsync(id, cancellationToken);

            if (item == null)
            {
                return NotFound("Клиента с таким Id нет!");
            }

            return Ok(new ClientResponse
            {
                Id = item.Id,
                FirstName= item.FirstName,
                Age = item.Age,
                LastName = item.LastName,
                Patronymic= item.Patronymic
            });
        }
    }
}
