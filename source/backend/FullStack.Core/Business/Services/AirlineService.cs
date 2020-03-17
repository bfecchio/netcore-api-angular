using FluentValidation;
using FullStack.Domain.Entities;
using Microsoft.Extensions.Logging;
using FullStack.Domain.Interfaces.Business.Services;
using FullStack.Domain.Interfaces.Data.Repositories;

namespace FullStack.Core.Business.Services
{
    public sealed class AirlineService : GenericService<Airline, int, IValidator<Airline>, IAirlineRepository>, IAirlineService
    {
        #region Constructors

        public AirlineService(ILogger<AirlineService> logger, IValidator<Airline> validator, IAirlineRepository repository)
            : base(logger, validator, repository)
        { }

        #endregion
    }
}
