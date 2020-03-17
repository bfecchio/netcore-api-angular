using FluentValidation;
using FullStack.Domain.Entities;
using FullStack.Domain.Interfaces.Data.Repositories;

namespace FullStack.Domain.Interfaces.Business.Services
{
    public interface ITicketService : IGenericService<Ticket, int, IValidator<Ticket>, ITicketRepository>
    {
        #region ITicketService Members

        #endregion
    }
}
