using System;
using AutoMapper;
using System.Linq;
using NSwag.Annotations;
using System.Threading.Tasks;
using FullStack.Core.Helpers;
using FullStack.Api.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using FullStack.Core.Extensions;
using FullStack.Domain.Entities;
using Microsoft.Extensions.Logging;
using FullStack.Api.Infrastructure;
using Microsoft.EntityFrameworkCore;
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
            var predicate = PredicateBuilder.True<Ticket>();

            if (request.AirlineId.HasValue)
                predicate = predicate.And(x => x.AirlineId == request.AirlineId.Value);

            if (request.OriginId.HasValue)
                predicate = predicate.And(x => x.OriginId == request.OriginId.Value);

            if (request.DestinationId.HasValue)
                predicate = predicate.And(x => x.DestinationId == request.DestinationId.Value);

            if (request.Scheduled.HasValue)
                predicate = predicate.And(x => x.Scheduled.Date == request.Scheduled.Value.Date);

            var result = await _ticketService.PagedList(request.PageIndex, request.PageSize
                , predicate
                , orderBy: x => x
                    .OrderBy(p => p.Scheduled)
                , include: x => x
                    .Include(p => p.Airline)
                    .Include(p => p.Origin)
                    .Include(p => p.Destination)
            );

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
                entity.CreatedBy = User.GetUserId();

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
        public async Task<IActionResult> Put([FromRoute]int ticketId, [FromBody]TicketPutRequest request)
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

        [HttpDelete("{ticketId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete([FromRoute]int ticketId)
        {            
            var entity = await _ticketService.Get(ticketId);
            if (entity == null)
                return NotFound(ErrorResponse.DefaultNotFoundResponse());

            await _ticketService.Delete(entity);
            await UnitOfWork.CompleteAsync();

            return NoContent();                        
        }

        #endregion
    }
}