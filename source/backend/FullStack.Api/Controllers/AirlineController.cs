using System;
using AutoMapper;
using System.Linq;
using NSwag.Annotations;
using System.Threading.Tasks;
using FullStack.Api.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using FullStack.Api.Infrastructure;
using FullStack.Api.Contracts.Schemas;
using FullStack.Api.Contracts.Responses;
using FullStack.Domain.Contracts.Responses;
using FullStack.Domain.Interfaces.Business;
using FullStack.Domain.Interfaces.Business.Services;

namespace FullStack.Api.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/airlines")]
    [Consumes("application/json"), Produces("application/json")]
    [OpenApiTag("Airline", Description = "Operações relacionadas as companias aéreas.")]
    public class AirlineController : BaseController
    {
        #region Private Read-Only Fields

        private readonly IAirlineService _airlineService;

        #endregion

        #region Constructors

        public AirlineController(ILogger<AirlineController> logger, IMapper mapper, IUnitOfWork unitOfWork
            , IAirlineService airlineService
        )
            : base(logger, mapper, unitOfWork)
        {
            _airlineService = airlineService ?? throw new ArgumentNullException(nameof(airlineService));
        }

        #endregion

        #region Controller Actions

        [HttpGet]
        [ProducesResponseType(typeof(AirlineListResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> List()
        {
            var collection = await _airlineService.GetAll();

            return Ok(new AirlineListResponse()
            {
                Data = Mapper.ConvertEntityToSchema<AirlineSchema>(collection)
                    .OrderBy(x => x.Callsign)
            });
        }

        #endregion
    }
}