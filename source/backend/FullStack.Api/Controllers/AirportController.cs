﻿using System;
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
    [Route("api/v{version:apiVersion}/airports")]
    [Consumes("application/json"), Produces("application/json")]
    [OpenApiTag("Airport", Description = "Operações relacionadas aos aéroportos.")]
    public sealed class AirportController : BaseController
    {
        #region Private Read-Only Fields

        private readonly IAirportService _airportService;

        #endregion

        #region Constructors

        public AirportController(ILogger<AirportController> logger, IMapper mapper, IUnitOfWork unitOfWork
            , IAirportService airportService
        )
            : base(logger, mapper, unitOfWork)
        {
            _airportService = airportService ?? throw new ArgumentNullException(nameof(airportService));
        }

        #endregion

        #region Controller Actions

        [HttpGet]
        [ProducesResponseType(typeof(AirportListResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> List()
        {
            var collection = await _airportService.GetAll();

            return Ok(new AirportListResponse()
            {
                Data = Mapper.ConvertEntityToSchema<AirportSchema>(collection)
                    .OrderBy(x => x.State)
                    .ThenBy(x => x.City)
            });
        }

        #endregion
    }
}