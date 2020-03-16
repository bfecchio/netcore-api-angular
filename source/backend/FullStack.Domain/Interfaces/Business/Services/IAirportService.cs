using FluentValidation;
using FullStack.Domain.Entities;
using FullStack.Domain.Interfaces.Data.Repositories;

namespace FullStack.Domain.Interfaces.Business.Services
{
    public interface IAirportService : IGenericService<Airport, int, IValidator<Airport>, IAirportRepository>
    {
        #region IAirportService Members

        #endregion
    }
}
