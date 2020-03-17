using FullStack.Domain.Entities;
using Microsoft.Extensions.Logging;
using FullStack.Domain.Interfaces.Data.Repositories;

namespace FullStack.Core.Data.Repositories
{
    public sealed class AirlineRepository : GenericRepository<Airline, int>, IAirlineRepository
    {
        #region Constructors

        public AirlineRepository(EFContext dbContext, ILogger<AirlineRepository> logger)
            : base(dbContext, logger)
        { }

        #endregion

        #region IAirlineRepository Members

        #endregion
    }
}
