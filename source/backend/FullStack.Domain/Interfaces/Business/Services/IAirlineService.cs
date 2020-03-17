using FluentValidation;
using FullStack.Domain.Entities;
using FullStack.Domain.Interfaces.Data.Repositories;

namespace FullStack.Domain.Interfaces.Business.Services
{
    public interface IAirlineService : IGenericService<Airline, int, IValidator<Airline>, IAirlineRepository>
    {
        #region IAirlineService Members

        #endregion
    }
}
