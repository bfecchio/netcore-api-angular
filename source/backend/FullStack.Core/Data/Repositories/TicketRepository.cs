using FullStack.Domain.Entities;
using Microsoft.Extensions.Logging;
using FullStack.Domain.Interfaces.Data.Repositories;

namespace FullStack.Core.Data.Repositories
{
    public sealed class TicketRepository : GenericRepository<Ticket, int>, ITicketRepository
    {
        #region Constructors

        public TicketRepository(EFContext dbContext, ILogger<TicketRepository> logger)
            : base(dbContext, logger)
        { }

        #endregion

        #region ITicketRepository Members

        #endregion
    }
}
