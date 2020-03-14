using System;
using System.Threading.Tasks;

namespace FullStack.Domain.Interfaces.Business
{
    public interface IUnitOfWork : IDisposable
    {
        #region IUnitOfWork Members

        int Complete();
        Task<int> CompleteAsync();

        #endregion
    }
}
