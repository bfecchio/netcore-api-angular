using FullStack.Domain.Entities;
using Microsoft.Extensions.Logging;
using FullStack.Domain.Interfaces.Data.Repositories;

namespace FullStack.Core.Data.Repositories
{
    public sealed class AirportRepository : GenericRepository<Airport, int>, IAirportRepository
    {
        #region Constructors

        public AirportRepository(EFContext dbContext, ILogger<AirportRepository> logger)
            : base(dbContext, logger)
        { }

        #endregion

        #region IAirportRepository Members

        #endregion
    }
}
