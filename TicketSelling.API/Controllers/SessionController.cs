using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using TicketSelling.API.Exceptions;
using TicketSelling.API.Models.CreateRequest;
using TicketSelling.API.Models.Request;
using TicketSelling.API.Models.Response;
using TicketSelling.Services.Contracts.ModelsRequest;
using TicketSelling.Services.Contracts.ServicesContracts;

namespace TicketSelling.API.Controllers
{
    /// <summary>
    /// CRUD контроллер по работе с Киносеанасами
    /// </summary>
    [ApiController]
    [Route("[Controller]")]
    [ApiExplorerSettings(GroupName = "Session")]
    public class SessionController : ControllerBase
    {
        private readonly ISessionService sessionService;
        private readonly IMapper mapper;

        public SessionController(ISessionService sessionService, IMapper mapper)
        {
            this.sessionService = sessionService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Получить список киносеансов
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(ICollection<SessionResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await sessionService.GetAllAsync(cancellationToken);
            var result2 = result.Select(x => mapper.Map<SessionResponse>(x));
            return Ok(result2);
        }

        /// <summary>
        /// Получить киносеанс по Id
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(SessionResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([Required] Guid id, CancellationToken cancellationToken)
        {
            var item = await sessionService.GetByIdAsync(id, cancellationToken);
            return Ok(mapper.Map<SessionResponse>(item));
        }

        /// <summary>
        /// Добавить киносеанс
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(SessionResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiValidationExceptionDetail), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Add(CreateSessionRequest request, CancellationToken cancellationToken)
        {
            var model = mapper.Map<SessionRequestModel>(request);
            var result = await sessionService.AddAsync(model, cancellationToken);
            return Ok(mapper.Map<SessionResponse>(result));
        }

        /// <summary>
        /// Изменить киносеанс по Id
        /// </summary>
        [HttpPut]
        [ProducesResponseType(typeof(SessionResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Edit(SessionRequest request, CancellationToken cancellationToken)
        {
            var model = mapper.Map<SessionRequestModel>(request);
            var result = await sessionService.EditAsync(model, cancellationToken);
            return Ok(mapper.Map<SessionResponse>(result));
        }

        /// <summary>
        /// Удалить киносеанс по Id
        /// </summary>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status417ExpectationFailed)]
        public async Task<IActionResult> Delete([Required] Guid id, CancellationToken cancellationToken)
        {
            await sessionService.DeleteAsync(id, cancellationToken);
            return Ok();
        }
    }
}
