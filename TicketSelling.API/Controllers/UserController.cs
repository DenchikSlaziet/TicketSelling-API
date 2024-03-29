﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using TicketSelling.API.Exceptions;
using TicketSelling.API.Models.CreateRequest;
using TicketSelling.API.Models.Response;
using TicketSelling.Services.Contracts.Models;
using TicketSelling.Services.Contracts.ServicesContracts;

namespace TicketSelling.API.Controllers
{
    /// <summary>
    /// CRUD контроллер по работе с клиентами
    /// </summary>
    [ApiController]
    [Route("[Controller]")]
    [ApiExplorerSettings(GroupName = "User")]
    public class UserController : ControllerBase
    {
        private readonly IUserService clientService;
        private readonly IMapper mapper;

        public UserController(IUserService clientService, IMapper mapper)
        {
            this.clientService = clientService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Получить список клиентов
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(ICollection<UserResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await clientService.GetAllAsync(cancellationToken);
            return Ok(result.Select(x => mapper.Map<UserResponse>(x)));
        }

        /// <summary>
        /// Получить клиента по Id
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([Required] Guid id, CancellationToken cancellationToken)
        {
            var item = await clientService.GetByIdAsync(id, cancellationToken);
            return Ok(mapper.Map<UserResponse>(item));
        }

        /// <summary>
        /// Добавить клиента
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiValidationExceptionDetail), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Add(CreateUserRequest model, CancellationToken cancellationToken)
        {
            var clientModel = mapper.Map<UserModel>(model);
            var result = await clientService.AddAsync(clientModel, cancellationToken);
            return Ok(mapper.Map<UserResponse>(result));
        }

        /// <summary>
        /// Изменить клиента по Id
        /// </summary>
        [HttpPut]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiValidationExceptionDetail), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Edit(UserRequest request, CancellationToken cancellationToken)
        {
            var model = mapper.Map<UserModel>(request);
            var result = await clientService.EditAsync(model, cancellationToken);
            return Ok(mapper.Map<UserResponse>(result));
        }

        /// <summary>
        /// Удалить клиента по Id
        /// </summary>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status417ExpectationFailed)]
        public async Task<IActionResult> Delete([Required] Guid id, CancellationToken cancellationToken)
        {
            await clientService.DeleteAsync(id, cancellationToken);
            return Ok();
        }
    }
}
