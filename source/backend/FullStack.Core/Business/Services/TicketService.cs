using FluentValidation;
using FullStack.Domain.Entities;
using Microsoft.Extensions.Logging;
using FullStack.Domain.Interfaces.Business.Services;
using FullStack.Domain.Interfaces.Data.Repositories;

namespace FullStack.Core.Business.Services
{
    public sealed class TicketService : GenericService<Ticket, int, IValidator<Ticket>, ITicketRepository>, ITicketService
    {
        #region Constructors

        public TicketService(ILogger<TicketService> logger, IValidator<Ticket> validator, ITicketRepository repository)
            : base(logger, validator, repository)
        { }

        #endregion
    }
}
