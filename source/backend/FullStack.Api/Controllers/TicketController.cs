using System;
using AutoMapper;
using NSwag.Annotations;
using System.Threading.Tasks;
using FullStack.Api.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using FullStack.Core.Extensions;
using FullStack.Domain.Entities;
using Microsoft.Extensions.Logging;
using FullStack.Api.Infrastructure;
using FullStack.Api.Contracts.Schemas;
using FullStack.Api.Contracts.Requests;
using FullStack.Api.Contracts.Responses;
using FullStack.Domain.Contracts.Responses;
using FullStack.Domain.Interfaces.Business;
using FullStack.Domain.Interfaces.Business.Services;

namespace FullStack.Api.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/tickets")]
    [Consumes("application/json"), Produces("application/json")]
    [OpenApiTag("Ticket", Description = "Operações relacionadas as passagens aéreas.")]
    public sealed class TicketController : BaseController
    {
        #region Private Read-Only Fields

        private readonly ITicketService _ticketService;

        #endregion

        #region Constructors

        public TicketController(ILogger<TicketController> logger, IMapper mapper, IUnitOfWork unitOfWork
            , ITicketService ticketService
        )
            : base(logger, mapper, unitOfWork)
        {
            _ticketService = ticketService ?? throw new ArgumentNullException(nameof(ticketService));
        }

        #endregion

        #region Controller Actions

        [HttpGet]
        [ProducesResponseType(typeof(TicketListResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> List([FromQuery]TicketListRequest request)
        {
            var result = await _ticketService.PagedList(request.PageIndex, request.PageSize);

            return Ok(new TicketListResponse(result.PageIndex, result.PageSize, result.Total)
            {
                Data = Mapper.ConvertEntityToSchema<TicketSchema>(result.Collection)
            });
        }

        [HttpGet("{ticketId:int}")]
        [ProducesResponseType(typeof(TicketGetResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get([FromRoute]int ticketId)
        {
            var result = await _ticketService.Get(ticketId);
            if (result == null)
                return NotFound(ErrorResponse.DefaultNotFoundResponse());

            return Ok(new TicketGetResponse
            {
                Data = Mapper.ConvertEntityToSchema<TicketSchema>(result)
            });
        }

        [HttpPost]
        [ProducesResponseType(typeof(TicketPostResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]        
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post(ApiVersion apiVersion, [FromBody]TicketPostRequest request)
        {
            if (ModelState.IsValid)
            {
                var entity = Mapper.ConvertRequestToEntity<Ticket>(request);

                await _ticketService.Create(entity);
                await UnitOfWork.CompleteAsync();

                return CreatedAtAction(nameof(Get), new { version = apiVersion.ToString(), ticketId = entity.Id }, new TicketPostResponse
                {
                    Data = Mapper.ConvertEntityToSchema<TicketSchema>(entity)
                });
            }

            return BadRequest(ModelState.ToErrorResponse());
        }

        [HttpPut("{ticketId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromRoute]int ticketId, [FromBody]TicketPutRequest request)
        {
            if (ModelState.IsValid)
            {
                var result = await _ticketService.Get(ticketId);
                if (result == null)
                    return NotFound(ErrorResponse.DefaultNotFoundResponse());

                var entity = Mapper.ConvertRequestToEntity<Ticket>(request);
                entity.Id = ticketId;

                await _ticketService.Update(entity);
                await UnitOfWork.CompleteAsync();

                return NoContent();
            }

            return BadRequest(ModelState.ToErrorResponse());
        }

        #endregion
    }
}