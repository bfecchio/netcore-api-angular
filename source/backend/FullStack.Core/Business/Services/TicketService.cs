using FluentValidation;
using System.Threading.Tasks;
using FullStack.Domain.Entities;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
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

        #region Overriden Methods

        public override async Task<Ticket> Get(int id)
        {
            return await _repository.Find(
                predicate: x=> x.Id == id
                , include: x => x
                    .Include(p => p.Airline)
                    .Include(p => p.Origin)
                    .Include(p => p.Destination)
            );            
        }

        #endregion

    }
}
