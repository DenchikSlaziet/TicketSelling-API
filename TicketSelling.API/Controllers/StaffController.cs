﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TicketSelling.API.Models.CreateRequest;
using TicketSelling.API.Models.Response;
using TicketSelling.Services.Contracts.Models;
using TicketSelling.Services.Contracts.ReadServices;

namespace TicketSelling.API.Controllers
{
    /// <summary>
    /// CRUD контроллер по работе с персоналом
    /// </summary>
    [ApiController]
    [Route("[Controller]")]
    [ApiExplorerSettings(GroupName = "Staff")]
    public class StaffController : ControllerBase
    {
        private readonly IStaffService staffService;
        private readonly IMapper mapper;

        public StaffController(IStaffService staffService, IMapper mapper)
        {
            this.staffService = staffService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Получить список сотрудников
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(ICollection<StaffResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await staffService.GetAllAsync(cancellationToken);
            return Ok(result.Select(x => mapper.Map<StaffResponse>(x)));
        }

        /// <summary>
        /// Получить сотрудника по Id
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(StaffResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var item = await staffService.GetByIdAsync(id, cancellationToken);

            if (item == null)
            {
                return NotFound("Персонала с таким Id нет!");
            }
            return Ok(mapper.Map<StaffResponse>(item));
        }

        /// <summary>
        /// Добавить сотрудника
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(StaffResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Add(CreateStaffRequest model, CancellationToken cancellationToken)
        {
            var result = await staffService.AddAsync(model.FirstName, model.LastName, model.Patronymic, model.Age, model.Post, cancellationToken);
            return Ok(mapper.Map<StaffResponse>(result));
        }

        /// <summary>
        /// Изменить cотрудника по Id
        /// </summary>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(StaffResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Edit(Guid id, CreateStaffRequest request, CancellationToken cancellationToken)
        {
            var model = mapper.Map<StaffModel>(request);
            model.Id = id;
            var result = await staffService.EditAsync(model, cancellationToken);
            return Ok(mapper.Map<StaffResponse>(result));
        }

        /// <summary>
        /// Удалить сотрудника по Id
        /// </summary>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await staffService.DeleteAsync(id, cancellationToken);
            return Ok();
        }
    }
}
