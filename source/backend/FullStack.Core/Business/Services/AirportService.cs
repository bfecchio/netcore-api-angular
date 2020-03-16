using FluentValidation;
using FullStack.Domain.Entities;
using Microsoft.Extensions.Logging;
using FullStack.Domain.Interfaces.Business.Services;
using FullStack.Domain.Interfaces.Data.Repositories;

namespace FullStack.Core.Business.Services
{
    public sealed class AirportService : GenericService<Airport, int, IValidator<Airport>, IAirportRepository>, IAirportService
    {
        #region Constructors

        public AirportService(ILogger<AirportService> logger, IValidator<Airport> validator, IAirportRepository repository)
            : base(logger, validator, repository)
        { }

        #endregion
    }
}
